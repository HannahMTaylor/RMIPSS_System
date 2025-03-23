using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
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
        ApplicationDbContext db = new ApplicationDbContext(options);
        _logger = new LoggerFactory().CreateLogger<StudentService>();
        _studenRepo = new StudentRepository(db);
        _consentFormRepo = new ConsentFormRepository(db);
        _sut = new StudentService(_logger, _studenRepo);
    }
    
    [Test]
    public async Task ShouldGetStudent()
    {
        
        // Arrange
        var student = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            SEProcessSteps = SEProcessSteps.SE4
            
        };

       student = _studenRepo.Save(student);
       Assert.IsNotNull(student.Id);
       var c = new ConsentForm
       {
           EnteredDate = new DateOnly(),
           To = "Parent",
           From = "Principal",
           ConsentOption = ConsentOption.NotGiven,
           Evaluation = true,
           StudentId = student.Id
       };
       //Act
       ConsentForm? savedConsentForm = await _consentFormRepo.SaveConsentFormAsync(c);
       if (savedConsentForm == null) throw new ArgumentNullException(nameof(savedConsentForm));
       Assert.IsNotNull(savedConsentForm.Id);
       var studentViewModel = _sut.GetStudentByIdAsync(student.Id,null).Result;
       Assert.IsNotNull(studentViewModel);
       Assert.That(studentViewModel.FirstName, Is.EqualTo(student.FirstName));
       Assert.That(studentViewModel.SEProcessSteps, Is.EqualTo(student.SEProcessSteps));
       Assert.IsNotNull(studentViewModel.upcomingSEForms);
       Assert.IsNotNull(studentViewModel.documentsList);
     
       
       //Remove from database
       _consentFormRepo.RemoveById(savedConsentForm.Id);
       _consentFormRepo.Save();
       _studenRepo.RemoveById(student.Id);
       _studenRepo.Save();
       ConsentForm? removedConsentForm = await _consentFormRepo.GetByIdAsync(savedConsentForm.Id);
       Student? removedStudent = await _studenRepo.GetByIdAsync(student.Id);
        
       //Assert
       Assert.That(removedConsentForm, Is.EqualTo(null));
       Assert.That(removedStudent, Is.EqualTo(null));

    }

    [Test]
    public async Task ShouldUpdateSeProcessSteps()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            SEProcessSteps = SEProcessSteps.SE1
        };

      await  _studenRepo.SaveAsync();
        
        // Act
        await _sut.UpdateSeProcessSteps(student.Id, SEProcessSteps.SE1, SEProcessSteps.SE2);
        Student? result = _sut.GetStudent(student.Id).Result;
        
        // Assert
        if (result != null)
        {
            Assert.That(result.SEProcessSteps, Is.Not.EqualTo(SEProcessSteps.SE1));
            Assert.That(result.SEProcessSteps, Is.EqualTo(SEProcessSteps.SE2));
            Assert.That(result.SEProcessCompletedDate, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now)));

            // Revert the changes
            _studenRepo.Remove(result);
            _studenRepo.Save();
        }

        result = _sut.GetStudent(student.Id).Result;
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ShouldGetPaginatedStudentsAsync()
    {
        // Arrange
        var student = new Student()
        {
            FirstName = "1234567890",
            LastName = "qwertyuiopasdfghjkl"
        };

        Student savedStudent = _studenRepo.Save(student);
        
        // Act
        var (students, totalStudents) =
            await _sut.GetPaginatedStudentsAsync("1234567890 qwertyuiopasdfghjkl", null, 1, 1);
        
        // Assert
        Assert.That(totalStudents, Is.EqualTo(1));
        Assert.That(students, Is.Not.Empty);
        
        // Revert the changes
        _studenRepo.Remove(student);
        _studenRepo.Save();
        Student? result = _sut.GetStudent(student.Id).Result;
        Assert.That(result, Is.Null);
    }
}