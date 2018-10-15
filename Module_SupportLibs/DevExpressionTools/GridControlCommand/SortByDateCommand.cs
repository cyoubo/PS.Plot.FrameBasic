
using PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.GridControlCommand
{
    public class SortByDateCommand : CustomSortCommand
    {
        private bool mIsDescOrder;

        public SortByDateCommand(bool IsDescOrder)
        {
            this.mIsDescOrder = IsDescOrder;
        }

        public override void ExecuteCustomSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Column != null && e.Value1 != null && e.Value2 != null)
            {
                string value1 = e.Value1.ToString();
                string value2 = e.Value2.ToString();
                int result = DateTime.Compare(Convert.ToDateTime(value1),Convert.ToDateTime(value2));
                e.Result = mIsDescOrder ? result * -1 : result;
                e.Handled = true;
            }
        }
    }
}
