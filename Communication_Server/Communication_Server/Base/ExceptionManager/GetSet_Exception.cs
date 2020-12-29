using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.ExceptionManager
{
    public partial class cException
    {
        public static cException GetInst { get { if (mInst == null) mInst = new cException(); return mInst; } }

        public List<string> GetErrMsgList_Class { get { return mErrMsgList_Class; } set { mErrMsgList_Class = value; } }

        public List<List<string>> GetErrMsgList { get { return mErrMsgList; } set { mErrMsgList = value; } }
        public List<string> GetErrMsgList_Method { get { return mErrMsgList_Method; } set { mErrMsgList_Class = value; } }
        public List<string> GetErrMsgList_Line { get { return mErrMsgList_Line; } set { mErrMsgList_Class = value; } }
        public List<string> GetErrMsgList_Msg { get { return mErrMsgList_Msg; } set { mErrMsgList_Msg = value; } }
    }
}
