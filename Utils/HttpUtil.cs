using System;
using System.IO;
using System.Net;
using System.Text;

namespace Utils
{
    public class HttpUtil
    {
        public static string GetRequest(string url, string user, string password)
        {
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + password));
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Basic " + encoded);
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    var output = new StringBuilder();
                    output.Append(reader.ReadToEnd());
                    return output.ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot get respont for request with url {url}: " + e.Message);
            }
        }
    }
}