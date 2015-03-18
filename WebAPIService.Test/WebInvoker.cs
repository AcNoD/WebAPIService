using System.Diagnostics;
using System.IO;
using System.Net;

namespace WebAPIService.Test
{
    class WebInvoker
    {
        public static string Invoke(string address, RequestMethodType method, ResponseFormat fromat, string postContent = null)
        {
            Trace.WriteLine("Address: " + address);
            Trace.WriteLine("Method: " + method);
            if (postContent != null)
                Trace.WriteLine("Body: " + postContent);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = GetContentType(fromat);
            httpWebRequest.Method = method.ToString();

            var responseText = "";
            if (method == RequestMethodType.POST)
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(postContent);
                }
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
            }
            Trace.WriteLine("Response: " + responseText);
            return responseText;
        }

        public static string InvokeByte(string address, RequestMethodType method, ResponseFormat fromat, byte[] postContent = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = GetContentType(fromat);
            httpWebRequest.Method = method.ToString();

            var responseText = "";
            if (method == RequestMethodType.POST)
            {
                using (var streamWriter = httpWebRequest.GetRequestStream())
                    streamWriter.Write(postContent, 0, postContent.Length);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
            }
            return responseText;
        }

        private static string GetContentType(ResponseFormat format)
        {
            return format == ResponseFormat.XML ? @"text/xml; charset=utf-8" : @"application/json; charset=utf-8";
        }
    }
}
