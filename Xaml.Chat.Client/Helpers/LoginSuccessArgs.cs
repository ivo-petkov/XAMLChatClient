using System;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.Helpers
{
    public class LoginSuccessArgs:EventArgs
    {
        public UserModel UserSetting { get; set; }

        public LoginSuccessArgs(UserModel userSettings)
            : base()
        {
            this.UserSetting = userSettings;
        }
    }
}
