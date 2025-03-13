using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;
using System.Diagnostics;

namespace RMIPSS_System.Controllers;
/// <summary>
/// Using primary constructors for classes, which simplify constructor declaration by allowing
/// you to declare parameters directly in the class declaration.
/// </summary>
/// <param name="logger"></param>
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult ProcessStep()
    {
        return View();
    }

    [Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
    public IActionResult Dashboard()
    {
        return View();
    }
    
    public IActionResult Referral()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
