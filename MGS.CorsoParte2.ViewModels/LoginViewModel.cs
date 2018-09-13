using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MGS.CorsoParte2.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels
{
    public class LoginViewModel : ApplicationViewModelBase
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                base.RaisePropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                base.RaisePropertyChanged();
            }
        }

        private bool remember;
        public bool Remember
        {
            get { return remember; }
            set
            {
                remember = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand<string> ShowHelpCommand { get; set; }

        public LoginViewModel()
        {
            this.Remember = true;
            this.Username = "emmegisoft";
            this.Password = "carpi";

            this.LoginCommand = new RelayCommand
                (loginCommandExecute, loginCommandCanExecute);
            this.ShowHelpCommand = new RelayCommand<string>
                (showHelpCommandExecute);
        }

        private void showHelpCommandExecute(string parameter)
        {
            Process.Start("http://www.google.com?q=" + parameter);
        }

        private bool loginCommandCanExecute()
        {
            if (string.IsNullOrEmpty(this.Username) ||
               string.IsNullOrEmpty(this.Password) ||
               this.IsBusy)
                return false;

            return true;
        }

        private async void loginCommandExecute()
        {
            this.IsBusy = true;

#if DEBUG
            await Task.Delay(2000);
#endif

            if (this.Username != "emmegisoft" && this.Password != "carpi")
            {
                // Login fallito
                this.Message = "Login fallito!";
            }
            else
            {
                // Login riuscito
                if (this.Remember)
                {
                    string content = $"{this.Username}  {this.Password}";
                    File.WriteAllText("E:\\Credentials.txt", content);
                }

                OpenNewViewMessage msg = new OpenNewViewMessage();
                msg.ViewName = "MainMenu";
                msg.Modal = true;
                msg.Maximized = true;
                Messenger.Default.Send<OpenNewViewMessage>(msg);
            }

            this.IsBusy = false;
        }

        public override void Init2()
        {
            
        }
    }
}