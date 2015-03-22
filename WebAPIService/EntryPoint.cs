using System;

namespace WebAPIService
{
    /// <summary>
    /// App entry point
    /// </summary>
    class EntryPoint
    {
        static void Main()
        {
            //var task = Task.Factory.StartNew(Hosting.WcfConfigurableSelfHost);
            //task.Wait(50000);

            /*var host = Hosting.WebApiSelfHost();

            Console.ReadKey();
            host.CloseAsync().Wait();*/
            using (var host = Hosting.WcfConfigurableSelfHost())
            {
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
