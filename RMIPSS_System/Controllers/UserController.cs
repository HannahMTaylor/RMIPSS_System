using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_USER)]
public class UserController : Controller
{
    private readonly IApplicationUserRepository _appUserRepo;
    private readonly UserService _userService;

    public UserController(IApplicationUserRepository db, UserService userService)
    {
        _appUserRepo = db;
        _userService = userService;
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
        return View();
    }

    public IActionResult Edit()
    {
        return Content("Edit Feature Comming Soon");
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(User user)
    {
        if (!ModelState.IsValid) return View();

        if (!_userService.IsPasswordSame(user.Password, user.ConfirmPassword))
        {
            ModelState.AddModelError("", "Password and Confirm Password do not match. Please try again.");
            return View();
        }

        // TODO Write Logic
        TempData["success"] = "User Created Successfully!";
        return RedirectToAction("List", "User");
    }
}
