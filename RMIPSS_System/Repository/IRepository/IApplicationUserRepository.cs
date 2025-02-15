using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IApplicationUserRepository : IRepository<ApplicationUser>
{
    void Update(ApplicationUser applicationUser);
    Task SaveAsync();
    Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser applicationUser, string password);
    Task AssignUserToRoleAsync(string userName, string rolename);
     Task<bool> DeleteUserAsync(String username);
}
