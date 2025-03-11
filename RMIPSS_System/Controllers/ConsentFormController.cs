using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System.Controllers
{
    
    [Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
    public class ConsentFormController(
        IConsentFormRepository consentFormRepository,
        ConsentFormService consentFormService)
        : Controller
    {
        [HttpPost]
        public async Task<IActionResult>  Create(ConsentFormViewModel obj)
        {
            await consentFormService.CreateConsentForm(obj);
            if (obj.ConsentId == 0)
            {
                TempData["success"] = "Consent Form Created Successfully!";
            }
            else
            {
                TempData["success"] = "Consent Form Updated Successfully!";
            }
           
            return RedirectToAction("StudentViewDetails", "Student", new { id = obj.StudentId});
 
        }
        
        public async Task<IActionResult>  ConsentFormEvaluationReevaluation([Bind(Prefix = "id")] int studentId)
        {
            if (studentId == 0)
            {
                TempData["error"] = "Select Student";
                return RedirectToAction("ListStudent", "Student");
            }
            else
            {
                ConsentForm? consentForm = await consentFormRepository.GetConsentFormByStudentId(studentId);
                if (consentForm != null)
                {
                   ConsentFormViewModel consentFormView = new ConsentFormViewModel()
                    {
                        ConsentId = consentForm.Id,
                        EnteredDate = consentForm.EnteredDate,
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
                        EnteredDate = DateOnly.FromDateTime(DateTime.Now),
                        Evaluation = null,
                        StudentId = studentId,
                        ConsentOption = (int)ConsentOption.NotSpecified
                    };
                    return View(consentFormView);
                }

                
            }
            
        }

    }
}
