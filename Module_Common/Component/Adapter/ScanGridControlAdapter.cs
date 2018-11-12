using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Adapter
{
    /// <summary>
    /// 只读数据对应数据表的适配器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ScanGridControlAdapter<T>
    {
        //中间变量
        protected IDictionary<string, object> m_TempParams;
        //用于展示的实际DataTable
        protected DataTable m_ResultTable;
        //数据表结构构建器
        protected BaseDataTableBuilder m_TableBuilder;
        /// <summary>
        /// 获取构建并填充了数据的数据表对象
        /// </summary>
        public DataTable ResultTable
        {
            get { return m_ResultTable; }
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="TableBuilder">数据表结构构建器</param>
        public void Initial(BaseDataTableBuilder TableBuilder)
        {
            this.m_TableBuilder = TableBuilder;
        }
        /// <summary>
        /// 清除数据表内容
        /// </summary>
        public void NotifyClearTable()
        {
            if (m_ResultTable != null)
                m_ResultTable.Clear();
        }
        /// <summary>
        /// 解除数据表结构
        /// </summary>
        public void NotifyDestoryTable()
        {
            NotifyClearTable();
            if (m_ResultTable != null)
                m_ResultTable.Columns.Clear();
        }
        /// <summary>
        /// 更新或者创建数据表
        /// </summary>
        public virtual void NotifyfreshDataTable(IList<T> datas)
        {
            NotifyClearTable();
            NotifyDestoryTable();
            m_ResultTable = m_TableBuilder.CreateDataTable();
            onPrepareCreated(m_ResultTable, m_TableBuilder);
            if (datas == null)
                return;
            for (int index = 0; index < datas.Count; index++)
            {
                onBeforeRowCreated(m_TableBuilder, index);
                DataRow tempRow = m_ResultTable.NewRow();
                onCreateDataRow(ref tempRow, m_TableBuilder, index, datas[index]);
                m_ResultTable.Rows.Add(tempRow);
                onAfterRowCreated(tempRow, m_TableBuilder, index);
            }
            onFinishRowsCreated(m_ResultTable);
        }
        /// <summary>
        /// 添加中间变量，若tag对应的值已经存在，则先移除
        /// </summary>
        /// <param name="tag">中间变量标签</param>
        /// <param name="param">中间变量值</param>
        protected void AddTempParams(string tag, object param)
        {
            if (m_TempParams == null)
                m_TempParams = new Dictionary<string, object>();
            if (m_TempParams.Keys.Contains(tag))
                m_TempParams.Remove(tag);
            m_TempParams.Add(tag, param);
        }
        /// <summary>
        /// 创建数据表中的一行记录
        /// </summary>
        /// <param name="tempRow">待创建的一行记录</param>
        /// <param name="builder">当前Adapter所使用的表结构构造器</param>
        /// <param name="RowIndex">当前索引序号</param>
        /// <param name="t">当前元素元素</param>
        public abstract void onCreateDataRow(ref DataRow tempRow, BaseDataTableBuilder builder, int RowIndex, T t);
        public virtual void onPrepareCreated(DataTable m_ResultTable, BaseDataTableBuilder m_TableBuilder) { }
        public virtual void onBeforeRowCreated(BaseDataTableBuilder m_TableBuilder, int index) { }
        public virtual void onFinishRowsCreated(DataTable m_ResultTable) { }
        public virtual void onAfterRowCreated(DataRow tempRow, BaseDataTableBuilder m_TableBuilder, int index) { }
    }
}
