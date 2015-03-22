using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace WebAPIService.Test
{
    public partial class WebApiSvcUnitTest
    {
        const string Ns = "http://tempuri.org/";

        public static string SerializeToXml<T>(T obj, string rootName, string xmlNamespace)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T), rootName, xmlNamespace);
                serializer.WriteObject(memoryStream, obj);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        public static T DeserializeFromXml2<T>(string xml, string ns)
        {
            var serializer = new XmlSerializer(typeof(string), new XmlRootAttribute("res"));
            var rdr = new StringReader(xml);
            string res = (string)serializer.Deserialize(rdr);
            return (T) serializer.Deserialize(rdr);
        }

        public string InvokeWcfMethod(string method, RequestMethodType methodType, ResponseFormat responseFormat,
                                    string body = null)
        {
            using (var host = Hosting.WcfConfigurableSelfHost())
            {
                var port = WebConfigurationManager.AppSettings["wcfServicePort"];
                var address = string.Format("http://localhost:{0}/DocumentService/{1}", port, method);
                var response = WebInvoker.Invoke(address, methodType, responseFormat, body);
                host.Close();
                return response;
            }
        }

        public string InvokeWebApiMethod(RequestMethodType methodType, ResponseFormat responseFormat,
                                       string body = null)
        {
            var host = Hosting.WebApiSelfHost();
            var port = WebConfigurationManager.AppSettings["webAPIServicePort"];
            var address = string.Format("http://localhost:{0}/api/documents", port);
            var response=  WebInvoker.Invoke(address, methodType, responseFormat, body);
            host.CloseAsync().Wait();
            return response;
        }
    }
}
