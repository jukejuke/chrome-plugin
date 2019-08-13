namespace MyApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnReceiveMsg = new System.Windows.Forms.Button();
            this.txtReceiveMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 45);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 82);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(269, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "SendMsg";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(47, 80);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(171, 25);
            this.txtContent.TabIndex = 2;
            // 
            // btnReceiveMsg
            // 
            this.btnReceiveMsg.Location = new System.Drawing.Point(269, 131);
            this.btnReceiveMsg.Name = "btnReceiveMsg";
            this.btnReceiveMsg.Size = new System.Drawing.Size(112, 39);
            this.btnReceiveMsg.TabIndex = 3;
            this.btnReceiveMsg.Text = "receiveMsg";
            this.btnReceiveMsg.UseVisualStyleBackColor = true;
            this.btnReceiveMsg.Click += new System.EventHandler(this.btnReceiveMsg_Click);
            // 
            // txtReceiveMsg
            // 
            this.txtReceiveMsg.Location = new System.Drawing.Point(47, 188);
            this.txtReceiveMsg.Multiline = true;
            this.txtReceiveMsg.Name = "txtReceiveMsg";
            this.txtReceiveMsg.Size = new System.Drawing.Size(675, 189);
            this.txtReceiveMsg.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 479);
            this.Controls.Add(this.txtReceiveMsg);
            this.Controls.Add(this.btnReceiveMsg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnReceiveMsg;
        private System.Windows.Forms.TextBox txtReceiveMsg;
    }
}

