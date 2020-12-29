using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Communication_Server.Preset;
using Communication_Server.Base;

namespace Communication_Server
{
    public partial class cServer : Form
    {
        //delegate void TimerEventFiredDelegate();
        //System.Timers.timer1 timer1 = new System.Timers.timer1();
        public cServer()
        {
            InitializeComponent();

        }
        #region Init
        private void fnInitData()
        {
            dlg = new DlgExampleGraph();

            recvStr = new byte[100];
            transferStr = new byte[100];

            btnSend.BackColor = Color.Lavender;
            btnSend.Enabled = false;
            btnConnect.BackColor = Color.Lavender;
            btnDisconnect.Enabled = false;
            btnDisconnect.BackColor = Color.Lavender;
            btnSaveData.Enabled = true;

            lvRcvMssg.Columns.Add("IP", 80, HorizontalAlignment.Left);
            lvRcvMssg.Columns.Add("Port", 80, HorizontalAlignment.Left);
            lvRcvMssg.Columns.Add("Message", 150, HorizontalAlignment.Left);

            lvSndMsg.Columns.Add("IP", 80, HorizontalAlignment.Left);
            lvSndMsg.Columns.Add("Port", 80, HorizontalAlignment.Left);
            lvSndMsg.Columns.Add("Message", 150, HorizontalAlignment.Left);

            lvRcvMssg.EndUpdate();
            lvRcvMssg.FullRowSelect = true;

            lvSndMsg.EndUpdate();
            lvSndMsg.FullRowSelect = true;


        }

        void fnGetIniData()
        {
            string IniStatus = "";
            List<string> IniGetData = null;
            cIni_Setting IniInfo = cIni_Setting.GetInst();

            IniInfo.Create(out IniStatus, IniInfo.DirPath, IniInfo.FilePath);

            if (IniStatus.Equals("Exists"))
            {
                IniInfo.GetData(out IniGetData, IniInfo.FilePath, IniInfo.DefaultSection, IniInfo.DefaultKey);
            }
            else if (IniStatus.Equals("Create"))
            {
                IniInfo.SetData(IniInfo.FilePath, IniInfo.DefaultSection, IniInfo.DefaultKey, IniInfo.DefaultValue);
                cGDef.objTimeManager.Delay(10);
                IniInfo.GetData(out IniGetData, IniInfo.FilePath, IniInfo.DefaultSection, IniInfo.DefaultKey);
            }

            {
                tbServerIP.Text = IniGetData[0];
                tbServerPort.Text = IniGetData[1];

                tbStartTime.Text = IniGetData[2];
                tbEndTime.Text = IniGetData[3];
            }
        }


        private bool fnSetIniData()
        {
            string[] dataValueArr = new string[4];
            cIni_Setting IniInfo = cIni_Setting.GetInst();

            bool ChkFn = false;

            try
            {
                dataValueArr[0] = tbServerIP.Text;
                dataValueArr[1] = tbServerPort.Text;

                dataValueArr[2] = tbStartTime.Text;
                dataValueArr[3] = tbEndTime.Text;

                IniInfo.DefaultValue = dataValueArr;
                IniInfo.SetData(IniInfo.FilePath, IniInfo.DefaultSection, IniInfo.DefaultKey, IniInfo.DefaultValue);

                ChkFn = true;
            }
            catch
            {
                ChkFn = false;
            }

            return ChkFn;
        }

        #endregion

        #region Connect
        public void Connect(string IP, string Port)
        {
            if (IP.Length <= 0 || Port.Length <= 0) MessageBox.Show("다시 입력해주세요.");
            else
            {



                try
                {
                    mServerIpEp = new IPEndPoint(IPAddress.Any, int.Parse(Port));

                    mServerSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                    mServerSock.Bind(mServerIpEp);

                    mServerSock.Listen(100);

                    //transferSock = mServerSock.Accept();

                    mServerSock.BeginAccept(new AsyncCallback(fnBeginAccept), mServerSock);
                    //transferSock.Connect(new IPEndPoint(IPAddress.Parse(IP), int.Parse(Port)));

                }
                catch
                {
                    //  transferSock = mServerSock.Accept();

                    MessageBox.Show("연결이 해제되었습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDisconnect.Enabled = false;
                    btnDisconnect.BackColor = Color.Gray;
                    btnConnect.Enabled = true;
                    btnConnect.BackColor = Color.Lavender;
                    // 해제되었을 경우 왜 이 아랫부분이 실행이 안되는걸까..
                }

            }
        }

        public void fnBeginAccept(IAsyncResult result)
        {
            Socket server = (Socket)result.AsyncState;
            transferSock = server.EndAccept(result);

            // client socket logic...
            //transferSock.BeginAccept(fnBeginAccept, mServerSock);

            //.BeginAccept(fnBeginAccept, server); // <- continue accepting connections


            
            transferSock.BeginReceive(recvStr, 0, recvStr.Length, SocketFlags.None, new AsyncCallback(receiveStr), transferSock);
            // Receive부분

            if (this.InvokeRequired == true)
            {
                this.Invoke(new Action(delegate ()
                {
                    btnSend.Enabled = true;
                    btnConnect.Enabled = false;
                    btnConnect.BackColor = Color.Gray;
                    btnDisconnect.Enabled = true;
                    btnDisconnect.BackColor = Color.Lavender;
                    MessageBox.Show("사용자와 연결되었습니다.", "Connect");
                }));
            }
            else
            {
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
                btnConnect.BackColor = Color.Gray;
                btnDisconnect.Enabled = true;
                btnDisconnect.BackColor = Color.Lavender;
                MessageBox.Show("사용자와 연결되었습니다.", "Connect");
            }


        }

        public void Disconnect()
        {
            mServerSock.Close();
            transferSock.Close();
        }

        /*
        public void fnBeginAccept(IAsyncResult State)
        {
            string OutMsg = "";
            OutMsg = (string)State.AsyncState;
            MessageBox.Show(OutMsg);

        }
        */
        #endregion

        #region Graph

        private void Draw_Graph()
        {
            try
            {
                timer1.Interval = 100;
                timer1.Start();

                timer2.Interval = 1000;
                timer1.Tick += new EventHandler(timer1_Tick);
                timer2.Tick += new EventHandler(timer2_Tick);
               
            }
            catch
            {

            }
        }

        void Sketch_Graph(List<int> List)
        {
            if (!IsGraphVisible)
            {
                dlg.Load_Graph(List);
                dlg.Show();
                IsGraphVisible = true;
            }
            else
            {
                dlg.Load_Graph(List);

                dlg.Show();
            } 
            dlg.Refresh();
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            
            Send_Mssg_4Graph(Value);    // 주기적으로 그래프를 그리기 위한 값을 보낸다.
            if (Value % 10 == 9) Value_sec++;
            // Value 를 클라이언트에게 수신한다
            // 수신분에 Lget_back에 값을 추가한다.

            IsStartTime = int.TryParse(tbStartTime.Text, out int StartTime);
            IsEndTime = int.TryParse(tbEndTime.Text, out int EndTime);
            if (!IsStartTime || !IsEndTime) return; // 둘중하나라도 int값이 아니라면 종료
            if (Value_sec >= StartTime)
            {
                // if(Value > arr.Length || Value> tmp.Length) return;
                // Array.Copy(tmp, Value, arr, Value, 1);
                // 예외선언 필요
                if (!isTimer2ON)
                {
                    timer2.Start();
                    isTimer2ON = true;
                }
               // dlg.Load_Graph(arr);
                dlg.Refresh();

            }
            if (Value_sec >= EndTime)
            {
                timer1.Stop();
                timer2.Stop();

                if (Compare_List_Size(Lget_back, Lgraph))
                {
                    List<int> valueList = new List<int>();
                    int start = Lgraph.Count;
                    int end = Lget_back.Count;



                    for (int i = start; i < end; i++)
                    {
                        valueList.Add((int)Lget_back[i]);
                        Lgraph.Add(Lget_back[i]);
                    }

                    Sketch_Graph(valueList);
                    valueList.Clear();
                }
                // 타이머가 종료되고 나서 작성되지 못한 그래프를 마저 그림
                Value = 0;
                Value_sec = 0;
                isTimer2ON = false;
                return;

            }

            Value++;
            //chart1.Series.Add(Value++);
            // 동작 구현
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            if (Compare_List_Size(Lget_back, Lgraph))
            {
                List<int> valueList = new List<int>();
                int start = Lgraph.Count;
                int end = Lget_back.Count;


                
                for (int i = start; i < end; i++)
                {
                    valueList.Add((int)Lget_back[i]);
                    Lgraph.Add(Lget_back[i]);
                }
                
                Sketch_Graph(valueList);
                valueList.Clear();
            }
                
            

        }

       public bool Compare_List_Size(List<uint> ListA, List<uint> ListB)
        {
            int SizeA = ListA.Count;
            int SizeB = ListB.Count;

            return SizeA > SizeB;
        }
        #endregion

        #region Send
        public void Send_Mssg()
        {
            byte BMsgNum = 0;

            bool IsMsgNum = false;

           
                cSendICD sMssg = new cSendICD();

                IP = tbServerIP.Text;
                Port = tbServerPort.Text;

                Msg = tbMessage.Text;
                if (Msg == "" || Msg.Length < 0) return;
                //Msg = tbEndTime.Text;
                /*
                IsMsgNum = byte.TryParse(Msg, out BMsgNum);
                if (!IsMsgNum) return;
                */

                sMssg.SetStruct(BMsgNum, Msg);
                //sMssg.SMsg.Msg2 
                // sMssg.Msg2  = Encoding.UTF8.GetBytes(Msg);
                //sMssg.Msg2 = Encoding.ASCII.GetBytes(Msg);

                transferStr = sMssg.Serialize(); 

                //Mbuf(IP, Msg, Msg.Length);
                //l_Msg = Mbuf.MsgToByte(out transferStr,IP, Msg, Msg.Length);
                ///////////////////////////////////////////////////
                //transferStr = Encoding.Default.GetBytes(Msg); //원래
                // 이 부분이 수신부이기 떄문에, 이 과정전에 원하는 헤더를 만들어 실어서 보내면 된다.
                transferSock.BeginSend(transferStr, 0, transferStr.Length, SocketFlags.None, new AsyncCallback(sendStr), transferSock);
                // 해제되었다가 연결될경우(미연결) 에러
                //transferSock.Send(Encoding.Default.GetBytes(Msg));
                //transferSock.Send(Encoding.Default.GetBytes(Port));

                // 발신메세지 기록하는 부분
                string[] row = { IP, Port, Msg };
                if (Msg.Length < 0) return;
                var listViewItem = new ListViewItem(row);
                lvSndMsg.Items.Add(listViewItem);

                IP = string.Empty;
                Port = string.Empty;
                Msg = string.Empty;
            
        }
        public void Send_Mssg_4Graph(int count)
        {
            string tmp = Convert.ToString(count);
            cSendICD sMssg = new cSendICD();

            EndTime = tbEndTime.Text;
            sMssg.SetGraphMsg(byte.Parse(EndTime), tmp);

            transferStr = sMssg.Serialize();
            try
            {
                transferSock.BeginSend(transferStr, 0, transferStr.Length, SocketFlags.None, new AsyncCallback(sendStr), transferSock);
            }
            catch
            {
                MessageBox.Show("메세지를 보낼 수 없습니다!", "Error!");
            }
            string[] row = { IP, Port, tmp };
            if (tmp.Length < 0) return;
            var listViewItem = new ListViewItem(row);
            lvSndMsg.Items.Add(listViewItem);

        }

        static void sendStr(IAsyncResult ar)
        {
           
            Socket transferSock = (Socket)ar.AsyncState;
            // ar.ASyncState를 이용하여 아까 beginsend()에서 맨마지막에 넘겨주었던
            // 소켓 파라미터를 가지고 올 수 있다.(transferSock을 의미하는걸까)

            int strLength = transferSock.EndSend(ar);
            // 전송이 끝나는 부분, 전송을 끝내기 위해 소켓 객체를 가져와야 함
            // EndSend로 끝낼 수 있으며, 반환값으로 데이터의 크기를 돌려줌
            

        } // 전송과정이 끝나면 호출되는 메소드, parameter로 꼮 IAsyncResult를 받아야 함
        #endregion


        #region Receive
        private void receiveStr(IAsyncResult ar)

        {
            try
            {
                Socket transferSock = (Socket)ar.AsyncState;
                int strLength = transferSock.EndReceive(ar);
                // 받은 후 실행

                RecvICD rMsg = new RecvICD();
                rMsg.Deserialize(ref recvStr);
                IsReceived = rMsg.Well_Recv();
                if (!IsReceived)
                {
                    mServerSock.BeginReceive(recvStr, 0, recvStr.Length, SocketFlags.None, new AsyncCallback(receiveStr), mServerSock);
                    return;
                    // 수신하는과정에서 error가 발생하거나, 잘못받은 경우 다시 수신을 실행한다.
                    // return을 실행하여도, callback을 위해 다시 함수를 실행하는것으로 보임
                }
                byte[] ByteTMp = rMsg.RMsg.Msg2;
                byte[] RcvMsg = new byte[4];

                Array.Copy(ByteTMp, 0, RcvMsg, 0, 4);
                //uint RCount = byteTouint((byte[])RcvMsg); // 계속 이 부분 문제가 해결되지 않는다

                string tmp = Encoding.Default.GetString(RcvMsg);
                if (isTimer2ON) 
                {
                    uint RCount = uint.Parse(tmp);

                    Lget_back.Add(RCount);
                }

                RecvMsg(Encoding.Default.GetString(RcvMsg));


                Array.Clear(recvStr, 0, recvStr.Length);
                // recvStr 초기화
                transferSock.BeginReceive(recvStr, 0, recvStr.Length, SocketFlags.None, new AsyncCallback(receiveStr), transferSock);
            }
            catch
            {
                MessageBox.Show("메세지를 보낼 수 없습니다.", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 이 부분이 자꾸 실행되는 이유가 무엇일까
                //MessageBox.Show("연결이 해제되었습니다.", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //transferSock.Close();
            }

        }

        private void RecvMsg(string str)
        {
           
                if (str.Length < 0 || str == null) return;
                string[] row = { tbServerIP.Text, tbServerPort.Text, str };
                var listViewItem = new ListViewItem(row);
                //    listView1.Items.Add(listViewItem);
                //    listView1.Items.Add(IP);
                //    listView1.Items.Add(Port);
                //    listView1.Items.Add(str);

                if (this.InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate () {
                        lvRcvMssg.Items.Add(listViewItem);
                    });
                }
                else
                {
                    lvRcvMssg.Items.Add(listViewItem);
                }
            


        }

        #endregion

        void EraseData()
        {
            try
            {
                lvRcvMssg.Items.Clear();
                lvSndMsg.Items.Clear();
            }
            catch { }
        }
    }
}
