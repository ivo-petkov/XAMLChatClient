namespace Xaml.Chat.Client.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xaml.Chat.Client.Models;

    public class ConversationsPersister
    {
        private static string baseUrl = "http://xamlchat.apphb.com/api/conversations/";
        
        private static Dictionary<string, string> headers = new Dictionary<string, string>();

        public static ConversationModel Start(string sessionKey, ConversationModel conversationData)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            return HttpRequester.Post<ConversationModel>(baseUrl + "start", conversationData, headers);
        }
        
        public static void MarkRead(string sessionKey, int id)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            string url = baseUrl + "markread/" + id;
            HttpRequester.Get(url, headers);
        }

        public static IEnumerable<MissedConversationModel> GetMissed(string sessionKey, int id)
        {
            headers[HttpRequester.sessionKeyHeaderName] = sessionKey;
            string url = baseUrl + "missed/" + id;
            return HttpRequester.Get<IEnumerable<MissedConversationModel>>(url, headers);
        }
    }
}