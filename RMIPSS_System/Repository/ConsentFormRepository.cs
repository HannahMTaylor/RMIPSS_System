using RMIPSS_System.Data;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ConsentFormRepository(ApplicationDbContext db) : Repository<ConsentForm>(db), IConsentFormRepository
{
    private readonly ApplicationDbContext _db = db;


    public void Update(ConsentForm? consentForm)
    {
        _db.ConsentForms.Update(consentForm); 
        _db.SaveChangesAsync();
    }
    

    public async Task<ConsentForm?> SaveConsentFormAsync(ConsentForm? consentForm)
    {
       await _db.ConsentForms.AddAsync(consentForm);
       await _db.SaveChangesAsync();
       return consentForm;
    }

    public async Task<ConsentForm?> GetConsentFormByStudentId(int id)
    {
        ConsentForm? consentForm = await GetAsync(c =>
            c != null && c.Student != null && c.Student.Id == id
        );
        return consentForm;

    }

    public async Task<ConsentForm?> UpdateConsentFormAsync(ConsentForm? consentForm)
    { 
        _db.ConsentForms.Update(consentForm);
       await _db.SaveChangesAsync();
        return  consentForm;
    }
}
