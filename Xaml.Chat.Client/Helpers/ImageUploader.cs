using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Xaml.Chat.Client.Helpers
{
    public class ImageUploader
    {
        public static string UploadImage(string filePath)
        {
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                    { "key", "6528448c258cff474ca9701c5bab6927" },
                    { "image", Convert.ToBase64String(File.ReadAllBytes(filePath)) }
                };

                byte[] response = w.UploadValues("http://api.imgur.com/2/upload", values);

                var doc = XDocument.Load(new MemoryStream(response));
                var url = doc.Element("upload").Element("links").Element("original").Value;
                return url;
            }
        }
    }
}
