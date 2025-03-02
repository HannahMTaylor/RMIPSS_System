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
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System_UnitTest;

public class ConsentFormUnitTest
{
    
    [Test]
    public void ShouldCreateConsentForm()
    {

        //Arrange
        ApplicationDbContextUnit unitConnection = new();
        DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
        ApplicationDbContext _db = new ApplicationDbContext(options);
        IConsentFormRepository _consentFormRepository=new ConsentFormRepository(_db);
        IRepository<Student> _repositoryStudent = new Repository<Student>(_db);
        IRepository<School> _repositorySchool = new Repository<School>(_db);
        ILogger<ConsentFormService> _logger = new Logger<ConsentFormService>(new LoggerFactory());
        ConsentFormService sut = new ConsentFormService( _logger, _consentFormRepository, _repositoryStudent);
        School school = new ()
        {
            Name = "ETSU",
            Address = "Johnson City",
            Phone = "123456789"
        };
        School schoolSaved = _repositorySchool.Save(school);
        Student student = new()
        {
            FirstName = "John",
            SchoolId = schoolSaved.Id,
            LastName = "Doe",
            ParentGuardianPrimaryLanguage = "ParentGuardianPrimaryLanguage",
            Phone = "123456789",
            GuardianName = "Johnny Doe",
        };
        Student studentSaved = _repositoryStudent.Save(student);
        ConsentFormViewModel c = new ConsentFormViewModel();
        c = new ConsentFormViewModel()
        {
            Date = new DateOnly(),
            To = "Parent",
            From = "Principal",
            ConsentOption = (int)ConsentOption.NotGiven,
            Evaluation = true,
            StudentId = studentSaved.Id
        };
        //Act
        ConsentForm SavedConsentForm = sut.CreateConsentForm(c).Result;
      
        //Assert
        Assert.That(SavedConsentForm.To, Is.EqualTo("Parent"));
        Assert.That(SavedConsentForm.Id, Is.Not.EqualTo(0));
        
        //Remove from database
        sut.RemoveById(SavedConsentForm.Id);
        sut.Save();
        ConsentForm removedConsentForm = sut.GetById(SavedConsentForm.Id);
        
        _repositoryStudent.RemoveById(studentSaved.Id);
        _repositoryStudent.Save();
        
        Student removedStudent = _repositoryStudent.GetById(studentSaved.Id);
        
        
        //Assert
        Assert.That(removedConsentForm, Is.EqualTo(null));
        Assert.That(removedStudent, Is.EqualTo(null));
        
    }
    
}