using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpes.Identity;

namespace Daisin.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IValidator<SignUpVM> _signUpValidator;
		private readonly IMapper _iMapper;

		public AuthenticationController(UserManager<AppUser> userManager, IValidator<SignUpVM> signUpValidator, IMapper iMapper)
		{
			_userManager = userManager;
			_signUpValidator = signUpValidator;
			_iMapper = iMapper;
		}

		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInVM request)
		{
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
			if(!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}
			var user = _iMapper.Map<AppUser>(request);
			var userCreateResult = await _userManager.CreateAsync(user, request.Password);
			if(!userCreateResult.Succeeded)
			{
				ModelState.AddModelErrorList(userCreateResult.Errors);
				return View();
			}
			return RedirectToAction("Login", "Authentication");
		}
	}
}
