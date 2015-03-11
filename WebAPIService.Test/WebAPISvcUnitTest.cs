using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPIService.Test
{
    [TestClass]
    public class WebAPISvcUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var thread = new Thread(Hosting.Host);
            thread.Start();
            Thread.Sleep(5000);
            Trace.WriteLine(InvokeJsonWebMethod());
            thread.Abort();
        }

        static string InvokeJsonWebMethod()
        {
            const string json = @"application/json; charset=utf-8";
            const string xml = @"text/xml; charset=utf-8";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:47863/DocumentService/GeDocument/100");
            httpWebRequest.ContentType = xml;
            httpWebRequest.Method = "GET";

            var str = httpWebRequest.ToString();
            var responseText = "";
            /*using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
               streamWriter.Write("34");//any parameter
            }*/
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
            }
            return responseText;
        }
    }
}
