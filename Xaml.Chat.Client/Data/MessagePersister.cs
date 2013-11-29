namespace Xaml.Chat.Client.Data
{
    using System;
    using System.Collections.Generic;
    using Xaml.Chat.Client.Models;

    class MessagePersister
    {
        private static string baseUrl = "http://xamlchat.apphb.com/api/messages/";
        private static Dictionary<string, string> headers = new Dictionary<string, string>();

        public static void Send(string sessionKey, MessageModel msg)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            HttpRequester.Post(baseUrl + "send", msg, headers);
        }

        public static IEnumerable<MessageModel> GetAllMsgsByConversation(string sessionKey, int id)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            string url = baseUrl + "byconversation/" + id;
            return HttpRequester.Get<IEnumerable<MessageModel>>(url, headers);
        }
    }
}