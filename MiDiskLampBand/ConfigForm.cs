using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace MiDeskLampBand
{
    public partial class ConfigForm : Form
    {
        List<Lamp> lamps = new List<Lamp>();
        RegCode regCode = new RegCode();
        LampDiscovery lampDiscovery = new LampDiscovery();
        
        public ConfigForm()
        {
            InitializeComponent();
        }
        int counst = 0;
        private void OnDiscoveryLamp(object sender, EventArgs e)
        {
            LampDiscoveryEventArgs lampDiscoveryEvent = (LampDiscoveryEventArgs)e;
            counst++;
        }
        /// <summary>
        /// 检查组件是否安装
        /// </summary>
        /// <returns></returns>
        private void Check_Registered()
        {
            bool isInstalled = regCode.IsRegistered(typeof(DeskBandEntry).GUID.ToString());
            if (isInstalled)
            {
                this.isInstall.ForeColor = Color.Green;
                this.isInstall.Text = "已注册";
            }
            else
            {
                this.isInstall.ForeColor = Color.Red;
                this.isInstall.Text = "未注册";
            }
        }
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            lampDiscovery.OnDiscoveryLamp += OnDiscoveryLamp;
            lampDiscovery.Start();
            Check_Registered();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lampDiscovery.OnDiscoveryLamp -= OnDiscoveryLamp;
            lampDiscovery.Stop();
        }

        private void RegistBtn_Click(object sender, EventArgs e)
        {
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Program)).Location;
            int ExitCode = regCode.RegistAssembly(fullPath);
            if (ExitCode == -1)
            {
                MessageBox.Show("权限提升失败");
            }
            else if (ExitCode == -2)
            {
                MessageBox.Show("发生了未知错误");
            }
            else if (ExitCode == -3)
            {
                MessageBox.Show("操作失败");
            }
            Check_Registered();
        }

        private void unregistBtn_Click(object sender, EventArgs e)
        {
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Program)).Location;
            int ExitCode = regCode.UnRegistAssembly(fullPath);
            if (ExitCode == 0)
            {
                Process.GetCurrentProcess().Kill();
            }
            if (ExitCode == -1)
            {
                MessageBox.Show("权限提升失败!");
            }
            else if (ExitCode == -2)
            {
                MessageBox.Show("发生了未知错误");
            }
            else if (ExitCode == -3)
            {
                MessageBox.Show("操作失败");
            }
            Check_Registered();
        }

        private void activeBtn_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}