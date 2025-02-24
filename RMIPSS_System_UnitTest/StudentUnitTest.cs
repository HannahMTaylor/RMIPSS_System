using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest;

public class StudentUnitTest
{
    
    private IStudentRepository _studenRepo;
    private ILogger<StudentService> _logger;
    private IConsentFormRepository _consentFormRepo;
    private StudentService _sut; // System Under Test
    [SetUp]
    public void Setup()
    {
        //Arrange
        ApplicationDbContextUnit unitConnection = new();
        DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
        ApplicationDbContext _db = new ApplicationDbContext(options);
        _logger = new LoggerFactory().CreateLogger<StudentService>();
        _studenRepo = new StudentRepository(_db);
        _consentFormRepo = new ConsentFormRepository(_db);
        _sut = new StudentService(_logger, _studenRepo);
    }
    
    [Test]
    public void ShouldGetStudent()
    {
        
        // Arrange
        StudentViewModel studentViewModel = new StudentViewModel();
        var student = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            SEProcessSteps = SEProcessSteps.SE4
            
        };

       student = _studenRepo.Save(student);
       Assert.IsNotNull(student.Id);
       ConsentForm c = new ConsentForm();
       c = new ConsentForm
       {
           Date = new DateOnly(),
           To = "Parent",
           From = "Principal",
           ConsentOption = ConsentOption.NotGiven,
           Evaluation = true,
           StudentId = student.Id
       };
       //Act
       ConsentForm SavedConsentForm = _consentFormRepo.SaveConsentFormAsync(c).Result;
       Assert.IsNotNull(SavedConsentForm.Id);
       studentViewModel =  _sut.GetStudentByIdAsync(student.Id).Result;
       Assert.IsNotNull(studentViewModel);
       Assert.AreEqual(student.FirstName, studentViewModel.FirstName);
       Assert.AreEqual(student.SEProcessSteps, studentViewModel.SEProcessSteps);
       Assert.IsNotNull(studentViewModel.upcomingSEForms);
       Assert.IsNotNull(studentViewModel.documentsList);
       Assert.IsNotNull(studentViewModel.documentsList.ContainsKey("Consent Form"));
     
       
       //Remove from database
       _consentFormRepo.RemoveById(SavedConsentForm.Id);
       _consentFormRepo.Save();
       _studenRepo.RemoveById(student.Id);
       _studenRepo.Save();
       ConsentForm removedConsentForm = _consentFormRepo.GetById(SavedConsentForm.Id);
       Student removedStudent = _studenRepo.GetById(student.Id);
        
       //Assert
       Assert.That(removedConsentForm, Is.EqualTo(null));
       Assert.That(removedStudent, Is.EqualTo(null));

    }
}