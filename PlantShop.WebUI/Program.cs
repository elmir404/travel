
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PlantShop.Business;
using PlantShop.DataAccess;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Users;
using PlantShop.Infrastructure.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<PlantShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlantShopDbConnection")));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
{
    config.Stores.MaxLengthForKeys = 128;
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequiredLength = 8;
    config.Password.RequiredUniqueChars = 0;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.Tokens.PasswordResetTokenProvider = ResetPasswordTokenProvider.ProviderKey;
    config.Tokens.EmailConfirmationTokenProvider = ResetPasswordTokenProvider.ProviderKey;

})
    .AddTokenProvider<ResetPasswordTokenProvider>(ResetPasswordTokenProvider.ProviderKey)
  .AddEntityFrameworkStores<PlantShopDbContext>()
  .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";


});
builder.Services.AddHttpContextAccessor();
builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(conf =>
{
    conf.IdleTimeout = TimeSpan.FromHours(1);
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

app.UseAuthorization();


var supportedCultures = new[]
{
    new CultureInfo("az"),
    new CultureInfo("en"),
    new CultureInfo("vi"),
    new CultureInfo("ja")
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
        name: "productdetails",
        pattern: "product/{slug}",
        defaults: new { controller = "Product", action = "Detail" });
    endpoints.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}/{value?}");

});

app.Run();


