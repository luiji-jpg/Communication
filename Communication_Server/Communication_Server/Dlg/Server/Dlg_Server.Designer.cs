namespace Communication_Server
{
    partial class cServer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
       
        /*
        protected override void Dispose(bool disposing)
        {
        //    mServerSock.Close();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            
        }
    */
        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbIP = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.lbServerPort = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lvRcvMssg = new System.Windows.Forms.ListView();
            this.tbInform = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lvSndMsg = new System.Windows.Forms.ListView();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnGraph = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.btnEraseGraph = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbIP.Location = new System.Drawing.Point(15, 21);
            this.lbIP.Name = "lbIP";
            this.lbIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbIP.Size = new System.Drawing.Size(43, 12);
            this.lbIP.TabIndex = 0;
            this.lbIP.Text = "IPNum";
            this.lbIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(90, 17);
            this.tbServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(138, 21);
            this.tbServerIP.TabIndex = 1;
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(320, 17);
            this.tbServerPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(138, 21);
            this.tbServerPort.TabIndex = 3;
            // 
            // lbServerPort
            // 
            this.lbServerPort.AutoSize = true;
            this.lbServerPort.Location = new System.Drawing.Point(245, 21);
            this.lbServerPort.Name = "lbServerPort";
            this.lbServerPort.Size = new System.Drawing.Size(54, 12);
            this.lbServerPort.TabIndex = 2;
            this.lbServerPort.Text = "PortNum";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(90, 50);
            this.tbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(368, 21);
            this.tbMessage.TabIndex = 5;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbMessage.Location = new System.Drawing.Point(15, 55);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbMessage.Size = new System.Drawing.Size(58, 12);
            this.lbMessage.TabIndex = 4;
            this.lbMessage.Text = "Message";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(470, 17);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(65, 21);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(470, 50);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 21);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "수신 메세지";
            // 
            // lvRcvMssg
            // 
            this.lvRcvMssg.GridLines = true;
            this.lvRcvMssg.HideSelection = false;
            this.lvRcvMssg.Location = new System.Drawing.Point(18, 150);
            this.lvRcvMssg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvRcvMssg.Name = "lvRcvMssg";
            this.lvRcvMssg.Size = new System.Drawing.Size(325, 142);
            this.lvRcvMssg.TabIndex = 10;
            this.lvRcvMssg.UseCompatibleStateImageBehavior = false;
            this.lvRcvMssg.View = System.Windows.Forms.View.Details;
            this.lvRcvMssg.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // tbInform
            // 
            this.tbInform.Location = new System.Drawing.Point(18, 322);
            this.tbInform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbInform.Name = "tbInform";
            this.tbInform.ReadOnly = true;
            this.tbInform.Size = new System.Drawing.Size(410, 21);
            this.tbInform.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "정보";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "발신 메세지";
            // 
            // lvSndMsg
            // 
            this.lvSndMsg.GridLines = true;
            this.lvSndMsg.HideSelection = false;
            this.lvSndMsg.Location = new System.Drawing.Point(359, 150);
            this.lvSndMsg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvSndMsg.Name = "lvSndMsg";
            this.lvSndMsg.Size = new System.Drawing.Size(325, 142);
            this.lvSndMsg.TabIndex = 13;
            this.lvSndMsg.UseCompatibleStateImageBehavior = false;
            this.lvSndMsg.View = System.Windows.Forms.View.Details;
            this.lvSndMsg.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(545, 17);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(65, 21);
            this.btnDisconnect.TabIndex = 15;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnGraph
            // 
            this.btnGraph.Location = new System.Drawing.Point(470, 83);
            this.btnGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(65, 21);
            this.btnGraph.TabIndex = 16;
            this.btnGraph.Text = "Graph";
            this.btnGraph.UseVisualStyleBackColor = true;
            this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(15, 89);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(76, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "StartTime(s)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbEndTime
            // 
            this.lbEndTime.AutoSize = true;
            this.lbEndTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbEndTime.Location = new System.Drawing.Point(245, 89);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbEndTime.Size = new System.Drawing.Size(73, 12);
            this.lbEndTime.TabIndex = 18;
            this.lbEndTime.Text = "EndTime(s)";
            this.lbEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(90, 83);
            this.tbStartTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(138, 21);
            this.tbStartTime.TabIndex = 19;
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(320, 83);
            this.tbEndTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(138, 21);
            this.tbEndTime.TabIndex = 20;
            // 
            // btnEraseGraph
            // 
            this.btnEraseGraph.Location = new System.Drawing.Point(545, 83);
            this.btnEraseGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEraseGraph.Name = "btnEraseGraph";
            this.btnEraseGraph.Size = new System.Drawing.Size(65, 21);
            this.btnEraseGraph.TabIndex = 22;
            this.btnEraseGraph.Text = "Erase";
            this.btnEraseGraph.UseVisualStyleBackColor = true;
            this.btnEraseGraph.Click += new System.EventHandler(this.btnEraseGraph_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Enabled = false;
            this.btnSaveData.Location = new System.Drawing.Point(620, 17);
            this.btnSaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(65, 21);
            this.btnSaveData.TabIndex = 23;
            this.btnSaveData.Text = "Save";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.EvnSaveData);
            // 
            // cServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.btnEraseGraph);
            this.Controls.Add(this.tbEndTime);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.lbEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGraph);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lvSndMsg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbInform);
            this.Controls.Add(this.lvRcvMssg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.lbServerPort);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.lbIP);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "cServer";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label lbServerPort;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvRcvMssg;
        private System.Windows.Forms.TextBox tbInform;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvSndMsg;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbEndTime;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Button btnEraseGraph;
        private System.Windows.Forms.Button btnSaveData;
    }
}

