using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIService.Test
{
    [TestClass]
    public partial class WebApiSvcUnitTest
    {
        [TestMethod]
        public void SendToMSMQ()
        {
            const string city = "Barcelona";
            InvokeWcfMethod("SendToQueue", RequestMethodType.POST, ResponseFormat.JSON, "{\"value\":\""+city+"\"");
            var value = Msmq.ReceiveMessage();
            Assert.AreEqual(city, value);
        }

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
        public void WCFSimplePost_SendXML()
        {
            InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.XML, string.Format("<PostData xmlns=\"{0}\"><value>Barcelona</value></PostData>", Ns));
        }

        [TestMethod]
        public void WCFSimplePost_SendJSON()
        {
            InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.JSON, "{\"value\":\"Barcelona\"");
        }

        [TestMethod]
        public void WCFComplexPost_SendJSON()
        {
            var document = new Document("Real Madrid", "Content of the document");
            var jObj = new JObject();
            jObj["document"] = JToken.FromObject(document);
            InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.JSON, jObj.ToString());
        }

        [TestMethod]
        public void WCFComplexPost_SendXML()
        {
            var document = new Document("Chelsea", "Content of the document");
            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("AddDocument", Ns);
            rootNode.InnerXml = SerializeToXml(document, "document", Ns);
            xmlDoc.AppendChild(rootNode);
            InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.XML, xmlDoc.OuterXml);
        }

        [TestMethod]
        public void WebAPIComplexGet_ReturnXML()
        {
            InvokeWebApiMethod(RequestMethodType.GET, ResponseFormat.XML);
        }

        [TestMethod]
        public void WebAPIComplexGet_ReturnJSON()
        {
            InvokeWebApiMethod(RequestMethodType.GET, ResponseFormat.JSON);
        }

        [TestMethod]
        public void WebAPIComplexPost_SendXML()
        {
            var document = new Document("Chelsea", "Content of the document");
            InvokeWebApiMethod(RequestMethodType.POST, ResponseFormat.XML, SerializeToXml(document, "Document", Ns));
        }

        [TestMethod]
        public void WebAPIComplexPost_SendJSON()
        {
            var document = new Document("Real Madrid", "Content of the document");
            InvokeWebApiMethod(RequestMethodType.POST, ResponseFormat.JSON, JsonConvert.SerializeObject(document));
        }
    }
}
