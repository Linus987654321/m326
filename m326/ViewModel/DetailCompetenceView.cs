using m326.Command;
using m326.Models;
using m326.Service;
using m326.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace m326.ViewModel
{
    public class DetailCompetenceView : ViewModelBase
    {
        public DetailCompetenceView()
        {
            //empty
        }
        public DetailCompetenceView(User user, Competence selectedCompetence, DetailCompetenceWindow detailCompetenceWindow)
        {
            _competence = selectedCompetence;
            List<Achievement> achievments = new List<Achievement>();
            achievments.Add(Achievement.NEUTRAL);
            achievments.Add(Achievement.ACHIEVED);
            achievments.Add(Achievement.NOT_ACHIEVED);
            _achievements = achievments;
            _user = user;
            _detailCompetenceWindow = detailCompetenceWindow;
            Links = _competence.Links;
        }

        private User _user;
        private readonly DetailCompetenceWindow _detailCompetenceWindow;
        
        public string Links { get; set; }
        
        private Competence _competence;
        public Competence Competence
        {
            get
            {

                return _competence;
            }
        }

        private List<Achievement> _achievements;
        public List<Achievement> Achievements
        {
            get
            {
                return _achievements;
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
                        mongo.updateUser(_user);
                        _detailCompetenceWindow.Close();
                    }
                );
            }
        }
    }
}
