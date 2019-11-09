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
        DeskBandEntry entity;

        TaskPopupForm form = new TaskPopupForm();
        public DeskBandControl(DeskBandEntry deskBandEntry)
        {
            InitializeComponent();
            this.entity = deskBandEntry;
        }

        private void taskButton1_Click(object sender, EventArgs e)
        {
            this.entity.activeLamp.SetPower(true);
        }

        private void taskButton2_Click(object sender, EventArgs e)
        {
            TaskButton button = (TaskButton)sender;
            MouseEventArgs em = (MouseEventArgs)e;
            if (em.Button == MouseButtons.Left)
                if (button.ProgressValue >= 100)
                {
                    button.ProgressValue = 0;
                }
                else
                {
                    button.ProgressValue += 10;
                }
        }

        private void taskButton2_DoubleClick(object sender, EventArgs e)
        {
            taskButton2_Click(sender, e);
        }
    }
}
