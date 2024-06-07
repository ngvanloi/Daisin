using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceLayer.Helpes.Identity;

namespace Daisin.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IValidator<SignUpVM> _signUpValidator;
		private readonly IValidator<LogInVM> _logInValidator;
		private readonly IMapper _iMapper;

		public AuthenticationController(
			UserManager<AppUser> userManager,
			IValidator<SignUpVM> signUpValidator,
			IMapper iMapper,
			IValidator<LogInVM> logInValidator,
			SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signUpValidator = signUpValidator;
			_iMapper = iMapper;
			_logInValidator = logInValidator;
			_signInManager = signInManager;
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
	}
}
