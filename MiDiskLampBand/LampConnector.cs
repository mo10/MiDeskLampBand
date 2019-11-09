using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiDeskLampBand
{
    class LampConnectorEventArgs : EventArgs
    {
        public Lamp lamp { get { return _lamp; } }
        private Lamp _lamp;
        public string message { get { return _message; } }
        private string _message;

        public LampConnectorEventArgs(Lamp lamp,string message)
        {
            this._lamp = lamp;
            this._message = message;
        }
    }
    class LampConnector
    {
        TcpClient client;
        NetworkStream stream;

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler OnMessage;

        private Lamp _lamp;
        public LampConnector()
        {

        }
        /// <summary>
        /// 与灯建立连接
        /// </summary>
        /// <param name="lamp"></param>
        public bool Open(Lamp lamp)
        {
            try
            {
                if (client.Connected)
                {
                    CleanUp();
                }
                client = new TcpClient(lamp.Address, lamp.Port);
                stream = client.GetStream();

                LampConnectorEventArgs eventArgs = new LampConnectorEventArgs(_lamp, "connected");
                OnConnected?.Invoke(this, eventArgs);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void CleanUp()
        {
            stream.Close();
            client.Close();
            LampConnectorEventArgs eventArgs = new LampConnectorEventArgs(_lamp, "disconnected");
            OnDisconnected?.Invoke(this, eventArgs);
        }
    }
}
