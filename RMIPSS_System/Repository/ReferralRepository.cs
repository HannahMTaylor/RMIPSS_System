using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Data;

namespace RMIPSS_System.Repository;
/// <summary>
/// The repository class implements an interface for database related functions to minimize coupling between controller and db
/// </summary>
public class ReferralRepository : Repository<Referral>, IReferralRepository
{
    private ApplicationDbContext _db;

    public ReferralRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Referral referral)
    {
        _db.Referrals.Update(referral);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public async Task<Referral> SaveReferralAsync(Referral referral)
    {
        await _db.Referrals.AddAsync(referral);
        await _db.SaveChangesAsync();
        return referral;
    }

    public async Task<Referral> GetReferralByStudentId(int studentId)
    {
        Referral referral = await GetAsync(r => r.Student.Id == studentId);
        return referral;
    }

    public async Task<Referral> UpdateReferralAsync(Referral referral)
    {
        _db.Referrals.Update(referral);
        _db.SaveChanges();
        return referral;
    }
}
