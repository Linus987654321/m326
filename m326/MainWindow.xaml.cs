using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using m326.Models;
using m326.Service;

namespace m326
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createUser_Click(object sender, RoutedEventArgs e)
        {
            MongoDb mongoDB = new MongoDb();
            User user = new User(0, Role.ADMIN, "admin");
            mongoDB.createUser(user);
            User user1 = new User(1, Role.TEACHER, "teacher");
            mongoDB.createUser(user1);
        }

        private void findUser_Click(object sender, RoutedEventArgs e)
        {
            MongoDb mongo = new MongoDb();
            User user = mongo.getUserWithId(0);
            user.Grid.TopicList = mongo.getAllTopics();
            mongo.updateUser(user);
        }

        private void createTopic_Click(object sender, RoutedEventArgs e)
        {
            Topic topic = new Topic("deutsch");
            List<Competence> competences = new List<Competence>();
            competences.Add(new Competence("Nomen", Difficulty.EASY, topic.Id));
            competences.Add(new Competence("Adjektiv", Difficulty.ADVANCED, topic.Id));
            competences.Add(new Competence("Verben", Difficulty.EXPERT, topic.Id));
            topic.Competences = competences;
            MongoDb mongo = new MongoDb();
            mongo.createTopic(topic);

        }

        private void updateTopic_Click(object sender, RoutedEventArgs e)
        {
            MongoDb mongo = new MongoDb();
            /*Topic topic = mongo.getAllTopics()[0];
            topic.Title = "Deutsch";
            topic.Competences[0].Achievment = Achievment.ACHIEVED;
            mongo.updateTopic(topic);*/
            mongo.updateFieldsTopic<string>("Title", "deutsch", 1);
        }

        private void updateCompetence_Click(object sender, RoutedEventArgs e)
        {
            MongoDb mongo = new MongoDb();
            mongo.updateFieldsCompetence<Achievment>("Achievment", Achievment.NOT_ACHIEVED, 1, 1);
        }
    }
}
