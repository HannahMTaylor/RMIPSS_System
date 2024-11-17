using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class UserService
{
    private readonly IApplicationUserRepository _appUserRepo;
    private readonly ILogger<UserService> _logger;

    public UserService(IApplicationUserRepository db, ILogger<UserService> logger)
    {
        _appUserRepo = db;
        _logger = logger;
    }

    public async Task<bool> IsUserExist(String username)
    {
        try
        {
            var existingUser = await _appUserRepo.GetAsync(user =>
                user.UserName.ToLower() == username.ToLower()
            );
            return existingUser != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {Username} exists.", username);
            throw;
        }
    }

    public async Task<bool> CreateUser(User user)
    {
        try
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user: {Username}", user.Email);
            throw;
        }

        return false;
    }
}
