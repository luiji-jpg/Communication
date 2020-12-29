using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Communication_Server.Base.UDPManager.UDP
{
    public partial class cUDP
    {
        public cUDP(){}

        ~cUDP(){}

        public void FnDataSet()
        {
            mTotalRecvBuff = new byte[mRecvBuffLen_Total];
            mCurrRecvBuff = new byte[mRecvBuffLen_Current];
            mRawUDP = new ConcurrentQueue<KeyValuePair<string, byte[]>>();
            mManualEvent = new ManualResetEvent(false);
        }

        enum enChkFnSocket
        {
            ChkPing = 1,
            ChkSrcIp,
            ChkSetSocket,
            ChkBind,
            ChkSetSockOption,
            ChkSetClient,
            ChkSetReceive,
        }

        public bool FnSocket(out string OutMsg)
        {
            bool ChkFn = false;

            int ChkMethodUnit = 0;

            string ErrMsg = "";

            mIsOpen = true;

            IPAddress SrcIp = null;

            try
            {
                Ping pingTest = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string pingData = "Ping Test";
                byte[] bufferPingData = ASCIIEncoding.ASCII.GetBytes(pingData);
                int pingTimeOut = 120;

                PingReply pingReply = pingTest.Send(IPAddress.Parse(mClientIp), pingTimeOut, bufferPingData, pingOption);
                
                if(pingReply.Status != IPStatus.Success)
                {
                    ErrMsg = "핑 테스트 실패 윈도우 통신 설정 확인!";
                    return ChkFn;
                }

                pingReply = pingTest.Send(IPAddress.Parse(cGDef.IPSrc_UDP), pingTimeOut, bufferPingData, pingOption);

                if (pingReply.Status != IPStatus.Success)
                {
                    ErrMsg = "노트북 PC 통신 연결 영역 IP 확인!";
                    return ChkFn;
                }

                ChkMethodUnit = (int)enChkFnSocket.ChkSrcIp;
                SrcIp = IPAddress.Parse(cGDef.IPSrc_UDP);

                mRecv = EvnAsyncRecv;
                mSend = EvnAsyncSend;

                ChkMethodUnit = (int)enChkFnSocket.ChkSetSocket;

                mServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                mClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                mEndPoint = new IPEndPoint(SrcIp, mRxPort);

                ChkMethodUnit = (int)enChkFnSocket.ChkBind;

                mServer.Bind(mEndPoint);

                ChkMethodUnit = (int)enChkFnSocket.ChkSetSockOption;

                if (mIsMulticast)
                {
                    IPAddress MultiIp = IPAddress.Parse(mMulticaseIp_1);
                    mServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(MultiIp, SrcIp));
                }
                else
                {
                    mServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                }

                ChkMethodUnit = (int)enChkFnSocket.ChkSetClient;

                if (mRxPort != mTxPort)
                {
                    mClientEndPoint = new IPEndPoint(IPAddress.Any, mTxPort);
                    mClient.Bind(mClientEndPoint);
                }

                ChkMethodUnit = (int)enChkFnSocket.ChkSetReceive;

                if (mIsMulticast)
                {
                    EndPoint ep = new IPEndPoint(SrcIp, 0);
                    mServer.BeginReceiveFrom(mCurrRecvBuff, 0, mCurrRecvBuff.Length, SocketFlags.None, ref ep, mRecv, mCurrRecvBuff);
                }
                else
                {
                    mServer.BeginReceiveFrom(mCurrRecvBuff, 0, mCurrRecvBuff.Length, SocketFlags.None, ref mEndPoint, mRecv, mCurrRecvBuff);
                }

                ChkFn = true;

                ErrMsg = "Success";
            }
            catch (Exception ex)
            {
                mIsOpen = false;

                string Class = "cUDP";
                string Method = "FnInit";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();
                

                switch (ChkMethodUnit)
                {
                    case (int)enChkFnSocket.ChkPing: ErrMsg = "핑 테스트 실패 윈도우 통신 설정 확인!"; break;
                    case (int)enChkFnSocket.ChkSrcIp: ErrMsg = "서버 IP 확인!"; break;
                    case (int)enChkFnSocket.ChkSetSocket: ErrMsg = "수신 포트 확인!"; break;
                    default: ErrMsg = "통신 케이블 확인"; break;
                        //case (int)enChkFnSocket.ChkBind: ErrMsg = "서버 소켓 바인딩 문제(4)"; break;
                        //case (int)enChkFnSocket.ChkSetSockOption: ErrMsg = "서버 소켓 옵션 설정 문제(5)"; break;
                        //case (int)enChkFnSocket.ChkSetClient: ErrMsg = "송신포트 바인딩 문제(6)"; break;
                        //case (int)enChkFnSocket.ChkSetReceive: ErrMsg = "서버 수신 셋팅 문제(7)"; break;
                }
                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }
            finally
            {
                OutMsg = ErrMsg;
            }

            return ChkFn;
        }

        public bool FnDespose()
        {
            bool chkFn = false;

            try
            {
                mIsOpen = false;

                if (mServer == null) return false;
                if (mClient == null) return false;

                mServer.Close();
                mClient.Close();

                mEndPoint = null;
                mServer = null;
                mClient = null;
                mRecv = null;

                chkFn = true;
            }
            catch (Exception ex)
            {
                string Class = "cUDP";
                string Method = "FnDespose";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }

            return chkFn;
        }

        public void FnSendTo(string address, int port, byte[] data)
        {
            Socket sendSocket = null;
            EndPoint sendEndPoint = null;

            try
            {
                if (!(mIsOpen)) return;

                mSendAddress = address;
                mSendData = data;

                if (mRxPort != mTxPort)
                {
                    sendSocket = mClient;
                    sendSocket.Connect(IPAddress.Parse(address), mSendPort);
                    sendSocket.BeginSend(data, 0, data.Length, SocketFlags.None, mSend, mCurrSendBuff);
                }
                else
                {
                    sendSocket = mServer;
                    sendEndPoint = new IPEndPoint(IPAddress.Parse(address), mSendPort);
                    sendSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, sendEndPoint, mSend, mCurrSendBuff);
                }
            }
            catch (Exception ex)
            {
                string Class = "cUDP";
                string Method = "SendTo";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }
        }
    }
}
