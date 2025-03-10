using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System_UnitTest;

public class ConsentFormUnitTest
{
    
    [Test]
    public async Task ShouldCreateConsentForm()
    {

        //Arrange
        ApplicationDbContextUnit unitConnection = new();
        DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
        ApplicationDbContext db = new ApplicationDbContext(options);
        IConsentFormRepository consentFormRepository=new ConsentFormRepository(db);
        IStudentRepository repositoryStudent = new StudentRepository(db);
        IRepository<School> repositorySchool = new Repository<School>(db);
        ILogger<ConsentFormService> logger = new Logger<ConsentFormService>(new LoggerFactory());
        ILogger<StudentService> loggerStudent = new Logger<StudentService>(new LoggerFactory());
        StudentService studentService = new StudentService(loggerStudent, repositoryStudent);
        ConsentFormService sut = new ConsentFormService( logger, consentFormRepository, studentService);
        School school = new ()
        {
            Name = "ETSU",
            Address = "Johnson City",
            Phone = "123456789"
        };
        School schoolSaved = repositorySchool.Save(school);
        Student student = new()
        {
            FirstName = "John",
            SchoolId = schoolSaved.Id,
            LastName = "Doe",
            ParentGuardianPrimaryLanguage = "ParentGuardianPrimaryLanguage",
            Phone = "123456789",
            GuardianName = "Johnny Doe",
        };
        Student studentSaved = repositoryStudent.Save(student);
        var c = new ConsentFormViewModel()
        {
            EnteredDate = new DateOnly(),
            To = "Parent",
            From = "Principal",
            ConsentOption = (int)ConsentOption.NotGiven,
            Evaluation = true,
            StudentId = studentSaved.Id
        };
        //Act
        ConsentForm? savedConsentForm = sut.CreateConsentForm(c).Result;
      
        //Assert
        Debug.Assert(savedConsentForm != null, nameof(savedConsentForm) + " != null");
        Assert.That(savedConsentForm.To, Is.EqualTo("Parent"));
        Assert.That(savedConsentForm.Id, Is.Not.EqualTo(0));
        
        //Remove from database
        sut.RemoveById(savedConsentForm.Id);
        await repositoryStudent.SaveAsync();
        ConsentForm? removedConsentForm = await sut.GetByIdAsync(savedConsentForm.Id);
        
        repositoryStudent.RemoveById(studentSaved.Id);
        repositoryStudent.Save();
        
        Student? removedStudent = await repositoryStudent.GetByIdAsync(studentSaved.Id);
        
        
        //Assert
        Assert.That(removedConsentForm, Is.EqualTo(null));
        Assert.That(removedStudent, Is.EqualTo(null));
        
    }
    
}