using Microsoft.AspNetCore.Identity;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager) : base(db)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Update(ApplicationUser applicationUser)
    {
        _db.ApplicationUsers.Update(applicationUser);
    }

    public async Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser applicationUser, string password)
    {
        await _userManager.CreateAsync(applicationUser, password);
        return applicationUser;
    }

    public async Task AssignUserToRoleAsync(string userName, string rolename)
    {

        // Fetch user using `UserManager` instead of tracking `DbContext`
        var user = await _userManager.FindByNameAsync(userName);

        if (user != null)
        {
            // Check if user is already in the role
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(rolename))
            {
                // Assign role
                await _userManager.AddToRoleAsync(user, rolename);
            }


        }
    }

    public async Task<bool> DeleteUserAsync(String username)
    {
        // Retrieve the user from UserManager
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return false; // User does not exist
        }

        // Check if the user has roles before removing them
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Any())
        {
            // Remove the roles from the user    
            await _userManager.RemoveFromRolesAsync(user, roles);
        }

        // Delete the user from UserManager
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}
