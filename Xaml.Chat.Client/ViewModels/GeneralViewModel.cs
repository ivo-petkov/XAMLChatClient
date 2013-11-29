using Xaml.Chat.Client.Helpers;

namespace Xaml.Chat.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using Xaml.Chat.Client.Behavior;
    using Xaml.Chat.Client.Data;
    using Xaml.Chat.Client.Models;

    public class GeneralViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "General";
            }
        }

        public UserModel CurrentUser { get; set; }

        private ObservableCollection<MissedConversationModel> conversations;

        private ObservableCollection<UserModel> contacts;

        public event EventHandler<ConversationStartedArgs> ConversationStarted;

        public event EventHandler<ViewContactProfileArgs> ViewContactProfile;

        private void RaiseConversationStarted(ConversationModel conversation)
        {
            if (this.ConversationStarted != null)
            {
                this.ConversationStarted(this, new ConversationStartedArgs(conversation));
            }
        }

        private UserModel currentUser;
        private ICommand closeConversation;
        private ICommand viewProfile;
        private ICommand startConversation; private string searchText;

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                this.searchText = value;
                HandleTextChanged(value);
            }
        }

        public IEnumerable<MissedConversationModel> Conversations
        {
            get
            {
                if (this.conversations == null)
                {
                    this.Conversations = ConversationsPersister.GetMissed(CurrentUser.SessionKey, this.currentUser.Id);
                }
                return this.conversations;
            }
            set
            {
                if (this.conversations == null)
                {
                    this.conversations = new ObservableCollection<MissedConversationModel>();
                }
                this.conversations.Clear();
                foreach (var item in value)
                {
                    this.conversations.Add(item);
                }
            }
        }

        public IEnumerable<UserModel> Contacts { get; set; }
        public IEnumerable<UserModel> FoundContacts { get; set; } 

        public ICommand CloseConversation
        {
            get
            {
                if (this.closeConversation == null)
                {
                    this.closeConversation = new RelayCommand(this.HandleCloseConversation);
                }
                return this.closeConversation;
            }
        }

        public ICommand ViewProfile
        {
            get
            {
                if (this.viewProfile == null)
                {
                    this.viewProfile = new RelayCommand(this.HandleViewProfile);
                }
                return this.viewProfile;
            }
        }

        public ICommand StartConversation
        {
            get
            {
                if (this.startConversation == null)
                {
                    this.startConversation = new RelayCommand(this.HandleStartConversation);
                }
                return this.startConversation;
            }
        }

        private void HandleStartConversation(object parameter)
        {
            if (parameter != null)
            {
                var user = parameter as UserModel;
                var conversation = new ConversationModel()
                                       {
                                           FirstUser = CurrentUser,
                                           SecondUser = user
                                       };

                var startedConversation = ConversationsPersister.Start(CurrentUser.SessionKey, conversation);
                RaiseConversationStarted(startedConversation);
            }
        }

        private void HandleViewProfile(object parameter)
        {
            if (parameter != null)
            {
                var user = parameter as UserModel;
                this.ViewContactProfile(this, new ViewContactProfileArgs(user));
            }
        }

        private void HandleCloseConversation(object parameter)
        {
            this.conversations.Remove(parameter as MissedConversationModel);
        }

        public GeneralViewModel()
        {
            this.Contacts = new List<UserModel>();
        }

        public GeneralViewModel(UserModel currentUser)
        {
            this.CurrentUser = currentUser;
            ReloadContacts();
            this.FoundContacts = new List<UserModel>(this.Contacts);
        }

        public void ReloadContacts()
        {
            this.Contacts = ContactsPersister.GetAllContacts(this.CurrentUser.SessionKey);
            this.FoundContacts = new List<UserModel>(Contacts);
            OnPropertyChanged("FoundContacts");
        }

        private void HandleTextChanged(string newText)
        {
            if (!string.IsNullOrEmpty(newText))
            {
                this.FoundContacts = this.Contacts
                    .Where(um => um.Username.ToLower().Contains(newText.ToLower()));

                OnPropertyChanged("FoundContacts");
            }
            else
            {
                this.FoundContacts = this.Contacts;
                OnPropertyChanged("FoundContacts");
            }
        }
    }
}