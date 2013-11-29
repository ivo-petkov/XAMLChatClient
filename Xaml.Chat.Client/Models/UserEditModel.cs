namespace Xaml.Chat.Client.Models
{
    public class UserEditModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string OldPasswordHash { get; set; }

        public string NewPasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}