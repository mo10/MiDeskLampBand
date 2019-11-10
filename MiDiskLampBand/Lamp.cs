using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyJSON;

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
        /// 设备支持的方法
        /// </summary>
        public List<string> Support { get; }
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
        public bool isConnected
        {
            get
            {
                if (client != null)
                    return client.Connected;
                else
                    return false;
            }
        }

        TcpClient client;
        NetworkStream stream;
        int commandId = 1;

        public event EventHandler OnDisconnected;
        public event EventHandler OnStatusChanged;

        public Lamp(Dictionary<string, string> headers)
        {
            this.Address        = headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[0];
            this.Port           = int.Parse(headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[1]);
            this.Support        = new List<string>(headers["support"].Split(new char[] { ' ' }));
            this.Id             = headers["id"];
            this._Power         = (headers["power"] == "on" ? true : false);
            this._Bright        = int.Parse(headers["bright"]);
            this._ColorTemp     = int.Parse(headers["ct"]);
        }
        /// <summary>
        /// 与设备建立连接
        /// </summary>
        public void Open()
        {
            try
            {
                // 关闭上一个连接
                if (client != null && client.Connected)
                {
                    CleanUp();
                }
                client = new TcpClient(this.Address, this.Port);
                stream = client.GetStream();
                stream.ReadTimeout = 1000;
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

            LampConnectorEventArgs eventArgs = new LampConnectorEventArgs(this, "disconnected");
            OnDisconnected?.Invoke(this, eventArgs);
        }
        public void SetPower(bool status)
        {

            YeelightControl control = new YeelightControl();
            control.id = commandId++;
            control.method = "set_power";
            control.@params.Add(status ? "on" : "off");
            control.@params.Add("smooth");
            control.@params.Add(2000);

            SendControl(control);

            this._Power = status;
        }
        private YeelightResult SendControl(YeelightControl control)
        {
            var jsonText = JSON.Dump(control, EncodeOptions.NoTypeHints) + "\r\n";
            byte[] data = System.Text.Encoding.ASCII.GetBytes(jsonText);
            stream.Write(data, 0, data.Length);
            stream.Flush();

            byte[] recvData = new byte[1024];
            StringBuilder myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;
            
            do
            {
                
                numberOfBytesRead = stream.Read(recvData, 0, recvData.Length);

                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(recvData, 0, numberOfBytesRead));

            }
            while (stream.DataAvailable);

            // Print out the received message to the console.
            MessageBox.Show(myCompleteMessage.ToString());
            return new YeelightResult();
        }
    }
}
