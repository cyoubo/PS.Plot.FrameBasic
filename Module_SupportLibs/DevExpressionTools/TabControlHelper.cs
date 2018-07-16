using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class TabControlHelper
    {
        private XtraTabControl TabControl;

        public event onTabPageCleaned TabPageCleanedEvent;

        public TabControlHelper(XtraTabControl TabControl)
        {
            this.TabControl = TabControl;
        }

        public void RegistCloseEventListener()
        {
            this.TabControl.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.TabControl.CloseButtonClick -= TabControl_CloseButtonClick;
            this.TabControl.CloseButtonClick += TabControl_CloseButtonClick;
        }

        public void ClearTabPage()
        {
            foreach (XtraTabPage item in this.TabControl.TabPages)
                item.Dispose();
            this.TabControl.TabPages.Clear();
        }

        public void AddorShowTabPage(string Caption, XtraUserControl control, string Tag = null)
        {
            Tag = string.IsNullOrEmpty(Tag) ? Caption : Tag;
            if (IsExsitTabPage(Tag))
                ShowTabPage(Tag);
            else
                AddTabPage(Caption, control, Tag);
        }


        public void AddTabPage(string Caption, XtraUserControl control,string Tag = null)
        {
            XtraTabPage page = new XtraTabPage();
            page.Text = Caption;
            page.Tag = string.IsNullOrEmpty(Tag) ? Caption : Tag;

            control.Dock = System.Windows.Forms.DockStyle.Fill;
            page.Controls.Clear();
            page.Controls.Add(control);


            if (control is ITabPageCloseCallBack)
                page.Disposed += (control as ITabPageCloseCallBack).onPageClosed;

            this.TabControl.TabPages.Add(page);
            this.TabControl.SelectedTabPage = page;
        }

        public bool IsExsitTabPage(string Tag)
        {
            foreach (XtraTabPage page in TabControl.TabPages)//遍历得到和关闭的选项卡一样的Text  
            {
                if (page.Tag.Equals(Tag))
                {
                    return true;
                }
            }
            return false;
        }

        public void ShowTabPage(string Tag)
        {
            foreach (XtraTabPage page in TabControl.TabPages)//遍历得到和关闭的选项卡一样的Text  
            {
                if (page.Tag.Equals(Tag))
                {
                    TabControl.SelectedTabPage = page;
                }
            }
        }


        private void TabControl_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string tag = (EArg.Page as XtraTabPage).Tag.ToString();//得到关闭的选项卡的text  
            foreach (XtraTabPage page in TabControl.TabPages)//遍历得到和关闭的选项卡一样的Text  
            {
                if (page.Tag.Equals(tag))
                {
                    TabControl.TabPages.Remove(page);
                    page.Dispose();
                    break;
                }
            }

            if (TabControl.TabPages.Count == 0 && TabPageCleanedEvent != null)
            {
                TabPageCleanedEvent.Invoke(TabControl);
            }
        }
    }

    /// <summary>
    /// TabPage关闭时的事件监听
    /// </summary>
    public interface ITabPageCloseCallBack
    {
        /// <summary>
        /// TabPage关闭时的事件监听
        /// </summary>
        void onPageClosed(object sender, EventArgs e);
    }

    public delegate void onTabPageCleaned(object sender);
    
}
