using m326.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace m326.Service
{
    public interface IMongoDb
    {
        List<Topic> getAllTopics();
        Topic getTopicWithId(ObjectId id);
        void updateUser(User user);
        void updateTopic(Topic topic);
        void createTopic(Topic topic);
        void deleteTopic(ObjectId topicId);
        User getUserWithId(int id);
    }
}
