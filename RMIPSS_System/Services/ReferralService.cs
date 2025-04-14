using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

/// <summary>
/// basic CRUD functionality and will utilize the repository and service classes for the referralVM process step
/// </summary>
public class ReferralService
{
    private readonly ILogger<ReferralService> _logger;
    private readonly IStudentRepository _studentRepo;
    private readonly IApplicationUserRepository _appUserRepo;
    private readonly IReferralRepository _referralRepo;

    //constructor
    public ReferralService(ILogger<ReferralService> logger, IStudentRepository studentRepo, IApplicationUserRepository appUserRepo)
    {
        _logger = logger;
        _studentRepo = studentRepo;
        _appUserRepo = appUserRepo;
    }

    /// <summary>
    /// The service class is used to get logged in user info, Student info, and other necessary information or system information to load pages from the controller.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<ApplicationUser> GetLoggedInUser(string? name)
    {
        ApplicationUser? appUser = await _appUserRepo.GetAsync(user =>
            user.UserName.ToLower() == name
        );
        return appUser;
    }

    //get Student id - if applicable
    /// <summary>
    /// This is for use of viewing/editing existing Referral forms by student id which should be the only time the Referral process requires a logged in user.
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Student> GetStudentById(int studentId)
    {
        Student? lookupStudent = await _studentRepo.GetByStudentIdAsync(studentId);
        return lookupStudent;

    }
    
    /// <summary>
    /// This should be used to view existing referrals only, shouldn't need to create new here -- done in controller
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Referral> GetReferralDataByStudentId(int studentId)
    {
        //get Referral id/data for view existing referrrals (if null - create new Referral? or redirect to home page so they can create one)
        Referral lookupReferral = await _referralRepo.GetReferralByStudentId(studentId);
        return lookupReferral;
    }
}
