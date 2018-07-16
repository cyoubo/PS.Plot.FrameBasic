using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PS.Plot.FrameBasic.Module_Common.Interface;
using PS.Plot.FrameBasic.Module_Common.Component;


namespace PS.Plot.FrameBasic.Module_Common.Form
{
    public partial class Frm_SingleFrom : DevExpress.XtraEditors.XtraForm
    {
        #region 单例模式
        private static Frm_SingleFrom frm;

        public static Frm_SingleFrom Create()
        {
            if (frm == null)
            {
                frm = new Frm_SingleFrom();
                frm.FormClosed += frm_FormClosed;
            }
            return frm;
        }

        private static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Dispose();
            frm = null;
        }
        private Frm_SingleFrom()
        {
            InitializeComponent();
        }
        #endregion

        public BaseUIMediator UIMeditor { get; set; }

        public void SetContent(UserControl control,int addWidth=20,int addHeight=50)
        {
            control.Dock = DockStyle.Fill;
            this.Width = control.Width + addWidth;
            this.Height = control.Height + addHeight;
            this.panelControl1.Controls.Clear();
            this.panelControl1.Controls.Add(control);
        }

        public void SetFrmTitle(string title)
        {
            this.Text = title;
        }

        public string GetContentName()
        {
            if (this.panelControl1.Controls.Count > 0)
                return this.panelControl1.Controls[0].GetType().Name;
            return "";
        }

        public void NotifiedCloseByContent()
        {
            //判断内容控件，是否要执行关闭监听
            if (this.panelControl1.Controls.Count > 0)
            {
                IContentClosedCallBack callback = this.panelControl1.Controls[0] as IContentClosedCallBack;
                if(callback!=null)
                    callback.onOwnerNotifyClosed();
            }
            this.Close();

            //移除调停者
            if (UIMeditor != null)
                UIMeditor.RemoveActor(GetContentName());
        }

        public void Display()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Show();
            this.BringToFront();
        }

        public void Display(string title, UserControl Content, BaseUIMediator meditor = null)
        {
            this.SetFrmTitle(title);
            this.SetContent(Content);
            this.UIMeditor = meditor;
            this.Display();
        }
    }
}