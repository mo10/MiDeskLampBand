using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiDeskLampBand.Controls
{
    public partial class DeskBandControl : UserControl
    {
        DeskBandEntry deskBandEntry;

        TaskPopupForm form = new TaskPopupForm();
        public DeskBandControl(DeskBandEntry deskBandEntry)
        {
            InitializeComponent();
            this.deskBandEntry = deskBandEntry;
        }

        private void taskButton1_Click(object sender, EventArgs e)
        {
            TaskButton button = (TaskButton)sender;
            if (form == null || form.IsDisposed)
            {
                form = new TaskPopupForm();
            }
            Point pos = button.Parent.PointToScreen(button.Location);
            form.Location = new Point(pos.X, pos.Y - form.Height);
            form.Show();

            form.Width = button.Width;

        }

        private void taskButton2_Click(object sender, EventArgs e)
        {
            TaskButton button = (TaskButton)sender;
            if(button.ProgressValue >= 100)
            {
                button.ProgressValue = 0;
            }
            else
            {
                button.ProgressValue += 10;
            }
        }
    }
}
