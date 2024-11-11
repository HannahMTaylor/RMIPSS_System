using Microsoft.AspNetCore.Identity;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Services;

public class Initializer
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public Initializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedUserAsync()
    {
        _db.Database.EnsureCreated();

        if (!_db.Roles.Any(r => r.Name.Equals(Constants.ROLE_STATE_USER)))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Constants.ROLE_STATE_USER });
        }

        if (!_db.Roles.Any(r => r.Name.Equals(Constants.ROLE_SCHOOL_USER)))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Constants.ROLE_SCHOOL_USER });
        }

        if (!_db.Users.Any(u => u.UserName.Equals("stateUser@rmipss.com")))
        {
            var user = new ApplicationUser
            {
                Email = "stateUser@rmipss.com",
                UserName = "stateUser@rmipss.com",
                FirstName = "State User",
                LastName = "RMIPSS"
            };
            await _userManager.CreateAsync(user, "Pass123!");
            await _userManager.AddToRoleAsync(user, Constants.ROLE_STATE_USER);
        }

        if (!_db.Users.Any(u => u.UserName.Equals("schoolUser@rmipss.com")))
        {
            var user = new ApplicationUser
            {
                Email = "schoolUser@rmipss.com",
                UserName = "schoolUser@rmipss.com",
                FirstName = "School User",
                LastName = "RMIPSS"
            };
            await _userManager.CreateAsync(user, "Pass123!");
            await _userManager.AddToRoleAsync(user, Constants.ROLE_SCHOOL_USER);
        }
    }
}
