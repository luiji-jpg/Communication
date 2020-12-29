using Communication_Server.Base;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base.TCPManager.TCP
{
    public partial class cTCP
    {
        enum enChkFnSocket
        {
            ChkPing = 1,
            ChkSamePort,
            ChkSrcIp,
            ChkSetSocket,
            ChkBind,
            ChkSetSockOption,
            ChkSetClient,
            ChkSetReceive,

        }
    }
}
