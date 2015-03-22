using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// WCF GET
        /// OUT: uri args
        /// IN: Complex XML
        /// </summary>
        [TestMethod]
        public void WCFComplexGet_ReturnXML()
        {
            const int docId = 2;
            var respnse = InvokeWcfMethod("GetDocument/" + docId, RequestMethodType.GET, ResponseFormat.XML);
            var document = DeserializeFromXml<Document>(respnse);
            var origin = DocumentStorage.Storage.GetDocument(docId);
            Assert.AreEqual(origin.Id, document.Id);
            Assert.AreEqual(origin.Name, document.Name);
            Assert.AreEqual(origin.Content, document.Content);
        }

        /// <summary>
        /// WCF GET
        /// OUT: uri args
        /// IN: Complex JSON
        /// </summary>
        [TestMethod]
        public void WCFComplexGet_ReturnJSON()
        {
           const int docId = 1;            
           var response = InvokeWcfMethod("GetDocument/" + docId, RequestMethodType.GET, ResponseFormat.JSON);
           var document = JsonConvert.DeserializeObject<Document>(response);
           var origin = DocumentStorage.Storage.GetDocument(docId);
           Assert.AreEqual(origin.Id, document.Id);
           Assert.AreEqual(origin.Name, document.Name);
           Assert.AreEqual(origin.Content, document.Content);
        }

        /// <summary>
        /// WCF GET
        /// OUT: uri args
        /// IN: Simple XML
        /// </summary>
        [TestMethod]
        public void WCFSimpleGet_ReturnXML()
        {
            const string value = "New York";
            var response = InvokeWcfMethod("GetData/" + value, RequestMethodType.GET, ResponseFormat.XML);
            var result = DeserializeFromXml<string>(response);
            Assert.AreEqual("You have sent " + value, result);
        }

        /// <summary>
        /// WCF GET
        /// OUT: uri args
        /// IN: Simple JSON
        /// </summary>
        [TestMethod]
        public void WCFSimpleGet_ReturnJSON()
        {
            const string value = "Detroit";
            var response = InvokeWcfMethod("GetData/" + value, RequestMethodType.GET, ResponseFormat.JSON);
            var result = JsonConvert.DeserializeObject<string>(response);
            Assert.AreEqual("You have sent " + value, result);
        }

        /// <summary>
        /// WCF POST
        /// OUT: Body XML
        /// IN: Simple XML
        /// </summary>
        [TestMethod]
        public void WCFSimplePost_SendXML()
        {
            const string value = "Barcelona";
            var response = InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.XML, string.Format("<PostData xmlns=\"{0}\"><value>{1}</value></PostData>", Ns, value));
            var result = DeserializeFromXml<string>(response);
            Assert.AreEqual("You have sent " + value, result);
        }

        /// <summary>
        /// WCF POST
        /// OUT: Body JSON
        /// IN: Simple JSON
        /// </summary>
        [TestMethod]
        public void WCFSimplePost_SendJSON()
        {
            const string value = "Madrid";
            var response = InvokeWcfMethod("PostData", RequestMethodType.POST, ResponseFormat.JSON, "{\"value\":\"" + value + "\"");
            var result = JsonConvert.DeserializeObject<string>(response);
            Assert.AreEqual("You have sent " + value, result);
        }

        /// <summary>
        /// WCF POST
        /// OUT: Body Complex XML
        /// IN: Simple XML
        /// </summary>
        [TestMethod]
        public void WCFComplexPost_SendXML()
        {
            var document = new Document("Chelsea", "Content of the document");

            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("AddDocument", Ns);
            rootNode.InnerXml = SerializeToXml(document, "document", Ns);
            xmlDoc.AppendChild(rootNode);

            var expectedId = DocumentStorage.Storage.GetMaxId() + 1;

            var response = InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.XML, xmlDoc.OuterXml);

            var id = DeserializeFromXml<long>(response);
            Assert.AreEqual(expectedId, id);

            var resultedDoc = DocumentStorage.Storage.GetDocument(id);
            Assert.AreEqual(document.Name, resultedDoc.Name);
            Assert.AreEqual(document.Content, resultedDoc.Content);
        }

        /// <summary>
        /// WCF POST
        /// OUT: Body Complex JSON
        /// IN: Simple JSON
        /// </summary>
        [TestMethod]
        public void WCFComplexPost_SendJSON()
        {
            var document = new Document("Real Madrid", "Content of the document");

            var jObj = new JObject();
            jObj["document"] = JToken.FromObject(document);

            var expectedId = DocumentStorage.Storage.GetMaxId() + 1;

            var response = InvokeWcfMethod("AddDocument", RequestMethodType.POST, ResponseFormat.JSON, jObj.ToString());

            var id = JsonConvert.DeserializeObject<long>(response);
            Assert.AreEqual(expectedId, id);

            var resultedDoc = DocumentStorage.Storage.GetDocument(id);
            Assert.AreEqual(document.Name, resultedDoc.Name);
            Assert.AreEqual(document.Content, resultedDoc.Content);
        }

        /// <summary>
        /// WebAPI GET
        /// OUT: none
        /// IN: Complex XML
        /// </summary>
        [TestMethod]
        public void WebAPIComplexGet_ReturnXML()
        {
            var response = InvokeWebApiMethod(RequestMethodType.GET, ResponseFormat.XML);
            var documentList = DeserializeFromXml<IEnumerable<Document>>(response);
            const long docId = 2;
            var expectedDoc = DocumentStorage.Storage.GetDocument(docId);
            var actualDoc = documentList.Single(x => x.Id == docId);
            Assert.AreEqual(expectedDoc.Id, actualDoc.Id);
            Assert.AreEqual(expectedDoc.Name, actualDoc.Name);
            Assert.AreEqual(expectedDoc.Content, actualDoc.Content);
        }

        /// <summary>
        /// WebAPI GET
        /// OUT: none
        /// IN: Complex JSON
        /// </summary>
        [TestMethod]
        public void WebAPIComplexGet_ReturnJSON()
        {
            var response = InvokeWebApiMethod(RequestMethodType.GET, ResponseFormat.JSON);
            const long docId = 3;
            var expectedDoc = DocumentStorage.Storage.GetDocument(docId);
            var documentList = JsonConvert.DeserializeObject<IEnumerable<Document>>(response);
            var actualDoc = documentList.Single(x => x.Id == docId);
            Assert.AreEqual(expectedDoc.Id, actualDoc.Id);
            Assert.AreEqual(expectedDoc.Name, actualDoc.Name);
            Assert.AreEqual(expectedDoc.Content, actualDoc.Content);
        }

        /// <summary>
        /// WebAPI Post
        /// OUT: Complex XML
        /// IN: Simple XML
        /// </summary>
        [TestMethod]
        public void WebAPIComplexPost_SendXML()
        {
            var document = new Document("Chelsea", "Content of the document");
            var expectedId = DocumentStorage.Storage.GetMaxId() + 1;
            var response = InvokeWebApiMethod(RequestMethodType.POST, ResponseFormat.XML, SerializeToXml(document, "Document", Ns));            
            var id = DeserializeFromXml<long>(response);
            Assert.AreEqual(expectedId, id);

            var resultedDoc = DocumentStorage.Storage.GetDocument(id);
            Assert.AreEqual(document.Name, resultedDoc.Name);
            Assert.AreEqual(document.Content, resultedDoc.Content);
        }


        /// <summary>
        /// WebAPI Post
        /// OUT: Complex JSON
        /// IN: Simple JSON
        /// </summary>
        [TestMethod]
        public void WebAPIComplexPost_SendJSON()
        {
            var document = new Document("Real Madrid", "Content of the document");
            var expectedId = DocumentStorage.Storage.GetMaxId() + 1;
            var response = InvokeWebApiMethod(RequestMethodType.POST, ResponseFormat.JSON, JsonConvert.SerializeObject(document));
            var id = JsonConvert.DeserializeObject<long>(response);
            Assert.AreEqual(expectedId, id);

            var resultedDoc = DocumentStorage.Storage.GetDocument(id);
            Assert.AreEqual(document.Name, resultedDoc.Name);
            Assert.AreEqual(document.Content, resultedDoc.Content);
        }

        /// <summary>
        /// Sends message to WCF service which put it to the queue,
        /// afterwards reads message from that queue
        /// </summary>
        [TestMethod]
        public void SendToMSMQ()
        {
            const string value = "Barcelona";
            InvokeWcfMethod("SendToQueue", RequestMethodType.POST, ResponseFormat.JSON, "{\"value\":\"" + value + "\"");
            var msg = Msmq.ReceiveMessage();
            Assert.AreEqual(value, msg);
        }
    }
}
