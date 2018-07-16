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
using PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.IClauseConvert;
using PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.CotrolsEdit
{
    public partial class DateRangeEdit : DevExpress.XtraEditors.XtraUserControl, IUserControlReceiver, IUserSQLControlDataPrivider
    {
        private UserSQLControlData SQLData = new UserSQLControlData();

        public ISQLDateFunctionCovert SQLDateFunction { get; set; }

        public bool IsContainsTime { get; private set; }

        public DateRangeEdit()
        {
            InitializeComponent();
        }

        private void DateRangeEdit_Load(object sender, EventArgs e)
        {
            
        }

        public void RespondBroadcast(BroadcastMessage Message)
        {
            //接收清除值的广播，并执行操作
            if (Message.Message.ToString().ToLower().Equals("clear"))
            {
                Date_End.Text = string.Empty;
                Date_Start.Text = string.Empty;
                Date_Start.DateTime = DateTime.MinValue;
                Date_End.DateTime = DateTime.MinValue;
            }
        }

        public void UpdateControlCaption(string startDateCaption, string endDateCaption)
        {
            if (string.IsNullOrEmpty(startDateCaption))
                layoutItem_Start.TextVisible = false;
            else
            {
                layoutItem_Start.TextVisible = true;
                layoutItem_Start.Text = startDateCaption;
            }

            if (string.IsNullOrEmpty(endDateCaption))
                layoutItem_End.TextVisible = false;
            else
            {
                layoutItem_End.TextVisible = true;
                layoutItem_End.Text = endDateCaption;
            }
        }

        public void UpdateDateEditContainsTime(bool isContainTime = false , string Format="yyyy-MM-dd")
        {
            IsContainsTime = isContainTime;
            if (IsContainsTime)
            {
                this.Date_Start.Properties.DisplayFormat.FormatString = Format;
                this.Date_Start.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Date_Start.Properties.EditFormat.FormatString = Format;
                this.Date_Start.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Date_Start.Properties.Mask.EditMask = Format;
                this.Date_Start.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                this.Date_Start.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;

                this.Date_End.Properties.DisplayFormat.FormatString = Format;
                this.Date_End.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Date_End.Properties.EditFormat.FormatString = Format;
                this.Date_End.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Date_End.Properties.Mask.EditMask = Format;
                this.Date_End.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                this.Date_End.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                this.Date_Start.Properties.DisplayFormat.FormatString = Format;
                this.Date_Start.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                this.Date_Start.Properties.EditFormat.FormatString = Format;
                this.Date_Start.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                this.Date_Start.Properties.Mask.EditMask = Format;
                this.Date_Start.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
                this.Date_Start.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.False;

                this.Date_End.Properties.DisplayFormat.FormatString = Format;
                this.Date_End.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                this.Date_End.Properties.EditFormat.FormatString = Format;
                this.Date_End.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                this.Date_End.Properties.Mask.EditMask = Format;
                this.Date_End.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
                this.Date_End.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        public UserSQLControlData ExtractData(params object[] Params)
        {
            if (string.IsNullOrEmpty(Date_Start.Text) && string.IsNullOrEmpty(Date_End.Text))
                SQLData.IsEmpty = true;
            else
            { 
                if(SQLDateFunction==null)
                    SQLDateFunction = SQLCaluseConvertFactory.CreateDataFunctionConvert("");
                if (string.IsNullOrEmpty(Date_Start.Text) && !string.IsNullOrEmpty(Date_End.Text))
                    SQLData.ResultText = SQLDateFunction.convertDateBetweenSQL(SQLData.ColName, new DateTime(1700, 1, 1, 0, 0, 0), Date_End.DateTime, IsContainsTime);
                else if (!string.IsNullOrEmpty(Date_Start.Text) && string.IsNullOrEmpty(Date_End.Text))
                    SQLData.ResultText = SQLDateFunction.convertDateBetweenSQL(SQLData.ColName, Date_Start.DateTime, new DateTime(2500, 1, 1, 0, 0, 0), IsContainsTime);
                else
                {
                    if (Date_Start.DateTime.CompareTo(Date_End) > 0)
                        SQLData.HasError = true;
                    else
                        SQLData.ResultText = SQLDateFunction.convertDateBetweenSQL(SQLData.ColName, Date_Start.DateTime, Date_End.DateTime, IsContainsTime);
                }
            } 
            return SQLData;
        }

        public void SetUserControlDataTag(string tag)
        {
            SQLData.Tag = tag;
        }

        public void SetDataBaseColName(string colName)
        {
            SQLData.ColName = colName;
        }
    }

   
}
