using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class ConversationViewModel : ViewModelBase, IPageViewModel
    {
        private const string PUBLISH_KEY = "pub-c-c91f17ec-a2d1-4afb-93d5-650cb2e2d610";
        private const string SUBSCRIBE_KEY = "sub-c-59c4b3f8-1d38-11e3-9231-02ee2ddab7fe";
        private const string SECRET_KEY = "sec-c-MzJhZjE1NmMtOWMxNC00NGViLWE1MDUtNGUyNzY3YWFkODE1";
        private static PubnubAPI pubnub;
        private string channelName;

        public ConversationViewModel(ConversationModel conversation, UserModel currentUser, UserModel partner)
        {
            this.CurrentConversation = conversation;
            this.CurrentUserInfo = currentUser;
            this.Partner = partner;
            this.CurrentConversation.Messages =
                MessagePersister.GetAllMsgsByConversation(CurrentUserInfo.SessionKey,
                                                            CurrentConversation.Id);

            OnPropertyChanged("CurrentConversation");

            pubnub = new PubnubAPI(PUBLISH_KEY, SUBSCRIBE_KEY, SECRET_KEY, true);

            var minId = Math.Min(CurrentUserInfo.Id, Partner.Id);
            var maxId = Math.Max(CurrentUserInfo.Id, Partner.Id);
            channelName = string.Format("channel-{0}-{1}",
                                               minId, maxId);

            Thread t = new Thread(() =>
                                  pubnub.Subscribe(channelName, HandleNewMessageReceived));
            t.IsBackground = true;
            t.Start();
        }

        private bool HandleNewMessageReceived(object message)
        {
            this.CurrentConversation.Messages =
                MessagePersister.GetAllMsgsByConversation(CurrentUserInfo.SessionKey,
                                                            CurrentConversation.Id);

            OnPropertyChanged("CurrentConversation");
            return true;
        }

        public string MessageToSend { get; set; }

        public UserModel CurrentUserInfo { get; set; }

        public UserModel Partner { get; set; }

        public ConversationModel CurrentConversation { get; set; }

        private ICommand sendMessage;
        public ICommand SendMessage
        {
            get
            {
                if (this.sendMessage == null)
                {
                    this.sendMessage = new RelayCommand(HandleSendMessageCommand);
                }

                return this.sendMessage;
            }
        }

        private void HandleSendMessageCommand(object parameter)
        {
            // TODo: SEND MESSAGE
            var message = new MessageModel()
                              {
                                  Sender = CurrentUserInfo,
                                  Conversation = CurrentConversation,
                                  Content = MessageToSend
                              };

            MessagePersister.Send(CurrentUserInfo.SessionKey, message);
            pubnub.Publish(channelName, "New message");
            this.MessageToSend = "";
            OnPropertyChanged("MessageToSend");
        }

        public string Name { get { return "Conversation Window"; } }
    }
}
