using System.Threading.Tasks;

namespace WebAPIService
{
    class EntryPoint
    {
        static void Main()
        {
            var task = Task.Factory.StartNew("http://localhost:9901/DocumentService");
            task.Wait(50000);
        }
    }
}
