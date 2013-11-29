using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class SearchFormViewModel : ViewModelBase, IPageViewModel
    {

        public SearchFormViewModel()
        {
        }

        public SearchFormViewModel(string sessionKey)
        {
            this.SessionKey = sessionKey;
        }

        public string SessionKey;
        public string Name { get { return "Search Form"; } }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public string QueryText { get; set; }
        public IEnumerable<UserModel> FoundUsers { get; set; } 

        private ICommand search;
        public ICommand Search
        {
            get
            {
                if(this.search == null)
                {
                    this.search = new RelayCommand(HandleSearch);
                }

                return this.search;
            }
        }

        private ICommand addToContacts;
        public ICommand AddToContacts
        {
            get
            {
                if(this.addToContacts == null)
                {
                    this.addToContacts = new RelayCommand(HandleAddToContacts);
                }

                return this.addToContacts;
            }
        }

        private void HandleAddToContacts(object parameter)
        {
            int index = (int)parameter;
            if (index > 0)
            {
                var userToAdd = FoundUsers.ElementAt(index);

                try
                {
                    ContactsPersister.AddUserToContacts(this.SessionKey, userToAdd.Id);
                    ErrorMessage = "";
                    SuccessMessage = "Contact request sent successfully";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                }
                catch (Exception)
                {
                    ErrorMessage = "You have already sent request to this person";
                    SuccessMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                }
            }
        }

        private void HandleSearch(object parameter)
        {
            try
            {
                var foundUsers = UserPersister.Search(this.SessionKey, new QueryModel() {QueryText = this.QueryText});
                this.FoundUsers = foundUsers;
                OnPropertyChanged("FoundUsers");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Could not search for users";
                SuccessMessage = "";
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("SuccessMessage");
            }
        }
    }
}
