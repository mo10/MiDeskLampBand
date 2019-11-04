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
            Bitmap bitmap = new Bitmap(Width,Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CompositingMode = CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            //绘制事件遮罩
            e.Graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, this.Width, this.Height);
            //绘制背景遮罩
            Color color = Color.FromArgb(20, Color.LightGray);
            int perY = this.Height - this.Height * (_ProgressValue / 100);
            e.Graphics.FillRectangle(new SolidBrush(color), 0, perY, this.Width, this.Height);
            //绘制状态条
            e.Graphics.FillRectangle(new SolidBrush(_StatusBarColor), 2, this.Height-2, this.Width-4, 2);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void TaskBarButton_MouseDown(object sender, MouseEventArgs e)
        {
            displayStyle = DisplayStyle.MouseDown;
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
            displayStyle = DisplayStyle.MouseUp;
            this.Refresh();
        }

        private void TaskButton_EnabledChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
