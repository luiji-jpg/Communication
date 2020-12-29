using Communication_Server.Base.UDPManager.UseUDP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base.UDPManager.MultiUDP
{
    public partial class cMultiUDP
    {
        public cMultiUDP()
        {
        }

        ~cMultiUDP()
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
                mUdpList = new List<cUseUDP>();
                mDevList = new Dictionary<string, cUseUDP>();
                mCallSenderList = new Dictionary<string, CallSender>();

                if (cGDef.nUDPNum <= 0) IsOpen = false;
                for (loop_1 = 0; loop_1 < cGDef.nUDPNum; loop_1++)
                {
                    mUdpList.Add(new cUseUDP(cGDef.nRecvBuffLen_Total, cGDef.nRecvBuffLen_Current));

                    mUdpList[loop_1].GetRxPort = cGDef.RxPortArr_UDP[loop_1];
                    mUdpList[loop_1].GetTxPort = cGDef.TxPortArr_UDP[loop_1];
                    mUdpList[loop_1].GetSendPort = cGDef.SendPortArr_UDP[loop_1];

                    mUdpList[loop_1].GetClientPort = cGDef.TxPortArr_UDP[loop_1];

                    mUdpList[loop_1].GetClientIp = cGDef.ClientIpArr_UDP[loop_1];
                    mUdpList[loop_1].GetMulticaseIp_1 = cGDef.Multicast_NameArr_UDP[loop_1];
                    mUdpList[loop_1].GetIsMulticast = cGDef.IsMulticastArr_UDP[loop_1];
                    IsOpen = mUdpList[loop_1].FnSetSocket(out ErrCode);

                    if (!(IsOpen)) ErrNum = loop_1 + 1;

                    //if (!IsOpen) return IsOpen;

                    mDevList.Add(cGDef.DevNameArr_UDP[loop_1], mUdpList[loop_1]);
                    mUdpList[loop_1].GetDevName = cGDef.DevNameArr_UDP[loop_1];
                    mCallSenderList.Add(cGDef.DevNameArr_UDP[loop_1], mUdpList[loop_1].FnSend);

                    ErrCodeList.Add(ErrCode);
                    /*
                    if (!mUdpList[loop_1].GetIsOpen)
                    {
                        IsOpen = false;
                        //ErrNum = loop_1;

                        return IsOpen;
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                string Class = "cMultiUDP";
                string Method = "FnInit";
                string Line = Regex.Replace((ex.StackTrace).Split(':')[(ex.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(ex.Message);

                //ErrCode = ex.Message;

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
                if (mUdpList == null) return;
                if (mUdpList.Count <= 0) IsOpen = false;
                foreach (cUseUDP item in mUdpList)
                {
                    if (!(item.GetIsOpen)) IsOpen = false;
                    item.FnDespose();
                    if (item.GetIsOpen) IsOpen = false;
                }
                if (mUdpList.Count > 0) mUdpList.Clear();
                mUdpList = null;

                if (IsOpen) ErrCode = "Success";
                else ErrCode = "Fail";
            }
            catch (Exception ex)
            {
                string Class = "cMultiUDP";
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
