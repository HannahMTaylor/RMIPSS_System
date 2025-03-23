using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.ServiceTesting;

public class AStudent
{
    
    private StudentService _sut; // System Under Test
    
    [SetUp]
    public void Setup()
    {
        //Arrange
        _sut = Services.StudentService;
    }
    
    [Test]
    public async Task ShouldGetStudent()
    {
        
        // Arrange
        Student student = await DummyObject.CreateDummyStudent();
        int se2Id = await DummyObject.CreateDummySe2Form(student.Id);
        int consentId = await DummyObject.CreateDummyConsentForm(student.Id);
        
       //Act
       var studentViewModel = await _sut.GetStudentByIdAsync(student.Id, null);
       
       //Assert
       Assert.That(studentViewModel, Is.Not.Null);
       Assert.That(studentViewModel.Id, Is.EqualTo(student.Id));
       Assert.That(studentViewModel.FirstName, Is.EqualTo(student.FirstName));
       Assert.That(studentViewModel.SEProcessSteps, Is.EqualTo(student.SEProcessSteps));
       Assert.That(studentViewModel.upcomingSEForms, Is.Not.Null );
       Assert.That(studentViewModel.documentsList.Count, Is.EqualTo(2));
     
       
       //Remove from database
       await DummyObject.DeleteDummySe2Form(se2Id);
       await DummyObject.DeleteDummyConsentForm(consentId);
       await DummyObject.DeleteDummyStudent(student.Id);

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

      await  Repositories._studentRepository.SaveAsync();
        
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
            Repositories._studentRepository.Remove(result);
            Repositories._studentRepository.Save();
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

        Student savedStudent = Repositories._studentRepository.Save(student);
        
        // Act
        var (students, totalStudents) =
            await _sut.GetPaginatedStudentsAsync("1234567890 qwertyuiopasdfghjkl", null, 1, 1);
        
        // Assert
        Assert.That(totalStudents, Is.EqualTo(1));
        Assert.That(students, Is.Not.Empty);
        
        // Revert the changes
        Repositories._studentRepository.Remove(student);
        Repositories._studentRepository.Save();
        Student? result = _sut.GetStudent(student.Id).Result;
        Assert.That(result, Is.Null);
    }
}