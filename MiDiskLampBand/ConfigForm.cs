using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Windows.UI.Notifications;

namespace MiDeskLampBand
{
    public partial class ConfigForm : Form
    {
        IPAddress mCastAddr = IPAddress.Parse("239.255.255.250");
        int CastPort = 1982;

        UdpClient udpSocket;
        IPEndPoint mCastEP;

        List<DeskLamp> lamps = new List<DeskLamp>();
        public ConfigForm()
        {
            InitializeComponent();

            // UDP Start
            udpSocket = new UdpClient(0);
            //将广播地址添加到多路广播组，生存期(路由器跳数)为10
            udpSocket.JoinMulticastGroup(mCastAddr);
            mCastEP = new IPEndPoint(mCastAddr, CastPort);
            //接收信息
            Task.Run(() => UdpReceive());
        }
        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

        private async Task UdpReceive()
        {
            while (true)
            {
                try
                {
                    var receivedResult = await udpSocket.ReceiveAsync();
                    string recvData = Encoding.ASCII.GetString(receivedResult.Buffer);
                    //解析返回数据
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    foreach(string line in recvData.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    {
                        if(line.IndexOf(':') != -1)
                        {
                            string key = line.Substring(0, line.IndexOf(": "));
                            string value = line.Substring(line.IndexOf(": ")+2);
                            headers.Add(key, value);
                        }
                    }
                    //设备地址

                    DeskLamp lamp = new DeskLamp(headers);
                    if(lamps.Count == 0 || !lamps.Exists(l => l.Id.Equals(lamp.Id)))
                    {
                        lamps.Add(lamp);
                    }
                    MessageBox.Show("sss");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            int a = udpSocket.Send(bytes, bytes.Length, mCastEP);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LampDiscovery();

            string Toast = "<toast><visual><binding template=\"ToastImageAndText01\"><text id = \"1\" >";
            Toast += "Default Text String";
            Toast += "</text></binding></visual></toast>";
            Windows.Data.Xml.Dom.XmlDocument tileXml = new Windows.Data.Xml.Dom.XmlDocument();
            tileXml.LoadXml(Toast);
            var toast = new ToastNotification(tileXml);
            ToastNotificationManager.CreateToastNotifier("NotificationTest.xxx").Show(toast);
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpSocket.Close();
        }
    }
}
