using Communication_Server.Base.UDPManager.UseUDP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.UDPManager.MultiUDP
{
    public partial class cMultiUDP
    {
        private static cMultiUDP mInst;

        private List<cUseUDP> mUdpList;
        private Dictionary<string, cUseUDP> mDevList;

        private Dictionary<string, CallSender> mCallSenderList;
    }
}
