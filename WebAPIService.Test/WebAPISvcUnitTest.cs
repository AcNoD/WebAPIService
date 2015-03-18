using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIService.Test
{
    [TestClass]
    public class WebApiSvcUnitTest
    {
        [TestMethod]
        public void WCFComplexGet_ReturnXML()
        {
            InvokeWcfMethod("GetDocument/2", RequestMethodType.GET, ResponseFormat.XML);
        }

        [TestMethod]
        public void WCFComplexGet_ReturnJSON()
        {
            InvokeWcfMethod("GetDocument/1", RequestMethodType.GET, ResponseFormat.JSON);
        }

        [TestMethod]
        public void WCFSimpleGet_ReturnXML()
        {
            InvokeWcfMethod("GetData/New York", RequestMethodType.GET, ResponseFormat.XML);
        }

        [TestMethod]
        public void WCFSimpleGet_ReturnJSON()
        {
            InvokeWcfMethod("GetData/New York", RequestMethodType.GET, ResponseFormat.JSON);
        }

        [TestMethod]
        public void WCFSimplePost_ReturnXML()
        {
            InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.XML, "<PostData xmlns=\"http://tempuri.org/\"><value>Barcelona</value></PostData>");
        }

        [TestMethod]
        public void WCFSimplePost_ReturnJSON()
        {
            InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.JSON, "{\"value\":\"Barcelona\"");
        }

        [TestMethod]
        public void WCFComplexPost_ReturnJSON()
        {
            var document = new Document("Real Madrid", "Content of the document");
            var jObj = new JObject();
            jObj["document"] = JToken.FromObject(document);
            InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.JSON, jObj.ToString());
        }

        [TestMethod]
        public void WCFComplexPost_ReturnXML()
        {
            var document = new Document("Chelsea", "Content of the document");

            const string ns = "http://tempuri.org/";

            var dcSerializer = new DataContractSerializer(typeof(Document), "document", ns);
            var raw = "";
            using (var mStream = new MemoryStream())
            {
                dcSerializer.WriteObject(mStream, document);
                raw = Encoding.UTF8.GetString(mStream.GetBuffer());
            }

            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("AddDocument", ns);
            rootNode.InnerXml = raw;
            xmlDoc.AppendChild(rootNode);
            InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.XML, xmlDoc.OuterXml);
        }

        private static string BuildUri(string postfix)
        {
            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            return string.Format("http://localhost:{0}/DocumentService/{1}", port, postfix);
        }

        public void InvokeWcfMethod(string method, RequestMethodType methodType, ResponseFormat responseFormat, string body = null)
        {
            using (var host = Hosting.WcfConfigurableSelfHost())
            {
                Trace.WriteLine(WebInvoker.Invoke(BuildUri(method), methodType, responseFormat, body));
                host.Close();
            }
        }

        public void InvokeWebApiMethod(string method, RequestMethodType methodType, ResponseFormat responseFormat)
        {
            var host = Hosting.WebApiSelfHost();

            Trace.WriteLine(WebInvoker.Invoke(method, methodType, responseFormat));

            host.CloseAsync().Wait();
        }

        [TestMethod]
        public void WebAPIComplexGet_ReturnXML()
        {
            InvokeWebApiMethod("http://localhost:8080/api/documents", RequestMethodType.GET, ResponseFormat.XML);
        }

        [TestMethod]
        public void WebAPIComplexGet_ReturnJSON()
        {
            InvokeWebApiMethod("http://localhost:8080/api/documents", RequestMethodType.GET, ResponseFormat.JSON);
        }
    }
}
