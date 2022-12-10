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
    class EditTopicView : ViewModelBase
    {
        public EditTopicView()
        {
            //empty
        }
        public EditTopicView(EditTopicWindow window, User user)
        {
            MongoDb mongo = new MongoDb();
            _topics = mongo.getAllTopics();
            _selectedTopic = _topics[0];
            _editTopicWindow = window;
            _user = user;

        }

        private readonly EditTopicWindow _editTopicWindow;
        private readonly User _user;

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
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }

        private List<Topic> _topics;
        public List<Topic> Topics
        {
            get
            {
                return _topics;
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
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

        public ICommand AddTopic
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {

                        MongoDb mongo = new MongoDb();
                        Topic topic = new Topic(_title);
                        mongo.createTopic(topic);
                        _editTopicWindow.Close();
                        AddCompetenceWindow window = new AddCompetenceWindow();
                        window.DataContext = new AddCompetenceView(topic, window, _user);
                        window.ShowDialog();
                    },
                    canExecute => _title != null
                );
            }
        }

        public ICommand DeleteCompetence
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        _selectedTopic.Competences.RemoveAll(c => c.Id.Equals(_selectedCompetence.Id));
                        MongoDb mongo = new MongoDb();
                        mongo.updateTopic(_selectedTopic);
                        updateSite();
                    },
                    canExecute => _selectedCompetence != null
                );
            }
        }

        public ICommand DeleteTopic
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        MongoDb mongo = new MongoDb();
                        mongo.deleteTopic(_selectedTopic.Id);
                        updateSite();
                    }
                );
            }
        }

        public ICommand Save
        {
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        MongoDb mongo = new MongoDb();
                        mongo.updateTopic(_selectedTopic);
                        _editTopicWindow.Close();
                        GridWindow window = new GridWindow();
                        window.DataContext = new GridView(_user, window);
                        window.ShowDialog();
                    },
                    canExecute => _selectedTopic != null
                );
            }
        }

        private void updateSite()
        {
            MongoDb mongo = new MongoDb();
            _topics = mongo.getAllTopics();
            OnPropertyChanged(nameof(Topics));
            _selectedTopic = _topics[0];
            OnPropertyChanged(nameof(SelectedTopic));
        }
    }
}
