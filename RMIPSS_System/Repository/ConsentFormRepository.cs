using Microsoft.EntityFrameworkCore;
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
        _db.SaveChanges();
        return consentForm;
    }

    public async Task<int> GetVersionByConsentFormId(int id)
    {
        ConsentForm? consentForm = await GetAsync(c =>
            c != null && c.Student != null && c.Student.Id == id
        );
        return consentForm.Version;
    }

    // public async Task<ConsentForm?> UpdateConsentFormAsync(ConsentForm? consentForm, int oldVersion)
    // { 
    //     Console.WriteLine(oldVersion);
    //     _db.Attach(consentForm);
    //     _db.Entry(consentForm).Property("Version").OriginalValue =oldVersion;
    //     // Increment the version manually
    //     consentForm.Version++;
    //     Console.WriteLine(consentForm.Version);
    //     // Optionally mark other properties as modified
    //     _db.Entry(consentForm).State = EntityState.Modified;
    //     
    //     try
    //     {
    //         await _db.SaveChangesAsync();
    //         return consentForm;
    //     }
    //     catch (DbUpdateConcurrencyException ex)
    //     {
    //         // THIS will be triggered if version doesn't match
    //         throw new DbUpdateConcurrencyException("Conflict detected while updating form.", ex);
    //     }
    // }
}
