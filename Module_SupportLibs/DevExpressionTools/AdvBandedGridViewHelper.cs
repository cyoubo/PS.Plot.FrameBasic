using DevExpress.XtraGrid.Views.BandedGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class AdvBandedGridViewHelper
    {
        private AdvBandedGridView targetView;

        public AdvBandedGridViewHelper(AdvBandedGridView targetView)
        {
            this.targetView = targetView;
        }

        public void UpdateHeaderStyle(bool isShowBand, bool isShowHeader = false, bool isAutoWith = true , bool isShowGroupPanel = false)
        {
            targetView.OptionsView.ShowGroupPanel = isShowGroupPanel;
            targetView.OptionsView.ShowColumnHeaders = isShowHeader;
            targetView.OptionsView.ShowBands = isShowBand;
            targetView.OptionsView.ColumnAutoWidth = isAutoWith;
        }

        public void ClearBand()
        {
            targetView.Bands.Clear();
        }

        public void AddBand(string name)
        {
            GridBand tempBand = new GridBand() { Caption = name, Name = name };
            targetView.Bands.Add(tempBand);
        }

        public GridBand GetBand(string name)
        {
            if (targetView.Bands.Count(x => x.Caption.Equals(name)) == 1)
                return targetView.Bands[name];
            return null;
        }

        public BandedGridColumn GetGridColumn(string colName)
        { 
            if(targetView.Columns.Count(x=>x.FieldName.Equals(colName)) == 1)
                return targetView.Columns[colName];
            return null;
        }

        public void UpdateColumnOption(BandedGridColumn col, GridBand Owner, bool isEditable = false, bool isFillDown = false, int rowIndex = 0)
        {
            if (col == null || Owner == null)
                return;

            col.OwnerBand = Owner;
            col.OptionsColumn.AllowEdit = isEditable;
            col.AutoFillDown = isFillDown;
            col.RowIndex = rowIndex;
        }

        public void UpdateBandStyle(string bandName, bool isFixedWith, int width = -1)
        {
            GridBand temp = GetBand(bandName);
            if (temp != null)
            {
                if (width != -1)
                    temp.Width = width;
                temp.OptionsBand.FixedWidth = isFixedWith;
            }
        }

        public void CustomColumnTextDisplayFormat(string colName, string formatStr)
        {
            BandedGridColumn temp = GetGridColumn(colName);
            if (temp != null)
            {
                temp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                temp.DisplayFormat.FormatString = formatStr;
            }
        }

        public void AddColumnRepository(string colName, DevExpress.XtraEditors.Repository.RepositoryItem repositoryItem)
        {
            BandedGridColumn temp = GetGridColumn(colName);
            if (temp != null)
            {
                temp.ColumnEdit = repositoryItem;
                temp.OptionsColumn.AllowEdit = true;
            }
        }
    }
}
