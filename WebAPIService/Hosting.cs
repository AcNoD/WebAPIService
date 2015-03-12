using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WebAPIService
{
    /// <summary>
    /// Self hosting for WCF and WebAPI services
    /// </summary>
    public class Hosting
    {
        public static void WcfSelfHost(string address)
        {
            // create host
            using (var host = new ServiceHost(typeof(DocumentService), new Uri(address)))
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

        public static void WcfConfigurableSelfHost(string address)
        {
            // create host
            using (var host = new ConfigurableServiceHost(typeof(DocumentService), new Uri(address)))
            {
                // open host
                host.Open();
                Console.WriteLine("WCF Service {0} is opened", typeof(IDocumentService));
                Console.ReadKey();
            }
        }
    }
}
