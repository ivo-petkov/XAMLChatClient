using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.Helpers
{
    public class RegisterSuccessArgs
    {
        public UserModel RegisteredUser;

        public RegisterSuccessArgs(UserModel user) : base()
        {
            this.RegisteredUser = user;
        }
    }
}
