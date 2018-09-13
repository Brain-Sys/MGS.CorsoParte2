using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value;
                base.RaisePropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value;
                base.RaisePropertyChanged();
            }
        }

        private bool remember;
        public bool Remember
        {
            get { return remember; }
            set { remember = value;
                base.RaisePropertyChanged();
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            // this.Message = "g ndf,mgndfkjgndkjgnsdkjngkfdngkjdfngkjdngfkjndfgkj";
            this.Username = "emmegisoft";
            this.Password = "carpi";

            this.LoginCommand = new RelayCommand
                (loginCommandExecute, loginCommandCanExecute);
        }

        private bool loginCommandCanExecute()
        {
            if (string.IsNullOrEmpty(this.Username) ||
               string.IsNullOrEmpty(this.Password))
                return false;

            return true;
        }

        private void loginCommandExecute()
        {
            if (this.Username != "emmegisoft" && this.Password != "carpi")
            {
                // Login fallito
                this.Message = "Login fallito!";
            }
            else
            {
                
            }
        }
    }
}