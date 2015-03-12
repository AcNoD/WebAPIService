using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebAPIService.Test
{
    class WebInvoker
    {
        public static string Invoke(string address, RequestMethodType method, ResponseFormat fromat, string postContent = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = GetContentType(fromat);
            httpWebRequest.Method = method.ToString();

            var responseText = "";
            if (method == RequestMethodType.POST)
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(postContent);
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
