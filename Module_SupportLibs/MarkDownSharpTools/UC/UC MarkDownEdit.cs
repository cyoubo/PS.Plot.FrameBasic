using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MarkdownSharp;
using PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MarkDownSharpTools.UC
{
    public partial class UC_MarkDownEdit : DevExpress.XtraEditors.XtraUserControl
    {
       
        private MarkDownHTMLHelper helper;

        public UC_MarkDownEdit()
        {
            InitializeComponent();
        }

        public void SetMarkDownStyle(string cssFileFullPath)
        {
            if(helper==null)
                helper = new MarkDownHTMLHelper();
            helper.AddCss(cssFileFullPath);
        }

        private void UC_MarkDownEdit_Load(object sender, EventArgs e)
        {
            if(helper==null)
                helper = new MarkDownHTMLHelper();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Equals(TabPage_Preview))
            {
                webBrowser1.Navigate("about:blank");
                webBrowser1.Refresh();
                webBrowser1.Document.Write(helper.CreateHTMLCode(tv_MarkdownEdit.Text,true));
            }
        }

        private void BarBtn_Space_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlAPI.InsertTextInCursor(tv_MarkdownEdit, MarkDownSymbol.Space);
        }

        private void Barbtn_Blod_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlAPI.InsertTextBetweenCursor(tv_MarkdownEdit, MarkDownSymbol.Bold);
        }

        private void BarBtn_Enter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlAPI.InsertTextInCursor(tv_MarkdownEdit, MarkDownSymbol.Enter);
        }

        private void BarBtn_Code_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlAPI.InsertTextBetweenCursor(tv_MarkdownEdit, MarkDownSymbol.CodeBlock);
        }

        private void BarBtn_Image_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlAPI.InsertTextInCursor(tv_MarkdownEdit, MarkDownSymbol.Image);
        }

        public string MarkDownText
        {
            get
            {
                return tv_MarkdownEdit.Text;
            }
            set
            {
                tv_MarkdownEdit.Text = value;
            }
        }

        public string MarkDownHTMLText
        { 
            get
            {
                if (helper == null)
                    helper = new MarkDownHTMLHelper();
                return helper.CreateHTMLCode(tv_MarkdownEdit.Text, true);
            }
        }




    }
}
