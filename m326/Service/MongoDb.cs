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
    public class MongoDb
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

        public Topic getTopicWithId(ObjectId id)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var topicDocument = collection.Find(filter).FirstOrDefault();
            Topic topic = null;
            try
            {
                topic = BsonSerializer.Deserialize<Topic>(topicDocument);

            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return topic;
        }


        public void updateUser(User user)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            var update = Builders<BsonDocument>.Update.Set("Role", user.Role).Set("Password", user.Password).Set("Topics", user.Topics);
            collection.UpdateOne(filter, update);
        }

        public void updateTopic(Topic topic)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topic.Id);
            var update = Builders<BsonDocument>.Update.Set("Competences", topic.Competences).Set("Title", topic.Title);
            collection.UpdateOne(filter, update);
        }

        public void createTopic(Topic topic)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var doc = topic.ToBsonDocument();
            collection.InsertOne(doc);
        }

        public void deleteTopic(ObjectId topicId)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("topic");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", topicId);
            collection.DeleteOne(filter);
        }

        public User getUserWithId(int id)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var userDocument = collection.Find(filter).FirstOrDefault();
            User user = null;
            try
            {
                user = BsonSerializer.Deserialize<User>(userDocument);

            } catch (ArgumentNullException)
            {
                throw;
            }

            return user;
        }
    }
}
