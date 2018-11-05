using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using GrapeKernel.Entities;


namespace GrapeKernel.SocketIO
{
    sealed class SocketTransmitter : IDisposable
    {
        private readonly List<ConnectionModel> _connections;
        private readonly PortListener _listener;


        public SocketTransmitter(string ipAddres, int port, int maxConcurrency)
        {
            _connections = new List<ConnectionModel>();
            _listener = new PortListener(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
            _listener.Bind(ipAddres, port);
            _listener.Listen(maxConcurrency);
            _connections.Add(_listener);
        }


        public async Task Start()
        {
            while (true)
            {
                for (int count = 0; count != _connections.Count; count++)
                {
                    if (_connections[count].ReadingStatus == true)
                    {
                        if (_connections[count].GetType() == typeof(PortListener))
                        {
                            Socket newConnection = await ((PortListener)_connections[count]).Accept();
                            var newClient = new ConnectionModel(newConnection);
                            _connections.Add(newClient);
                        }
                        else
                        {
                            await SendAsync(_connections[count], "Read\n");
                        }
                    }
                    else
                    {
                        if (_connections[count].GetType() != typeof(PortListener))
                        {
                            await SendAsync(_connections[count],
                            _connections[count].Id.ToString() + "\n");
                        }
                    }
                }
            }
        }


        /*public async Task SendAsync(ConnectionModel connection, byte[] data)
        {
            await Task.Factory.FromAsync(
                connection.ClientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, null, connection.ClientSocket),
                connection.ClientSocket.EndSend).ConfigureAwait(false);
        }*/


        public void Dispose()
        {
            if (!ReferenceEquals(_connections, null))
            {
                foreach (var clientModel in _connections)
                {
                    clientModel.ClientSocket.Dispose();
                }
            }

            if (!ReferenceEquals(_listener, null))
            {
                _listener.ClientSocket.Dispose();
            }
        }
    }
}
