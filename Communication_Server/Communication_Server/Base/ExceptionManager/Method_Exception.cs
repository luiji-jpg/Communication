using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_Server.Base.ExceptionManager
{
    public partial class cException
    {
        public cException()
        {
            FnInit();
        }

        ~cException()
        {
            FnDispose();
        }

        private void FnInit()
        {
            mErrMsgList = new List<List<string>>();

            mErrMsgList_Class = new List<string>();
            mErrMsgList_Method = new List<string>();
            mErrMsgList_Line = new List<string>();
            mErrMsgList_Msg = new List<string>();

            mErrMsgList.Add(mErrMsgList_Class);
            mErrMsgList.Add(mErrMsgList_Method);
            mErrMsgList.Add(mErrMsgList_Line);
            mErrMsgList.Add(mErrMsgList_Msg);
        }

        private void FnDispose()
        {
            if (mErrMsgList != null)
            {
                if (mErrMsgList.Count > 0)
                {
                    foreach (List<string> item in mErrMsgList)
                    {
                        if (item != null)
                        {
                            if (item.Count > 0)
                            {
                                item.Clear();
                            }
                        }
                    }

                    for (int loop = 0; loop < mErrMsgList.Count; loop++)
                    {
                        mErrMsgList[loop] = null;
                    }

                    mErrMsgList.Clear();

                    mErrMsgList = null;
                }
            }
        }
    }
}
