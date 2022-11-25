using m326.Service;
using System.Collections.Generic;

namespace m326.Models
{
    public class Grid
    {
        public List<Topic> TopicList { get; set; }

        public Grid()
        {
            MongoDb mongo = new MongoDb();
            TopicList = mongo.getAllTopics();
        }
    }
}
