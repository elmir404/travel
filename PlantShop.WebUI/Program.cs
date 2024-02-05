using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PlantShop.Business;
using PlantShop.DataAccess;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Users;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.AddDbContext<PlantShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlantShopDbConnection")));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
{
    config.Stores.MaxLengthForKeys = 128;
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequiredLength = 4;
    config.Password.RequiredUniqueChars = 0;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;

})
  .AddEntityFrameworkStores<PlantShopDbContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 524288000; // 500 MB limit (adjust as needed)
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();

builder.Services.AddDataAccessServices();
builder.Services.AddBusinessServices();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(conf =>
{
    conf.IdleTimeout = TimeSpan.FromMinutes(1);
    conf.Cookie.HttpOnly = false;
    conf.Cookie.IsEssential = true;
    conf.Cookie.SameSite = SameSiteMode.None;
    conf.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});
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
app.UseAuthentication();
app.UseSession();
app.UseRouting();
app.UseRequestLocalization();
app.UseAuthorization();
var supportedCultures = new[]
{
    new CultureInfo("az"),
    new CultureInfo("en"),
    new CultureInfo("ja-JP"),
    new CultureInfo("vi"),
    new CultureInfo("ru"),
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("az"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "blogdetails",
        pattern: "blog/{slug}",
        defaults: new { controller = "Blog", action = "Detail" });
    endpoints.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=About}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}/{value?}");
});

app.Run();