using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.ExceptionManager
{
    public partial class cException
    {
        private static cException mInst;

        private List<List<string>> mErrMsgList;

        private List<string> mErrMsgList_Class;
        private List<string> mErrMsgList_Method;
        private List<string> mErrMsgList_Line;
        private List<string> mErrMsgList_Msg;
    }
}
