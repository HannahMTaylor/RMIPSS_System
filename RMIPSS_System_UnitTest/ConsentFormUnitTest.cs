using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Repository;

namespace RMIPSS_System_UnitTest;

public class ConsentFormUnitTest
{
    
    [Test]

    public void ShouldCreateConsentForm()
    {

        //Arrange
        ApplicationDbContextUnit unitConnection = new();
        DbContextOptions<ApplicationDbContext> options = unitConnection.getOptions();
        ApplicationDbContext _db = new ApplicationDbContext(options);
        ConsentFormRepository sut = new ConsentFormRepository(_db);
        ConsentForm c = new()
        {
            Date = new DateOnly(),
            To = "Parent",
            From = "Principal",
            ConsentOption = ConsentOption.NotGiven,
            Evaluation = true
        };
        
        
        //Act
        ConsentForm SavedConsentForm= sut.SaveConsentForm(c);
      
        //Assert
        Assert.That(SavedConsentForm.To, Is.EqualTo("Parent"));
        Assert.That(SavedConsentForm.Id, Is.Not.EqualTo(0));
        
        //Remove from database
        sut.RemoveById(SavedConsentForm.Id);
        sut.Save();
        ConsentForm removedConsentForm = sut.GetById(SavedConsentForm.Id);
        
        //Assert
        Assert.That(removedConsentForm, Is.EqualTo(null));
        
    }
    
}