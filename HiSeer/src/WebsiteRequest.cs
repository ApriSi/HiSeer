using System.IO;
using System.Net;
using System.Collections.Generic;

namespace HiSeer.src
{
    public static class WebsiteRequest
    {
        public static string GetWebJson(string url, Dictionary<string, string> headers)
        {
            using (var s = GetWebRequest(url, headers).GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    return contributorsAsJson;
                }
            }
        }

        static HttpWebRequest GetWebRequest(string url, Dictionary<string, string> headers)
        {
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null)
                return null;

            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";
            if(headers != null) {
                foreach(var header in headers)
                {
                    System.Console.WriteLine(header.Value);
                    webRequest.Headers.Add(header.Key, header.Value);
                }
            }
                

            return webRequest;
        }
    }
}
