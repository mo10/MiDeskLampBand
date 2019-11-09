using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiDeskLampBand
{
    public class Lamp
    {
        /// <summary>
        /// 设备IP
        /// </summary>
        public string Address { get; }
        /// <summary>
        /// 设备端口
        /// </summary>
        public int Port { get; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// 设备是否开启
        /// </summary>
        public bool Power
        {
            get { return _Power; }
            set
            {
                SetPower(value);
            }
        }
        bool _Power;
        /// <summary>
        /// 亮度
        /// </summary>
        public int Bright { get { return _Bright; } }
        int _Bright;
        /// <summary>
        /// 色温
        /// </summary>
        public int ColorTemp { get { return _ColorTemp; } }
        int _ColorTemp;
        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool isConnected { get { return _isConnected; } }
        bool _isConnected;

        TcpClient client;
        NetworkStream stream;
        int commandId = 1;
        public event EventHandler OnDisconnected;

        public Lamp(Dictionary<string, string> headers)
        {
            this.Address = headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[0];
            this.Port = int.Parse(headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[1]);

            this.Id = headers["id"];
            this._Power = (headers["power"] == "on" ? true : false);
            this._Bright = int.Parse(headers["bright"]);
            this._ColorTemp = int.Parse(headers["ct"]);
            this._isConnected = false;
        }
        /// <summary>
        /// 与灯建立连接
        /// </summary>
        /// <param name="lamp"></param>
        public void Open()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    CleanUp();
                }
                client = new TcpClient(this.Address, this.Port);
                stream = client.GetStream();
                this._isConnected = client.Connected;
            }
            catch (Exception ex)
            {
                CleanUp();
            }
        }
        private void CleanUp()
        {
            if (stream != null)
                stream.Close();
            if(client != null || client.Connected)
                client.Close();
            this._isConnected = false;

            LampConnectorEventArgs eventArgs = new LampConnectorEventArgs(this, "disconnected");
            OnDisconnected?.Invoke(this, eventArgs);
        }
        public void SetPower(bool status)
        {
            this._Power = status;
            string payloadFormat = "{{\"id\":{0},\"method\":\"{1}\",\"params\":[{2}]}}\r\n";
            string payloadFormat2 = "{\"id\":1,\"method\":\"set_power\",\"params\":[\"on\", \"smooth\", 500]}\r\n";
            string pld = String.Format(payloadFormat, this.commandId++, "toggle", "");

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(payloadFormat2);
            MessageBox.Show(String.Format("{0},{1}", pld, (client.Connected ? "y" : "n")));
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }
    }
}
