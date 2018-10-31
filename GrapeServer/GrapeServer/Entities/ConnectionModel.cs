using System;
using System.Net.Sockets;


namespace GrapeKernel.Entities
{
    public class ConnectionModel
    {
        public Guid Id { get; }
        public Socket ClientSocket { get; }

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
            ClientSocket = clientSocket;
        }
    }
}
