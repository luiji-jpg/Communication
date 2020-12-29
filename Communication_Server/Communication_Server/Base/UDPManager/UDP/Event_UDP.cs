using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace Communication_Server.Base.UDPManager.UDP
{
    public partial class cUDP
    {
        private void EvnAsyncSend(IAsyncResult ar)
        {
            Socket sendSocket = null;
            bool PortChk = false;

            try
            {
                PortChk = (mRxPort != mTxPort);
                mCurrSendBuff = (byte[])ar.AsyncState;

                if (PortChk)
                {
                    sendSocket = mClient;
                    sendSocket.EndSend(ar);
                }
                else
                {
                    sendSocket = mServer;
                    sendSocket.EndSendTo(ar);
                }


            }
            catch (Exception ex)
            {
                string Class = "cUDP";
                string Method = "EvnAsyncSend";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }
        }




        private void EvnAsyncRecv(IAsyncResult result)
        {
            int RecvCnt = 0;


            try
            {
                if (!(mIsOpen)) return;

                byte[] CurrBuff = (byte[])result.AsyncState;
                RecvCnt = mServer.EndReceiveFrom(result, ref mEndPoint);

                if (RecvCnt <= 0)
                {
                    if (mRecv == null) return;
                    mServer.BeginReceiveFrom(CurrBuff, 0, mCurrRecvBuff.Length, SocketFlags.None, ref mEndPoint, mRecv, mCurrRecvBuff);
                    return;
                }

                byte[] Data = new byte[RecvCnt];
                Array.Copy(CurrBuff, 0, Data, 0, RecvCnt);
                mRawUDP.Enqueue(new KeyValuePair<string, byte[]>(mDevName, Data));
                mManualEvent.Set();

                if (mRecv == null) return;
                mServer.BeginReceiveFrom(CurrBuff, 0, mCurrRecvBuff.Length, SocketFlags.None, ref mEndPoint, mRecv, mCurrRecvBuff);
            }
            catch (Exception ex)
            {
                string Class = "cUDP";
                string Method = "EvnAsyncRecv";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                if (mRecv == null) return;
                mServer.BeginReceiveFrom(mCurrRecvBuff, 0, mCurrRecvBuff.Length, SocketFlags.None, ref mEndPoint, mRecv, mCurrRecvBuff);
            }
        }

    }
}
