using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication_Server.Base.TCPManager.TCP
{
    public class StateObject
    {
        public byte[] Buffer;
        public Socket WorkingSock;
        public readonly int BuffSize;

        public StateObject(int BuffSize)
        {
            this.BuffSize = BuffSize;
            this.Buffer = new byte[this.BuffSize];
        }

        public void ClearBuff()
        {
            Array.Clear(this.Buffer, 0, this.BuffSize);
        }
    }
}
