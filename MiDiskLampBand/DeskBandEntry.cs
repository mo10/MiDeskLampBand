using CSDeskBand.ContextMenu;
using MiDeskLampBand.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiDeskLampBand
{
    [ComVisible(true)]
    [Guid("212FDD21-0365-44F0-8067-2E4C9C0EE776")]
    [CSDeskBand.CSDeskBandRegistration(Name = "XiaoMi Desk Lamp", ShowDeskBand = false)]
    public class DeskBandEntry : CSDeskBand.CSDeskBandWin
    {
        protected override Control Control => _control;
        private static Control _control;
        
        private DeskBandMenuAction _openConfigForm = new DeskBandMenuAction("打开设置");
        private DeskBandMenu _deviceListMenu = new DeskBandMenu("可用设备");
        private DeskBandMenuAction searching = new DeskBandMenuAction("正在搜索设备...");
        private DeskBandMenuSeparator separator = new DeskBandMenuSeparator();
        private List<DeskBandMenuItem> ContextMenuItems
        {
            get
            {
                searching.Enabled = false;
                _deviceListMenu.Items.Add(searching);
                _deviceListMenu.Items.Add(separator);
                return new List<DeskBandMenuItem>() { _openConfigForm, _deviceListMenu, separator };
            }
        }

        LampDiscovery lampDiscovery = new LampDiscovery();
        Form configForm = new ConfigForm();
        public int TaskBarHeight = 0;
        public DeskBandEntry()
        {
            // 显示设置窗体
            _openConfigForm.Clicked += (sender, args) =>
            {
                if (configForm == null || configForm.IsDisposed)
                    configForm = new ConfigForm();
                configForm.Show();
            };

            _control = new DeskBandControl(this);
            // 设置最小尺寸
            Options.MinHorizontalSize = new Size(_control.Size.Width, TaskbarInfo.Size.Height);
            TaskBarHeight = TaskbarInfo.Size.Height;
            
            // 设置菜单项
            Options.ContextMenuItems = ContextMenuItems;
            
            // 启动发现服务
            lampDiscovery.OnDiscoveryLamp += OnDiscoveryLamp;
            lampDiscovery.Start();
        }
        /// <summary>
        /// 关闭工具栏事件
        /// </summary>
        protected override void DeskbandOnClosed()
        {
            // 销毁设置窗体
            if (configForm != null && !configForm.IsDisposed)
            {
                configForm.Close();
                configForm.Dispose();
            }
            // 销毁控件
            if (_control != null && !_control.IsDisposed)
            {
                _control.Dispose();
            }
            // 停止服务
            lampDiscovery.OnDiscoveryLamp -= OnDiscoveryLamp;
            lampDiscovery.Stop();

            base.DeskbandOnClosed();
        }

        /// <summary>
        /// 发现新设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDiscoveryLamp(object sender, EventArgs e)
        {
            Lamp lamp = ((LampDiscoveryEventArgs)e).lamp;
            try
            {
                //查重
                foreach (DeskBandMenuItem item in _deviceListMenu.Items)
                {
                    if (item.tag != null)
                    {
                        if (((Lamp)item.tag).Address == lamp.Address)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DeskBandMenuAction subMenuItem = new DeskBandMenuAction(lamp.Address);
            subMenuItem.tag = lamp;
            _deviceListMenu.Items.Add(subMenuItem);
        }
        /// <summary>
        /// 列表被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnLampMenuItem_Clicked(object sender,EventArgs args)
        {

        }
        /// <summary>
        /// 触发开关
        /// </summary>
        public void TogglePower()
        {

        }
    }
}
