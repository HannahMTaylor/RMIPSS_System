﻿using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using Microsoft.AspNetCore.Authorization;
using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Controllers;

/// <summary>
/// The controller handles basic CRUD functionality and will utilize the repository and service classes for the referralVM process step
/// </summary>
/// 


public class ReferralController : Controller
{
    private readonly ILogger<ReferralController> _logger;
    private readonly IReferralRepository _referralRepo;
    private readonly ReferralService _referralService;

    public ReferralController(ILogger<ReferralController> logger, IReferralRepository referralRepo, ReferralService referralService)
    {
        _logger = logger;
        _referralRepo = referralRepo;
        _referralService = referralService;
    }


    //create new referralVM when click on referral button on home screen or dashboard
    //pull logged in user info as referrer if possible - but leave editable
    public async Task<IActionResult> CreateReferralForm()
    {
        //is this method needed? how to decrease coupling between dashboard nav links and home controller that just return the view?
        //how does amit control loading of SE2 with prepop data
        try
        {
            ApplicationUser appUser = await _referralService.GetLoggedInUser(User.Identity.Name);
            if (appUser != null)
            {
                ReferralViewModel viewModelWithuser = new ReferralViewModel
                {
                    student = new(),
                    person = new ReferrerPerson
                    {
                        FullName = appUser.FirstName + " " + appUser.LastName,
                        Phone = appUser.PhoneNumber,
                        Email = appUser.Email,
                        DateFilledReferral = DateOnly.FromDateTime(DateTime.UtcNow)
                    },
                    referral = new()
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
            student = new(),
            person = new(),
            referral = new()
        };
        return View(viewModel);
    }



    //(on submit button press)
    //need to add some sort of functionality to save incrementally?? might add in next sprint
    //
    //*** need to go back through and make sure Student and Referrer are getting created and mapped properly to Referral on submission
    /// <summary>
    /// Save referral form - submit button (includes input validation)
    /// </summary>
    /// <param name="referralVM"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateReferralForm(ReferralViewModel referralVM)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    ApplicationUser appUser = await _referralService.GetLoggedInUser(User.Identity.Name);
                    if (appUser != null)
                    {
                        ReferralViewModel viewModelWithuser = new ReferralViewModel
                        {
                            student = new(),
                            person = new ReferrerPerson
                            {
                                FullName = appUser.FirstName + " " + appUser.LastName,
                                Phone = appUser.PhoneNumber,
                                Email = appUser.Email,
                                DateFilledReferral = DateOnly.FromDateTime(DateTime.UtcNow)
                            },
                            referral = new()
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
                    student = new(),
                    person = new(),
                    referral = new()
                };
                return View(viewModel);
            }
            //use service class to convert view model to model and pass model to controllers
            Referral refer = await _referralService.ConvertViewModel(referralVM);

            //then update database
            await _referralRepo.SaveReferralAsync(refer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving referral form. Please try again later");
            return RedirectToAction("Error", "Home");

        }
        return RedirectToAction("Index", "Home"); //how to redirect back to previous page -- they could need to go to dashboard not be logged out
        
    }

    //load/read existing referralVM -- needs logged in user permissions
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

            //if student is valid... continue fetching referral data
            Referral referralEntForStudent = await _referralService.GetReferralDataByStudentId(studentId);
            if (referralEntForStudent != null)
            {
                ReferralViewModel referralViewModel = new ReferralViewModel
                {
                    student = student,
                    person = referralEntForStudent.Referrer!,
                    referral = referralEntForStudent
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
