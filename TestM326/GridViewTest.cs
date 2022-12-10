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
    public class GridViewTest
    {
        [TestMethod]
        public void TestUpdateTopics_minimal()
        {
            //Arrange
            List<Topic> updatedTopics = new List<Topic>();
            
            var mock = new Mock<IMongoDb>();
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);
            
            List<Topic> userTopics = new List<Topic>();

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);
            
            //Assert
            Assert.AreEqual(result, updatedTopics);
        }

        [TestMethod]
        public void TestUpdateTopics_userCompetencesNull()
        {
            //Arrange
            List<Topic> updatedTopics = new List<Topic>();
            updatedTopics.Add(new Topic("updatedTopic"));
            
            var mock = new Mock<IMongoDb>();
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);
            
            List<Topic> userTopics = new List<Topic>();
            userTopics.Add(new Topic("userTopic"));

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);
            
            //Assert
            Assert.AreEqual(result, updatedTopics);
        }

        [TestMethod]
        public void TestUpdateTopics_differentTopicID()
        {
            //Arrange
            List<Topic> updatedTopics = new List<Topic>();
            updatedTopics.Add(new Topic("updatedTopic"));
            
            var mock = new Mock<IMongoDb>();
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);

            List<Topic> userTopics = new List<Topic>();
            userTopics.Add(new Topic("userTopic"));

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);
            
            //Assert
            Assert.AreEqual(result, updatedTopics);
        }

        [TestMethod]
        public void TestUpdateTopics_AchievementNeutral()
        {
            //Arrange
            Topic topic = new Topic("test");
            
            var mock = new Mock<IMongoDb>();
            List<Topic> updatedTopics = new List<Topic>();
            updatedTopics.Add(topic);
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);

            List<Topic> userTopics = new List<Topic>();
            topic.Competences.Add(new Competence("test", Difficulty.ADVANCED));
            userTopics.Add(topic);

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);
            
            //Assert
            Assert.AreEqual(result, updatedTopics);
        }

        [TestMethod]
        public void TestUpdateTopics_CompetencesDifferentID()
        {
            //Arrange
            Topic topic = new Topic("test");

            List<Topic> updatedTopics = new List<Topic>();
            updatedTopics.Add(topic);

            var mock = new Mock<IMongoDb>();
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);
            
            List<Topic> userTopics = new List<Topic>();
            Competence competence = new Competence("test", Difficulty.ADVANCED);
            competence.Achievement = Achievement.ACHIEVED;
            topic.Competences.Add(competence);
            userTopics.Add(topic);

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);
            
            //Assert
            Assert.AreEqual(result, updatedTopics);
        }

        [TestMethod]
        public void TestUpdateTopics_sameCompetencesID()
        {
            //Arrange
            Topic topic = new Topic("test");
            Competence competence = new Competence("test", Difficulty.ADVANCED);
            topic.Competences.Add(competence);

            List<Topic> updatedTopics = new List<Topic>();
            updatedTopics.Add(topic);

            var mock = new Mock<IMongoDb>();
            mock.Setup(x => x.getAllTopics()).Returns(updatedTopics);
            GridView gridView = new GridView(mock.Object);

            List<Topic> userTopics = new List<Topic>();
            competence.Achievement = Achievement.ACHIEVED;
            userTopics.Add(topic);

            //Act
            List<Topic> result = gridView.updateTopics(userTopics);

            //Assert
            Assert.AreEqual(result[0].Competences[0].Achievement, Achievement.ACHIEVED);
            Assert.AreEqual(result[0].Competences[0].Difficulty, Difficulty.ADVANCED);
            Assert.AreEqual(result[0].Competences[0].Title, "test");

        }
    }
}
