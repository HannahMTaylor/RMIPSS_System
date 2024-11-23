using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ConsentFormRepository : Repository<ConsentForm>, IConsentFormRepository
{
    private ApplicationDbContext _db;

    public ConsentFormRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(ConsentForm consentForm)
    {
        _db.ConsentForms.Update(consentForm);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public async Task<ConsentForm> SaveConsentFormAsync(ConsentForm consentForm)
    {
        _db.ConsentForms.Add(consentForm);
        _db.SaveChanges();
        return consentForm;
    }

    public async Task<ConsentForm> GetConsentFormByStudentId(int id)
    {
        ConsentForm consentForm = await GetAsync(c =>
            c.Student.Id == id
        );
        return consentForm;

    }

    public async Task<ConsentForm> UpdateConsentFormAsync(ConsentForm consentForm)
    {
        _db.ConsentForms.Update(consentForm);
        _db.SaveChanges();
        return consentForm;
    }
}
