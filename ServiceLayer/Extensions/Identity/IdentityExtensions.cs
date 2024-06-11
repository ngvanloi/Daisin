using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using ServiceLayer.Customization.Identity.ErrorDescriber;
using ServiceLayer.Customization.Identity.Validators;
using ServiceLayer.Helpes.Identity.EmailHelper;
using ServiceLayer.Requirement;

namespace ServiceLayer.Extensions.Identity
{
    public static class IdentityExtensions
	{
		public static IServiceCollection LoadIdentityExtensions(this IServiceCollection services, IConfiguration config)
		{
			services.AddIdentity<AppUser, AppRole>(opt =>
			{
				opt.Password.RequiredLength = 10;
				opt.Password.RequireNonAlphanumeric = true;
				opt.Password.RequireUppercase = true;
				opt.Password.RequiredUniqueChars = 2;
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
				opt.Lockout.MaxFailedAccessAttempts = 3;
				opt.User.RequireUniqueEmail = true;
			})
				.AddRoleManager<RoleManager<AppRole>>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders()
				.AddErrorDescriber<LocalizationErrorDescriber>()
				.AddPasswordValidator<CustomPasswordValidator>()
				.AddUserValidator<CustomUserValidator>();

			services.ConfigureApplicationCookie(opt =>
			{
				var newCookie = new CookieBuilder();
				newCookie.Name = "DaisinCompany";
				opt.LoginPath = new PathString("/Authentication/LogIn");
				opt.LogoutPath = new PathString("/Authentication/LogOut");
				opt.AccessDeniedPath = new PathString("/Authentication/AccessDenied");
				opt.Cookie = newCookie;
				opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
			});

			services.Configure<DataProtectionTokenProviderOptions>(opt =>
			{
				opt.TokenLifespan = TimeSpan.FromSeconds(20);
			});
			services.AddScoped<IEmailSendMethod, EmailSendMethod>();
			services.Configure<GmailInfomationVM>(config.GetSection("EmailSettings"));

            services.AddScoped<IAuthorizationHandler, AdminObserverRequirementHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminObserver", policy =>
                {
                    policy.AddRequirements(new AdminObserverRequirement());
                });
            });

            return services;
		}
	}
}
