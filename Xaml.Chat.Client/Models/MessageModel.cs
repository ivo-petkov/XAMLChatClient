namespace Xaml.Chat.Client.Models
{
    using System;
    
    public class MessageModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public ConversationModel Conversation { get; set; }

        public UserModel Sender { get; set; }
    }
}