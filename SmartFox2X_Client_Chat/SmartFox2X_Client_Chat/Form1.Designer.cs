namespace SmartFox2X_Client_Chat
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ServerIP = new System.Windows.Forms.TextBox();
            this.textBox_ZoneName = new System.Windows.Forms.TextBox();
            this.textBox_ServerPort = new System.Windows.Forms.TextBox();
            this.textBox_RoomName = new System.Windows.Forms.TextBox();
            this.richTextBox_Char = new System.Windows.Forms.RichTextBox();
            this.textBox_UserMsg = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_Login = new System.Windows.Forms.Button();
            this.button_ServerStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "ServerIP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "ServerPort:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "ZoneName:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(303, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "RoomName:";
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.Location = new System.Drawing.Point(122, 21);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(158, 29);
            this.textBox_ServerIP.TabIndex = 1;
            this.textBox_ServerIP.Text = "192.168.133.57";
            // 
            // textBox_ZoneName
            // 
            this.textBox_ZoneName.Location = new System.Drawing.Point(122, 63);
            this.textBox_ZoneName.Name = "textBox_ZoneName";
            this.textBox_ZoneName.Size = new System.Drawing.Size(158, 29);
            this.textBox_ZoneName.TabIndex = 1;
            this.textBox_ZoneName.Text = "BasicExamples";
            // 
            // textBox_ServerPort
            // 
            this.textBox_ServerPort.Location = new System.Drawing.Point(403, 21);
            this.textBox_ServerPort.Name = "textBox_ServerPort";
            this.textBox_ServerPort.Size = new System.Drawing.Size(158, 29);
            this.textBox_ServerPort.TabIndex = 1;
            this.textBox_ServerPort.Text = "9478";
            // 
            // textBox_RoomName
            // 
            this.textBox_RoomName.Location = new System.Drawing.Point(403, 63);
            this.textBox_RoomName.Name = "textBox_RoomName";
            this.textBox_RoomName.Size = new System.Drawing.Size(158, 29);
            this.textBox_RoomName.TabIndex = 1;
            this.textBox_RoomName.Text = "The Lobby";
            // 
            // richTextBox_Char
            // 
            this.richTextBox_Char.Location = new System.Drawing.Point(33, 121);
            this.richTextBox_Char.Name = "richTextBox_Char";
            this.richTextBox_Char.Size = new System.Drawing.Size(528, 324);
            this.richTextBox_Char.TabIndex = 2;
            this.richTextBox_Char.Text = "";
            // 
            // textBox_UserMsg
            // 
            this.textBox_UserMsg.Location = new System.Drawing.Point(33, 451);
            this.textBox_UserMsg.Name = "textBox_UserMsg";
            this.textBox_UserMsg.Size = new System.Drawing.Size(445, 29);
            this.textBox_UserMsg.TabIndex = 1;
            // 
            // button_Send
            // 
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(484, 452);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(77, 28);
            this.button_Send.TabIndex = 3;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_Login
            // 
            this.button_Login.Enabled = false;
            this.button_Login.Location = new System.Drawing.Point(695, 21);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(107, 73);
            this.button_Login.TabIndex = 4;
            this.button_Login.Text = "Disconnect";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // button_ServerStart
            // 
            this.button_ServerStart.Location = new System.Drawing.Point(567, 19);
            this.button_ServerStart.Name = "button_ServerStart";
            this.button_ServerStart.Size = new System.Drawing.Size(110, 71);
            this.button_ServerStart.TabIndex = 5;
            this.button_ServerStart.Text = "StartServer";
            this.button_ServerStart.UseVisualStyleBackColor = true;
            this.button_ServerStart.Click += new System.EventHandler(this.button_ServerStart_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.Location = new System.Drawing.Point(567, 121);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.Size = new System.Drawing.Size(528, 324);
            this.richTextBox_Log.TabIndex = 2;
            this.richTextBox_Log.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 496);
            this.Controls.Add(this.button_ServerStart);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.richTextBox_Log);
            this.Controls.Add(this.richTextBox_Char);
            this.Controls.Add(this.textBox_ZoneName);
            this.Controls.Add(this.textBox_UserMsg);
            this.Controls.Add(this.textBox_RoomName);
            this.Controls.Add(this.textBox_ServerPort);
            this.Controls.Add(this.textBox_ServerIP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ServerIP;
        private System.Windows.Forms.TextBox textBox_ZoneName;
        private System.Windows.Forms.TextBox textBox_ServerPort;
        private System.Windows.Forms.TextBox textBox_RoomName;
        private System.Windows.Forms.RichTextBox richTextBox_Char;
        private System.Windows.Forms.TextBox textBox_UserMsg;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Button button_ServerStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
    }
}

