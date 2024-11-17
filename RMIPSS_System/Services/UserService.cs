using Microsoft.AspNetCore.Identity;
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

    public UserService(ILogger<UserService> logger, IApplicationUserRepository db, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _appUserRepo = db;
        _roleManager = roleManager;
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

    public async Task<List<SelectListItem>> GetRoleList()
    {
        try
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<SelectListItem> roleList = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = TransformRoleName(r.Name)
            }).ToList();

            return roleList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching role list.");
            throw;
        }
    }

    public string TransformRoleName(string roleName)
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
}
