using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace WebAPIService
{
    /// <summary>
    /// Self hosting for WCF and WebAPI services
    /// </summary>
    class Hosting
    {
        /// <summary>
        /// Host services
        /// </summary>
        public static void Host()
        {
            var httpBaseAddress = new Uri("http://localhost:47863/DocumentService");

            using (var host = new ServiceHost(typeof(DocumentService), httpBaseAddress))
            {
                // add endpoint
                host.AddServiceEndpoint(typeof (IDocumentService), new WSHttpBinding(), string.Empty);
                
                // create behavior
                var behavior = new ServiceMetadataBehavior {HttpGetEnabled = true};
                
                // add behavior to host
                host.Description.Behaviors.Add(behavior);

                // open host
                host.Open();

                Console.WriteLine("WCF Service {0} is opened", typeof(IDocumentService));

                Console.ReadKey();
            }
        }
    }
}
