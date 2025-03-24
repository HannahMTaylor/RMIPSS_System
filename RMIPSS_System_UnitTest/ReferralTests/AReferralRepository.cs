using System;
using System.Collections.Generic;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System_UnitTest.Common;

namespace RMIPSS_System_UnitTest.ReferralTests;

public class AReferralRepository
{
    [Test]
    public void ShouldSaveReferralsToTheDatabase()
    {
        //Arrange
        Student testStudent = new Student
        {
            FirstName = "Test",
            LastName = "TestLast",
            Email = "TestEmail@gmail.com"
        };
        Repositories._studentRepository.Save(testStudent);

        DateOnly testDate = new DateOnly(2025, 3, 24);

        ReferrerPerson testPerson = new ReferrerPerson
        {
            FullName = "Test",
            Phone = "1234567890",
            Email = "1234567890@mail.com",
            DateFilledReferral = testDate
        };
        Repositories._refPersonRepo.Save(testPerson);

        ReferralRepository sut = new ReferralRepository();

        //Act

        //Assert
    }
}
