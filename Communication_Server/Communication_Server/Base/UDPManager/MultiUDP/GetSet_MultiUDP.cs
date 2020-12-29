using Communication_Server.Base.UDPManager.UseUDP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.UDPManager.MultiUDP
{
    public partial class cMultiUDP
    {
        public static cMultiUDP GetInst { get { if (mInst == null) mInst = new cMultiUDP(); return mInst; } }
        public Dictionary<string, cUseUDP> GetDevList { get { return mDevList; } }
        public Dictionary<string, CallSender> GetCallSenderList { get { return mCallSenderList; } }
    }
}
