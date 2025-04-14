using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

namespace RMIPSS_System.Models.ViewModel;

public class ReferralViewModel
{
    public ReferralViewModel() //this is added to hopefully fix null reference exception on pdf upload in ReferralController line 107
    {
        Referral = new Referral();
    }
    public Student Student {  get; set; }
    public ReferrerPerson Person { get; set; }
    public Referral Referral { get; set; }

    public IFormFile? UploadedFile { get; set; }
}
