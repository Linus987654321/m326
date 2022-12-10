using m326.Command;
using m326.Models;
using m326.Service;
using m326.View;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace m326.ViewModel
{
    public class LoginView : ViewModelBase
    {

        LoginView()
        {
            //empty
        }
        public LoginView(IMongoDb mock)
        {
            mongo = mock;
        }

        private readonly IMongoDb mongo = new MongoDb();
        private User _user;

        private string _errorText;
        public string ErrorText{ 
            get 
            { 
                return _errorText; 
            } 
            set 
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        private int _id;
        public string Id
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                try
                {
                    _id = Int32.Parse(value);
                    OnPropertyChanged(nameof(Id));
                }
                catch (FormatException)
                {
                    ErrorText = "Id contains only numbers";
                }
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand 
        { 
            get
            {
                return new RelayCommand(
                    exex =>
                    {
                        if (isLoginValid())
                        {
                            GridWindow gridWindow = new GridWindow();
                            gridWindow.DataContext = new GridView(_user, gridWindow);
                            gridWindow.ShowDialog();
                        }
                    } 
                );
            }
        }

        public bool isLoginValid()
        {
            bool validLogin = false;
            if (_password != null)
            {
                try
                {
                    User user = mongo.getUserWithId(_id);
                    if (user.Password == this._password)
                    {
                        validLogin = true;
                        this._user = user;
                    }
                    else
                    {
                        ErrorText = "Password is false";
                    }
                }
                catch (ArgumentNullException)
                {
                    ErrorText = "No User found with this Id";
                }
            }
            
            return validLogin;
        }
    }
}
