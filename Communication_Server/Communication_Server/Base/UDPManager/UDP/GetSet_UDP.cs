using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Communication_Server.Base.UDPManager.UDP
{
    public partial class cUDP
    {
        public static cUDP GetInst { get { if (mInst == null) mInst = new cUDP(); return mInst; } }
        public bool GetIsOpen { get { return mIsOpen; } }
        public int GetRxPort { get { return mRxPort; } set { mRxPort = value; } }
        public int GetTxPort { get { return mTxPort; } set { mTxPort = value; } }
        public int GetSendPort { get { return mSendPort; } set { mSendPort = value; } }
        public string GetClientIp { get { return mClientIp; } set { mClientIp = value; } }
        public int GetClientPort { get { return mClientPort; } set { mClientPort = value; } }
        public string GetIp { get { return mIp; } set { mIp = value; } }
        public string GetDevName { get { return mDevName; } set { mDevName = value; } }

        public Socket GetClient { get { return mClient; } }
        public Socket GetServer { get { return mServer; } }

        public bool GetIsMulticast { get { return mIsMulticast; } set { mIsMulticast = value; } }

        public string GetMulticaseIp_1 { get { return mMulticaseIp_1; } set { mMulticaseIp_1 = value; } }


    }
}
