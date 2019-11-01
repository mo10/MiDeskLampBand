using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace MiDeskLampBand
{
    public partial class BandControl : UserControl
    {
        IPAddress mCastAddr = IPAddress.Parse("239.255.255.250");
        int CastPort = 1982;

        UdpClient udpSocket;
        IPEndPoint mCastEP;

        public BandControl(CSDeskBand.CSDeskBandWin w)
        {
            InitializeComponent();
            this.Disposed += OnDispose;

            // UDP Start
            udpSocket = new UdpClient(0);
            //将广播地址添加到多路广播组，生存期(路由器跳数)为10
            udpSocket.JoinMulticastGroup(mCastAddr, 10);
            mCastEP = new IPEndPoint(mCastAddr, CastPort);
            //接收信息
            Task.Run(() => UdpReceive());
        }
        private async Task UdpReceive()
        {
            while (true)
            {
                try
                {
                    var receivedResult = await udpSocket.ReceiveAsync();
                    MessageBox.Show(Encoding.ASCII.GetString(receivedResult.Buffer));
                }
                catch (ObjectDisposedException ex)
                {
                    return;
                }
            }
        }
        private void LampDiscovery()
        {
            string payload = "M-SEARCH * HTTP/1.1\r\n" +
                "HOST: 239.255.255.250:1982\r\n" +
                "MAN: \"ssdp:discover\"\r\n" +
                "ST: wifi_bulb";

            byte[] bytes = Encoding.ASCII.GetBytes(payload);
            udpSocket.Send(bytes, bytes.Length, mCastEP);
        }
        private void BandControl_Load(object sender, EventArgs e)
        {

        }
        private void OnDispose(object sender, EventArgs e)
        {
            udpSocket.Close();
        }
        private void panel_MouseEnter(object sender, EventArgs e)
        {
            // 鼠标移入变换背景
            Control control = sender as Control;
            control.Tag = true;
            Refresh();
        }

        private void panel_MouseLeave(object sender, EventArgs e)
        {
            // 鼠标移入变换背景
            Control control = sender as Control;
            control.Tag = false;
            Refresh();
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            // 鼠标移入变换背景
            Control control = sender as Control;
            if (control.Tag != null && (bool)control.Tag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.LightGray)), 0, 0, control.Width, control.Height);
            }
        }

        private void Panel1_Click(object sender, EventArgs e)
        {
            LampDiscovery();
            MessageBox.Show("Sened!");
        }
    }
}
