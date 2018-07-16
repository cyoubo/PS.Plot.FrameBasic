namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.CotrolsEdit
{
    partial class DateRangeEdit
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
            this.Date_End = new DevExpress.XtraEditors.DateEdit();
            this.Date_Start = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItem_Start = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItem_End = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Date_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_Start.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_Start.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_Start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_End)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Date_End);
            this.layoutControl1.Controls.Add(this.Date_Start);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.FitControlsToDisplayAreaHeight = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(542, 66);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Date_End
            // 
            this.Date_End.EditValue = null;
            this.Date_End.Location = new System.Drawing.Point(310, 18);
            this.Date_End.Name = "Date_End";
            this.Date_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_End.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_End.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Date_End.Size = new System.Drawing.Size(214, 28);
            this.Date_End.StyleController = this.layoutControl1;
            this.Date_End.TabIndex = 5;
            // 
            // Date_Start
            // 
            this.Date_Start.EditValue = null;
            this.Date_Start.Location = new System.Drawing.Point(18, 18);
            this.Date_Start.Name = "Date_Start";
            this.Date_Start.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_Start.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_Start.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Date_Start.Size = new System.Drawing.Size(253, 28);
            this.Date_Start.StyleController = this.layoutControl1;
            this.Date_Start.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(542, 64);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutItem_Start
            // 
            this.layoutItem_Start.BestFitWeight = 50;
            this.layoutItem_Start.Control = this.Date_Start;
            this.layoutItem_Start.Location = new System.Drawing.Point(0, 0);
            this.layoutItem_Start.Name = "layoutItem_Start";
            this.layoutItem_Start.Size = new System.Drawing.Size(259, 34);
            this.layoutItem_Start.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItem_Start.TextVisible = false;
            // 
            // layoutItem_End
            // 
            this.layoutItem_End.BestFitWeight = 50;
            this.layoutItem_End.Control = this.Date_End;
            this.layoutItem_End.Location = new System.Drawing.Point(259, 0);
            this.layoutItem_End.Name = "layoutItem_End";
            this.layoutItem_End.Size = new System.Drawing.Size(253, 34);
            this.layoutItem_End.Text = "----";
            this.layoutItem_End.TextSize = new System.Drawing.Size(28, 22);
            // 
            // DateRangeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "DateRangeEdit";
            this.Size = new System.Drawing.Size(542, 66);
            this.Load += new System.EventHandler(this.DateRangeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Date_End.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_Start.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_Start.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_Start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItem_End)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit Date_End;
        private DevExpress.XtraEditors.DateEdit Date_Start;
        private DevExpress.XtraLayout.LayoutControlItem layoutItem_Start;
        private DevExpress.XtraLayout.LayoutControlItem layoutItem_End;
    }
}
