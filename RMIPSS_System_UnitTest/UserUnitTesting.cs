using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using RMIPSS_System.Data;
using RMIPSS_System.Models;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest
{
    [TestFixture]
    public class UserUnitTesting
    {
        private IEmailSender _emailSender;
        private IApplicationUserRepository _appUserRepo;
        private ILogger<UserService> _logger;
        private UserService _sut; // System Under Test

        [SetUp]
        public void Setup()
        {
                 //Arrange
        ApplicationDbContextUnit unitConnection = new();
        DbContextOptions<ApplicationDbContext> options = unitConnection.GetOptions();
        ApplicationDbContext _db = new ApplicationDbContext(options);
       
           UserManager<ApplicationUser> _userManager = unitConnection.GetUserManager();

           RoleManager<IdentityRole> _roleManager  = unitConnection.GetRoleManager();

            _emailSender = unitConnection.GetEmailSender(); //Use a simple email sender
            _logger = new LoggerFactory().CreateLogger<UserService>();
            _appUserRepo = new ApplicationUserRepository(_db, _userManager);
            _sut = new UserService(_logger, _appUserRepo, _roleManager, _emailSender);
        }

        [Test]
        public async Task ShouldCreateNewUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "sangam.shrestha1997@gmail.com",
                PhoneNumber = "1234567890",
                Password = "Test@1234",
                ConfirmPassword = "Test@1234",
                Role = "ROLE_SCHOOL_USER"
            };

            var userExistsBefore = await _sut.IsUserExist(user.Email);
            Assert.IsFalse(userExistsBefore);

            var result = await _sut.CreateUser(user);

            var userExistsAfter = await _sut.IsUserExist(user.Email);
            Assert.IsTrue(result);
            Assert.IsTrue(userExistsAfter);

            // Verify Email Was Sent
           // Act & Assert - Ensure No Exception is Thrown
            Assert.DoesNotThrowAsync(async () => await _sut.SendUserCreationEmail(user));

            var deleteResult= await _sut.DeleteUserAsync(user.Email);
            Assert.IsTrue(deleteResult);

            var userExistsAfterDelete = await _sut.IsUserExist(user.Email);
            Assert.IsFalse(userExistsBefore);

        }
    }

    // Fake Email Sender for Testing
    public class FakeEmailSender : IEmailSender
    {
        public bool EmailSent { get; private set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            EmailSent = true;
            return Task.CompletedTask;
        }
    }

   
}
