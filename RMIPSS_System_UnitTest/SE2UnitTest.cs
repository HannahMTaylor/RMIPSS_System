using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;
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
}