
using Communication_Server.Base.TCPManager.UseTCP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.TCPManager.MultiTCP
{
    public partial class cMultiTCP
    {
        public static cMultiTCP GetInst { get { if (mInst == null) mInst = new cMultiTCP(); return mInst; } }
        public Dictionary<string, cUseTCP> GetDevList { get { return mDevList; } }
        public Dictionary<string, CallSender> GetCallSenderList { get { return mCallSenderList; } }
    }
}
