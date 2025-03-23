using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.ServiceTesting;

public class ConsentFormUnitTest
{
    private ConsentFormService _sut; // System Under Test
    
    [SetUp]
    public void Setup()
    {
        //Arrange
        _sut = Services.ConsentFormService;
    }
    
    [Test]
    public async Task ShouldCreateConsentForm()
    {
        //Arrange
        Student student = await DummyObject.CreateDummyStudent();
        ConsentFormViewModel consentForm = DummyObject.CreateConsentFormViewModel(student.Id);
        
        //Act
        ConsentForm? savedConsentForm = await _sut.CreateConsentForm(consentForm);
      
        //Assert
        Assert.That(savedConsentForm, Is.Not.Null);
        Assert.That(savedConsentForm.Id, Is.Not.EqualTo(0));
        Assert.That(savedConsentForm.To, Is.EqualTo("Parent"));
        Assert.That(savedConsentForm.StudentId, Is.EqualTo(student.Id));
        
        //Remove from database
        await DummyObject.DeleteDummyConsentForm(savedConsentForm.Id);
        await DummyObject.DeleteDummyStudent(student.Id);
        
        
    }
    
}