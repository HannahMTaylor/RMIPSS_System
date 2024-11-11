using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RMIPSS_System.Controllers
{
   
    public class ConsentFormController : Controller
    {
         private readonly ILogger<ConsentFormController> _logger;

        public ConsentFormController(ILogger<ConsentFormController> logger)
        {
            _logger = logger;
        }

          public IActionResult Index()
        {
            return View();
        }

    }
}
