
using Communication_Server.Base.TCPManager.UseTCP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base.TCPManager.MultiTCP
{
    public partial class cMultiTCP
    {
        public cMultiTCP()
        {
        }

        ~cMultiTCP()
        {
        }

        public bool FnInit(ref List<string> ErrCodeList)
        {
            int loop_1 = 0;
            bool IsOpen = true;
            int ErrNum = -1;

            string ErrCode = "";

            try
            {
                mTcpList = new List<cUseTCP>();
                mDevList = new Dictionary<string, cUseTCP>();
                mCallSenderList = new Dictionary<string, CallSender>();

                if (cGDef.nTCPNum <= 0) IsOpen = false;
                for (loop_1 = 0; loop_1 < cGDef.nTCPNum; loop_1++)
                {
                    mTcpList.Add(new cUseTCP(cGDef.nRecvBuffLen_Total, cGDef.nRecvBuffLen_Current));

                    mTcpList[loop_1].GetSrcIp = cGDef.IPSrc_TCP;
                    mTcpList[loop_1].GetSendIp = cGDef.ClientIpArr_TCP[loop_1];

                    mTcpList[loop_1].GetRxPort = cGDef.RxPortArr_TCP[loop_1];
                    mTcpList[loop_1].GetTxPort = cGDef.TxPortArr_TCP[loop_1];
                    mTcpList[loop_1].GetSendPort = cGDef.SendPortArr_TCP[loop_1];

                    IsOpen = mTcpList[loop_1].FnSetSocket(out ErrCode);
                    if(!(IsOpen)) ErrNum = loop_1 + 1;

                    //if (!IsOpen) return IsOpen;

                    mDevList.Add(cGDef.DevNameArr_TCP[loop_1], mTcpList[loop_1]);
                    mTcpList[loop_1].GetDevName = cGDef.DevNameArr_TCP[loop_1];
                    mCallSenderList.Add(cGDef.DevNameArr_TCP[loop_1], mTcpList[loop_1].FnSend);
                    ErrCodeList.Add(ErrCode);

                    /*
                    if (!mTcpList[loop_1].GetIsOpen)
                    {
                        IsOpen = false;
                        ErrNum = loop_1;
                    }
                    */
                }
                
                
            }
            catch (Exception ex)
            {
                string Class = "cMultiTCP";
                string Method = "FnInit";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                ErrCode = ex.Message;

                IsOpen = false;
            }

            if (ErrNum != 0) IsOpen = false;

            return IsOpen;
        }

        public void FnDespose(ref string ErrCode)
        {
            bool IsOpen = true;
            try
            {
                if (mTcpList == null) return;
                if (mTcpList.Count <= 0) IsOpen = false;
                foreach (cUseTCP item in mTcpList)
                {
                    if (!(item.GetIsOpen)) IsOpen = false;
                    item.FnDespose();
                    if (item.GetIsOpen) IsOpen = false;
                }
                if (mTcpList.Count > 0) mTcpList.Clear();
                mTcpList = null;

                if (IsOpen) ErrCode = "Success";
                else ErrCode = "Fail";
            }
            catch (Exception ex)
            {
                string Class = "cMultiTCP";
                string Method = "FnDespose";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                ErrCode = ex.Message;
            }
        }
    }
}
