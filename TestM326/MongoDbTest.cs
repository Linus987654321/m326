using Microsoft.VisualStudio.TestTools.UnitTesting;
using m326.Service;
using System.Collections.Generic;
using m326.Models;
using System;
using Moq;
using m326.ViewModel;
using MongoDB.Bson;

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
        public void TestGetTopicWithId_invalidID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            ObjectId id = new ObjectId();

            Action throwingAction = () =>
            {
                mongo.getTopicWithId(id);
            };
            //Assert
            Assert.ThrowsException<ArgumentNullException>(throwingAction);
        }

        [TestMethod]
        public void TestGetTopicWithId_validID()
        {
            //Arrange
            MongoDb mongo = new MongoDb();
            ObjectId id = mongo.getAllTopics()[0].Id;

            //Act
            Topic topic = mongo.getTopicWithId(id);
            //Assert
            Assert.IsNotNull(topic);
        }

        [TestMethod]
        public void TestGetUserWithId_validID()
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
