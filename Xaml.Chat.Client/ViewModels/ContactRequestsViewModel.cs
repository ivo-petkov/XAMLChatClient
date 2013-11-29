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
    public class ContactRequestsViewModel : ViewModelBase, IPageViewModel
    {
        public string Name { get { return "Contact Requests"; } }

        public string SessionKey { get; set; }

        public IEnumerable<ContactRequestModel> ContactRequests { get; set; }

        public ContactRequestsViewModel()
        {
            this.ContactRequests = new List<ContactRequestModel>();
        }

        public ContactRequestsViewModel(string sessionKey)
        {
            this.SessionKey = sessionKey;
            this.ContactRequests = ContactsPersister.GetAllRequests(sessionKey);
            OnPropertyChanged("ContactRequests");
        }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private ICommand acceptRequest;
        public ICommand AcceptRequest
        {
            get
            {
                if (this.acceptRequest == null)
                {
                    this.acceptRequest = new RelayCommand(HandleAcceptRequest);
                }

                return this.acceptRequest;
            }
        }

        private void HandleAcceptRequest(object parameter)
        {
            if (parameter != null)
            {
                var request = parameter as ContactRequestModel;

                try
                {
                    ContactsPersister.AcceptUserRequest(this.SessionKey, request.Id);

                    ErrorMessage = "";
                    SuccessMessage = "User added to contacts";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");

                    this.ContactRequests = ContactsPersister.GetAllRequests(this.SessionKey);
                    OnPropertyChanged("ContactRequests");
                }
                catch (Exception)
                {
                    ErrorMessage = "Could not accept contact request";
                    SuccessMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                }
            }
        }

        private ICommand denyRequest;
        public ICommand DenyRequest
        {
            get
            {
                if (this.denyRequest == null)
                {
                    this.denyRequest = new RelayCommand(HandleDenyRequest);
                }

                return this.denyRequest;
            }
        }

        private void HandleDenyRequest(object parameter)
        {
            if (parameter != null)
            {
                var request = parameter as ContactRequestModel;

                try
                {
                    ContactsPersister.DenyUserRequest(this.SessionKey, request.Id);

                    ErrorMessage = "";
                    SuccessMessage = "Contact request denied";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");

                    this.ContactRequests = ContactsPersister.GetAllRequests(this.SessionKey);
                    OnPropertyChanged("ContactRequests");
                }
                catch (Exception)
                {
                    ErrorMessage = "Could not deny contact request";
                    SuccessMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    OnPropertyChanged("SuccessMessage");
                }
            }
        }
    }
}
