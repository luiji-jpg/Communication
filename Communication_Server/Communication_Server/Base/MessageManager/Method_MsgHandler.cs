using Communication_Server.Base.ICD.Header;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication_Server.Base.MessageManager
{
    public partial class cMsgHandler
    {
        public cMsgHandler()
        {
            FnInit();
        }

        public void FnInit()
        {
            mCallRecv += FnRecvMsg;
            Task.Factory.StartNew(new Action(FnRecvMsgCtrl));
        }

        private void FnDespose()
        {
        }

        public void FnRecvMsgCtrl()
        {
            string ipInfo = "";
            cICD_Header msg = null;
            RecvTaskSw = true;
            ushort Id = 0;
            int Port = 0;


            int loop1 = 0;


            while (RecvTaskSw)
            {
                if (mMsgList_Msg.Count <= 0) continue;

                Id = 0;

                for (loop1 = 0; loop1 < mMsgList_Msg.Count; loop1++)
                {
                    msg = mMsgList_Msg.Dequeue();
                    Id = mIdList.Dequeue();
                    ipInfo = mIpList.Dequeue();
                    Port = mPortList.Dequeue();

                    foreach (Callback_Proc proc in mAddRecvList)
                    {
                        proc?.Invoke(ipInfo, Port, msg);

                    }

                    if (Id > 0)
                    {
                        foreach (ushort item in mSetRecvIdList)
                        {
                            if (item == Id)
                            {
                                mMap[Id].timeOut = 1;
                                mMap[Id].onProc?.Invoke(ipInfo, Port, msg);
                            }
                        }
                    }

                    Application.DoEvents();
                    Task.Delay(120);
                }
            }
        }

        private void FnRecvMsg(string ClientIpAddr, int ClientPort,ushort msgId, cICD_Header msg)
        {
            mIpList.Enqueue(ClientIpAddr);
            mPortList.Enqueue(ClientPort);
            mIdList.Enqueue(msgId);
            mMsgList_Msg.Enqueue(msg);
        }

        public void AddReciever(Callback_Proc _proc)
        {
            mAddRecvList.Add(_proc);
        }

        public void SetReciever(UInt16 _id, Callback_Proc _proc, int _timeout = 0)
        {
            bool IsRepeat = false;

            Filter filter = new Filter();
            filter.timeOut = _timeout;
            filter.onProc = _proc;

            if (mMap.Count > 0)
            {
                foreach (ushort item in mSetRecvIdList)
                {
                    if (item == _id)
                    {
                        mMap[_id] = filter;
                        IsRepeat = true;
                    }
                }

                if (!IsRepeat)
                {
                    mMap.Add(_id, filter);
                    mSetRecvIdList.Add(_id);
                }
            }
            else
            {
                mMap.Add(_id, filter);
                mSetRecvIdList.Add(_id);
            }
        }
    }
}
