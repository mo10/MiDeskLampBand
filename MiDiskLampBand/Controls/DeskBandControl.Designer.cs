namespace MiDeskLampBand.Controls
{
    partial class DeskBandControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.taskButton2 = new MiDeskLampBand.Controls.TaskButton();
            this.taskButton1 = new MiDeskLampBand.Controls.TaskButton();
            this.SuspendLayout();
            // 
            // taskButton2
            // 
            this.taskButton2.BackColor = System.Drawing.Color.Transparent;
            this.taskButton2.BackgroundImage = global::MiDeskLampBand.Resource.Brightness_16x;
            this.taskButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.taskButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.taskButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.taskButton2.Location = new System.Drawing.Point(25, 0);
            this.taskButton2.Margin = new System.Windows.Forms.Padding(0);
            this.taskButton2.Name = "taskButton2";
            this.taskButton2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.taskButton2.ProgressValue = 50;
            this.taskButton2.Size = new System.Drawing.Size(40, 68);
            this.taskButton2.StatusBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.taskButton2.TabIndex = 1;
            this.taskButton2.Click += new System.EventHandler(this.taskButton2_Click);
            this.taskButton2.DoubleClick += new System.EventHandler(this.taskButton2_DoubleClick);
            // 
            // taskButton1
            // 
            this.taskButton1.BackColor = System.Drawing.Color.Transparent;
            this.taskButton1.BackgroundImage = global::MiDeskLampBand.Resource.Disconnect_16x;
            this.taskButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.taskButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.taskButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.taskButton1.Location = new System.Drawing.Point(65, 0);
            this.taskButton1.Margin = new System.Windows.Forms.Padding(0);
            this.taskButton1.Name = "taskButton1";
            this.taskButton1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.taskButton1.ProgressValue = 0;
            this.taskButton1.Size = new System.Drawing.Size(40, 68);
            this.taskButton1.StatusBarColor = System.Drawing.Color.Red;
            this.taskButton1.TabIndex = 0;
            this.taskButton1.Click += new System.EventHandler(this.taskButton1_Click);
            // 
            // DeskBandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.taskButton2);
            this.Controls.Add(this.taskButton1);
            this.Name = "DeskBandControl";
            this.Size = new System.Drawing.Size(105, 68);
            this.ResumeLayout(false);

        }

        #endregion

        private TaskButton taskButton1;
        private TaskButton taskButton2;
    }
}
