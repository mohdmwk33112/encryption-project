namespace ChatApp
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnConnectClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(26, 525);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(133, 20);
            this.txtServerIP.TabIndex = 1;
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.White;
            this.btnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnStartServer.Location = new System.Drawing.Point(55, 25);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 32);
            this.btnStartServer.TabIndex = 3;
            this.btnStartServer.Text = "Login";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click_1);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.DarkGray;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSend.Location = new System.Drawing.Point(684, 532);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 51);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send Message";
            this.btnSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.Color.Teal;
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.txtChat.FormattingEnabled = true;
            this.txtChat.ItemHeight = 20;
            this.txtChat.Location = new System.Drawing.Point(185, 0);
            this.txtChat.Name = "txtChat";
            this.txtChat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtChat.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.txtChat.Size = new System.Drawing.Size(586, 520);
            this.txtChat.TabIndex = 6;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(202, 532);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(476, 51);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(185, 521);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(586, 94);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // btnConnectClient
            // 
            this.btnConnectClient.BackColor = System.Drawing.Color.Gray;
            this.btnConnectClient.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnectClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnConnectClient.Location = new System.Drawing.Point(43, 551);
            this.btnConnectClient.Name = "btnConnectClient";
            this.btnConnectClient.Size = new System.Drawing.Size(102, 32);
            this.btnConnectClient.TabIndex = 4;
            this.btnConnectClient.Text = "Join Chat";
            this.btnConnectClient.UseVisualStyleBackColor = false;
            this.btnConnectClient.Click += new System.EventHandler(this.btnConnectClient_Click_1);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(771, 598);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.btnConnectClient);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ChatForm";
            this.Text = "Messaging App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox txtChat;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnConnectClient;
    }
}

