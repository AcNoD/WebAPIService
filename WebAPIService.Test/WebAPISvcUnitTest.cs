using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json;

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

        [TestMethod]
        public void TestAddDocumentToWCFEndpoint()
        {
            var host = Hosting.WcfConfigurableSelfHost();

            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            var address = string.Format("http://localhost:{0}/DocumentService/AddDocument", port);

            var document = new Document
                {
                    Id = 1001,
                    Name = "NewDocument",
                    Content = "new test doc content"
                };

            var serializer = new JavaScriptSerializer();
            var jsonString = "{\"document\":" + serializer.Serialize(document) + "}";

            var response = WebInvoker.Invoke(address, RequestMethodType.POST, ResponseFormat.JSON, jsonString);
            Trace.WriteLine("Web response: " + response);
            Assert.AreEqual("1001", response);
            host.Close();
        }

        [TestMethod]
        public void TestAddDocumentToWCFEndpointXML()
        {
            var host = Hosting.WcfConfigurableSelfHost();

            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            var address = string.Format("http://localhost:{0}/DocumentService/AddDocument", port);

            var document = new Document
            {
                Id = 1001,
                Name = "NewDocument",
                Content = "new test doc content"
            };

            //var xmlString = SerializeToXml(document);
            //var xmlString = "<AddDocument xmlns=\"http://tempuri.org/\"><Document xmlns=\"http://www.test.com/Docns\"><Id>1001</Id><Name>NewDocument</Name><Content>new test doc content</Content></Document></AddDocument>";
            var xmlString = "<AddDocument xmlns=\"http://tempuri.org/\"><document><Id>1001</Id><Name>NewDocument</Name><Content>new test doc content</Content></document></AddDocument>";
            Trace.WriteLine(xmlString);
            var encoding = new ASCIIEncoding();
            var data = encoding.GetBytes(xmlString);
            var response = WebInvoker.InvokeByte(address, RequestMethodType.POST, ResponseFormat.XML, data);
            Trace.WriteLine("Web response: " + response);
            Assert.AreEqual("1001", response);
            host.Close();
        }

        [TestMethod]
        public void TestAddDocumentToWCFEndpointXML2()
        {
            var host = Hosting.WcfConfigurableSelfHost();

            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            var address = string.Format("http://localhost:{0}/DocumentService/AddSimple", port);

            var document = "same info";
            var xmlString = SerializeToXml(document);
            var response = WebInvoker.Invoke(address, RequestMethodType.POST, ResponseFormat.XML, "<document>wefwefwe</document>");
            Trace.WriteLine("Web response: " + response);
            Assert.AreEqual("1001", response);
            host.Close();
        }

        private static byte[] ToByteArray<T>(T requestBody)
        {
            byte[] bytes;
            using (var s = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(s, requestBody);
                bytes = s.ToArray();
            }
            return bytes;
        }
        
        [TestMethod]
        public void Test()
        {
            var host = Hosting.WebApiSelfHost();

            Trace.WriteLine(WebInvoker.Invoke("http://localhost:8080/api/documents", RequestMethodType.GET, ResponseFormat.JSON));

            host.CloseAsync().Wait();
        }

        private static string SerializeToXml<T>(T value)
        {
            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}
