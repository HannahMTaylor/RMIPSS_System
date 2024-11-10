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
}
