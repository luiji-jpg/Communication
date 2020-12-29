using Communication_Server.Base.UDPManager;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base.ICD.Header
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public partial class cICD_Header
    {
        public cICD_Header() { }

        public cICD_Header Copy()
        {
            byte[] copyMsg = null;

            Type type = null;

            cICD_Header ReturnData = null;
            cICD_Header copiedMsg = null;

            try
            {
                copyMsg = Serialize();
                type = GetType();
                copiedMsg = (cICD_Header)Activator.CreateInstance(type);
                copiedMsg.Deserialize(ref copyMsg);

                if (ReturnData != null) copiedMsg = ReturnData;
            }
            catch (Exception e)
            {
                string Class = "cICD_Header";
                string Method = "Copy";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(e.Message);
            }

            return copiedMsg;
        }

        public byte[] Serialize()
        {
            byte[] buffer = null;

            GCHandle gch;

            IntPtr pBuffer;

            try
            {
                buffer = new byte[Marshal.SizeOf(this)];
                gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                pBuffer = gch.AddrOfPinnedObject();
                Marshal.StructureToPtr(this, pBuffer, false);
                gch.Free();
            }
            catch (Exception e)
            {
                string Class = "cICD_Header";
                string Method = "Serialize";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(e.Message);
            }

            return buffer;
        }

        public void Deserialize(ref byte[] data)
        {
            GCHandle gch;

            try
            {
                gch = GCHandle.Alloc(data, GCHandleType.Pinned);
                Marshal.PtrToStructure(gch.AddrOfPinnedObject(), this);
                gch.Free();
            }
            catch (Exception e)
            {
                string Class = "cICD_Header";
                string Method = "Deserialize";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(e.Message);

            }
        }

        public int GetSize()
        {
            int result = 0;

            try
            {
                result = Marshal.SizeOf(this);
            }
            catch (Exception e)
            {
                string Class = "cICD_Header";
                string Method = "GetSize";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(e.Message);
            }

            return result;
        }

        public static int HeaderSize()
        {
            int result = 0;

            try
            {
                result = Marshal.SizeOf(typeof(cICD_Header));
            }
            catch (Exception e)
            {
                string Class = "cICD_Header";
                string Method = "HeaderSize";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);
                cGDef.objExcHandler.GetErrMsgList_Msg.Add(e.Message);
            }

            return result;
        }
    }
}
