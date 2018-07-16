namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.CotrolsEdit
{
    partial class NumberRangeEdit
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tv_Start = new DevExpress.XtraEditors.TextEdit();
            this.layoutItem_Start = new DevExpress.XtraLayout.LayoutControlItem();
            this.tv_End = new DevExpress.XtraEditors.TextEdit();
            this.layoutItem_End = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_Start.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_Start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_End)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tv_End);
            this.layoutControl1.Controls.Add(this.tv_Start);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(695, 150);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItem_Start,
            this.layoutItem_End});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(695, 150);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // tv_Start
            // 
            this.tv_Start.Location = new System.Drawing.Point(18, 18);
            this.tv_Start.Name = "tv_Start";
            this.tv_Start.Size = new System.Drawing.Size(334, 28);
            this.tv_Start.StyleController = this.layoutControl1;
            this.tv_Start.TabIndex = 4;
            // 
            // layoutItem_Start
            // 
            this.layoutItem_Start.Control = this.tv_Start;
            this.layoutItem_Start.Location = new System.Drawing.Point(0, 0);
            this.layoutItem_Start.Name = "layoutItem_Start";
            this.layoutItem_Start.Size = new System.Drawing.Size(340, 120);
            this.layoutItem_Start.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItem_Start.TextVisible = false;
            // 
            // tv_End
            // 
            this.tv_End.Location = new System.Drawing.Point(392, 18);
            this.tv_End.Name = "tv_End";
            this.tv_End.Size = new System.Drawing.Size(285, 28);
            this.tv_End.StyleController = this.layoutControl1;
            this.tv_End.TabIndex = 5;
            // 
            // layoutItem_End
            // 
            this.layoutItem_End.Control = this.tv_End;
            this.layoutItem_End.Location = new System.Drawing.Point(340, 0);
            this.layoutItem_End.Name = "layoutItem_End";
            this.layoutItem_End.Size = new System.Drawing.Size(325, 120);
            this.layoutItem_End.Text = "----";
            this.layoutItem_End.TextSize = new System.Drawing.Size(28, 22);
            // 
            // NumberRangeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "NumberRangeEdit";
            this.Size = new System.Drawing.Size(695, 150);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_Start.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_Start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_End)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit tv_End;
        private DevExpress.XtraEditors.TextEdit tv_Start;
        private DevExpress.XtraLayout.LayoutControlItem layoutItem_Start;
        private DevExpress.XtraLayout.LayoutControlItem layoutItem_End;
    }
}
