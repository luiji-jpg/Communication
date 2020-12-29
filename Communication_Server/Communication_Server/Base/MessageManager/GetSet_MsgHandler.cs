using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.MessageManager
{
    public partial class cMsgHandler
    {
        public static cMsgHandler GetInst { get { if (mInst == null) mInst = new cMsgHandler(); return mInst; } }
        public Callback_Recv GetCallRecv { get { return mCallRecv; } }
    }
}
