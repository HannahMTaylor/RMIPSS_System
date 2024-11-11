using Microsoft.AspNetCore.Identity;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(db)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
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
        var roleExists = await _roleManager.RoleExistsAsync(rolename);
        if (!roleExists) {
            await _roleManager.CreateAsync(new IdentityRole(rolename));
        }

        var user = await GetAsync(u =>
            u.UserName.ToLower() == userName.ToLower()
        );
        if (user != null) {
            await _userManager.AddToRoleAsync(user, rolename);
        }
    }
}
