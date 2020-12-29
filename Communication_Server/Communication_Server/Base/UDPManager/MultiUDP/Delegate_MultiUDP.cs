using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.UDPManager.MultiUDP
{
    public partial class cMultiUDP
    {
        public delegate void CallSender(string address, int port, byte[] data);
    }
}
