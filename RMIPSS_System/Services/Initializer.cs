using Microsoft.AspNetCore.Identity;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Services;

public class Initializer(
    ApplicationDbContext db,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    public async Task SeedUserAsync()
    {
        await db.Database.EnsureCreatedAsync();

        if (!db.Roles.Any(r => r.Name != null && r.Name.Equals(Constants.ROLE_STATE_USER)))
        {
            await roleManager.CreateAsync(new IdentityRole { Name = Constants.ROLE_STATE_USER });
        }

        if (!db.Roles.Any(r => r.Name != null && r.Name.Equals(Constants.ROLE_SCHOOL_USER)))
        {
            await roleManager.CreateAsync(new IdentityRole { Name = Constants.ROLE_SCHOOL_USER });
        }

        if (!db.Users.Any(u => u.UserName != null && u.UserName.Equals("stateUser@rmipss.com")))
        {
            var user = new ApplicationUser
            {
                Email = "stateUser@rmipss.com",
                UserName = "stateUser@rmipss.com",
                FirstName = "State User",
                LastName = "RMIPSS"
            };
            await userManager.CreateAsync(user, "Pass123!");
            await userManager.AddToRoleAsync(user, Constants.ROLE_STATE_USER);
        }
        
        if (!db.Users.Any(u => u.UserName != null && u.UserName.Equals("stateUser12@rmipss.com")))
        {
            var user = new ApplicationUser
            {
                Email = "stateUser12@rmipss.com",
                UserName = "stateUser12@rmipss.com",
                FirstName = "State User12",
                LastName = "RMIPSS"
            };
            await userManager.CreateAsync(user, "Pass123@");
            await userManager.AddToRoleAsync(user, Constants.ROLE_STATE_USER);
        }

        if (!db.Users.Any(u => u.UserName != null && u.UserName.Equals("schoolUser@rmipss.com")))
        {
            var user = new ApplicationUser
            {
                Email = "schoolUser@rmipss.com",
                UserName = "schoolUser@rmipss.com",
                FirstName = "School User",
                LastName = "RMIPSS"
            };
            await userManager.CreateAsync(user, "Pass123!");
            await userManager.AddToRoleAsync(user, Constants.ROLE_SCHOOL_USER);
        }
    }
}
