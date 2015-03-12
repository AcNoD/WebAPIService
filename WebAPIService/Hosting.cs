using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace WebAPIService
{
    /// <summary>
    /// Self hosting for WCF and WebAPI services
    /// </summary>
    public class Hosting
    {
        /// <summary>
        /// Host services
        /// </summary>
        public static void Host()
        {
            var httpBaseAddress = new Uri("http://localhost:9900/DocumentService");

            using (var host = new ServiceHost(typeof(DocumentService), httpBaseAddress))
            {
                // add endpoint
                var ep = host.AddServiceEndpoint(typeof(IDocumentService), new WebHttpBinding(), string.Empty);                
                ep.Behaviors.Add(new WebHttpBehavior());

                /*var httpEp = new WebHttpEndpoint(ContractDescription.GetContract(typeof(IDocumentService)))
                {
                    Address = new EndpointAddress(httpBaseAddress),
                    AutomaticFormatSelectionEnabled = true

                };
                host.AddServiceEndpoint(httpEp);*/

                // create behavior
                var behavior = new ServiceMetadataBehavior {HttpGetEnabled = true };
                
                
                // add behavior to host
                host.Description.Behaviors.Add(behavior);

                // open host
                host.Open();

                Console.WriteLine("WCF Service {0} is opened", typeof(IDocumentService));
                Console.ReadKey();
            }
        }

        public static void Host2()
        {
            using (var host = new WebServiceHost(typeof(DocumentService), new Uri("http://localhost:9900/")))
            {
                host.AddServiceEndpoint(typeof (IDocumentService), new WebHttpBinding(), "");
                var sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                sdb.HttpHelpPageEnabled = false;
                host.Open();

                Console.WriteLine("WCF Service {0} is opened", typeof(IDocumentService));
            }
        }

        public static void Host3()
        {
            var httpBaseAddress = new Uri("http://localhost:9901/DocumentService");

            using (var host = new ConfigurableServiceHost(typeof(DocumentService), httpBaseAddress))
            //using (var host = new ConfigurableServiceHost())
            {
                host.Open();
                Console.ReadKey();
            }
        }
    }
}
