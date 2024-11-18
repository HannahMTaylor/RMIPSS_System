using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Controllers
{
   
    public class ConsentFormController : Controller
    {
        private readonly ILogger<ConsentFormController> _logger;
        private readonly IConsentFormRepository _consentFormRepository;
        public ConsentFormController(IConsentFormRepository consentFormRepository)
        {
            _consentFormRepository = consentFormRepository;
        }

          public IActionResult Index()
        {
            return View();
        }
          
        [HttpPost]
        public IActionResult Create(ConsentForm obj)
        {
            _consentFormRepository.SaveConsentForm(obj);
            TempData["success"] = "Consent Form Created Successfully!";
            return RedirectToAction("Index","Home");

        }

    }
}
