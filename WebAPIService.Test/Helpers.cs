using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Configuration;

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

        private static string BuildUri(string postfix)
        {
            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            return string.Format("http://localhost:{0}/DocumentService/{1}", port, postfix);
        }

        public void InvokeWcfMethod(string method, RequestMethodType methodType, ResponseFormat responseFormat,
                                    string body = null)
        {
            using (var host = Hosting.WcfConfigurableSelfHost())
            {
                WebInvoker.Invoke(BuildUri(method), methodType, responseFormat, body);
                host.Close();
            }
        }

        public void InvokeWebApiMethod(string method, RequestMethodType methodType, ResponseFormat responseFormat,
                                       string body = null)
        {
            using (var host = Hosting.WebApiSelfHost())
            {
                WebInvoker.Invoke(method, methodType, responseFormat, body);
                host.CloseAsync().Wait();
            }
        }
    }
}
