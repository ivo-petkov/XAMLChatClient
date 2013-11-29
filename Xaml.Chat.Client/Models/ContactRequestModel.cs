namespace Xaml.Chat.Client.Models
{
    public class ContactRequestModel
    {
        public int Id { get; set; }

        public virtual UserModel Sender { get; set; }
    }
}