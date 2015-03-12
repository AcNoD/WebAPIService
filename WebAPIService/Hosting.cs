using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebAPIService
{
    /// <summary>
    /// Self hosting for WCF and WebAPI services
    /// </summary>
    public class Hosting
    {
        public static HttpSelfHostServer WebApiSelfHost()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            var host = new HttpSelfHostServer(config);

            host.OpenAsync().Wait();

            return host;
        }

        public static ServiceHost WcfSelfHost()
        {
            // create host
            var uri = new Uri(GetAddressFromConfig());
            var host = new ServiceHost(typeof(DocumentService), uri);

            // add endpoint
            var ep = host.AddServiceEndpoint(typeof (IDocumentService), new WebHttpBinding(), string.Empty);
            ep.Behaviors.Add(new WebHttpBehavior());

            /*var httpEp = new WebHttpEndpoint(ContractDescription.GetContract(typeof(IDocumentService)))
                {
                    Address = new EndpointAddress(httpBaseAddress),
                    AutomaticFormatSelectionEnabled = true

                };
                host.AddServiceEndpoint(httpEp);*/

            // create behavior
            var behavior = new ServiceMetadataBehavior {HttpGetEnabled = true};

            // add behavior to host
            host.Description.Behaviors.Add(behavior);

            // open host
            host.Open();

            Console.WriteLine("WCF Service {0} is opened", typeof (IDocumentService));
            return host;
        }

        public static ServiceHost WcfConfigurableSelfHost()
        {
            // create host
            var uri = new Uri(GetAddressFromConfig());
            var host = new ConfigurableServiceHost(typeof (DocumentService), uri);

            // open host
            host.Open();
            Console.WriteLine("WCF Service {0} is opened", typeof (IDocumentService));
            return host;
        }

        private static string GetAddressFromConfig()
        {
            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            return string.Format("http://localhost:{0}/DocumentService", port);
        }
    }
}
