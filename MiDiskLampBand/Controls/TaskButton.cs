using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MiDeskLampBand.Controls
{
    public enum DisplayStyle
    {
        Normal,
        MouseDown,
        MouseUp,
        MouseEnter,
        MouseLeave
    }
    public partial class TaskButton : UserControl
    {
        private DisplayStyle displayStyle;
        /// <summary>
        /// 控件状态条颜色
        /// </summary>
        public Color StatusBarColor
        {
            get
            {
                return _StatusBarColor;
            }
            set
            {
                _StatusBarColor = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 背景遮罩比例值 0 - 100
        /// </summary>
        public int ProgressValue
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                if (value > 100)
                    _ProgressValue = 100;
                else if (value < 0)
                    _ProgressValue = 0;
                else
                    _ProgressValue = value;
                this.Refresh();
            }
        }
        private int _ProgressValue = 0;
        private Color _StatusBarColor = Color.LightGray;
        public TaskButton()
        {
            InitializeComponent();

            this.Paint += TaskBarButton_Paint;
            this.MouseUp += TaskBarButton_MouseUp;
            this.MouseDown += TaskBarButton_MouseDown;
            this.MouseEnter += TaskBarButton_MouseEnter;
            this.MouseLeave += TaskBarButton_MouseLeave;
            this.EnabledChanged += TaskButton_EnabledChanged;

            this.Click += TaskButton_Click;
            this.DoubleClick += TaskButton_Click;
            this.MouseClick += TaskButton_Click;
            this.MouseDoubleClick += TaskButton_Click;
        }

        private void TaskBarButton_Paint(object sender, PaintEventArgs e)
        {
            Color backgroundColor = Color.FromArgb(0, Color.Black);
            // 事件样式
            switch (displayStyle)
            {
                case DisplayStyle.Normal:

                case DisplayStyle.MouseLeave:
                    backgroundColor = Color.FromArgb(0, Color.Black);
                    break;
                case DisplayStyle.MouseUp:
                case DisplayStyle.MouseEnter:
                    backgroundColor = Color.FromArgb(50, Color.LightGray);
                    break;
                case DisplayStyle.MouseDown:
                    backgroundColor = Color.FromArgb(30, Color.LightGray);
                    break;
            }
            if (!this.Enabled)
            {
                backgroundColor = Color.FromArgb(0, Color.Black);
            }
            //绘制事件遮罩
            e.Graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, this.Width, this.Height);

            e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            //绘制进度遮罩
            Color color = Color.FromArgb(20, Color.LightGray);
            int perHeight = (int)(this.Height * (_ProgressValue / 100.0f));
            e.Graphics.FillRectangle(new SolidBrush(color), 0, this.Height - perHeight, this.Width, perHeight);
            //绘制状态条
            e.Graphics.FillRectangle(new SolidBrush(_StatusBarColor), 2, this.Height-2, this.Width-4, 2);
        }

        private void TaskBarButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                displayStyle = DisplayStyle.MouseDown;
            else
                displayStyle = DisplayStyle.Normal;
            this.Refresh();
        }

        private void TaskBarButton_MouseEnter(object sender, EventArgs e)
        {
            displayStyle = DisplayStyle.MouseEnter;
            this.Refresh();
        }

        private void TaskBarButton_MouseLeave(object sender, EventArgs e)
        {
            displayStyle = DisplayStyle.MouseLeave;
            this.Refresh();
        }

        private void TaskBarButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                displayStyle = DisplayStyle.MouseUp;
            else
                displayStyle = DisplayStyle.Normal;
            this.Refresh();
        }

        private void TaskButton_EnabledChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

    }
}
