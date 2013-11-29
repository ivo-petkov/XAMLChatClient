using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class MissedConversationsViewModel : ViewModelBase, IPageViewModel
    {
        public string Name { get { return "Missed Conversations"; } }

        public IEnumerable<MissedConversationModel> MissedConversations { get; set; }
        public UserModel CurrentUserInfo { get; set; }

        private ICommand viewMissedConversation;
        public ICommand ViewMissedConversation
        {
            get
            {
                if(this.viewMissedConversation == null)
                {
                    this.viewMissedConversation = new RelayCommand(HandleViewMissedConversation);
                }

                return this.viewMissedConversation;
            }
        }

        public event EventHandler<ConversationStartedArgs> ViewConversation;
        private void RaiseViewConversation(ConversationModel conversation)
        {
            if (this.ViewConversation != null)
            {
                this.ViewConversation(this, new ConversationStartedArgs(conversation));
            }
        }

        private void HandleViewMissedConversation(object parameter)
        {
            if (parameter != null)
            {
                var missedConversation = parameter as MissedConversationModel;
                var conversationData = new ConversationModel()
                                           {
                                               FirstUser = new UserModel() {Username = CurrentUserInfo.Username},
                                               SecondUser = new UserModel() {Username = missedConversation.Username}
                                           };

                var startedConversation = ConversationsPersister.Start(CurrentUserInfo.SessionKey, conversationData);
                RaiseViewConversation(startedConversation);
            }
        }
    }
}
