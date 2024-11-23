using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using RMIPSS_System.ViewModel;

namespace RMIPSS_System.Controllers
{
   
    public class ConsentFormController : Controller
    {
        private readonly ILogger<ConsentFormController> _logger;
        private readonly IConsentFormRepository _consentFormRepository;
        private readonly ConsentFormService _consentFormService;
        public ConsentFormController(IConsentFormRepository consentFormRepository, ILogger<ConsentFormController> logger, ConsentFormService consentFormService)
        {
            _consentFormRepository = consentFormRepository;
            _logger = logger;
            _consentFormService = consentFormService;
        }

       
        [HttpPost]
        public async Task<IActionResult>  Create(ConsentFormViewModel obj)
        {
            await _consentFormService.CreateConsentForm(obj);
            if (obj.Id == 0)
            {
                TempData["success"] = "Consent Form Created Successfully!";
            }
            else
            {
                TempData["success"] = "Consent Form Updated Successfully!";
            }
           
            return RedirectToAction("Index","Home");
 
        }
        
        public async Task<IActionResult>  ConsentFormEvaluationReevaluation(int studentId)
        {
            if (studentId == null || studentId == 0)
            {
                TempData["error"] = "Select Student";
                return RedirectToAction("Index","Home");
            }
            else
            {
                ConsentForm consentForm = await _consentFormRepository.GetConsentFormByStudentId(studentId);
                if (consentForm != null)
                {
                   ConsentFormViewModel consentFormView = new ConsentFormViewModel()
                    {
                        Id = consentForm.Id,
                        Date = consentForm.Date,
                        To = consentForm.To,
                        From = consentForm.From,
                        ConsentOption = (int)consentForm.ConsentOption,
                        Evaluation = consentForm.Evaluation,
                        StudentId = consentForm.StudentId,
                        Status = consentForm.Status,
                        SubmittedDate = consentForm.SubmittedDate
                    };
                    return View(consentFormView);
                }
                else
                {
                    
                    ConsentFormViewModel consentFormView = new ConsentFormViewModel()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Evaluation = null,
                        StudentId = studentId,
                        ConsentOption = 3
                    };
                    return View(consentFormView);
                }

                
            }
            
        }

    }
}
