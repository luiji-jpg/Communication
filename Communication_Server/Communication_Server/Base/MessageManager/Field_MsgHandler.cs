using Communication_Server.Base.ICD.Header;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.MessageManager
{
    public partial class cMsgHandler
    {
        private static cMsgHandler mInst = null;

        private Queue<cICD_Header> mMsgList_Msg = new Queue<cICD_Header>();
        public List<Callback_Proc> mAddRecvList = new List<Callback_Proc>();

        private Queue<string> mIpList = new Queue<string>();
        private Queue<int> mPortList = new Queue<int>();
        private Queue<ushort> mIdList = new Queue<ushort>();
        public List<ushort> mSetRecvIdList = new List<ushort>();
        private Dictionary<UInt16, Filter> mMap = new Dictionary<UInt16, Filter>();

        private Callback_Recv mCallRecv;

        private bool RecvTaskSw;
    }
}
