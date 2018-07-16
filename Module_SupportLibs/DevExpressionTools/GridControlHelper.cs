using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class GridControlHelper
    {
        private GridView gridView;

        private GridControl gridControl;

        private HighLightRowCommand highLightRowCommand;

        private CustomSortCommand sortCommand;


        public GridControlHelper(GridView gridview, GridControl gridControl)
        {
            this.gridView = gridview;
            this.gridControl = gridControl;
        }

        public void SetColunmOption(string colName,bool isEditable, bool isVisual)
        {
            gridView.Columns[colName].Visible = isVisual;
            gridView.Columns[colName].OptionsColumn.AllowEdit = isEditable;
        }

        public void SetCellResposity(string ColName, RepositoryItem item,bool IsEditalbe = true)
        {
            gridView.Columns[ColName].OptionsColumn.AllowEdit = IsEditalbe;
            gridView.Columns[ColName].ColumnEdit = item;
        }

        public void SetColMaxWidth(string ColName, int width)
        {
            gridView.Columns[ColName].MaxWidth = width;
        }

        public void Group(string colName , int groupIndex=0)
        {
            gridView.Columns[colName].GroupIndex = groupIndex;
            gridView.Columns[colName].Group();
        }

        public void UnGroup()
        {
            foreach (GridColumn item in gridView.Columns)
            {
                item.UnGroup();
            }
        }

        public void Order(string colName, bool isDescend)
        {
            gridView.Columns[colName].SortOrder = isDescend ? DevExpress.Data.ColumnSortOrder.Descending : DevExpress.Data.ColumnSortOrder.Ascending;
        }

        public void ExpandOrCollapseGroups(DevExpress.XtraBars.ItemClickEventArgs e, string ExpandText = "展开分组", string CollapseText = "收起分组")
        {
            if (e.Item.Caption.Equals(ExpandText))
            {
                gridView.ExpandAllGroups();
                e.Item.Caption = CollapseText;
            }
            else
            {
                gridView.CollapseAllGroups();
                e.Item.Caption = ExpandText;
            }
        }

        /// <summary>
        /// 依据着色命令对表格行进行着色
        /// </summary>
        /// <param name="command">着色命令</param>
        public void HighLightRowByCommand(HighLightRowCommand command)
        {
            if (command == null)
                return;

            //如果没有设置高亮filter
            if (this.highLightRowCommand == null)
            {
                this.highLightRowCommand = command;
                gridView.RowStyle += gridView_RowStyle;

                for (int index = 0; index < this.gridView.RowCount; index++)
                    this.gridView.RefreshRow(index);
            }
            else
            {
                //判断是否和当前filter一致
                if (this.highLightRowCommand.Tag.Equals(command.Tag) == false)
                {
                    gridView.RowStyle -= gridView_RowStyle;
                    gridView.RowStyle += gridView_RowStyle;

                    for (int index = 0; index < this.gridView.RowCount; index++)
                        this.gridView.RefreshRow(index);
                }
            }
        }

        /// <summary>
        /// 移除着色命令
        /// </summary>
        public void RemoveHighLightRowByCommand()
        {
            try
            {
                this.highLightRowCommand = null;
                gridView.RowStyle -= gridView_RowStyle;
            }
            catch (Exception) {}
            finally
            {
                for (int index = 0; index < this.gridView.RowCount; index++)
                    this.gridView.RefreshRow(index);
            }
        }

        public void DisplayRowIndex(int IndexRowWidth = 30)
        {
            this.gridView.CustomDrawRowIndicator -= gridView_CustomDrawRowIndicator;
            this.gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            this.gridView.IndicatorWidth = IndexRowWidth;
        }

        private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 按照自定义排序规则排序
        /// </summary>
        /// <param name="colName">待排序字段</param>
        /// <param name="sortCommand">排序命令</param>
        public void Order(string colName, CustomSortCommand sortCommand)
        {
            

            if (sortCommand == null)
                return;
            //如果没有设置高亮filter
            if (this.sortCommand == null)
            {
                this.sortCommand = sortCommand;
                gridView.CustomColumnSort += gridView_CustomColumnSort;
            }
            else
            {
                //判断是否和当前filter一致
                if (this.sortCommand.Tag.Equals(sortCommand.Tag) == false)
                {
                    gridView.CustomColumnSort -= gridView_CustomColumnSort;
                    gridView.CustomColumnSort += gridView_CustomColumnSort;
                }
            }

            gridView.Columns[colName].SortMode = ColumnSortMode.Custom;
            gridView.Columns[colName].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            //gridControl.RefreshDataSource();
            
        }

        public void RemoveSortByCommand()
        {
            try
            {
                this.sortCommand = null;
                gridView.CustomColumnSort -= gridView_CustomColumnSort;
            }
            catch (Exception) { }
            finally
            {
                for (int index = 0; index < this.gridView.RowCount; index++)
                    this.gridView.RefreshRow(index);
            }
        }

        private void gridView_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            sortCommand.ExecuteCustomSort(sender, e);
        }


        public GridView GridView
        {
            get { return gridView; }
        }

        public GridControl GridControl
        {
            get { return gridControl; }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            highLightRowCommand.ExecuteHighLight(this,e);
        }

        

        public void SetAllColumnVisible(bool p)
        {
            for (int index = 0; index < this.gridView.Columns.Count; index++)
                this.gridView.Columns[index].Visible = p;
        }

        public void SetAllColumnEditable(bool p)
        {
            for (int index = 0; index < this.gridView.Columns.Count; index++)
                this.gridView.Columns[index].OptionsColumn.AllowEdit = p;
        }

        public void DisplayNewInputLine()
        {
            this.gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        public DataTable DataTableSource 
        {
            get
            {
                return this.GridControl.DataSource as DataTable;
            }
        }

        public void DeleteFocusedRow()
        {
            this.gridView.DeleteRow(this.gridView.FocusedRowHandle);
        }

        public void SetShowFilterPanel(bool visual)
        {
            if (visual)
                this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            else
                this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
        }

        public void SetRowsFilterString(string filterString, bool isFilterPanelVisual = false)
        {
            SetShowFilterPanel(isFilterPanelVisual);
            this.gridView.ActiveFilterString = filterString;
        }

        public int getFocuseRowCellValue_Int(string colName)
        {
            return int.Parse(this.gridView.GetFocusedRowCellValue(colName).ToString());
        }

        public String getFocuseRowCellValue_String(string colName)
        {
            return this.gridView.GetFocusedRowCellValue(colName).ToString();
        }

        public long getFocuseRowCellValue_Long(string colName)
        {
            return long.Parse(this.gridView.GetFocusedRowCellValue(colName).ToString());
        }

        public void setColunmsVisual(bool IsVisual, params string[] colNames)
        {
            foreach (string item in colNames)
            {
                this.gridView.Columns[item].Visible = IsVisual;
            }
        }

        public void setColunmsEdit(bool IsEdit, params string[] colNames)
        {
            foreach (string item in colNames)
            {
                this.gridView.Columns[item].OptionsColumn.AllowEdit = IsEdit;
            }
        }

        public void DestoryDataSource()
        {
            if (this.DataTableSource != null)
            {
                this.DataTableSource.Rows.Clear();
                this.DataTableSource.Columns.Clear();
            }
        }
    }

    public abstract class HighLightRowCommand
    {
        public abstract void ExecuteHighLight(GridControlHelper gridControlHelper, RowStyleEventArgs e);
        public virtual string Tag { get { return this.GetType().Name; } }

    }

    /// <summary>
    /// 自定义排序命令类
    /// </summary>
    public abstract class CustomSortCommand
    {
        public abstract void ExecuteCustomSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e);
        public virtual string Tag { get { return this.GetType().Name; } }
    }

}
