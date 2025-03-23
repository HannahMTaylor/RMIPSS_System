using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System_UnitTest;

public class DummyObject
{
    public async static Task<Student> CreateDummyStudent()
    {
        // Arrange
        var student = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            SEProcessSteps = SEProcessSteps.SE4
            
        };

        Student savedStudent =  Repositories._studentRepository.Save(student);
       // CreateDummyConsentForm(savedStudent.Id);
        return savedStudent;

    }

    public async static Task<int> CreateDummyConsentForm(int studentId)
    {
        var c = new ConsentForm
        {
            EnteredDate = new DateOnly(),
            To = "Parent",
            From = "Principal",
            ConsentOption = ConsentOption.NotGiven,
            Evaluation = true,
            StudentId = studentId,
        };
        //Act
       ConsentForm? consentForm  = await Repositories._consentFormRepo.SaveConsentFormAsync(c);
       if (consentForm != null) 
           return consentForm.Id;
       
       return 0;
    }
    
    public  static ConsentFormViewModel CreateConsentFormViewModel(int studentId)
    {
        var c = new ConsentFormViewModel()
        {
            EnteredDate = new DateOnly(),
            To = "Parent",
            From = "Principal",
            ConsentOption = (int)ConsentOption.NotGiven,
            Evaluation = true,
            StudentId = studentId,
        };
        return c;
    }
    
    public async static Task<int> CreateDummySe2Form(int studentId)
    {
        SE2? se2 = new()
        {
            StudentId = studentId,
            CompletedByName = "Prince",
            CompletedByRelationship = "Teacher",
            CompletedByPhone = "555-555-5555",
            CompletedByEmail = "prince@gmail.com"
        };
        //Act
        SE2? savedsSe2 = Repositories._se2Repository.Save(se2);
        if (savedsSe2 != null) 
            return savedsSe2.Id;
       
        return 0;
    }

    public async static Task DeleteDummyConsentForm(int consentFormId)
    {
        await Repositories._consentFormRepo.RemoveByIdAsync(consentFormId);
        Repositories._consentFormRepo.Save();
        ConsentForm? removedConsentForm = await Repositories._consentFormRepo.GetByIdAsync(consentFormId);
        Assert.That(removedConsentForm, Is.EqualTo(null));
    }

    public async static Task DeleteDummySe2Form(int se2Id)
    {
        await Repositories._se2Repository.RemoveByIdAsync(se2Id);
        Repositories._se2Repository.Save();
        SE2? savedse2 = await Repositories._se2Repository.GetByIdAsync(se2Id);
        Assert.That(savedse2, Is.EqualTo(null));
    }

    public async static Task DeleteDummyStudent(int studentId)
    {
        await Repositories._studentRepository.RemoveByIdAsync(studentId);
        Repositories._studentRepository.Save();
        Student? removedStudent = await Repositories._studentRepository.GetByIdAsync(studentId);
        Assert.That(removedStudent, Is.EqualTo(null));
    }

    public static User GenerateDummyUser()
    {
        var user = new User
        {
            FirstName = "Tester",
            LastName = "Doe",
            Email = "test@gmail.com",
            PhoneNumber = "1234567890",
            Password = "Test@1234",
            ConfirmPassword = "Test@1234",
            Role = "ROLE_SCHOOL_USER"
        };
        return user;
    }
    
    public static async Task<User> CreateDummyUser()
    {
        var user = new User
        {
            FirstName = "Tester",
            LastName = "Doe",
            Email = "test@gmail.com",
            PhoneNumber = "1234567890",
            Password = "Test@1234",
            ConfirmPassword = "Test@1234",
            Role = "ROLE_SCHOOL_USER"
        };
        _ = await Services.UserService.CreateUser(user);
      /*  ApplicationUser applicationUser = await Repositories._appUserRepo.CreateApplicationUserAsync(user, "Test@1234");
      //  var savedUser = new User()
     //   {
     //     FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Email = applicationUser.Email,
            PhoneNumber = applicationUser.PhoneNumber,
        };
          */
        Console.WriteLine("check"+ user.Email);
      
        return user;
    }

    public static async Task DeleteDummyUser(String email)
    {
        await Repositories._appUserRepo.DeleteUserAsync(email);
    }
    
}