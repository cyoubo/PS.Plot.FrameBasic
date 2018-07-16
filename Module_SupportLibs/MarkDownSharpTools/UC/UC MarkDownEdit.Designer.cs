namespace PS.Plot.FrameBasic.Module_SupportLibs.MarkDownSharpTools.UC
{
    partial class UC_MarkDownEdit
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_MarkDownEdit));
            this.TabPage_Preview = new DevExpress.XtraTab.XtraTabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.TabPage_Edit = new DevExpress.XtraTab.XtraTabPage();
            this.tv_MarkdownEdit = new DevExpress.XtraEditors.MemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BarBtn_Enter = new DevExpress.XtraBars.BarButtonItem();
            this.Barbtn_Blod = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtn_Space = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtn_Image = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.TabPage_Preview.SuspendLayout();
            this.TabPage_Edit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tv_MarkdownEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPage_Preview
            // 
            this.TabPage_Preview.Controls.Add(this.webBrowser1);
            this.TabPage_Preview.Name = "TabPage_Preview";
            this.TabPage_Preview.Size = new System.Drawing.Size(777, 444);
            this.TabPage_Preview.Text = "预览";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(777, 444);
            this.webBrowser1.TabIndex = 0;
            // 
            // TabPage_Edit
            // 
            this.TabPage_Edit.Controls.Add(this.tv_MarkdownEdit);
            this.TabPage_Edit.Controls.Add(this.barDockControlLeft);
            this.TabPage_Edit.Controls.Add(this.barDockControlRight);
            this.TabPage_Edit.Controls.Add(this.barDockControlBottom);
            this.TabPage_Edit.Controls.Add(this.barDockControlTop);
            this.TabPage_Edit.Name = "TabPage_Edit";
            this.TabPage_Edit.Size = new System.Drawing.Size(777, 444);
            this.TabPage_Edit.Text = "编辑";
            // 
            // tv_MarkdownEdit
            // 
            this.tv_MarkdownEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_MarkdownEdit.Location = new System.Drawing.Point(0, 46);
            this.tv_MarkdownEdit.MenuManager = this.barManager1;
            this.tv_MarkdownEdit.Name = "tv_MarkdownEdit";
            this.tv_MarkdownEdit.Size = new System.Drawing.Size(777, 398);
            this.tv_MarkdownEdit.TabIndex = 5;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.TabPage_Edit;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BarBtn_Space,
            this.BarBtn_Enter,
            this.Barbtn_Blod,
            this.BarBtn_Image});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RightToLeft = DevExpress.Utils.DefaultBoolean.False;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.Barbtn_Blod, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarBtn_Space, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarBtn_Image, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // BarBtn_Enter
            // 
            this.BarBtn_Enter.Caption = "换行";
            this.BarBtn_Enter.Id = 1;
            this.BarBtn_Enter.Name = "BarBtn_Enter";
            this.BarBtn_Enter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_Enter_ItemClick);
            // 
            // Barbtn_Blod
            // 
            this.Barbtn_Blod.Caption = "加粗";
            this.Barbtn_Blod.Glyph = ((System.Drawing.Image)(resources.GetObject("Barbtn_Blod.Glyph")));
            this.Barbtn_Blod.Id = 2;
            this.Barbtn_Blod.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("Barbtn_Blod.LargeGlyph")));
            this.Barbtn_Blod.Name = "Barbtn_Blod";
            this.Barbtn_Blod.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Barbtn_Blod_ItemClick);
            // 
            // BarBtn_Space
            // 
            this.BarBtn_Space.Caption = "空格";
            this.BarBtn_Space.Glyph = ((System.Drawing.Image)(resources.GetObject("BarBtn_Space.Glyph")));
            this.BarBtn_Space.Id = 0;
            this.BarBtn_Space.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarBtn_Space.LargeGlyph")));
            this.BarBtn_Space.Name = "BarBtn_Space";
            this.BarBtn_Space.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_Space_ItemClick);
            // 
            // BarBtn_Image
            // 
            this.BarBtn_Image.Caption = "图片";
            this.BarBtn_Image.Glyph = ((System.Drawing.Image)(resources.GetObject("BarBtn_Image.Glyph")));
            this.BarBtn_Image.Id = 4;
            this.BarBtn_Image.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarBtn_Image.LargeGlyph")));
            this.BarBtn_Image.Name = "BarBtn_Image";
            this.BarBtn_Image.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_Image_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(777, 46);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 444);
            this.barDockControlBottom.Size = new System.Drawing.Size(777, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 398);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(777, 46);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 398);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xtraTabControl1.RightToLeftLayout = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.SelectedTabPage = this.TabPage_Edit;
            this.xtraTabControl1.Size = new System.Drawing.Size(787, 489);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPage_Edit,
            this.TabPage_Preview});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // UC_MarkDownEdit
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "UC_MarkDownEdit";
            this.Size = new System.Drawing.Size(787, 489);
            this.Load += new System.EventHandler(this.UC_MarkDownEdit_Load);
            this.TabPage_Preview.ResumeLayout(false);
            this.TabPage_Edit.ResumeLayout(false);
            this.TabPage_Edit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tv_MarkdownEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage TabPage_Preview;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private DevExpress.XtraTab.XtraTabPage TabPage_Edit;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BarBtn_Space;
        private DevExpress.XtraBars.BarButtonItem BarBtn_Enter;
        private DevExpress.XtraBars.BarButtonItem Barbtn_Blod;
        private DevExpress.XtraEditors.MemoEdit tv_MarkdownEdit;
        private DevExpress.XtraBars.BarButtonItem BarBtn_Image;

    }
}
