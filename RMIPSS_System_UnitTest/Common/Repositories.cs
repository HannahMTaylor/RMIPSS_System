using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System_UnitTest.Common;

public static class Repositories
{
    private static ApplicationDbContextUnit unitConnection = new();
    private static DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
    private static ApplicationDbContext _db = new(options);
    
    private static UserManager<ApplicationUser> _userManager = unitConnection.GetUserManager();

    public static ISe2Repository _se2Repository = new Se2Repository(_db);
    public static IStudentRepository _studentRepository = new StudentRepository(_db);
    public static IApplicationUserRepository _appUserRepo = new ApplicationUserRepository(_db, _userManager);

}