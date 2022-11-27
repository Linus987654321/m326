using m326.Command;
using m326.Models;
using m326.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace m326.ViewModel
{
    class LoginView : ViewModelBase
    {
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
                            ErrorText = "top";
                        }
                    }
                );
            }
        }

        private bool isLoginValid()
        {
            bool validLogin = false;
            if (_password != null)
            {
                try
                {
                    MongoDb mongo = new MongoDb();
                    User user = mongo.getUserWithId(_id);
                    if (user.Password == this._password)
                    {
                        validLogin = true;
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
