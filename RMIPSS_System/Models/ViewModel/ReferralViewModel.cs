using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

namespace RMIPSS_System.Models.ViewModel
{
    public class ReferralViewModel
    {
        public Student student {  get; set; }
        public ReferrerPerson person { get; set; }
        public Referral referral { get; set; }
    }
}
