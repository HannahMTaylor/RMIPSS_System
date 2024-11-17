using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_USER)]
public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
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
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state during add new user.");
            return View();
        }

        try
        {
            if (await _userService.IsUserExist(user.Email))
            {
                _logger.LogInformation("User with email {Email} already exists.", user.Email);
                ModelState.AddModelError("", "A user with this email already exists. Please try a different email.");
                return View();
            }

            if (await _userService.CreateUser(user))
            {
                _logger.LogInformation("User {Email} created successfully.", user.Email);
                TempData["success"] = "User Created Successfully!";
            } else
            {
                _logger.LogError("Error creating user: {Email}", user.Email);
                TempData["error"] = "Error: User Not Created. Please try again.";
            }
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "An exception occurred while adding a user.");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        }
        
        return RedirectToAction("List", "User");
    }
}
