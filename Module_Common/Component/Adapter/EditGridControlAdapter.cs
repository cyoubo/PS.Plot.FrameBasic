using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Adapter
{
    public abstract class EditGridControlAdapter<T> : ScanGridControlAdapter<T>
    {
        public abstract T onDesrialize(BaseDataTableBuilder builder, System.Data.DataRow row);

        public T GetEntryByRowHandle(int RowHandle)
        { 
            if(RowHandle >= 0 && m_ResultTable.Rows.Count > RowHandle)
                return onDesrialize(base.m_TableBuilder, base.m_ResultTable.Rows[RowHandle]);
            else
                return default(T);
        }

        public IList<T> ConvertToEntriesDataList()
        {
            IList<T> result = new List<T>();
            foreach (DataRow itemRow in base.m_ResultTable.Rows)
                result.Add(onDesrialize(base.m_TableBuilder, itemRow));
            return result;
        }

        public void AddEntry(T entry,bool refresh = true)
        { 
            DataRow tempRow = base.m_ResultTable.NewRow();
            onCreateDataRow(ref tempRow, m_TableBuilder, this.m_ResultTable.Rows.Count, entry);
            base.m_ResultTable.Rows.Add(tempRow);
        }

        public void RemoveEntryByRowHandle(int RowHandle)
        {
            if (RowHandle >= 0 && RowHandle < base.m_ResultTable.Rows.Count)
                base.m_ResultTable.Rows.RemoveAt(RowHandle);
        }

        public virtual void NotifyCreateNewResultDataTable()
        {
            NotifyClearTable();
            NotifyDestoryTable();
            m_ResultTable = m_TableBuilder.CreateDataTable();
        }

        public void UpdateCellText(int RowHandle, string colName, object value)
        {
            if(RowHandle >= 0)
                base.m_ResultTable.Rows[RowHandle][colName] = value;
        }

        
    }
}
