using Communication_Server.Base.ICD.Header;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.MessageManager
{
    public partial class cMsgHandler
    {
        public delegate void Callback_Proc(string TargetIp, int TargetPort, cICD_Header msg);
        public delegate void Callback_Recv(string IpAddr, int Port, ushort msgId, cICD_Header msg);
    }
}
