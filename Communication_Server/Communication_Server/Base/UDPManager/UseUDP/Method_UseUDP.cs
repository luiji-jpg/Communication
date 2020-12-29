using Communication_Server.Base.UDPManager.UDP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Communication_Server.Base.UDPManager.UseUDP
{
    public partial class cUseUDP : cUDP
    {
        public cUseUDP(int nRecvBuffLen_Total, int nRecvBuffLen_Current)
        {
            mRecvBuffLen_Total = nRecvBuffLen_Total;
            mRecvBuffLen_Current = nRecvBuffLen_Current;

            FnDataSet();
            FnSetRawToMsg();
        }

        public void FnSetRawToMsg()
        {
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 10;
            mTimer.Tick += EvnRawToMsg;
            mTimer.Tag = false;
        }

        public bool FnSetSocket(out string ErrMsg)
        {
            bool ChkFn = false;
            bool UseTimer = false;
            bool ChkSocket = false;

            UseTimer = (bool)mTimer.Tag;

            ErrMsg = "UDP 연결을 중복실행할 수 없습니다.";

            if (UseTimer) return ChkFn;

            ChkSocket = FnSocket(out ErrMsg);
            
            if (!(ChkSocket)) return ChkFn;
            mTimer.Tag = true;
            mTimer.Start();
            ChkFn = true;

            return ChkFn;
        }

        public bool FnDesposeSocket()
        {
            bool ChkFn = false;
            bool UseTimer = false;
            bool IsChkSocket = false;

            UseTimer = (bool)mTimer.Tag;

            if (!(UseTimer)) return ChkFn;

            mTimer.Stop();

            IsChkSocket = FnDespose();

            if (!(IsChkSocket)) return ChkFn;

            mTimer.Tag = false;

            ChkFn = true;
            return ChkFn;
        }

        public void FnSend(string address, int port, byte[] data)
        {
            FnSendTo(address, port, data);
        }
    }
}

