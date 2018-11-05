using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapeKernel.Entities;

namespace GrapeKernel.SocketIO
{
    public class SocketReader
    {
        public void Read(ConnectionModel connection)
        {
            var stream = connection.Stream;
        }
    }
}
