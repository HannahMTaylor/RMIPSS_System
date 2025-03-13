using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Models;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;
using System.Globalization;

namespace RMIPSS_System.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IApplicationUserRepository _appUserRepo;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSender _emailSender;

    public UserService(ILogger<UserService> logger, IApplicationUserRepository db, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
    {
        _logger = logger;
        _appUserRepo = db;
        _roleManager = roleManager;
        _emailSender = emailSender;
    }

    public async Task<bool> IsUserExist(String username)
    {
        try
        {
            var existingUser = await _appUserRepo.GetAsync(user =>
                user != null && user.UserName != null && user.UserName.ToLower() == username.ToLower()
            );
            return existingUser != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {Username} exists.", username);
            throw;
        }
    }
    
    public async Task<ApplicationUser?> GetUserByUsername(String username)
    {
        try
        {
            return await _appUserRepo.GetAsync(user =>
                user != null && user.UserName != null && user.UserName.ToLower() == username.ToLower()
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user for {Username}.", username);
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

    public async Task<List<SelectListItem>> GetRoleList()
    {
        try
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<SelectListItem> roleList = roles.Select(r =>
            {
                if (r.Name != null)
                    return new SelectListItem
                    {
                        Value = r.Name,
                        Text = TransformRoleName(r.Name)
                    };
                throw new InvalidOperationException();
            }).ToList();

            return roleList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching role list.");
            throw;
        }
    }

    private string TransformRoleName(string roleName)
    {
        // Check if the input starts with "ROLE_"
        if (roleName.StartsWith("ROLE_", StringComparison.OrdinalIgnoreCase))
        {
            // Remove the "ROLE_" prefix
            roleName = roleName.Substring(5);
        }

        // Replace underscores with spaces
        roleName = roleName.Replace("_", " ");

        // Capitalize the first letter of each word
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        roleName = textInfo.ToTitleCase(roleName.ToLower());

        return roleName;
    }

    public async Task SendUserCreationEmailAsync(User user)
    {
        try
        {
            // Send email for username
            string subject = "User Created - Username";
            string body = $"Dear {user.LastName},<br><br>" +
                          "Your account has been created successfully. Below are the details for your username:<br>" +
                          $"<b>Username</b>: {user.Email}<br><br>" +
                          "Please keep this information secure.<br><br>" +
                          "Best regards,<br>" +
                          "RMIPSS Team";
            await _emailSender.SendEmailAsync(user.Email, subject, body);

            // Send email for password
            subject = "User Created - Password";
            body = $"Dear {user.LastName},<br><br>" +
                    "Your account password has been set. Below are the details:<br>" +
                    $"<b>Password</b>: {user.Password}<br><br>" +
                    "For security purposes, please log in to system and change your password immediately.<br><br>" +
                    "Best regards,<br>" +
                    "RMIPSS Team";
            await _emailSender.SendEmailAsync(user.Email, subject, body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending user creation email to {email}", user.Email);
            throw;
        }
    }

      public async Task<bool> DeleteUserAsync(String username)
    {
        try
        {

            var result = await _appUserRepo.DeleteUserAsync(username);
            return result;
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user: {Username}", username);
            throw;
        }
    }

}
