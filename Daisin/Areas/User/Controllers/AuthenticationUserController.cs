using AutoMapper;
using CoreLayer.Enumerators;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Helpes.Identity.ModelStateHelper;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;
using System.Net.Cache;

namespace Daisin.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("User/AuthenticationUser")]
    public class AuthenticationUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<UserEditVM> _validator;
        private readonly IAuthenticationUserService _authenticationUserService;
        private readonly IToastNotification _toasty;

        public AuthenticationUserController(UserManager<AppUser> userManager, IValidator<UserEditVM> validator, IAuthenticationUserService authenticationUserService, IToastNotification toasty)
        {
            _userManager = userManager;
            _validator = validator;
            _authenticationUserService = authenticationUserService;
            _toasty = toasty;
        }

        [HttpGet("UserEdit")]
        public async Task<ActionResult> UserEdit()
        {
            var userEditVM = await _authenticationUserService.FindUserAsync(HttpContext);
            return View(userEditVM);
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

            var userEditResult = await _authenticationUserService.UserEditAsync(request, user!);
            if (!userEditResult.Succeeded)
            {
                ViewBag.Result = "FailedUserEdit";
                ModelState.AddModelErrorList(userEditResult.Errors);
                return View();
            }
            ViewBag.Username = user!.UserName;
            _toasty.AddInfoToastMessage(
                NotificationMessagesIdentity.UserEdit(user.UserName!),
                new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle });

            return RedirectToAction("Index", "Dashboard", new { Area = "User" });
        }
    }
}
