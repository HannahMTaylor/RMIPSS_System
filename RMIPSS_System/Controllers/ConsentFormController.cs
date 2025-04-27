using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Using primary constructors for classes, which simplify constructor declaration by allowing
    /// you to declare parameters directly in the class declaration.
    /// </summary>
    /// <param name="consentFormRepository"></param>
    /// <param name="consentFormService"></param>
    
    [Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
    public class ConsentFormController(
        IConsentFormRepository consentFormRepository,
        ConsentFormService consentFormService)
        : Controller
    {
        [HttpPost]
        public async Task<IActionResult>  Create(ConsentFormViewModel obj)
        {
            try
            {
                int versionId =  await consentFormRepository.GetVersionByConsentFormId(obj.ConsentId);
                Console.WriteLine("old"+versionId);
                Console.WriteLine("new"+obj.Version);
                
                if (versionId != obj.Version)
                {
                    return Conflict(new { message = "Someone else has updated this form. Please refresh and try again." });
                }
                else
                {
                    await consentFormService.CreateConsentForm(obj);

                    TempData["success"] = obj.ConsentId == 0
                        ? "Consent Form Created Successfully!"
                        : "Consent Form Updated Successfully!";
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred.";
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
                        SubmittedDate = consentForm.SubmittedDate,
                        Version = consentForm.Version,
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
