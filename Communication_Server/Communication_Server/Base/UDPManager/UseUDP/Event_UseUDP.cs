using Communication_Server.Base.ICD.Header;
using Communication_Server.Base.UDPManager.UDP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Communication_Server.Base.UDPManager.UseUDP
{
    public partial class cUseUDP:cUDP
    {
        private void EvnRawToMsg(object sender, EventArgs e)
        {
            KeyValuePair<string, byte[]> item = new KeyValuePair<string, byte[]>();

            while (mRawUDP.TryDequeue(out item))
            {
                int RecvCnt = 0;
                cICD_Header msg = null;
                byte[] data = item.Value;

                RecvCnt = data.Length;
                Array.Copy(data, 0, mTotalRecvBuff, mRecvOffset, RecvCnt);
                mRecvOffset += data.Length;

                msg = cGDef.callConvertBytesToMsgList[mDevName].Invoke(mTotalRecvBuff, mRecvOffset);

                if (cGDef.IsSaveChange_UDP || cGDef.IsUdpChange_UDP)
                {
                    mRecvOffset = 0;
                    cGDef.IsRecvMsg_UDP = false;
                }

                if (msg != null)
                {

                    byte[] DataSerialize = msg.Serialize();

                    UInt16 MsgId = (UInt16)(DataSerialize[1] << 8);
                    MsgId |= (UInt16)DataSerialize[0];

                    cGDef.objMsgHandler.GetCallRecv.Invoke(mClientIp, mClientPort, MsgId, msg);

                    cGDef.IsRecvMsg_UDP = false;
                    mRecvOffset = 0;

                }
            }
        }
    }
}
