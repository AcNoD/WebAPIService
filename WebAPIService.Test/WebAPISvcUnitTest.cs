using System.Diagnostics;
using System.Threading;
using System.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPIService.Test
{
    [TestClass]
    public class WebApiSvcUnitTest
    {
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
            var host = Hosting.WcfConfigurableSelfHost();

            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            var address = string.Format("http://localhost:{0}/DocumentService/GetDocument", port);

            Trace.WriteLine(WebInvoker.Invoke(address, RequestMethodType.GET, responseFormat));

            host.Close();
        }
    }
}
