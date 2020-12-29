using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Communication_Server.Base.TCPManager.TCP
{
    public partial class cTCP
    {
        protected string mIp;
        protected string mDevName;
        protected byte[] mTotalRecvBuff;

        protected int mRecvOffset;
        protected int mRecvBuffLen_Total;
        protected int mRecvBuffLen_Current;

        protected ConcurrentQueue<KeyValuePair<string, byte[]>> mRawTCP;

        private static cTCP mInst;
        private int mRxPort;
        private int mTxPort;



        protected string mSendIp;
        protected int mSendPort;


        private Socket mClient;
        private Socket mServer;
        private Socket mAcceptedClient;

        private EndPoint mServerEndPoint;
        private EndPoint mClientEndPoint;
        private EndPoint mSendEndPoint;


        private AsyncCallback mRecv;
        private AsyncCallback mSend;
        private AsyncCallback mAccept;
        private AsyncCallback mConn;

        private Socket mSendSocket;
        private StateObject mStateObj_Server;

        private byte[] mCurrSendBuff;

        private byte[] mCurrRecvBuff;

        private bool mIsOpen;

        private static ManualResetEvent connDone;

        private static ManualResetEvent mManualEvent;

        private bool ChkSetSocket;
    }
}
