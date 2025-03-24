using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using Microsoft.AspNetCore.Authorization;
using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Controllers;

/// <summary>
/// The controller handles API requests received from front end and passes it along to Service and Repo for handling
/// Increases Code Reusability
/// </summary>
/// 


public class ReferralController : Controller
{
    private readonly ILogger<ReferralController> _logger;
    private readonly IReferralRepository _referralRepo;
    private readonly ReferralService _referralService;
    private readonly IStudentRepository _studentRepository;
    private readonly IReferrerPersonRepository _referrerPersonRepo;

    public ReferralController(ILogger<ReferralController> logger, IReferralRepository referralRepo, ReferralService referralService, IStudentRepository studentRepository, IReferrerPersonRepository referrerPersonRepo)
    {
        _logger = logger;
        _referralRepo = referralRepo;
        _referralService = referralService;
        _studentRepository = studentRepository;
        _referrerPersonRepo = referrerPersonRepo;
    }


    //create new referralVM when click on Referral button on home screen or dashboard
    //pull logged in user info as referrer if possible - but leave editable
    public async Task<IActionResult> CreateReferralForm()
    {
        try
        {
            ApplicationUser appUser = await _referralService.GetLoggedInUser(User.Identity.Name);
            if (appUser != null)
            {
                ReferralViewModel viewModelWithuser = new ReferralViewModel
                {
                    Student = new(),
                    Person = new ReferrerPerson
                    {
                        FullName = appUser.FirstName + " " + appUser.LastName,
                        Phone = appUser.PhoneNumber,
                        Email = appUser.Email,
                        DateFilledReferral = DateOnly.FromDateTime(DateTime.UtcNow)
                    },
                    Referral = new()
                };
                return View(viewModelWithuser);
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting logged in user");

        }

        ReferralViewModel viewModel = new ReferralViewModel
        {
            Student = new(),
            Person = new(),
            Referral = new()
        };
        return View(viewModel);
    }



    //(on submit button press)
    //need to add some sort of functionality to save incrementally?? might add in next sprint
    //
    //*** need to go back through and make sure Student and Referrer are getting created and mapped properly to Referral on submission
    /// <summary>
    /// Save Referral form - submit button (includes input validation)
    /// </summary>
    /// <param name="referralVM"></param>
    /// <returns></returns>
    public async Task<IActionResult> SaveReferralForm(ReferralViewModel referralVM)
    {
        try
        {
            //use service class to call to repo instead

            //save Student
            await _studentRepository.AddAsync(referralVM.Student);
            await _studentRepository.SaveAsync();
            //save referrerPerson
            //await _referrerPersonRepo.AddAsync(referralVM.Person);
            //await _referrerPersonRepo.SaveAsync();
            await _referrerPersonRepo.SaveReferrerPersonAsync(referralVM.Person);
            //then map Ids to Referral and save Referral
            //Referral refer = await _referralService.ConvertViewModel(referralVM);

            //then update database
            referralVM.Referral.Student = referralVM.Student;
            referralVM.Referral.Referrer = referralVM.Person;
            await _referralRepo.SaveReferralAsync(referralVM.Referral);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving referral form. Please try again later");
            return RedirectToAction("Error", "Home");

        }
        return RedirectToAction("Index", "Home"); //how to redirect back to previous page -- they could need to go to dashboard not be logged out
        
    }

    //load/read existing referralVM -- needs logged in user permissions
    //this is not being used yet
    [Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
    public async Task<IActionResult> ViewExistingReferralByStudentId([Bind(Prefix = "id")] int studentId)
    {
        try
        {
            Student student = await _referralService.GetStudentById(studentId);
            if (student == null)
            {
                _logger.LogError($"Error retrieving Student record with id: {studentId} from student database. Please try again later.");
                return RedirectToAction("Error", "Home");
            }

            //if Student is valid... continue fetching Referral data
            Referral referralEntForStudent = await _referralService.GetReferralDataByStudentId(studentId);
            if (referralEntForStudent != null)
            {
                ReferralViewModel referralViewModel = new ReferralViewModel
                {
                    Student = student,
                    Person = referralEntForStudent.Referrer!,
                    Referral = referralEntForStudent
                };
                return View(referralViewModel);
            }
            else
            {
                _logger.LogError($"Referral form does not exist for student with id: {studentId}");
                return RedirectToAction("Error", "Home");
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error loading Referral for student with id: {studentId}");
            return RedirectToAction("Error", "Home");
        }
    }

    //update existing referralVM -- needs logged in user permissions

    //delete existing referralVM -- needs logged in user permissions
}
