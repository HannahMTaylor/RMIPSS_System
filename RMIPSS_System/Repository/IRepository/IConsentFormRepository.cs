using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

namespace RMIPSS_System.Repository.IRepository;

public interface IConsentFormRepository : IRepository<ConsentForm>
{
    void Update(ConsentForm? school);
   
    Task<ConsentForm?> SaveConsentFormAsync(ConsentForm? consentForm);
    
    Task<ConsentForm?> GetConsentFormByStudentId(int id);
    
    Task<ConsentForm?> UpdateConsentFormAsync(ConsentForm? consentForm);
    
    Task<int> GetVersionByConsentFormId(int id);
}
