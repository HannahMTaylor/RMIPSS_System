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
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
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
    public async Task<IActionResult> Add(User user)
    {
        if (!ModelState.IsValid) return View();

        if (await _userService.IsUserExist(user.Email))
        {
            ModelState.AddModelError("", "A user with this email already exists. Please try a different email.");
            return View();
        }

        if (await _userService.CreateUser(user))
        {
            TempData["success"] = "User Created Successfully!";
        } else
        {
            TempData["error"] = "Error: User Not Created. Please try again.";
        }
        return RedirectToAction("List", "User");
    }
}
