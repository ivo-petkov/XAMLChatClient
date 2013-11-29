using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.Helpers
{
    public class ConversationStartedArgs
    {
        public ConversationModel Conversation { get; set; }

        public ConversationStartedArgs(ConversationModel conversation)
        {
            Conversation = conversation;
        }
    }
}
