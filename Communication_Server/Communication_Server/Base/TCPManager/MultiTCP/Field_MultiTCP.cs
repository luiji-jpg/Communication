
using Communication_Server.Base.TCPManager.UseTCP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.TCPManager.MultiTCP
{
    public partial class cMultiTCP
    {
        private static cMultiTCP mInst;

        private List<cUseTCP> mTcpList;
        private Dictionary<string, cUseTCP> mDevList;

        private Dictionary<string, CallSender> mCallSenderList;
    }
}
