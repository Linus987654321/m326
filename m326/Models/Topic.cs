using m326.Service;
using MongoDB.Bson;
using System.Collections.Generic;

namespace m326.Models
{
    public class Topic
    {
        public ObjectId Id { get; set; }
        public List<Competence> Competences { get; set; }

        public string Title { get; set; }

        public Topic( string title)
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            Competences = new List<Competence>();
        }
    }
}
