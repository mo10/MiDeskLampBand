using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsPowerOn { get { return _IsPowerOn; } }
        bool _IsPowerOn;
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

        
        public Lamp(Dictionary<string, string> headers)
        {
            this.Address = headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[0];
            this.Port = int.Parse(headers["Location"].Remove(0, "yeelight://".Length).Split(new char[] { ':' })[1]);

            this.Id = headers["id"];
            this._IsPowerOn = (headers["power"] == "on" ? true : false);
            this._Bright = int.Parse(headers["bright"]);
            this._ColorTemp = int.Parse(headers["ct"]);
        }
        public void SetPowerOn(bool status)
        {
            this._IsPowerOn = status;
        }
    }
}
