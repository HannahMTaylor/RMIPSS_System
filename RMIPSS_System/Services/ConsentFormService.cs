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
            ConsentForm? consentFormResult;
            if (objFromDb == null)
            {
                objFromDb = new ConsentForm
                {
                    EnteredDate = consentForm.EnteredDate,
                    To = consentForm.To,
                    From = consentForm.From,
                    Evaluation = consentForm.Evaluation,
                    StudentId = consentForm.StudentId
                };
                consentFormResult =  await db.SaveConsentFormAsync(objFromDb);
               
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
                consentFormResult =  await db.UpdateConsentFormAsync(objFromDb);
                
            }
            await studentService.UpdateSeProcessSteps(consentForm.StudentId, SEProcessSteps.SE3A, SEProcessSteps.SE4);
            return consentFormResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error creating consent form");
            throw;
        }
    }

    public void RemoveById(int resultId)
    {
        db.RemoveById(resultId);
    }
    

    public async Task<ConsentForm?> GetByIdAsync(int resultId)
    {
        return await db.GetByIdAsync(resultId);
    }
}