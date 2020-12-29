using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms.DataVisualization.Charting;


namespace Communication_Server
{
    public partial class cServer : Form
    {
        protected override void Dispose(bool disposing)
        {
            

            if (disposing && (components != null))
            {
                components.Dispose();

                
            }

            if(dlg != null)
            {
                dlg.fnDispose(disposing);
            }
            
            base.Dispose(disposing);
            
        }

        

        

        private void Form1_Load(object sender, EventArgs e)
        {
            fnGetIniData();
            //SetName();
            fnInitData();
        }
        
        /* 
        private void SetName()
        {
            lbIP.Text = "IP";
            lbServerPort.Text = "Port";
            lbMessage.Text = "전송데이터";
            btnConnect.Text = "연결";
            btnSend.Text = "전송";
            btnDisconnect.Text = "연결해제";
           
            label4.Text = "수신데이터";
            label5.Text = "선택한 데이터";
            label6.Text = "발신데이터";

        }
        */
        private void btnConnect_Click(object sender, EventArgs e)
        {
            IP = tbServerIP.Text;
            Port = tbServerPort.Text;
            if (!(IP is null) && !(Port is null)) Connect(IP, Port);
            else
            {
                MessageBox.Show("Nothing");
                Connect(IP,Port);

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send_Mssg();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            btnDisconnect.Enabled = false;
            btnDisconnect.BackColor = Color.Gray;
            btnConnect.Enabled = true;
            btnConnect.BackColor = Color.Lavender;

            Disconnect();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Click_list1(sender, e);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Click_list2(sender, e);
        }

        private void Select_Click_list1(object sender, EventArgs e)
        {
            if (lvRcvMssg.SelectedItems.Count > 0)
            {
                tbInform.Text = "Receive Message : " + lvRcvMssg.Items[lvRcvMssg.SelectedItems[0].Index].SubItems[2].Text;
            }
        }
        private void Select_Click_list2(object sender, EventArgs e)
        {
            if (lvSndMsg.SelectedItems.Count > 0)
            {
                tbInform.Text = "Send Message : " + lvSndMsg.Items[lvSndMsg.SelectedItems[0].Index].SubItems[2].Text;
            }
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            //chart1.Series[0].ChartType = SeriesChartType.Line;
            Draw_Graph();
        }

        private void EvnSaveData(object sender, EventArgs e)
        {
            fnSetIniData();
        }

        private void btnEraseGraph_Click(object sender, EventArgs e)
        {
            EraseData();
        }
    }
}
