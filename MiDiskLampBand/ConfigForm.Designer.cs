namespace MiDeskLampBand
{
    partial class ConfigForm
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
            this.registBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.isInstall = new System.Windows.Forms.Label();
            this.unregistBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registBtn
            // 
            this.registBtn.Location = new System.Drawing.Point(149, 8);
            this.registBtn.Name = "registBtn";
            this.registBtn.Size = new System.Drawing.Size(75, 23);
            this.registBtn.TabIndex = 0;
            this.registBtn.Text = "注册组件";
            this.registBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.registBtn.UseVisualStyleBackColor = true;
            this.registBtn.Click += new System.EventHandler(this.RegistBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "组件注册状态:";
            // 
            // isInstall
            // 
            this.isInstall.AutoSize = true;
            this.isInstall.Location = new System.Drawing.Point(102, 13);
            this.isInstall.Name = "isInstall";
            this.isInstall.Size = new System.Drawing.Size(41, 12);
            this.isInstall.TabIndex = 2;
            this.isInstall.Text = "未注册";
            // 
            // unregistBtn
            // 
            this.unregistBtn.Location = new System.Drawing.Point(230, 8);
            this.unregistBtn.Name = "unregistBtn";
            this.unregistBtn.Size = new System.Drawing.Size(75, 23);
            this.unregistBtn.TabIndex = 3;
            this.unregistBtn.Text = "卸载组件";
            this.unregistBtn.UseVisualStyleBackColor = true;
            this.unregistBtn.Click += new System.EventHandler(this.unregistBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "重新启动 Explorer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 69);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.unregistBtn);
            this.Controls.Add(this.isInstall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.registBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XiaoMi Desk Lamp Controller";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button registBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label isInstall;
        private System.Windows.Forms.Button unregistBtn;
        private System.Windows.Forms.Button button1;
    }
}