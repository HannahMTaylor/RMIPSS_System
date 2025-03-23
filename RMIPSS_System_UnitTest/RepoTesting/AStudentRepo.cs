using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System_UnitTest.RepoTesting;

public class AStudentRepo
{
    private IStudentRepository _sut;
    [SetUp]
    public void Setup()
    {
        //Arrange
        _sut = Repositories._studentRepository;
    }

    [Test]
    public async Task ShouldGetStudentByIdAsync()
    {
        //Arrange
        Student student = await DummyObject.CreateDummyStudent();
        
        //Act
       Student? receivedStudent = await _sut.GetByStudentIdAsync(student.Id);
       
       //Assert
       Assert.That(receivedStudent, Is.Not.Null);
       Assert.That(receivedStudent.Id, Is.EqualTo(student.Id));
       
       //Remove from database
       await DummyObject.DeleteDummyStudent(student.Id);
       
    }

    [Test]
    public async Task ShouldGetProcessStepIdsByStudentId()
    {
        //Arrange
        Student student = await DummyObject.CreateDummyStudent();
        int se2Id = await DummyObject.CreateDummySe2Form(student.Id);
        int consentFormId = await DummyObject.CreateDummyConsentForm(student.Id);
        
        //Act
        // Fetch all required process step IDs in a single database call
        var processStepIds = await _sut.GetProcessStepIdsByStudentId(student.Id, new[]
        {
            typeof(ConsentForm),
            typeof(SE2)
        });
        
        // Extract IDs from dictionary
        int consentId = processStepIds.GetValueOrDefault(typeof(ConsentForm), 0);
        int savedse2Id = processStepIds.GetValueOrDefault(typeof(SE2), 0);
        
        //Assert
        Assert.False(consentId.Equals(0));
        Assert.That(consentId, Is.EqualTo(consentFormId));
        Assert.False(savedse2Id.Equals(0));
        Assert.That(savedse2Id, Is.EqualTo(se2Id));
        
        //Remove from database
        await DummyObject.DeleteDummySe2Form(se2Id);
        await DummyObject.DeleteDummyConsentForm(consentId);
        await DummyObject.DeleteDummyStudent(student.Id);
        
    }

    
   
    
}