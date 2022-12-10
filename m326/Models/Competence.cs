using m326.Service;
using MongoDB.Bson;
using System.Collections.Generic;

namespace m326.Models
{
    public class Competence
    {
        public ObjectId Id { get; set; }
        public string Links { get; set; }
        public string Title { get; set; }
        public Difficulty Difficulty { get; set; }

        public Achievement Achievement { get; set; }

        public Competence(string title, Difficulty difficulty)
        {
            Id = ObjectId.GenerateNewId();
            Achievement = Achievement.NEUTRAL;
            Title = title;
            Difficulty = difficulty;
        }
    }
}
