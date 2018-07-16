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

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.CotrolsEdit
{
    public partial class NumberRangeEdit : DevExpress.XtraEditors.XtraUserControl, IUserControlReceiver, IUserSQLControlDataPrivider
    {
        private UserSQLControlData SQLData = new UserSQLControlData();

        public bool IsSupportFloatNumber { get; private set; }

        public NumberRangeEdit()
        {
            InitializeComponent();
        }

        public UserSQLControlData ExtractData(params object[] Params)
        {
            if (string.IsNullOrEmpty(tv_Start.Text) && string.IsNullOrEmpty(tv_End.Text))
                SQLData.IsEmpty = true;
            else 
            {
                string format = "{0} between {1} and {2}";
                if (string.IsNullOrEmpty(tv_Start.Text) && !string.IsNullOrEmpty(tv_End.Text))
                    SQLData.ResultText = string.Format(format,IsSupportFloatNumber ? Double.MinValue:Int32.MinValue,tv_End.Text);
                else if (!string.IsNullOrEmpty(tv_Start.Text) && string.IsNullOrEmpty(tv_End.Text))
                    SQLData.ResultText = string.Format(format, tv_Start.Text, IsSupportFloatNumber ? Double.MaxValue : Int32.MaxValue);
                else
                {
                    if (Double.Parse(tv_Start.Text) > Double.Parse(tv_End.Text))
                        SQLData.HasError = true;
                    else
                        SQLData.ResultText = string.Format(format, tv_Start.Text, tv_End.Text);
                }
                    
            }
            return SQLData;
        }

        public void UpdateControlCaption(string startCaption, string endCaption)
        {
            if (string.IsNullOrEmpty(startCaption))
                layoutItem_Start.TextVisible = false;
            else
            {
                layoutItem_Start.TextVisible = true;
                layoutItem_Start.Text = startCaption;
            }

            if (string.IsNullOrEmpty(endCaption))
                layoutItem_End.TextVisible = false;
            else
            {
                layoutItem_End.TextVisible = true;
                layoutItem_End.Text = endCaption;
            }
        }

        public void UpdateSupportFloatNumber(bool isSupport,int tail = 2)
        {
            this.IsSupportFloatNumber = isSupport;
            if (IsSupportFloatNumber)
            {
                tv_Start.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                tv_Start.Properties.Mask.EditMask = "f"+tail;
                tv_End.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                tv_End.Properties.Mask.EditMask = "f" + tail;
            }
            else
            {
                tv_Start.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                tv_Start.Properties.Mask.EditMask = "n0";
                tv_End.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                tv_End.Properties.Mask.EditMask = "n0";
            }
        
        }

        public void SetUserControlDataTag(string tag)
        {
            SQLData.Tag = tag;
        }

        public void SetDataBaseColName(string colName)
        {
            SQLData.ColName = colName;
        }

        public void RespondBroadcast(BroadcastMessage Message)
        {
            //接收清除值的广播，并执行操作
            if (Message.Message.ToString().ToLower().Equals("clear"))
            {
                tv_Start.Text = string.Empty;
                tv_End.Text = string.Empty;
            }
        }
    }
}
