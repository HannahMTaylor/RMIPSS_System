using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.Common;

public static class Repositories
{
    private static ApplicationDbContextUnit unitConnection = new();
    private static DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
    private static ApplicationDbContext _db = new(options);
    public static IEmailSender _emailSender = unitConnection.GetEmailSender(); 
    private static UserManager<ApplicationUser> _userManager = unitConnection.GetUserManager();

    public static RoleManager<IdentityRole> _roleManager  = unitConnection.GetRoleManager();
    public static SchoolRepository schoolRepository = new(_db);
    public static ISe2Repository _se2Repository = new Se2Repository(_db);
    public static IStudentRepository _studentRepository = new StudentRepository(_db);
    public static IApplicationUserRepository _appUserRepo = new ApplicationUserRepository(_db, _userManager);
    public static IConsentFormRepository _consentFormRepo = new ConsentFormRepository(_db);
    public static IReferrerPersonRepository _refPersonRepo = new ReferrerPersonRepository(_db);
    public static IReferralRepository _referralRepo = new ReferralRepository(_db);

}