using m326.Service;
using System.Collections.Generic;

namespace m326.Models
{
    class Topic
    {
        public long Id { get; set; }
        public List<Competence> Competences { get; set; }

        public string Title { get; set; }

        public Topic( string title)
        {
            MongoDb mongo = new MongoDb();
            Id = mongo.getNumberOfTopics() + 1;
            Title = title;
        }
    }
}
