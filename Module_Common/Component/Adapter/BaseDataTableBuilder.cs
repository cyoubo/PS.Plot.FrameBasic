using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Adapter
{
    /// <summary>
    /// GridControl的数据表构造器
    /// </summary>
    public abstract class BaseDataTableBuilder
    {
        //数据表列集合
        protected IList<DataColumn> m_TableColum;

        public BaseDataTableBuilder()
        {
            m_TableColum = new List<DataColumn>();
        }
        /// <summary>
        /// 工具方法：添加一个属性列
        /// </summary>
        /// <param name="Name">列名</param>
        /// <param name="type">数据类型</param>
        protected void onCreateDataColumn(string Name, Type type)
        {
            m_TableColum.Add(new DataColumn(Name, type));
        }

        /// <summary>
        /// 工具方法：添加一个属性列
        /// </summary>
        /// <param name="Name">列名</param>
        /// <param name="type">数据类型</param>
        protected void onCreateDataColumn(string Name)
        {
            m_TableColum.Add(new DataColumn(Name, typeof(string)));
        }

        /// <summary>
        /// 外部方法：创建指定结构的空数据表
        /// </summary>
        /// <returns>指定结构的空数据表</returns>
        public DataTable CreateDataTable()
        {
            if (m_TableColum.Count == 0)
                AddDataColumn();

            DataTable result = new DataTable();
            result.Columns.AddRange(m_TableColum.ToArray());
            return result;
        }
        /// <summary>
        /// 清除表结构
        /// </summary>
        public void ClearDataColums()
        {
            if (m_TableColum != null)
                m_TableColum.Clear();
        }
        /// <summary>
        /// 待实现方法：添加自定义的属性列，添加过程可调用onCreateDataColumn方法实现
        /// </summary>
        protected abstract void AddDataColumn();
    }
}
