using System.Net.Sockets;
using System.Net;


namespace GrapeKernel.Entities
{
    public class PortListener : ConnectionModel
    {
        public PortListener(Socket clientSocket) : base(clientSocket)
        { }


        public void Bind(string ipAddress, int port)
        {
            var hostPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            ClientSocket.Bind(hostPoint);
        }


        public void Listen(int maxConcurrency)
        {
            ClientSocket.Listen(maxConcurrency);
        }


        public Socket ListenPort()
        {
            var connection = ClientSocket.Accept();
            return connection;
        }
    }
}
