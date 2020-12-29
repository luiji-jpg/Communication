using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.TCPManager.MultiTCP
{
    public partial class cMultiTCP
    {
        public delegate void CallSender(string address, int port, byte[] data);
    }
}
