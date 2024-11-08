using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models;
using System.Diagnostics;

namespace RMIPSS_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult HandleButtonClick(string buttonClicked)
        {
            if (buttonClicked == "Refer")
            {
                //return Content("Refer button clicked");
                return RedirectToAction("Referral");
            }
                
            else if (buttonClicked == "Login")
            {
                //return Content("Login button clicked.");
                return RedirectToAction("Index");
                //return RedirectToAction("Login");
            }


            else
            {
                //return Content("No button matched.");
                return RedirectToAction("Index");
            }
                
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
}
