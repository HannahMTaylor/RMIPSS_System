using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

/// <summary>
/// The controller handles basic CRUD functionality and will utilize the repository and service classes for the referral process step
/// </summary>
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


    //create new referral

    //load/read existing referral

    //update existing referral

    //delete existing referral
}
