using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapeKernel.ConnectionProcessors;

namespace GrapeServer
{
    public static class PhantomKernel
    {
        static void Main()
        {
            ServerStarter().Wait();
        }


        static async Task ServerStarter()
        {
            var server = new SocketTransmitter(11000, 20);
            var serverTask = server.Start();
            await serverTask.ConfigureAwait(false);

            Console.ReadKey();
        }
    }
}
