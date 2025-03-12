using RMIPSS_System.Models.ProcessSteps;
namespace RMIPSS_System.Repository.IRepository;

public interface IReferralRepository : IRepository<Referral>
{
    void Update(Referral referral);
    void Save();
    Task<Referral> SaveReferralAsync(Referral referral);

    Task<Referral> GetReferralByStudentId(int studentId);

    Task<Referral> UpdateReferralAsync(Referral referral);
}
