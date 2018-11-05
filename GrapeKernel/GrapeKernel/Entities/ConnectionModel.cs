using System;
using System.Net.Sockets;


namespace GrapeKernel.Entities
{
    public class ConnectionModel
    {
        public Guid Id { get; }
        protected Socket ClientSocket { get; set; }
        public NetworkStream Stream { get; private set; }

        public bool ReadingStatus
        {
            get
            {
                return ClientSocket.Poll(100, SelectMode.SelectRead);
            }
        }


        public bool WrittingStatus
        {
            get
            {
                return ClientSocket.Poll(100, SelectMode.SelectWrite);
            }
        }


        public ConnectionModel(Socket clientSocket)
        {
            Id = Guid.NewGuid();
            this.ClientSocket = clientSocket;
            this.Stream = new NetworkStream(clientSocket);
        }
    }
}
