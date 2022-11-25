using Microsoft.VisualStudio.TestTools.UnitTesting;
using m326.Service;
using System.Collections.Generic;
using m326.Models;
using System;

namespace TestM326
{
    [TestClass]
    public class MongoDbTest
    {
        [TestMethod]
        public void TestGetAllTopics()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            //Act
            List<Topic> topics = mongo.getAllTopics();
            //Assert
            Assert.IsNotNull(topics[0]);

        }

        [TestMethod]
        public void TestGetNumberOfTopics()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            //Act
            long number = mongo.getNumberOfTopics();
            //Assert
            Assert.IsTrue(number > 0);
        }

        [TestMethod]
        public void TestGetNumberOfCompetencesOnTopic_validID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            long topicId = 1;
            //Act
            long number = mongo.getNumberOfCompetencesOnTopic(topicId);
            //Assert
            Assert.IsTrue(number > -1);
        }

        [TestMethod]
        public void TestGetNumberOfCompetencesOnTopic_invalidID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            long topicId = -1;
            //Act
            long number = mongo.getNumberOfCompetencesOnTopic(topicId);
            //Assert
            Assert.IsTrue(number == 0);
        }

        [TestMethod]
        public void TestGetUserWithIdc_validID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            int id = 1;
            //Act

            User user = mongo.getUserWithId(id);
            //Assert
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestGetUserWithId_invalidID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            int id = -1;
            //Act 
            Action throwingAction = () =>
            {
                mongo.getUserWithId(id);
            };
            //Assert
            Assert.ThrowsException<ArgumentNullException>(throwingAction);
        }
    }
}
