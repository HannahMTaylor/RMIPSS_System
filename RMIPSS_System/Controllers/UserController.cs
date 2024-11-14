using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_USER)]
public class UserController : Controller
{
    private readonly IApplicationUserRepository _appUserRepo;

    public UserController(IApplicationUserRepository db)
    {
        _appUserRepo = db;
    }

    public async Task<IActionResult> Create()
    {
        var username = "admin@etsu.edu";
        var existingUser = await _appUserRepo.GetAsync(user =>
            user.UserName.ToLower() == username.ToLower()
        );
        if (existingUser == null) {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = username,
                FirstName = "Admin",
                LastName = "ETSU"
            };
            await _appUserRepo.CreateApplicationUserAsync(user, "Pass123!");
            return Content("User created.");
        }

        return Content("The user was already created.");
    }

    public async Task<IActionResult> AssignUserToRole()
    {
        await _appUserRepo.AssignUserToRoleAsync("admin@etsu.edu", "SchoolLevelUser");
        return Content("Assigned 'admin@etsu.edu' to role 'SchoolLevelUser'");
    }

    public IActionResult List()
    {
        return Content("Feature Comming Soon");
    }

    public IActionResult Edit()
    {
        return Content("Edit Feature Comming Soon");
    }

    public IActionResult Add()
    {
        return View();
    }
}
