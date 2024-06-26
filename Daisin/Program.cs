using NLog.Web;
using NToastNotify;
using RepositoryLayer.Extensions;
using ServiceLayer.Extensions;
using ServiceLayer.Middlewares.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
{
	ProgressBar = false,
	PositionClass = ToastPositions.TopCenter,
});
builder.Services.LoadRepositoryLayerExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtensions(builder.Configuration);

builder.Logging.ClearProviders();
builder.Host.UseNLog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseExceptionHandler("/Error/GeneralExceptions");


//app.UseStatusCodePagesWithRedirects("/Error/PageNotFound");
app.UseStatusCodePagesWithReExecute("/Error/PageNotFound");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SecurityStampCheck>();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

#pragma warning disable ASP0014
app.UseEndpoints(ep =>
{
	ep.MapAreaControllerRoute(
		name: "Admin",
		areaName: "Admin",
		pattern: "{area=Admin}/{controller=Dashboard}/{action=Index}/{id?}");
	ep.MapAreaControllerRoute(
		name: "User",
		areaName: "User",
		pattern: "{area=User}/{controller=Dashboard}/{action=Index}/{id?}");
	ep.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=index}/{id?}");
});
app.Run();
