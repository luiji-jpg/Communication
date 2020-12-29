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
        protected string mIp;
        protected string mDevName;
        protected byte[] mTotalRecvBuff;

        protected int mRecvOffset;
        protected int mRecvBuffLen_Total;
        protected int mRecvBuffLen_Current;

        protected ConcurrentQueue<KeyValuePair<string, byte[]>> mRawUDP;

        private static cUDP mInst;
        private int mRxPort;
        private int mTxPort;


        protected string mClientIp;
        protected int mClientPort;
        

        private Socket mClient;
        private Socket mServer;

        private EndPoint mEndPoint;
        private EndPoint mClientEndPoint;

        private AsyncCallback mRecv;
        private AsyncCallback mSend;

        private byte[] mCurrSendBuff;

        private byte[] mCurrRecvBuff;

        private bool mIsOpen;

        private string mSendAddress;
        private int mSendPort;
        byte[] mSendData;

        private bool mIsMulticast;
        private string mMulticaseIp_1;

        private MulticastOption MultiOption;

        private static ManualResetEvent mManualEvent;


        
    }
}
