using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDeskLampBand
{
    class YeelightControl
    {
        public int id;
        public string method;
        public List<object> @params =new List<object>();
    }

    class YeelightResult
    {
        public int id;
        public string method;
        public List<object> result = new List<object>();
    }
}