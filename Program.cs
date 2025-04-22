using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Services;
using UITraining.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Runtime.InteropServices;
using SINTIA_DWI_ARGANI.Models;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
 
builder.Services.AddDbContext<ApplicationContext>(
    DbContextOptions => DbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("MyConnectionStrings"), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

/*// Ganti kode registrasi PDF converter dengan ini:
var pdfToolsPath = Path.Combine(AppContext.BaseDirectory,
    RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
        "libwkhtmltox.dll" : "libwkhtmltox.so");

if (!File.Exists(pdfToolsPath))
{
    throw new FileNotFoundException("PDF tools library not found", pdfToolsPath);
}

var converter = new SynchronizedConverter(new PdfTools());
builder.Services.AddSingleton(typeof(IConverter), converter);
builder.Services.AddScoped<IPdfService, PdfService>();*/

builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<IOrder, OrderServices>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddScoped<ILoginLayout, LoginLayoutServices>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Authentication/LoginCashier";
        options.LogoutPath = "/Authentication/Logout";
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    });

// Tambahkan ini sebelum builder.Build()
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout 30 menit
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
        pattern: "{controller=Authentication}/{action=LoginCashier}/{id?}");
//pattern: "{controller=Order}/{action=Index}/{id?}");

/*app.MapControllerRoute(
name: "report",
pattern: "Report/{action=Index}",
defaults: new { controller = "Order" });*/

app.Run();
