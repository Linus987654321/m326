using m326.Command;
using m326.Models;
using m326.Service;
using m326.View;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace m326.ViewModel
{
    class CompetenceEditView : ViewModelBase
    {
        public CompetenceEditView()
        {
            //Empty
        }
        public CompetenceEditView(CompetenceEditWindow window, ObjectId topicId, ObjectId competenceId, User user)
        {
            MongoDb mongo = new MongoDb();
            _topic = mongo.getTopicWithId(topicId);
            _competence = _topic.Competences.Find(c => c.Id.Equals(competenceId));
            List<Difficulty> difficulties = new List<Difficulty>();
            difficulties.Add(Difficulty.EASY);
            difficulties.Add(Difficulty.ADVANCED);
            difficulties.Add(Difficulty.EXPERT);
            _difficulties = difficulties;
            _competenceEditWindow = window;
            _user = user;
        }
        private readonly User _user;
        private readonly CompetenceEditWindow _competenceEditWindow;

        private Topic _topic;
        public Topic Topic
        {
            get
            {
                return _topic;
            }
        }

        private Competence _competence;
        public Competence Competence
        {
            get
            {

                return _competence;
            }
        }

        private List<Difficulty> _difficulties;
        public List<Difficulty> Difficulties
        {
            get
            {
                return _difficulties;
            }
        }
        public ICommand SaveAndClose
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        MongoDb mongo = new MongoDb();
                        mongo.updateTopic(_topic);
                        _competenceEditWindow.Close();
                        GridWindow gridWindow = new GridWindow();
                        gridWindow.DataContext = new GridView(_user, gridWindow);
                        gridWindow.ShowDialog();
                    }
                );
            }
        }

    }
}

