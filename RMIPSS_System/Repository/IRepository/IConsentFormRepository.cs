using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IConsentFormRepository : IRepository<ConsentForm>
{
    void Update(ConsentForm school);
    void Save();
    ConsentForm SaveConsentForm(ConsentForm consentForm);
}
