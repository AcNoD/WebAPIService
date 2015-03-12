using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebAPIService
{
    class EntryPoint
    {
        static void Main()
        {
            //var task = Task.Factory.StartNew(Hosting.WcfConfigurableSelfHost);
            //task.Wait(50000);

            var host = Hosting.WebApiSelfHost();

            Console.ReadKey();
            host.CloseAsync().Wait();
        }
    }
}
