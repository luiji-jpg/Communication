using Communication_Server.Base.ICD.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Communication_Server
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class cSendICD: cICD_Header
    {
        const int MAX_LENGTH = 50;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct stSMsg
        {
            public byte SOF;
            public ushort ID;
            public byte Len;
            public byte cntNum;
            public byte EOF;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LENGTH)]
            public byte[] Msg2;
        }

        public stSMsg SMsg;
        //public byte[] Msg2= new byte[10];
        // 구조체로 헤더를 보내고 메세지를 붙여서 보낼수 있는것인가
        public void SetStruct(byte _cntNum,string strMsg)
        {
            const byte _SOF = 0xAB;
            const ushort _ID = 0xCA01;
            const byte _EOF = 0xBA;
            

            try
            {
                SMsg.SOF = _SOF;
                SMsg.ID = _ID;
                SMsg.Len = (byte)this.GetSize();
                SMsg.cntNum = _cntNum;
                SMsg.EOF = _EOF;

                byte[] byte_msg = Encoding.ASCII.GetBytes(strMsg);
                int Msg_Length = byte_msg.Length;
                if (Msg_Length < 0 || Msg_Length > MAX_LENGTH) return;
                SMsg.Msg2 = new byte[MAX_LENGTH];

                //SMsg.Msg2 = new byte[Msg_Length];
                Array.Copy(byte_msg, 0, SMsg.Msg2, 0, Msg_Length);
                //SMsg.Len -=(byte)( MAX_LENGTH - Msg_Length);
            }
            catch
            {

            }
            
        }

        public void SetGraphMsg(byte _cntNum, string strMsg)
        {
            const byte _SOF = 0xAB;
            const ushort _ID = 0xCD01;
            const byte _EOF = 0xBA;

            
            try
            {
                SMsg.SOF = _SOF;
                SMsg.ID = _ID;
                SMsg.Len = (byte)this.GetSize();
                SMsg.cntNum = _cntNum;
                SMsg.EOF = _EOF;

                byte[] byte_msg = Encoding.ASCII.GetBytes(strMsg);
                int Msg_Length = byte_msg.Length;
                if (Msg_Length < 0 || Msg_Length > MAX_LENGTH) return;
                SMsg.Msg2 = new byte[MAX_LENGTH];

                //SMsg.Msg2 = new byte[Msg_Length];
                Array.Copy(byte_msg, 0, SMsg.Msg2, 0, Msg_Length);
                //SMsg.Len -=(byte)( MAX_LENGTH - Msg_Length);
            }
            catch
            {

            }

        }
        /*
        public int MsgToByte(out byte[] Out_Byte,string ID,string Message, int _length)
        {
            int Total_length = 0;
            byte[] Total = new byte[3];
            Total = Encoding.Default.GetBytes(ID + Message + Convert.ToString(_length));
            Out_Byte = Total;
            Total_length = ID.Length + Message.Length + _length;

            return Total_length;
        }
        */
    }
}
