using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.Helpers
{
    public class ViewContactProfileArgs
    {
        public UserModel ContactInfo { get; set; }

        public ViewContactProfileArgs(UserModel userSettings)
            : base()
        {
            this.ContactInfo = userSettings;
        }
    }
}
