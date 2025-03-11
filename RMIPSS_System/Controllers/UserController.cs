using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMIPSS_System.Models;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_USER)]
public class UserController(UserService userService, ILogger<UserController> logger) : Controller
{
    public IActionResult List()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return Content("Edit Feature Coming Soon");
    }

    public async Task<IActionResult> Add()
    {
        try
        {
            List<SelectListItem> roleList = await userService.GetRoleList();

            AddEditUserViewModel addEditUserViewModel = new AddEditUserViewModel
            {
                User = new User(),
                Roles = roleList
            };

            return View(addEditUserViewModel);
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error occurred while trying to retrieve role list or create Add/Edit user view.");
            TempData["error"] = "An error occurred while loading the user creation page. Please try again later.";
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddEditUserViewModel addUserViewModel)
    {
        if (!ModelState.IsValid)
        {
            addUserViewModel.Roles = await userService.GetRoleList();
            logger.LogWarning("Invalid model state during add new user.");
            return View(addUserViewModel);
        }

        try
        {
            User? user = addUserViewModel.User;
            Debug.Assert(user != null, nameof(user) + " != null");
            if (await userService.IsUserExist(user.Email))
            {
                addUserViewModel.Roles = await userService.GetRoleList();
                logger.LogInformation("User with email {Email} already exists.", user.Email);
                ModelState.AddModelError("", "A user with this email already exists. Please try a different email.");
                return View(addUserViewModel);
            }

            if (await userService.CreateUser(user))
            {
                await userService.SendUserCreationEmailAsync(user);
                logger.LogInformation("User {Email} created successfully.", user.Email);
                TempData["success"] = "User Created Successfully!";
            } else
            {
                logger.LogError("Error creating user: {Email}", user.Email);
                TempData["error"] = "Error: User Not Created. Please try again.";
            }
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "An exception occurred while adding a user.");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        }
        
        return RedirectToAction("List", "User");
    }
}
