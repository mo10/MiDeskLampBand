using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiDeskLampBand
{
    class LampDiscoveryEventArgs : EventArgs
    {
        public Lamp lamp { get { return _lamp; } }
        private Lamp _lamp;
        public LampDiscoveryEventArgs(Lamp lamp)
        {
            this._lamp = lamp;
        }
    }
    class LampDiscovery
    {
        public event EventHandler OnDiscoveryLamp;

        /// <summary>
        /// 广播地址
        /// </summary>
        private IPAddress mCastAddr = IPAddress.Parse("239.255.255.250");
        /// <summary>
        /// 监听端口
        /// </summary>
        private int CastPort = 1982;

        private UdpClient udpSocket;
        private IPEndPoint mCastEP;
        /// <summary>
        /// 启动设备发现服务
        /// </summary>
        public void Start()
        {
            // UDP Start
            udpSocket = new UdpClient(0);
            //将广播地址添加到多路广播组，生存期(路由器跳数)为10
            udpSocket.JoinMulticastGroup(mCastAddr);
            mCastEP = new IPEndPoint(mCastAddr, CastPort);
            
            Task.Run(() => UdpReceive());
            Task.Run(() => SendCast());
        }
        /// <summary>
        /// 停止设备发现服务
        /// </summary>
        public void Stop()
        {
            udpSocket.Close();
        }
        /// <summary>
        /// 接收UDP广播数据
        /// </summary>
        /// <returns></returns>
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
                    foreach (string line in recvData.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    {
                        if (line.IndexOf(':') != -1)
                        {
                            string key = line.Substring(0, line.IndexOf(": "));
                            string value = line.Substring(line.IndexOf(": ") + 2);
                            headers.Add(key, value);
                        }
                    }

                    Lamp lamp = new Lamp(headers);

                    LampDiscoveryEventArgs eventArgs = new LampDiscoveryEventArgs(lamp);
                    OnDiscoveryLamp?.Invoke(this, eventArgs);
                }
                catch (ObjectDisposedException ex)
                {
                    // UDP Socket已释放 退出
                    return;
                }
            }
        }
        /// <summary>
        /// 不停的发送探测广播
        /// </summary>
        /// <returns></returns>
        private async Task SendCast()
        {
            while (true)
            {
                try
                {
                    string payload = "M-SEARCH * HTTP/1.1\r\n" +
                    "HOST: 239.255.255.250:1982\r\n" +
                    "MAN: \"ssdp:discover\"\r\n" +
                    "ST: wifi_bulb";

                    byte[] bytes = Encoding.ASCII.GetBytes(payload);

                    int a = udpSocket.Send(bytes, bytes.Length, mCastEP);
                    await Task.Delay(2000);
                }
                catch (ObjectDisposedException ex)
                {
                    // UDP Socket已释放 退出
                    return;
                }
            }
        }
    }
}