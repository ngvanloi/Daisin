﻿using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Helpes.Identity.ModelStateHelper;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;
using System.Security.Claims;

namespace Daisin.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IValidator<SignUpVM> _signUpValidator;
		private readonly IValidator<LogInVM> _logInValidator;
		private readonly IValidator<ForgotPasswordVM> _forgotPasswordValidator;
		private readonly IValidator<ResetPasswordVM> _resetPasswordValidator;
		private readonly IMapper _iMapper;
		private readonly IAuthenticationMainService _authMainService;
		private readonly IToastNotification _toasty;

		public AuthenticationController(
			UserManager<AppUser> userManager,
			IValidator<SignUpVM> signUpValidator,
			IMapper iMapper,
			IValidator<LogInVM> logInValidator,
			SignInManager<AppUser> signInManager,
			IValidator<ForgotPasswordVM> forgotPasswordValidator,
			IValidator<ResetPasswordVM> resetPasswordValidator,
			IAuthenticationMainService authMainService,
			IToastNotification toasty)
		{
			_userManager = userManager;
			_signUpValidator = signUpValidator;
			_iMapper = iMapper;
			_logInValidator = logInValidator;
			_signInManager = signInManager;
			_forgotPasswordValidator = forgotPasswordValidator;
			_resetPasswordValidator = resetPasswordValidator;
			_authMainService = authMainService;
			_toasty = toasty;
		}

		[HttpGet]
		public IActionResult LogIn(string? errorMessage)
		{
			if (errorMessage != null && errorMessage == IdentityMessage.SecurityStampError)
			{
				ViewBag.Result = "NotSucceed";
				ModelState.AddModelErrorList(new List<string> { errorMessage });
				return View();
			}

			if (errorMessage != null)
			{
				return RedirectToAction("/Error/PageNotFound");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInVM request, string? returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Action("Index", "Dashboard", new { Area = "Admin" });
			var validation = await _logInValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}
			var hasUser = await _userManager.FindByEmailAsync(request.Email);
			if (hasUser == null)
			{
				ViewBag.Result = "NotSucceed";
				ModelState.AddModelErrorList(new List<string> { "Email or Password is wrong" });
				return View();
			}

			var logInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);
			if (logInResult.Succeeded)
			{
				_toasty.AddSuccessToastMessage(
					NotificationMessagesIdentity.LogInSuccess,
					new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle }
					);
				return Redirect(returnUrl!);
			}

			if (logInResult.IsLockedOut)
			{
				ViewBag.Result = "LockedOut";
				ModelState.AddModelErrorList(new List<string> { "Your account is locked Out for 30 seconds" });
				return View();
			}
			ViewBag.Result = "FailedAttempt";
			ModelState.AddModelErrorList(new List<string> { $"Email or Password is wrong! Failed attempt" +
				$"{await _userManager.GetAccessFailedCountAsync(hasUser)}"});

			return View();
		}

		[HttpGet("SignUp")]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(SignUpVM request)
		{
			var validation = await _signUpValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}
			var user = _iMapper.Map<AppUser>(request);
			var userCreateResult = await _userManager.CreateAsync(user, request.Password);
			if (!userCreateResult.Succeeded)
			{
				ViewBag.Result = "NotSucceed";
				ModelState.AddModelErrorList(userCreateResult.Errors);
				return View();
			}

			var assignRoleResult = await _userManager.AddToRoleAsync(user, "Member");
			if (!assignRoleResult.Succeeded)
			{
				await _userManager.DeleteAsync(user);
				ViewBag.Result = "NotSucceed";
				ModelState.AddModelErrorList(userCreateResult.Errors);
				return View();
			}
			var defaultClaim = new Claim("AdminObserverExpireDate", DateTime.Now.AddDays(-1).ToString());
			var addClaimResult = await _userManager.AddClaimAsync(user, defaultClaim);
			if (!addClaimResult.Succeeded)
			{
				await _userManager.RemoveFromRoleAsync(user, "Member");
				await _userManager.DeleteAsync(user);
				ViewBag.Result = "NotSucceed";
				ModelState.AddModelErrorList(userCreateResult.Errors);
				return View();
			}

			_toasty.AddSuccessToastMessage(NotificationMessagesIdentity.SignUp(user.UserName!), new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle });
			return RedirectToAction("Login", "Authentication");
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordVM request)
		{
			var validation = await _forgotPasswordValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}

			var hasUser = await _userManager.FindByEmailAsync(request.Email);
			if (hasUser == null)
			{
				ViewBag.Result = "UserDoesNotExist";
				ModelState.AddModelErrorList(new List<string> { "User does not exist!" });
				return View();
			}

			_toasty.AddSuccessToastMessage(
				NotificationMessagesIdentity.PasswordReset,
				new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle });
			await _authMainService.CreateResetCredentitalsAndSend(hasUser, HttpContext, Url);

			return RedirectToAction("LogIn", "Authentication");
		}

		[HttpGet]
		public IActionResult ResetPassword(string userId, string token, List<string> errors)
		{
			TempData["UserId"] = userId;
			TempData["Token"] = token;

			if (errors.Any())
			{
				ViewBag.Result = "Error";
				ModelState.AddModelErrorList(errors);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM request)
		{
			var userId = TempData["UserId"];
			var token = TempData["Token"];

			if (userId == null || token == null)
			{
				_toasty.AddErrorToastMessage(
                    NotificationMessagesIdentity.TokenValidationError,
					new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
				return RedirectToAction("LogIn", "Authentication");
			}

			var validation = await _resetPasswordValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				List<string> errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
				return RedirectToAction("ResetPassword", "Authentication", new { userId, token, errors });
			}

			var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);
			if (hasUser == null)
			{
				_toasty.AddErrorToastMessage(
                    NotificationMessagesIdentity.TokenValidationError,
					new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
				return RedirectToAction("LogIn", "Authentication");
			}

			var resetPasswordResult = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);
			if (resetPasswordResult.Succeeded)
			{
				_toasty.AddSuccessToastMessage(
                    NotificationMessagesIdentity.PasswordChangeSuccess,
					new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle });
				return RedirectToAction("LogIn", "Authentication");
			}
			else
			{
				List<string> errors = resetPasswordResult.Errors.Select(x => x.Description).ToList();
				return RedirectToAction("ResetPassword", "Authentication", new { userId, token, errors });
			}
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}
