using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MiDeskLampBand
{
    [ComVisible(true)]
    [Guid("212FDD21-0365-44F0-8067-2E4C9C0EE776")]
    [CSDeskBand.CSDeskBandRegistration(Name = "XiaoMi Desk Lamp", ShowDeskBand = false)]
    public class Deskband : CSDeskBand.CSDeskBandWin
    {
        private static Control _control;

        public Deskband()
        {
            Options.MinHorizontalSize = new Size(100, 30);
            _control = new BandControl(this);
        }

        protected override Control Control => _control;
    }
}
