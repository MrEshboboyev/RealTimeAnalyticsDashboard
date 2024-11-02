using Microsoft.AspNetCore.Authentication.Cookies;
using RealTimeAnalyticsDashboard.Application.Mappings;
using RealTimeAnalyticsDashboard.Infrastructure.Configurations;
using RealTimeAnalyticsDashboard.Presentation.Services.IServices;
using RealTimeAnalyticsDashboard.Presentation.Services;
using RealTimeAnalyticsDashboard.Infrastructure.Services;
using RealTimeAnalyticsDashboard.Application.Common.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// configure database
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection") ?? "postgresConnection";
builder.Services.AddDatabaseConfiguration(connectionString);

// add JwtOptions configuring
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

// configure identity
builder.Services.AddIdentityConfiguration();

// configure lifetime for services
builder.Services.AddApplicationServices();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// configure automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// adding authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
await app.SeedDatabaseAsync();
app.Run();