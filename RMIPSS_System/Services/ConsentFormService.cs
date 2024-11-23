using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.ViewModel;

namespace RMIPSS_System.Services;

public class ConsentFormService
{
    private readonly ILogger<ConsentFormService> _logger;
    private readonly IConsentFormRepository _consentFormRepository;
    private readonly IRepository<Student> _repositoryStudent;

    public ConsentFormService(ILogger<ConsentFormService> logger, IConsentFormRepository db, IRepository<Student> repositoryStudent)
    {
        _logger = logger;
        _consentFormRepository = db;
        _repositoryStudent = repositoryStudent;
    }
    
    public async Task<ConsentForm> CreateConsentForm(ConsentFormViewModel consentForm)
    {
        Student student = _repositoryStudent.GetById(consentForm.StudentId);
        try
        {
            ConsentForm objFromDb = _consentFormRepository.GetAsync(c => c.Id == consentForm.Id).Result;
            if (objFromDb == null)
            {
                objFromDb = new ConsentForm();
                objFromDb.Date = consentForm.Date;
                objFromDb.To = consentForm.To;
                objFromDb.From = consentForm.From;
                objFromDb.Evaluation = consentForm.Evaluation;
                objFromDb.StudentId = consentForm.StudentId;
                ConsentForm consentFormResult =  await _consentFormRepository.SaveConsentFormAsync(objFromDb);
                return consentFormResult;
            }
            else
            {
             
                objFromDb.Id = consentForm.Id;
                objFromDb.Date = consentForm.Date;
                objFromDb.To = consentForm.To;
                objFromDb.From = consentForm.From;
                objFromDb.ConsentOption = (ConsentOption)consentForm.ConsentOption;
                objFromDb.Evaluation = consentForm.Evaluation;
                objFromDb.StudentId = consentForm.StudentId;
                objFromDb.Status = consentForm.Status;
                objFromDb.SubmittedDate = consentForm.SubmittedDate;
                ConsentForm consentFormResult =  await _consentFormRepository.UpdateConsentFormAsync(objFromDb);
                return consentFormResult;
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating consent form for : {student.FirstName}");
            throw;
        }
    }

    public void RemoveById(int resultId)
    {
        _consentFormRepository.RemoveById(resultId);
    }

    public void Save()
    {
        _consentFormRepository.Save();
    }

    public ConsentForm GetById(int resultId)
    {
        return _consentFormRepository.GetById(resultId);
    }
}