using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Communication_Server.Base.TCPManager.TCP
{
    public partial class cTCP
    {
        public static cTCP GetInst { get { if (mInst == null) mInst = new cTCP(); return mInst; } }
        public bool GetIsOpen { get { return mIsOpen; } }
        public string GetSrcIp { get { return mIp; } set { mIp = value; } }
        public string GetSendIp { get { return mSendIp; } set { mSendIp = value; } }
        public int GetRxPort { get { return mRxPort; } set { mRxPort = value; } }
        public int GetTxPort { get { return mTxPort; } set { mTxPort = value; } }
        public int GetSendPort { get { return mSendPort; } set { mSendPort = value; } }
        
        public string GetDevName { get { return mDevName; } set { mDevName = value; } }
        public Socket GetClient { get { return mClient; } }
        public Socket GetServer { get { return mServer; } }
    }
}
