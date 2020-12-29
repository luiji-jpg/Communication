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
    public class RecvICD : cICD_Header
    {
        const int MAX_LENGTH = 50;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct stRMsg
        {
            public byte SOF;
            public ushort ID;
            public byte Len;
            public byte cntNum;
            public byte EOF;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LENGTH)]
            public byte[] Msg2;
        }

        public stRMsg RMsg;
        public bool Well_Recv()
        {
            const byte _SOF = 0xEF;
            const ushort _ID = 0xAC01;
            const ushort Get_Back = 0xDC01;
            const byte _EOF = 0xFE;
            bool Chk = false;

            try
            {
                if (RMsg.SOF != _SOF || RMsg.EOF != _EOF) return Chk;
                

                
                if (RMsg.Len < 0 || RMsg.Msg2.Length < 0 || RMsg.Msg2.Length > MAX_LENGTH) return Chk;
                switch (RMsg.ID)
                {
                    case _ID:
                        Chk = true;
                        break;
                    case Get_Back:
                        Chk = true;
                        break;
                    default:
                        break;
                }
                return Chk;
            }
            catch
            {
                return Chk;
            }
        }

        
    }
}

