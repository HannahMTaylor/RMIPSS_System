using RMIPSS_System_UnitTest.Common;
using RMIPSS_System.Models;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.ServiceTesting
{
    [TestFixture]
    public class UserUnitTesting
    {
        private UserService _sut; // System Under Test

        [SetUp]
        public void Setup()
        {
                 //Arrange
                 _sut = Services.UserService;
        }

        [Test]
        public async Task ShouldCreateNewUser()
        {
            // Arrange
            User user = DummyObject.GenerateDummyUser();
            
            //Act
            var result = await _sut.CreateUser(user);
            

            // Verify Email Was Sent
           // Act & Assert - Ensure No Exception is Thrown
            Assert.DoesNotThrowAsync(async () => await _sut.SendUserCreationEmailAsync(user));
            Assert.That(result, Is.True);
            
            //Remove from database
            await DummyObject.DeleteDummyUser(user.Email);

        }

        [Test]
        public async Task ShouldNotExistUser()
        {
            //Arrange
            var user = DummyObject.GenerateDummyUser();
            
            //Act
            var userExistsBefore = await _sut.IsUserExist(user.Email);
            
            //Assert
            Assert.That(userExistsBefore, Is.False);
            
        }
        
        [Test]
        public async Task ShouldExistUser()
        {
            //Arrange
            var user = await DummyObject.CreateDummyUser();
            
            //Act
            var userExistsBefore = await _sut.IsUserExist(user.Email);
            
            //Assert
            Assert.That(userExistsBefore, Is.True);
                
            //Remove from database
            await DummyObject.DeleteDummyUser(user.Email);
            
        }
        
    }
    

   
}
