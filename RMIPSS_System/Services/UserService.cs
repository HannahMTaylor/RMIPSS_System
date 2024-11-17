using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class UserService
{
    private readonly IApplicationUserRepository _appUserRepo;

    public UserService(IApplicationUserRepository db)
    {
        _appUserRepo = db;
    }

    public async Task<bool> IsUserExist(String username)
    {
        var existingUser = await _appUserRepo.GetAsync(user =>
            user.UserName.ToLower() == username.ToLower()
        );

        return existingUser != null;
    }

    public async Task<bool> CreateUser(User user)
    {
        var newUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };

        if (await _appUserRepo.CreateApplicationUserAsync(newUser, user.Password) != null)
        {
            await _appUserRepo.AssignUserToRoleAsync(user.Email, user.Role);
            return true;
        }

        return false;
    }
}
