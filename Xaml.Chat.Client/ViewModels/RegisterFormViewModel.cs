using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class RegisterFormViewModel : ViewModelBase, IPageViewModel
    {
        public string Name { get { return "Register Form"; } }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        private ICommand register;
        public ICommand Register
        {
            get
            {
                if(this.register == null)
                {
                    this.register = new RelayCommand(HandleRegister);
                }

                return this.register;
            }
        }

        private ICommand selectProfilePicture;
        public ICommand SelectProfilePicture
        {
            get
            {
                if(this.selectProfilePicture == null)
                {
                    this.selectProfilePicture = new RelayCommand(HandleSelectProfilePicture);
                }

                return this.selectProfilePicture;
            }
        }

        private void HandleSelectProfilePicture(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                var url = ImageUploader.UploadImage(filePath);
                ProfilePictureUrl = url;
                OnPropertyChanged("ProfilePictureUrl");
            }
        }

        public event EventHandler<RegisterSuccessArgs> RegisterSuccess;
 
        private void RaiseRegisterSuccess(UserModel registeredUser)
        {
            if(this.RegisterSuccess != null)
            {
                this.RegisterSuccess(this, new RegisterSuccessArgs(registeredUser));
            }
        }

        private void HandleRegister(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;
            string passwordHash = Sha1Encrypter.CalculateSHA1(password);

            try
            {

                var registeredUser = UserPersister.RegisterUser(new RegisterUserModel()
                {
                    Username = this.Username,
                    PasswordHash = passwordHash,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    ProfilePictureUrl = this.ProfilePictureUrl
                });

                RaiseRegisterSuccess(registeredUser);
            }
            catch (Exception)
            {
                MessageToUser = "Could not register user";
                OnPropertyChanged("MessageToUser");
            }
        }

        public string MessageToUser { get; set; }
    }
}
