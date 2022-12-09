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
        private User _user;
        private readonly DetailCompetenceWindow _detailCompetenceWindow;

        public DetailCompetenceView(User user, Competence selectedCompetence, DetailCompetenceWindow detailCompetenceWindow)
        {
            _competence = selectedCompetence;
            List<Achievment> achievments = new List<Achievment>();
            achievments.Add(Achievment.NEUTRAL);
            achievments.Add(Achievment.ACHIEVED);
            achievments.Add(Achievment.NOT_ACHIEVED);
            _achievments = achievments;
            _user = user;
            _detailCompetenceWindow = detailCompetenceWindow;
            Links = _competence.Links;
        }

        public string Links { get; set; }
        private Competence _competence;
        public Competence Competence
        {
            get
            {

                return _competence;
            }
        }

        private List<Achievment> _achievments;
        public List<Achievment> Achievments
        {
            get
            {
                return _achievments;
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
