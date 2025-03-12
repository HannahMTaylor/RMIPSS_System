using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

namespace RMIPSS_System.Models.ViewModel;

public class ReferralViewModel
{
    public Student Student {  get; set; }
    public ReferrerPerson Person { get; set; }
    public Referral Referral { get; set; }
}
