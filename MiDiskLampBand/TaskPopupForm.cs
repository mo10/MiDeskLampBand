using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiDeskLampBand
{
    public partial class TaskPopupForm : Form
    {
        public TaskPopupForm()
        {
            InitializeComponent();
            this.LostFocus += TaskPopupForm_LostFocus;
        }

        private void TaskPopupForm_LostFocus(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TaskPopupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
