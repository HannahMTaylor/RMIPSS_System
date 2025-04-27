using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System.Services;

public class ConsentFormService(
    ILogger<ConsentFormService> logger,
    IConsentFormRepository db,
    StudentService studentService)
{
    public async Task<ConsentForm?> CreateConsentForm(ConsentFormViewModel consentForm)
    {
        try
        {
            ConsentForm? objFromDb = await db.GetByIdAsync(consentForm.ConsentId);
            ConsentForm? consentFormResult = null;
            if (objFromDb == null)
            {
                objFromDb = new ConsentForm
                {
                    EnteredDate = consentForm.EnteredDate,
                    To = consentForm.To,
                    From = consentForm.From,
                    Evaluation = consentForm.Evaluation,
                    StudentId = consentForm.StudentId,
                    Version = 1,
                };
                consentFormResult = await db.SaveConsentFormAsync(objFromDb);

            }
            else
            {
                objFromDb.Id = consentForm.ConsentId;
                objFromDb.EnteredDate = consentForm.EnteredDate;
                objFromDb.To = consentForm.To;
                objFromDb.From = consentForm.From;
                objFromDb.ConsentOption = (ConsentOption)consentForm.ConsentOption;
                objFromDb.Evaluation = consentForm.Evaluation;
                objFromDb.StudentId = consentForm.StudentId;
                objFromDb.Status = consentForm.Status;
                objFromDb.SubmittedDate = consentForm.SubmittedDate;
                objFromDb.Version ++;
                consentFormResult = await db.UpdateConsentFormAsync(objFromDb);

            }

            await studentService.UpdateSeProcessSteps(consentForm.StudentId, SEProcessSteps.SE3A, SEProcessSteps.SE4);
            return consentFormResult;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException("Someone else updated this form. Please refresh and try again.", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error creating consent form");
            throw;
        }
    }
    
    // public async Task<ConsentForm?> CreateConsent(ConsentFormViewModel consentForm)
    // {
    //     try
    //     {
    //         ConsentForm? objFromDb = new ConsentForm();
    //         ConsentForm? consentFormResult = null;
    //         if (consentForm.ConsentId == null)
    //         {
    //             objFromDb = new ConsentForm
    //             {
    //                 EnteredDate = consentForm.EnteredDate,
    //                 To = consentForm.To,
    //                 From = consentForm.From,
    //                 Evaluation = consentForm.Evaluation,
    //                 StudentId = consentForm.StudentId,
    //                 Version = 1,
    //             };
    //             consentFormResult = await db.SaveConsentFormAsync(objFromDb);
    //
    //         }
    //         else
    //         {
    //             objFromDb.Id = consentForm.ConsentId;
    //             objFromDb.EnteredDate = consentForm.EnteredDate;
    //             objFromDb.To = consentForm.To;
    //             objFromDb.From = consentForm.From;
    //             objFromDb.ConsentOption = (ConsentOption)consentForm.ConsentOption;
    //             objFromDb.Evaluation = consentForm.Evaluation;
    //             objFromDb.StudentId = consentForm.StudentId;
    //             objFromDb.Status = consentForm.Status;
    //             objFromDb.SubmittedDate = consentForm.SubmittedDate;
    //             consentFormResult = await db.UpdateConsentFormAsync(objFromDb,consentForm.Version);
    //
    //         }
    //
    //         await studentService.UpdateSeProcessSteps(consentForm.StudentId, SEProcessSteps.SE3A, SEProcessSteps.SE4);
    //         return consentFormResult;
    //     }
    //     catch (DbUpdateConcurrencyException ex)
    //     {
    //         throw new DbUpdateConcurrencyException("Someone else updated this form. Please refresh and try again.", ex);
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError(ex, $"Error creating consent form");
    //         throw;
    //     }
    // }

    public void RemoveById(int resultId)
    {
        db.RemoveById(resultId);
    }
    

    public async Task<ConsentForm?> GetByIdAsync(int resultId)
    {
        return await db.GetByIdAsync(resultId);
    }
}