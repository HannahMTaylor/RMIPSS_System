using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<Initializer>();
        builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
        builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
        builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        builder.Services.AddScoped<IConsentFormRepository, ConsentFormRepository>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<ConsentFormService>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        var app = builder.Build();
        await SeedDataAsync(app);

        static async Task SeedDataAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var initializer = services.GetRequiredService<Initializer>();
                await initializer.SeedUserAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("An error occurred while seeding the database: {Message}", ex.Message);
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        app.Run();
    }
}
