using System;
using System.IO;
using System.Net;

namespace DeanIS.YBSquare.Web.Services
{
    public class HttpDownloader : IDownloader
    {
        public string GetString(Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            using (var webResponse = webRequest.GetResponse())
            {
                var response = (HttpWebResponse) webResponse;

                var responseStream = response.GetResponseStream();

                if (responseStream == null) return string.Empty;

                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}