using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class ViewUserProfileVewModel : ViewModelBase, IPageViewModel
    {

        public UserModel SelectedContact { get; set; }

        public ViewUserProfileVewModel(UserModel selectedContact)
        {
            this.SelectedContact = selectedContact;
            this.Username = selectedContact.Username;
            this.LastName = selectedContact.LastName;
            this.FirstName = selectedContact.FirstName;
            this.ProfilePictureUrl = selectedContact.ProfilePictureUrl;

            OnPropertyChanged("Username");
            OnPropertyChanged("LastName");
            OnPropertyChanged("FirstName");
            OnPropertyChanged("ProfilePictureUrl");
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string Name
        {
            get
            {
                return "View user profile";
            }
        }
    }
}
