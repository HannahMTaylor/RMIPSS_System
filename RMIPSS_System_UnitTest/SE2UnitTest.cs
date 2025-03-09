using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest;

public class ASE2UnitTest
{
    [Test]
    public void ShouldGetStudent()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student savedStudent = Repositories._studentRepository.Save(student);

        SE2Service sut = new SE2Service(Loggers._se2Logger,
                                        Repositories._se2Repository,
                                        Repositories._studentRepository,
                                        Repositories._appUserRepo);
        
        // Act
        Student result = sut.GetStudent(student.Id).Result;
        
        // Assert
        Assert.That(result, Is.EqualTo(student));
        
        // Revert the changes
        Repositories._studentRepository.Remove(student);
        Repositories._studentRepository.Save();
        result = sut.GetStudent(student.Id).Result;
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public void GetLoggedInUser()
    {
        // Arrange
        ApplicationUser appUser = new ApplicationUser()
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe123456@gmail.com",
        };

        ApplicationUser savedAppUser = Repositories._appUserRepo.Save(appUser);

        SE2Service sut = new SE2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
        // Act
        ApplicationUser result = sut.GetLoggedInUser(appUser.UserName).Result;
        
        // Assert
        Assert.That(result, Is.EqualTo(appUser));
        
        // Revert the changes
        Repositories._appUserRepo.Remove(appUser);
        Repositories._appUserRepo.Save();
        result = sut.GetLoggedInUser(appUser.UserName).Result;
        Assert.That(result, Is.Null);
    }

    [Test]
    public void saveFormDataAndGetSE2Data()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student savedStudent = Repositories._studentRepository.Save(student);

        SE2 se2 = new()
        {
            Student = student,
            CompletedByName = "Prince",
            CompletedByRelationship = "Teacher",
            CompletedByPhone = "555-555-5555",
            CompletedByEmail = "prince@gmail.com"
        };

        SE2Service sut = new SE2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
        // Act
        sut.saveFormData(se2);
        SE2 result = sut.GetSE2Data(student.Id).Result;
        
        // Assert
        Assert.That(result, Is.EqualTo(se2));
        
        /*
         * **** Revert the changes - Student ****
         * 
         * Automatically deletes the associated SE2 form when a student is deleted due to
         * configured cascading delete behavior.
         */
        Repositories._studentRepository.Remove(savedStudent);
        Repositories._studentRepository.Save();
        Student deletedStudent = sut.GetStudent(student.Id).Result;
        Assert.That(deletedStudent, Is.Null);
    }
    
    [Test]
    public void updateFormData()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student savedStudent = Repositories._studentRepository.Save(student);

        SE2 se2 = new()
        {
            Student = student,
            CompletedByName = "Prince",
            CompletedByRelationship = "Teacher",
            CompletedByPhone = "555-555-5555",
            CompletedByEmail = "prince@gmail.com"
        };

        SE2Service sut = new SE2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
        sut.saveFormData(se2);
        
        se2.CompletedByName = "James";
        
        // Act
        sut.updateFormData(se2);
        SE2 result = sut.GetSE2Data(student.Id).Result;
        
        // Assert
        Assert.That(result.CompletedByName, Is.EqualTo("James"));
        
        /*
         * **** Revert the changes - Student ****
         *
         * Automatically deletes the associated SE2 form when a student is deleted due to
         * configured cascading delete behavior.
         */
        Repositories._studentRepository.Remove(savedStudent);
        Repositories._studentRepository.Save();
        Student deletedStudent = sut.GetStudent(student.Id).Result;
        Assert.That(deletedStudent, Is.Null);
    }
}