namespace TCPServer
{
    partial class TCPServerForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.tbIPAddress = new System.Windows.Forms.TextBox();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.btnStartListening = new System.Windows.Forms.Button();
			this.btnFindIPv4IP = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSendToAllClients = new System.Windows.Forms.Button();
			this.btnPrintClients = new System.Windows.Forms.Button();
			this.btnStartGame = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "IP: ";
			// 
			// tbIPAddress
			// 
			this.tbIPAddress.Location = new System.Drawing.Point(41, 12);
			this.tbIPAddress.Name = "tbIPAddress";
			this.tbIPAddress.Size = new System.Drawing.Size(100, 20);
			this.tbIPAddress.TabIndex = 5;
			this.tbIPAddress.Text = "127.0.0.1";
			// 
			// tbPort
			// 
			this.tbPort.Location = new System.Drawing.Point(41, 38);
			this.tbPort.Name = "tbPort";
			this.tbPort.Size = new System.Drawing.Size(100, 20);
			this.tbPort.TabIndex = 6;
			this.tbPort.Text = "2737";
			// 
			// btnStartListening
			// 
			this.btnStartListening.Location = new System.Drawing.Point(41, 75);
			this.btnStartListening.Name = "btnStartListening";
			this.btnStartListening.Size = new System.Drawing.Size(206, 31);
			this.btnStartListening.TabIndex = 7;
			this.btnStartListening.Text = "Start Listening";
			this.btnStartListening.UseVisualStyleBackColor = true;
			this.btnStartListening.Click += new System.EventHandler(this.btnStartListening_Click);
			// 
			// btnFindIPv4IP
			// 
			this.btnFindIPv4IP.Location = new System.Drawing.Point(147, 12);
			this.btnFindIPv4IP.Name = "btnFindIPv4IP";
			this.btnFindIPv4IP.Size = new System.Drawing.Size(100, 46);
			this.btnFindIPv4IP.TabIndex = 9;
			this.btnFindIPv4IP.Text = "Find IP";
			this.btnFindIPv4IP.UseVisualStyleBackColor = true;
			this.btnFindIPv4IP.Click += new System.EventHandler(this.btnFindIPv4IP_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Port: ";
			// 
			// btnSendToAllClients
			// 
			this.btnSendToAllClients.Location = new System.Drawing.Point(41, 313);
			this.btnSendToAllClients.Name = "btnSendToAllClients";
			this.btnSendToAllClients.Size = new System.Drawing.Size(206, 34);
			this.btnSendToAllClients.TabIndex = 11;
			this.btnSendToAllClients.Text = "Send msg to all clients";
			this.btnSendToAllClients.UseVisualStyleBackColor = true;
			this.btnSendToAllClients.Click += new System.EventHandler(this.btnSendToAllClients_Click);
			// 
			// btnPrintClients
			// 
			this.btnPrintClients.Location = new System.Drawing.Point(41, 269);
			this.btnPrintClients.Name = "btnPrintClients";
			this.btnPrintClients.Size = new System.Drawing.Size(206, 38);
			this.btnPrintClients.TabIndex = 12;
			this.btnPrintClients.Text = "Print clients";
			this.btnPrintClients.UseVisualStyleBackColor = true;
			this.btnPrintClients.Click += new System.EventHandler(this.btnPrintClients_Click);
			// 
			// btnStartGame
			// 
			this.btnStartGame.Location = new System.Drawing.Point(41, 112);
			this.btnStartGame.Name = "btnStartGame";
			this.btnStartGame.Size = new System.Drawing.Size(206, 73);
			this.btnStartGame.TabIndex = 13;
			this.btnStartGame.Text = "Start Game";
			this.btnStartGame.UseVisualStyleBackColor = true;
			this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
			// 
			// TCPServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(285, 359);
			this.Controls.Add(this.btnStartGame);
			this.Controls.Add(this.btnPrintClients);
			this.Controls.Add(this.btnSendToAllClients);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnFindIPv4IP);
			this.Controls.Add(this.btnStartListening);
			this.Controls.Add(this.tbPort);
			this.Controls.Add(this.tbIPAddress);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "TCPServerForm";
			this.Text = "TCP Server";
			this.Load += new System.EventHandler(this.TCPServerForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnStartListening;
        private System.Windows.Forms.Button btnFindIPv4IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendToAllClients;
		private System.Windows.Forms.Button btnPrintClients;
		private System.Windows.Forms.Button btnStartGame;
	}
}

