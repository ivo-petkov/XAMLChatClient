namespace Xaml.Chat.Client.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xaml.Chat.Client.Models;

    public class ContactsPersister
    {
        private static string baseUrl = "http://xamlchat.apphb.com/api/contacts/";
        private static Dictionary<string, string> headers = new Dictionary<string, string>();

        public static IEnumerable<UserModel> GetAllContacts(string sessionKey)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            return HttpRequester.Get<IEnumerable<UserModel>>(baseUrl + "all", headers);
        }

        public static IEnumerable<ContactRequestModel> GetAllRequests(string sessionKey)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            return HttpRequester.Get<IEnumerable<ContactRequestModel>>(baseUrl + "requests", headers);
        }

        public static void AddUserToContacts(string sessionKey, int userId)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            HttpRequester.Get(baseUrl + "add/" + userId, headers);
        }

        public static void AcceptUserRequest(string sessionKey, int userId)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            HttpRequester.Get(baseUrl + "accept/" + userId, headers);
        }

        public static void DenyUserRequest(string sessionKey, int userId)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            HttpRequester.Get(baseUrl + "deny/" + userId, headers);
        }
    }
}