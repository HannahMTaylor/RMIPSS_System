using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RMIPSS_System.Configuration;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest;

public class ApplicationDbContextUnit
{
    private IServiceProvider _serviceProvider;
        public ApplicationDbContextUnit()
        {
            var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

           //Configure Email Settings
            var emailConfig = new EmailConfiguration
            {
               SmtpServer= "mail.smtp2go.com",
                SmtpPort= 2525,
                SmtpUsername= "rmipss",
                SmtpPassword="s1cRNuo8GRQDnmlD",
                FromEmail= "capstone-rmipss@yopmail.com"
            };
       builder.Services.AddSingleton<IEmailSender>(new EmailSender(Options.Create(emailConfig)));

        builder.Services.AddScoped<Initializer>();
        builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
        builder.Services.AddScoped<IRepository<ConsentForm>, Repository<ConsentForm>>();
        builder.Services.AddScoped<IRepository<SE2>, Repository<SE2>>();
        builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
        builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        //builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IConsentFormRepository, ConsentFormRepository>();
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<ISe2Repository, Se2Repository>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<ConsentFormService>();
        builder.Services.AddScoped<LanguageTranslateService>();
        builder.Services.AddScoped<IProcessStepService, ProcessStepService>();
       // builder.Services.AddScoped(IStudentRepository,StudentRepository)();
        
        _serviceProvider = builder.Services.BuildServiceProvider();

        }


    public DbContextOptions<ApplicationDbContext> GetOptions()
    {
       

        DbContextOptions<ApplicationDbContext> options = _serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
        return options;
    }

     // Get `UserManager<ApplicationUser>`
        public UserManager<ApplicationUser> GetUserManager()
        {
            return _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

         // ✅ Get `UserManager<ApplicationUser>`
        public RoleManager<IdentityRole> GetRoleManager()
        {
            return _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        // ✅ Get `UserManager<ApplicationUser>`
        public IEmailSender GetEmailSender()
        {
            return _serviceProvider.GetRequiredService<IEmailSender>();
        }

}