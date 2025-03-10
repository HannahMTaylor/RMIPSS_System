using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;
using System.Diagnostics;

using RMIPSS_System.Controllers;

namespace RMIPSS_System.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
    public IActionResult Dashboard()
    {
        return View();
    }
    
    //public IActionResult Referral()
    //{
    //    //return RedirectToAction("CreateReferralForm", "Referral");
    //    //return ReferralController.CreateReferralForm();
    //    return View();
    //}
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
