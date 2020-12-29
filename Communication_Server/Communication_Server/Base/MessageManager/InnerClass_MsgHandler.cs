using Communication_Server.Base.ICD.Header;
using System;
using System.Collections.Generic;
using System.Text;
using static Communication_Server.Base.MessageManager.cMsgHandler;

namespace Communication_Server.Base.MessageManager
{
    public class Filter
    {
        public int timeOut = 0;
        public Callback_Proc onProc = null;
    }
}
