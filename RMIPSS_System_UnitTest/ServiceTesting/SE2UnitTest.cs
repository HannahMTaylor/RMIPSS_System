using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.ServiceTesting;

public class Ase2UnitTest
{
    [Test]
    public async Task ShouldGetStudent()
    {
        // Arrange
        Student? student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student savedStudent = await Repositories._studentRepository.Save(student);

        Se2Service sut = new Se2Service(Loggers._se2Logger,
                                        Repositories._se2Repository,
                                        Repositories._studentRepository,
                                        Repositories._appUserRepo);
        
        // Act
        Student? result = sut.GetStudent(student.Id).Result;
        
        // Assert
        Assert.That(result, Is.EqualTo(student));
        
        // Revert the changes
        Repositories._studentRepository.Remove(student);
        Repositories._studentRepository.Save();
        result = await sut.GetStudent(student.Id);
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task ShouldGetLoggedInUser()
    {
        // Arrange
        ApplicationUser? appUser = new ApplicationUser()
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe123456@gmail.com",
        };

        ApplicationUser savedAppUser = await Repositories._appUserRepo.Save(appUser);

        Se2Service sut = new Se2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
        // Act
        ApplicationUser? result = await sut.GetLoggedInUser(appUser.UserName);
        
        // Assert
        Assert.That(result, Is.EqualTo(appUser));
        
        // Revert the changes
        await Repositories._appUserRepo.Remove(appUser);
        await Repositories._appUserRepo.Save();
        result = await sut.GetLoggedInUser(appUser.UserName);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ShouldSaveFormDataAndGetSE2Data()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student? savedStudent = await Repositories._studentRepository.Save(student);

        SE2? se2 = new()
        {
            Student = student,
            CompletedByName = "Prince",
            CompletedByRelationship = "Teacher",
            CompletedByPhone = "555-555-5555",
            CompletedByEmail = "prince@gmail.com"
        };

        Se2Service sut = new Se2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
        // Act
        sut.SaveFormData(se2);
        SE2? result = await sut.GetSe2Data(student.Id);
        
        // Assert
        Assert.That(result, Is.EqualTo(se2));
        
        /*
         * **** Revert the changes - Student ****
         * 
         * Automatically deletes the associated SE2 form when a Student is deleted due to
         * configured cascading delete behavior.
         */
        Repositories._studentRepository.Remove(savedStudent);
        Repositories._studentRepository.Save();
        Student? deletedStudent =await sut.GetStudent(student.Id);
        Assert.That(deletedStudent, Is.Null);
    }
    
    [Test]
    public async Task ShouldUpdateFormData()
    {
        // Arrange
        Student student = new Student()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
        };

        Student? savedStudent = await Repositories._studentRepository.Save(student);

        SE2? se2 = new()
        {
            Student = student,
            CompletedByName = "Prince",
            CompletedByRelationship = "Teacher",
            CompletedByPhone = "555-555-5555",
            CompletedByEmail = "prince@gmail.com"
        };

        Se2Service sut = new Se2Service(Loggers._se2Logger,
            Repositories._se2Repository,
            Repositories._studentRepository,
            Repositories._appUserRepo);
        
       await sut.SaveFormData(se2);
        
        se2.CompletedByName = "James";
        
        // Act
        await sut.UpdateFormData(se2);
        SE2? result = await sut.GetSe2Data(student.Id);
        
        // Assert
        Assert.That(result.CompletedByName, Is.EqualTo("James"));
        
        /*
         * **** Revert the changes - Student ****
         *
         * Automatically deletes the associated SE2 form when a Student is deleted due to
         * configured cascading delete behavior.
         */
        await Repositories._studentRepository.Remove(savedStudent);
        await  Repositories._studentRepository.Save();
        Student? deletedStudent =await sut.GetStudent(student.Id);
        Assert.That(deletedStudent, Is.Null);
    }
}