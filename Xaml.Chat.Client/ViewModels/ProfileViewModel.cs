using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Models;
using Xaml.Chat.Client.Helpers;
using System.Windows;
using Xaml.Chat.Client.Data;
using Microsoft.Win32;

namespace Xaml.Chat.Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase, IPageViewModel
    {
        public UserModel CurrentUserSettings { get; set; }

        public ProfileViewModel(UserModel currentUserSettings)
        {
            this.CurrentUserSettings = currentUserSettings;
            this.Username = currentUserSettings.Username;
            this.LastName = currentUserSettings.LastName;
            this.FirstName = currentUserSettings.FirstName;
            this.ProfilePictureUrl = currentUserSettings.ProfilePictureUrl;

            OnPropertyChanged("Username");
            OnPropertyChanged("LastName");
            OnPropertyChanged("FirstName");
            OnPropertyChanged("ProfilePictureUrl");
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private ICommand editProfile;

        public string Name
        {
            get
            {
                return "User profile";
            }
        }

        public ICommand EditProfile
        {
            get
            {
                if (this.editProfile == null)
                {
                    this.editProfile = new RelayCommand(this.HandleEditProfileCommand);  
                }
                return this.editProfile;
            }
        }

        private ICommand editPicture;

        public ICommand EditPicture
        {

            get
            {
                if (this.editPicture == null)
                {
                    this.editPicture = new RelayCommand(this.HandleSelectProfilePicture);
                }
                return this.editPicture;
            }
            
        }

        public event EventHandler<LoginSuccessArgs> EditSuccess;

        public void RaiseEditSuccess(UserModel newUserSetting)
        {
            if (this.EditSuccess != null)
            {
                this.EditSuccess(this, new LoginSuccessArgs(newUserSetting));
            }
        }

        private void HandleSelectProfilePicture(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                var url = ImageUploader.UploadImage(filePath);
                this.ProfilePictureUrl = url;
                OnPropertyChanged("ProfilePictureUrl");
            }
        }

        private void HandleEditProfileCommand(object parameter)
        {

            if ((this.NewPassword == null || this.NewPassword.Length < 2) && this.OldPassword!=null)
            {
                ErrorMessage = "Invalid new password";
                SuccessMessage = "";
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("SuccessMessage");
            }
            else
            {
                try
                {
                    var editProfile = new UserEditModel()
                    {
                        Id = CurrentUserSettings.Id,
                        Username = Username,
                        FirstName = FirstName,
                        LastName = LastName,
                        ProfilePictureUrl = ProfilePictureUrl
                    };

                    if (this.OldPassword != null)
                    { 
                        editProfile.OldPasswordHash = Sha1Encrypter.CalculateSHA1(OldPassword);
                        editProfile.NewPasswordHash = Sha1Encrypter.CalculateSHA1(NewPassword);
                    }

                    this.CurrentUserSettings = UserPersister.EditUser(CurrentUserSettings.SessionKey, editProfile);
                    this.FirstName = this.CurrentUserSettings.FirstName;
                    this.LastName = this.CurrentUserSettings.LastName;
                    
                    this.OldPassword = "";
                    this.NewPassword = "";
                    OnPropertyChanged("OldPassword");
                    OnPropertyChanged("NewPassword");
                    OnPropertyChanged("FirstName");
                    OnPropertyChanged("LastName");

                    ErrorMessage = "";
                    SuccessMessage = "Profile updated successfully";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                    RaiseEditSuccess(CurrentUserSettings);
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Could not edit profile";
                    SuccessMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                }

            }
        }
    }
}
