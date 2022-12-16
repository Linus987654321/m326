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
    class AddCompetenceView : ViewModelBase
    {
        public AddCompetenceView()
        {
            //empty
        }

        public AddCompetenceView(Topic topic, AddCompetenceWindow window, User user)
        {
            MongoDb mongo = new MongoDb();
            _topics = mongo.getAllTopics();
            _selectedTopic = _topics.Find(t => t.Id.Equals(topic.Id));
            _competences = _selectedTopic.Competences;
            List<Difficulty> difficulties = new List<Difficulty>();
            difficulties.Add(Difficulty.EASY);
            difficulties.Add(Difficulty.ADVANCED);
            difficulties.Add(Difficulty.EXPERT);
            _difficulties = difficulties;
            _addCompetenceWindow = window;
            _user = user;
        }

        private readonly User _user;
        private readonly AddCompetenceWindow _addCompetenceWindow;
        
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

        private List<Difficulty> _difficulties;
        public List<Difficulty> Difficulties
        {
            get
            {
                return _difficulties;
            }
        }

        private Difficulty _difficulty;
        public Difficulty Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        private string _links;
        public string Links
        {
            get
            {
                return _links;
            }
            set
            {
                _links = value;
                OnPropertyChanged(nameof(Links));
            }
        }

        private List<Competence> _competences;
        public List<Competence> Competences
        {
            get
            {
                return _competences;
            }
            set
            {
                _competences = value;
                OnPropertyChanged(nameof(Competences));
            }
        }

        public ICommand AddCompetence
        {

            get
            {
                return new RelayCommand(
                    exex =>
                    {

                        MongoDb mongo = new MongoDb();
                        Competence competence = new Competence(_title, _difficulty);
                        if (_links == null)
                        {
                            competence.Links = "";
                        } else
                        {
                            competence.Links = _links;
                        }
                        _selectedTopic.Competences.Add(competence);
                        mongo.updateTopic(_selectedTopic);
                        updateSite();

                    },
                    canExecute => _title != "" && _title != null
                );
            }
        }
        public ICommand Close
        {

            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        _addCompetenceWindow.Close();
                        GridWindow gridWindow = new GridWindow();
                        gridWindow.DataContext = new GridView(_user, gridWindow);
                        gridWindow.ShowDialog();
                    }
                );
            }
        }

        private void updateSite()
        {
            MongoDb mongo = new MongoDb();
            _topics = mongo.getAllTopics();
            _selectedTopic = _topics.Find(t => t.Id.Equals(_selectedTopic.Id));
            _competences = _selectedTopic.Competences;
            _title = "";
            _difficulty = Difficulty.EASY;
            _links = "";

            OnPropertyChanged(nameof(SelectedTopic));
            OnPropertyChanged(nameof(Competences));
            OnPropertyChanged(nameof(Topics));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Difficulty));
            OnPropertyChanged(nameof(Links));
        }
    }
}
