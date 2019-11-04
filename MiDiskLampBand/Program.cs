using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiDeskLampBand
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main(string[] Args)
        {
            
            if(Args.Length > 0)
            {
                if (Args[0] == "-regist")
                {
                    RegCode reg = new RegCode();
                    int code = reg.RegistAssembly(Application.ExecutablePath);
                    return code;
                }
                if(Args[0] == "-unregist")
                {
                    RegCode reg = new RegCode();
                    int code = reg.UnRegistAssembly(Application.ExecutablePath);
                    return code;

                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConfigForm());
            return 0;
        }
    }
}
