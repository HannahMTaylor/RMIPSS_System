using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Controllers;

public class ApplicationUserController : Controller
{
    private readonly IApplicationUserRepository _appUserRepo;

    public ApplicationUserController(IApplicationUserRepository db)
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
        }

        return Content("The user was already created.");
    }
}
