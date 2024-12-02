using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IConsentFormRepository : IRepository<ConsentForm>
{
    void Update(ConsentForm school);
    void Save();
    Task<ConsentForm> SaveConsentFormAsync(ConsentForm consentForm);
    
    Task<ConsentForm> GetConsentFormByStudentId(int id);
    
    Task<ConsentForm> UpdateConsentFormAsync(ConsentForm consentForm);
}
