using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIService
{
    class EntryPoint
    {
        static void Main()
        {
            var task = Task.Factory.StartNew(Hosting.Host3);
            task.Wait(50000);
        }
    }
}
