using Communication_Server.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Communication_Server.Base.TCPManager.TCP
{
    public partial class cTCP
    {
        public cTCP() { }
        ~cTCP() { }
        public void FnDataSet()
        {
            try
            {
                mTotalRecvBuff = new byte[mRecvBuffLen_Total];
                mCurrRecvBuff = new byte[mRecvBuffLen_Current];
                mRawTCP = new ConcurrentQueue<KeyValuePair<string, byte[]>>();
                mManualEvent = new ManualResetEvent(false);
                mStateObj_Server = new StateObject(mRecvBuffLen_Current);

                connDone = new ManualResetEvent(false);
                
            }
            catch (Exception ex)
            {
                mIsOpen = false;

                string Class = "cTCP";
                string Method = "FnDataSet";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }
        }

        public bool FnSocket(out string OutMsg)
        {
            ChkSetSocket = false;
            mIsOpen = false;

            IPAddress SrcIp = null;
            IPAddress TrgIp = null;

            IAsyncResult Connect = null;

            int ChkMethodUnit = 0;

            string ErrMsg = "";

            try
            {
                
                ChkMethodUnit = (int)enChkFnSocket.ChkPing;

                Ping pingTest = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string pingData = "Ping Test";
                byte[] bufferPingData = ASCIIEncoding.ASCII.GetBytes(pingData);
                int pingTimeOut = 120;

                PingReply pingReply = pingTest.Send(IPAddress.Parse(mSendIp), pingTimeOut, bufferPingData, pingOption);

                if (pingReply.Status != IPStatus.Success)
                {
                    ErrMsg = "핑 테스트 실패 윈도우 통신 설정 확인!";
                    return ChkSetSocket;
                }

                pingReply = pingTest.Send(IPAddress.Parse(cGDef.IPSrc_TCP), pingTimeOut, bufferPingData, pingOption);

                if (pingReply.Status != IPStatus.Success)
                {
                    ErrMsg = "노트북 PC 통신 연결 영역 IP 확인!";
                    return ChkSetSocket;
                }

                
                mRecv = EvnAsyncRecv;
                mSend = EvnAsyncSend;
                mAccept = EvnAsyncAccept;
                mConn = EvnAsyncConn;

                ChkMethodUnit = (int)enChkFnSocket.ChkSrcIp;

                SrcIp = IPAddress.Parse(cGDef.IPSrc_TCP);
                TrgIp = IPAddress.Parse(mSendIp);

                ChkMethodUnit = (int)enChkFnSocket.ChkSetSocket;

                mServerEndPoint = new IPEndPoint(SrcIp, mRxPort);
                mClientEndPoint = new IPEndPoint(SrcIp, mTxPort);
                mSendEndPoint   = new IPEndPoint(TrgIp, mSendPort);

                mServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                mServer.Bind(mServerEndPoint);

                mServer.Listen(1);

                mServer.BeginAccept(mAccept, mServer);

                Connect = mClient.BeginConnect(mSendEndPoint, mConn, mClient);
                connDone.WaitOne();

                if (!(ChkSetSocket))
                {
                    ErrMsg = "연결 실패 IP & Port 확인요청!";
                }
                else
                {
                    ErrMsg = "Success";
                    mIsOpen = true;
                }
                
            }
            catch (Exception ex)
            {
                mIsOpen = false;

                string Class = "cTCP";
                string Method = "FnInit";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                switch (ChkMethodUnit)
                {
                    case (int)enChkFnSocket.ChkPing: ErrMsg = "핑 테스트 실패 윈도우 통신 설정 확인!"; break;
                    case (int)enChkFnSocket.ChkSrcIp: ErrMsg = "클라이언트, 서버 IP 확인!"; break;
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
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ErrMsg);

            }
            finally
            {
                OutMsg = ErrMsg;
            }

            return ChkSetSocket;
        }

        public bool FnDespose()
        {
            bool chkFn = false;

            try
            {
                mIsOpen = false;

                if (mServer == null) return false;
                if (mClient == null) return false;

                mServer.Dispose();
                mClient.Dispose();

                mServer.Close();
                mClient.Close();

                mServerEndPoint = null;
                mClientEndPoint = null;
                mSendEndPoint = null;

                mServer = null;
                mClient = null;
                mRecv = null;

                chkFn = true;

                cGDef.objTimeManager.Delay(100);
            }
            catch (Exception ex)
            {
                string Class = "cTCP";
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
            try
            {
                if (!(mIsOpen)) return;

                mSendSocket = mClient;

                mSendSocket.BeginSend(data, 0, data.Length, SocketFlags.None, mSend, mSendSocket);
            }
            catch (Exception ex)
            {
                string Class = "cTCP";
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
