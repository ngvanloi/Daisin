﻿using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpes.Identity.EmailHelper;
using ServiceLayer.Helpes.Identity.ModelStateHelper;
using ServiceLayer.Services.Identity.Abstract;

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

		public AuthenticationController(
			UserManager<AppUser> userManager,
			IValidator<SignUpVM> signUpValidator,
			IMapper iMapper,
			IValidator<LogInVM> logInValidator,
			SignInManager<AppUser> signInManager,
			IValidator<ForgotPasswordVM> forgotPasswordValidator,
			IValidator<ResetPasswordVM> resetPasswordValidator,
			IAuthenticationMainService authMainService)
		{
			_userManager = userManager;
			_signUpValidator = signUpValidator;
			_iMapper = iMapper;
			_logInValidator = logInValidator;
			_signInManager = signInManager;
			_forgotPasswordValidator = forgotPasswordValidator;
			_resetPasswordValidator = resetPasswordValidator;
			_authMainService = authMainService;
		}

		[HttpGet]
		public IActionResult LogIn()
		{
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

		[HttpGet]
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
				return RedirectToAction("LogIn", "Authentication");
			}

			var resetPasswordResult = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);
			if (resetPasswordResult.Succeeded)
			{
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
