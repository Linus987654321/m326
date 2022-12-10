using Microsoft.VisualStudio.TestTools.UnitTesting;
using m326.Service;
using System.Collections.Generic;
using m326.Models;
using System;
using Moq;
using m326.ViewModel;

namespace TestM326
{
    [TestClass]
    public class LoginViewTest
    {
        [TestMethod]
        public void TestIsPasswordValid_PasswordNull()
        {
            //Arrange
            var mock = new Mock<IMongoDb>();
            User fakeUser = new User(1, Role.ADMIN, "Psw");
            mock.Setup(x => x.getUserWithId(It.IsAny<int>())).Returns(fakeUser);
            LoginView loginView = new LoginView(mock.Object);

            //Act
            bool result = loginView.isLoginValid();
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsPasswordValid_validPassword()
        {
            //Arrange
            var mock = new Mock<IMongoDb>();
            User fakeUser = new User(1, Role.ADMIN, "Psw");
            mock.Setup(x => x.getUserWithId(It.IsAny<int>())).Returns(fakeUser);
            LoginView loginView = new LoginView(mock.Object);
            loginView.Password = "Psw";

            //Act
            bool result = loginView.isLoginValid();
            
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsPasswordValid_invalidPassword()
        {
            //Arrange
            var mock = new Mock<IMongoDb>();
            User fakeUser = new User(1, Role.ADMIN, "Psw");
            mock.Setup(x => x.getUserWithId(It.IsAny<int>())).Returns(fakeUser);
            LoginView loginView = new LoginView(mock.Object);
            loginView.Password = "123";

            //Act
            bool result = loginView.isLoginValid();
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(loginView.ErrorText, "Password is false");
        }
    }
}
