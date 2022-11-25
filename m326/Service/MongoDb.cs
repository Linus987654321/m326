using m326.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m326.Service
{
    class MongoDb
    {
        private readonly IMongoDatabase db;
        private const string connection = "mongodb://localhost:27017";
        private const string dbName = "m326";


        public MongoDb()
        {
            MongoClient mongo = new MongoClient(connection);
            db = mongo.GetDatabase(dbName);
        }

        public void createUser(User user)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("user");
            
            var doc = user.ToBsonDocument();
            collection.InsertOne(doc);
        }

        public List<Topic> getAllTopics()
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var topicDocuments = collection.Find(new BsonDocument()).ToList();
            List<Topic> topics = new List<Topic>();
            foreach(BsonDocument doc in topicDocuments)
            {
                Topic topic = BsonSerializer.Deserialize<Topic>(doc);
                topics.Add(topic);
            }
            return topics;
        }

        public long getNumberOfTopics()
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            long doc = collection.CountDocuments(new BsonDocument());
            return doc;
        }

        public long getNumberOfCompetencesOnTopic(long id)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            long doc = collection.CountDocuments(filter);
            return doc;
        }

        public void updateFieldsTopic<T>(string fieldName, T data, long topicId)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topicId);
            var update = Builders<BsonDocument>.Update.Set(fieldName, data);
            collection.UpdateOne(filter, update);
        }

        public void updateFieldsCompetence<T>(string fieldName, T data, long topicId, long competenceId) 
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topicId) & Builders<BsonDocument>.Filter.Eq("_id", competenceId);
            var update = Builders<BsonDocument>.Update.Set(fieldName, data);
            collection.UpdateOne(filter, update);
        }

        public void updateUser(User user)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            var update = Builders<BsonDocument>.Update.Set("Role", user.Role).Set("Password", user.Password).Set("Grid", user.Grid);
            collection.UpdateOne(filter, update);
        }

        public void updateTopic(Topic topic)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topic.Id);
            var update = Builders<BsonDocument>.Update.Set("Competences", topic.Competences).Set("Title", topic.Title);
            collection.UpdateOne(filter, update);
        }

        public void updateCompetence(long topicId, Competence competence)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topicId) & Builders<BsonDocument>.Filter.Eq("_id", competence.Id);
            var update = Builders<BsonDocument>.Update.Set("Links", competence.Links).Set("Title", competence.Title)
                .Set("Difficulty", competence.Difficulty).Set("Achievement", competence.Achievment);
            collection.UpdateOne(filter, update);
        }

        public void createTopic(Topic topic)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var doc = topic.ToBsonDocument();
            collection.InsertOne(doc);
        }

        public User getUserWithId(int id)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var userDocument = collection.Find(filter).FirstOrDefault();
            User user = BsonSerializer.Deserialize<User>(userDocument);
 
            return user;
        }
    }
}
