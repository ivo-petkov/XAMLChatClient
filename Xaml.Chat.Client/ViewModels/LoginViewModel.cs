using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase, IPageViewModel
    {
        public string Username { get; set; }

        public string MessageToUser { get; set; }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if(this.login == null)
                {
                    this.login = new RelayCommand(HandleLogin);
                }

                return this.login;
            }
        }

        private ICommand goToRegister;
        public ICommand GoToRegister
        {
            get
            {
                if(this.goToRegister == null)
                {
                    this.goToRegister = new RelayCommand(HandleGoToRegister);
                }

                return this.goToRegister;
            }
        }

        private void HandleGoToRegister(object parameter)
        {
            RaiseNavigateToRegister();
        }

        public event EventHandler NavigateToRegister;

        private void RaiseNavigateToRegister()
        {
            if(this.NavigateToRegister != null)
            {
                this.NavigateToRegister(this, null);
            }
        }

        public event EventHandler<LoginSuccessArgs> LoginSuccess;

        private void RaiseLoginSuccess(UserModel loggedUser)
        {
            if (this.LoginSuccess != null)
            {
                this.LoginSuccess(this, new LoginSuccessArgs(loggedUser));
            }
        }

        private void HandleLogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            var passwordHash = Sha1Encrypter.CalculateSHA1(password);

            try
            {
                var loggedUser = UserPersister.LoginUser(this.Username, passwordHash);
                RaiseLoginSuccess(loggedUser);
            }
            catch (Exception ex)
            {
                MessageToUser = "Wrong username or password";
                OnPropertyChanged("MessageToUser");
            }
        }

        public string Name { get { return "Login"; } }
    }
}
