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

        string mIpAddr = "127.0.0.1";
        Socket mServerSock;
        Socket transferSock;
        IPAddress mIpAddr_Server;


        IPEndPoint mServerIpEp;

        public string Msg = "";
        byte[] transferStr;
        byte[] recvStr;

        string IP;
        string Port;

        bool IsGraphVisible = false;
        bool IsReceived = false;
        bool isTimer2ON = false;
        DlgExampleGraph dlg;

        #region graph에서 사용하는 변수
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        List<uint> Lget_back = new List<uint>();
        List<uint> Lgraph = new List<uint>();

        bool IsStartTime = false;
        bool IsEndTime = false;
        private int Value = 0;
        private int Value_sec = 0;
        private string EndTime = "";
        private int[] arr = new int[50];
        private int[] Arr_Graph = { };
        private int[] tmp = new int[] { 0,1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, };
        #endregion
    }
}
