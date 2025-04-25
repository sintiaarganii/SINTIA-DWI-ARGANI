/*using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Services;
using SINTIA_DWI_ARGANI.Validator;
using UITraining.Services;


var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<ApplicationContext>(
    DbContextOptions => DbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("MyConnectionStrings"), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddScoped<ILoginLayout, LoginLayoutServices>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IValidator<RegistCashierDTO>, ValidatorRequestRegistCashier>();
builder.Services.AddScoped<IValidator<CategoryDTO>, ValidatorRequestCategory>();
builder.Services.AddScoped<IValidator<ProductDTO>, ValidatorRequestProduct>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Authentication/LoginCashier";
        options.LogoutPath = "/Authentication/Logout";
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    });

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout 30 menit
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidatorRequestCashier>());
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidatorRequestCategory>());
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidatorRequestProduct>());

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Authentication}/{action=LoginCashier}/{id?}");
pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
*/

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Services;
using SINTIA_DWI_ARGANI.Validator;
using UITraining.Services;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

// Add services to the container.
//builder.Services.AddDbContext<ApplicationContext>(
//    DbContextOptions => DbContextOptions
//        .UseMySql(builder.Configuration.GetConnectionString("MyConnectionStrings"), serverVersion)
//        .LogTo(Console.WriteLine, LogLevel.Information)
//        .EnableSensitiveDataLogging()
//        .EnableDetailedErrors()
//);
builder.Services.AddDbContext<ApplicationContext>(
    DbContextOptions => DbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("MyConnectionStrings"), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Register services
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddScoped<ILoginLayout, LoginLayoutServices>();
builder.Services.AddScoped<IOrdering, OrderingServices>();

// Register validators
builder.Services.AddScoped<IValidator<RegistCashierDTO>, ValidatorRequestRegistCashier>();
builder.Services.AddScoped<IValidator<CategoryDTO>, ValidatorRequestCategory>();
builder.Services.AddScoped<IValidator<ProductDTO>, ValidatorRequestProduct>();

// Add distributed memory cache (for session)
builder.Services.AddDistributedMemoryCache();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/LoginCashier";
        options.LogoutPath = "/Authentication/Logout";
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    });

// Logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();