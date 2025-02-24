using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;
/// <summary>
/// The service class is used to get logged in user info, student info, and other necessary information or system information to load pages from the controller.
/// 
/// This is for use of viewing/editing existing referral forms by student id which should be the only time the referral process requires a logged in user.
/// 
/// Creating a new referral should be accessible by anyone.
/// </summary>
public class ReferralService
{
    private readonly ILogger<ReferralService> _logger;
    private readonly IStudentRepository _studentRepo;
    private readonly IApplicationUserRepository _appUserRepo;


    //get logged in user
    public async Task<ApplicationUser> GetLoggedInUser(string? name)
    {
        throw new NotImplementedException();
    }

    //get student id - if applicable
    public async Task<Student> GetStudentById(int studentId)
    {
        throw new NotImplementedException();
    }

    //get referral id/data for view existing referrrals (if null - create new referral? or redirect to home page so they can create one)
    public async Task<Referral> GetReferralDataByStudentId(int studentId)
    {
        throw new NotImplementedException();
    }

    //map viewmodel to referral data
    public Task<Referral> ConvertViewModel(ReferralViewModel referralVM)
    {
        Referral referralEntity = new();
        try
        {
            referralEntity = referralVM.referral;
            return Task.FromResult(referralEntity);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error converting view model to model");
            throw;
        }

    }

    
}
