using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMIPSS_System.Models;
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

    //constructor
    public ReferralService(ILogger<ReferralService> logger, IStudentRepository studentRepo, IApplicationUserRepository appUserRepo)
    {
        _logger = logger;
        _studentRepo = studentRepo;
        _appUserRepo = appUserRepo;
    }

    //get logged in user
    public async Task<ApplicationUser> GetLoggedInUser(string? name)
    {
        ApplicationUser appUser = await _appUserRepo.GetAsync(user =>
            user.UserName.ToLower() == name
        );
        return appUser;
    }

    //get student id - if applicable
    /// <summary>
    /// This should be used when viewing existing referrals for students
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Student> GetStudentById(int studentId)
    {
        throw new NotImplementedException();
    }

    //get referral id/data for view existing referrrals (if null - create new referral? or redirect to home page so they can create one)
    /// <summary>
    /// This should be used to view existing referrals only, shouldn't need to create new here -- done in controller
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Referral> GetReferralDataByStudentId(int studentId)
    {
        throw new NotImplementedException();
    }

    //map viewmodel to referral data
    public Task<Referral> ConvertViewModel(ReferralViewModel referralVM)
    {
        
        try
        {
            //break this out and map inputs directly to make sure no data is null
            Referral referralEntity = referralVM.referral;
            return Task.FromResult(referralEntity);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error converting view model to model");
            throw;
        }

    }

    
}
