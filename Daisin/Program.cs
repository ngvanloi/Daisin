using RepositoryLayer.Extensions;
using ServiceLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.LoadRepositoryLayerExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

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
	ep.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=index}/{id?}");
});
app.Run();
