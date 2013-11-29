namespace Xaml.Chat.Client.Models
{
    using System.Collections.Generic;

    public class ConversationModel
    {
        public int Id { get; set; }

        public UserModel FirstUser { get; set; }

        public UserModel SecondUser { get; set; }

        public IEnumerable<MessageModel> Messages { get; set; }
    }
}