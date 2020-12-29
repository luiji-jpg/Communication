using Communication_Server.Base;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base.TCPManager.TCP
{
    public partial class cTCP
    {
        

        private void EvnAsyncConn(IAsyncResult ar)
        {
            Socket client = null;
            try
            {
                client = (Socket)ar.AsyncState;

                client.EndConnect(ar);
                connDone.Set();

                ChkSetSocket = true;
            }
            catch (Exception ex)
            {
                string Class = "cTCP";
                string Method = "EvnAsyncConn";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                connDone.Set();
                ChkSetSocket = false;
            }
        }

        private void EvnAsyncAccept(IAsyncResult ar)
        {
            try
            {
                mAcceptedClient = (ar.AsyncState as Socket).EndAccept(ar);
                mStateObj_Server.WorkingSock = mAcceptedClient;
                mAcceptedClient.BeginReceiveFrom(mStateObj_Server.Buffer, 0, mRecvBuffLen_Current, SocketFlags.None, ref mServerEndPoint, mRecv, mStateObj_Server);
            }
            catch (Exception ex)
            {

                string Class = "cTCP";
                string Method = "EvnAsyncAccept";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);
            }
        }

        private void EvnAsyncSend(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                
                mSendSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                string Class = "cTCP";
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

            StateObject state = null;

            try
            {
                state = (StateObject)result.AsyncState;
                RecvCnt = state.WorkingSock.EndReceive(result);

                if (RecvCnt <= 0)
                {
                    if (mAcceptedClient == null) return;
                    mAcceptedClient.BeginReceiveFrom(state.Buffer, 0, mRecvBuffLen_Current, SocketFlags.None, ref mServerEndPoint, mRecv, state);
                    return;
                }
                byte[] Data = new byte[RecvCnt];
                Array.Copy(state.Buffer, 0, Data, 0, RecvCnt);
                mRawTCP.Enqueue(new KeyValuePair<string, byte[]>(mDevName, Data));
                mManualEvent.Set();

                if (mRecv == null) return;
                mAcceptedClient.BeginReceiveFrom(state.Buffer, 0, mRecvBuffLen_Current, SocketFlags.None, ref mServerEndPoint, mRecv, state);
            }
            catch (Exception ex)
            {
                string Class = "cTCP";
                string Method = "EvnAsyncRecv";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                if (state != null && mServerEndPoint != null && mRecv != null)
                {
                    if (mRecv == null) return;
                    mAcceptedClient.BeginReceiveFrom(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, ref mServerEndPoint, mRecv, state);
                }
            }
        }
    }
}
