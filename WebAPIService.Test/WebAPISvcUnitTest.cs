using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPIService.Test
{
    [TestClass]
    public class WebAPISvcUnitTest
    {
        private const string address = "http://localhost:9901/DocumentService/GetDocument";

        [TestMethod]
        public void InvokeWebMethod_XML()
        {
            InvokeWebMethod(ResponseFormat.XML);
        }

        [TestMethod]
        public void InvokeWebMethod_JSON()
        {
            InvokeWebMethod(ResponseFormat.JSON);
        }

        public void InvokeWebMethod(ResponseFormat responseFormat)
        {
            var thread = new Thread(Hosting.Host3);
            thread.Start();
            Thread.Sleep(5000);
            Trace.WriteLine(WebInvoker.Invoke(address, RequestMethodType.GET, responseFormat));
            thread.Abort();
        }
    }
}
