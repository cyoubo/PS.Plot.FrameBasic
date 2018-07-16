using DevExpress.XtraVerticalGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class VGridControlHelper
    {
        private VGridControl vGridControl;

        public VGridControlHelper(VGridControl vGridControl)
        {
            this.vGridControl = vGridControl;
        }

        public void SetRowHeight(int minHeight,int maxHeight = -1 )
        {
            this.vGridControl.OptionsView.MinRowAutoHeight = minHeight;
            if (maxHeight != -1 && maxHeight > minHeight)
            {
                this.vGridControl.OptionsView.MaxRowAutoHeight = maxHeight;
            }
        }

        public void BestFitRowHeight()
        {
            this.vGridControl.SizeChanged += vGridControl_SizeChanged_BestFitRowHeight;
        }

        private void vGridControl_SizeChanged_BestFitRowHeight(object sender, EventArgs e)
        {
            int recordWidth = (vGridControl.Width - vGridControl.RowHeaderWidth) / (this.vGridControl.DataSource as DataTable).Rows.Count;
            if (recordWidth > vGridControl.RecordMinWidth)
            {
                vGridControl.RecordWidth = recordWidth;
                vGridControl.ScrollVisibility = DevExpress.XtraVerticalGrid.ScrollVisibility.Vertical;
            }
            else
            {
                vGridControl.ScrollVisibility = DevExpress.XtraVerticalGrid.ScrollVisibility.Auto;
            }
        }

        public void SetReadOnly(bool ReadOnly)
        {
            this.vGridControl.OptionsBehavior.Editable = !ReadOnly;
        }
    }
}
