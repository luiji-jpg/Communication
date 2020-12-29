using Communication_Server.Base.ExceptionManager;
using Communication_Server.Base.ICD.Header;
using Communication_Server.Base.MessageManager;
using Communication_Server.Base.TCPManager.MultiTCP;
using Communication_Server.Base.UDPManager.MultiUDP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base
{
    public class cGDef
    {
        #region Form Setting
        public static int Layout_X;
        public static int Layout_Y;

        public static double mRes_X;
        public static double mRes_Y;

        public static bool IsFormExit = false;

        #endregion

        #region Object Package
        public static cException objExcHandler = cException.GetInst;
        public static cTimeManager objTimeManager = cTimeManager.GetInst;
        public static cMultiUDP objMultiUDP = cMultiUDP.GetInst;
        public static cMultiTCP objMultiTCP = cMultiTCP.GetInst;
        public static cMsgHandler objMsgHandler = cMsgHandler.GetInst;
        public static cControlManager objControlManager = cControlManager.GetInst;
        public static cBuildManager objBuildManager = cBuildManager.GetInst;
        public static cFileManager objFileManager = cFileManager.GetInst;
        #endregion

        #region ICD Info
        public delegate cICD_Header CallConvertBytesToMsg(byte[] buf, int bufLen);
        public static Dictionary<string, CallConvertBytesToMsg> callConvertBytesToMsgList;
        #endregion

        #region Packet Info
        public const int MtuSize = 9 * 1024;
        public const int MaxSize = 100*1024 * 1024;

        #endregion

        #region Communication
        

        public static string[] RxPortList;
        public static string[] TxPortList;
        public static string[] SendPortList;
        public static string[] SendIPList;

        public static int PortCnt;
        public static int nRecvBuffLen_Current;
        public static int nRecvBuffLen_Total;
        #endregion

        #region UDP Set

        public const int nUDPNum = 3;

        public static int[] RxPortArr_UDP;
        public static int[] TxPortArr_UDP;
        public static int[] SendPortArr_UDP;

        public static string[] ClientIpArr_UDP;
        public static string[] DevNameArr_UDP;

        public static bool IsRecvMsg_UDP;
        public static uint CurrRecvMsgLen_UDP;

        public static string IPSrc_UDP;

        public static bool IsGetLen_UDP;

        public static bool IsSaveChange_UDP;
        public static bool IsUdpChange_UDP;

        public static bool[] IsMulticastArr_UDP;
        public static string[] Multicast_NameArr_UDP;


        #endregion

        #region TCP Set
        public const int nTCPNum = 3;

        public static int[] RxPortArr_TCP;
        public static int[] TxPortArr_TCP;
        public static int[] SendPortArr_TCP;

        public static string[] ClientIpArr_TCP;
        public static string[] DevNameArr_TCP;

        public static bool IsRecvMsg_TCP;
        public static uint CurrRecvMsgLen_TCP;

        public static string IPSrc_TCP;

        public static bool IsGetLen_TCP;

        public static bool IsSaveChange_TCP;
        public static bool IsTcpChange_TCP;

        public static bool[] IsMulticastArr_TCP;
        public static string[] Multicast_1Arr_TCP;
        
        #endregion

        #region ini File
        public const string IniPath_Directory = "\\PreSet_TCPServer";

        public const string IniPath_Setting = IniPath_Directory + "\\Setting.ini";
        #endregion

        #region Message List
        public delegate void CallAddMsg(cICD_Header _msg, string _headInfo);
        #endregion

        #region Other
        public static int DebugMode_RecvDelay;
        #endregion 
    }
}
