using m326.Service;
using System.Collections.Generic;

namespace m326.Models
{
    public class Competence
    {
        public long Id { get; set; }
        public List<string> Links { get; set; }
        public string Title { get; set; }
        public Difficulty Difficulty { get; set; }

        public Achievment Achievment { get; set; }

        public Competence(string title, Difficulty difficulty, long topicId)
        {
            MongoDb mongo = new MongoDb();
            Id = mongo.getNumberOfCompetencesOnTopic(topicId) + 1;
            Achievment = Achievment.NEUTRAL;
            Title = title;
            Difficulty = difficulty;
        }
    }
}
