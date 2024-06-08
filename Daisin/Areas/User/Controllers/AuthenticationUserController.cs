using AutoMapper;
using CoreLayer.Enumerators;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Helpes.Identity.ModelStateHelper;

namespace Daisin.Areas.User.Controllers
{
	[Authorize]
	[Area("User")]
	[Route("User/AuthenticationUser")]
	public class AuthenticationUserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;
		private readonly IValidator<UserEditVM> _validator;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IImageHelper _imageHelper;

		public AuthenticationUserController(UserManager<AppUser> userManager, IMapper mapper, IValidator<UserEditVM> validator, SignInManager<AppUser> signInManager, IImageHelper imageHelper)
		{
			_userManager = userManager;
			_mapper = mapper;
			_validator = validator;
			_signInManager = signInManager;
			_imageHelper = imageHelper;
		}

		[HttpGet("UserEdit")]
		public async Task<ActionResult> UserEdit()
		{
			var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
			var userEditVm = _mapper.Map<UserEditVM>(user);
			return View(userEditVm);
		}

		[HttpPost("UserEdit")]
		public async Task<IActionResult> UserEdit(UserEditVM request)
		{
			var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

			var validation = await _validator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}

			var passwordCheck = await _userManager.CheckPasswordAsync(user!, request.Password);
			if (!passwordCheck)
			{
				ViewBag.Result = "FailedPassword";
				ModelState.AddModelErrorList(new List<string> { "Wrong Password!" });
				return Redirect(nameof(UserEdit));
			}

			if (request.NewPassword != null)
			{
				var passwordChange = await _userManager.ChangePasswordAsync(user!, request.Password, request.NewPassword);
				if (!passwordChange.Succeeded)
				{
					ViewBag.Result = "NewPasswordFailure";
					ModelState.AddModelErrorList(passwordChange.Errors);
					return Redirect(nameof(UserEdit));
				}
			}

			var oldFilename = user!.FileName;
			var oldFiletype = user!.FileType;
			if (request.Photo != null)
			{
				var image = await _imageHelper.ImageUpload(request.Photo, ImageType.identity, null);
				if (image.Error != null)
				{
					if (request.NewPassword != null)
					{
						await _userManager.ChangePasswordAsync(user, request.NewPassword, request.Password);
						await _userManager.UpdateSecurityStampAsync(user);
						await _signInManager.SignOutAsync();
						await _signInManager.SignInAsync(user, false);
					}
					return Redirect(nameof(UserEdit));
				}
				request.FileName = image.FileName;
				request.FileType = request.Photo.ContentType;
			}
			else
			{
				request.FileName = oldFilename;
				request.FileType = oldFiletype;
			}

			var mappedUser = _mapper.Map<AppUser>(request);
			var userUpdate = await _userManager.UpdateAsync(mappedUser);

			if (userUpdate.Succeeded)
			{
				if (request.Photo != null)
				{
					if (oldFilename != null)
					{
						_imageHelper.DeleteImage(oldFilename);
					}
				}

				await _userManager.UpdateSecurityStampAsync(user);
				await _signInManager.SignOutAsync();
				await _signInManager.SignInAsync(user, false);
				return RedirectToAction("Index", "Dashboard", new { Area = "User" });
			}

			if (request.FileName != null)
			{
				_imageHelper.DeleteImage(request.FileName);
			}

			if (request.NewPassword != null)
			{
				await _userManager.ChangePasswordAsync(user, request.NewPassword, request.Password);
				await _userManager.UpdateSecurityStampAsync(user);
				await _signInManager.SignOutAsync();
				await _signInManager.SignInAsync(user, false);
			}

			ViewBag.Username = user.UserName;

			return Redirect(nameof(UserEdit));
		}
	}
}
