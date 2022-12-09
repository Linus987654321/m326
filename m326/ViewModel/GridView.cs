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
    public class GridView : ViewModelBase
    {
        public GridView()
        {
            //empty
        }
        public GridView(User user, GridWindow window)
        {
            this._user = user;
            user.Topics = updateTopics(user.Topics);
            MongoDb mongo = new MongoDb();
            mongo.updateUser(user);
            this._topics = user.Topics;
            this._selectedTopic = _topics[0];
            this._competences = SelectedTopic.Competences;
            _window = window;
        }

        private readonly User _user;

        private readonly GridWindow _window;
        //hohlt alle Topics aus der DB und falls bei welchen Competencen schon Achievment gesetzt ist wird das übernommen
        private List<Topic> updateTopics(List<Topic> userTopics)
        {
            MongoDb mongo = new MongoDb();
            List<Topic> updatedTopics = mongo.getAllTopics();
            if (userTopics != null)
            {
                foreach (Topic topic in userTopics)
                {
                    if (updatedTopics.Exists(t => t.Id.Equals(topic.Id)) && topic.Competences != null)
                    {
                        foreach (Competence competence in topic.Competences)
                        {
                            if (competence.Achievment != Achievment.NEUTRAL)
                            {
                                updatedTopics.Find(t => t.Id.Equals(topic.Id)).Competences.Find(c => c.Id.Equals(competence.Id)).Achievment = competence.Achievment;
                            }
                        }
                    }
                }
            }

            return updatedTopics;
        }

        private List<Topic> _topics;
        public List<Topic> Topics
        {
            get
            {
                return _topics;
            }
        }
        private Topic _selectedTopic;
        public Topic SelectedTopic
        {
            get
            {
                return _selectedTopic;
            }
            set
            {
                _selectedTopic = value;
                _competences = _selectedTopic.Competences;
                OnPropertyChanged(nameof(Competences));
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }
        private List<Competence> _competences;
        public List<Competence> Competences
        {
            get
            {
                return _competences;
            }
        }
        private Competence _selectedCompetence;
        public Competence SelectedCompetence
        {
            get
            {
                return _selectedCompetence;
            }
            set
            {
                _selectedCompetence = value;
                OnPropertyChanged(nameof(SelectedCompetence));
            }
        }

        public ICommand OpenCompetence
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        if (_user.Role == Role.ADMIN)
                        {
                            CompetenceEditWindow window = new CompetenceEditWindow();
                            _window.Close();
                            window.DataContext = new CompetenceEditView(window, _selectedTopic.Id, SelectedCompetence.Id, _user);
                            window.ShowDialog();
                        }
                        else
                        {
                            DetailCompetenceWindow window = new DetailCompetenceWindow();
                            window.DataContext = new DetailCompetenceView(_user, SelectedCompetence, window);
                            window.ShowDialog();
                        }
                    },
                    canExec => SelectedCompetence != null

                ); 
            }
        }

        public ICommand OpenEditTopic
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        _window.Close();
                        EditTopicWindow topicWindow = new EditTopicWindow();
                        topicWindow.DataContext = new EditTopicView(topicWindow, _user);
                        topicWindow.ShowDialog();
                    },
                    canExecute => _user != null && _user.Role == Role.ADMIN
                );
            }
        }

        public ICommand CreateCompetence
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        _window.Close();
                        AddCompetenceWindow window = new AddCompetenceWindow();
                        window.DataContext = new AddCompetenceView(_selectedTopic, window, _user);
                        window.ShowDialog();
                    },
                    canExecute => _user != null && _user.Role == Role.ADMIN
                );
            }
        }
    }
}
