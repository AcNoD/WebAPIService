using System;
using System.ServiceModel;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Http.ExceptionHandling;
using NLog;
using WebAPIService.WebAPI;

namespace WebAPIService
{
    /// <summary>
    /// Self hosting for WCF and WebAPI services
    /// </summary>
    public class Hosting
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static HttpSelfHostServer WebApiSelfHost()
        {
            var port = WebConfigurationManager.AppSettings["webAPIServicePort"];
            var address = string.Format("http://localhost:{0}", port);
            var config = new HttpSelfHostConfiguration(address);

            config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            var host = new HttpSelfHostServer(config);
            host.Configuration.Services.Add(typeof(IExceptionLogger), new UnhandledExceptionLogger(Logger));
            host.OpenAsync().Wait();

            return host;
        }

        public static ServiceHost WcfConfigurableSelfHost()
        {
            var port = WebConfigurationManager.AppSettings["wcfServicePort"];
            var address = string.Format("http://localhost:{0}/DocumentService", port);
            // create host
            var uri = new Uri(address);
            var host = new ConfigurableServiceHost(typeof (DocumentService), uri);            
            host.Opened += host_Opened;
            host.Closed += host_Closed;
            host.Faulted += host_Faulted;
            host.UnknownMessageReceived += host_UnknownMessageReceived;
            // open host
            host.Description.Behaviors.Insert(0, new GlobalExceptionHandlerBehavior());
            host.Open();
            
            return host;
        }

        static void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Logger.Error("Unknown message has been reseived. EventArgs Message {0}", e.Message);
        }

        static void host_Closed(object sender, EventArgs e)
        {
            Logger.Info("WCF Service {0} is closed", typeof(IDocumentService));
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Logger.Info("WCF Service {0} is opened", typeof(IDocumentService));
        }

        static void host_Faulted(object sender, EventArgs e)
        {
            Logger.Fatal("WCF host has faulted " + e);
        }
    }
}
