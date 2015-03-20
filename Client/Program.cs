using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using Client.ServiceReference1;
using Model;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new ServiceReference1.DocumentServiceClient();
            Document doc= proxy.GetDocument("1");

        }
    }
}
