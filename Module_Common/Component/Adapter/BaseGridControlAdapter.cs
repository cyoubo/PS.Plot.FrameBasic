using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PS.Plot.FrameBasic.Module_Common.Component.Adapter
{
    /// <summary>
    /// GridControl的适配器基类
    /// </summary>
    /// <typeparam name="T">数据的类型</typeparam>
    public abstract class BaseGridControlAdapter<T>
    {
        //待展示的数据集合
        protected IList<T> m_Data;
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
        /// <param name="Data">待展示的数据</param>
        /// <param name="TableBuilder">数据表结构构建器</param>
        public void Initial(IList<T> Data, BaseDataTableBuilder TableBuilder)
        {
            this.m_Data = new List<T>(Data);
            this.m_TableBuilder = TableBuilder; ;
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="TableBuilder">数据表结构构建器</param>
        public void Initial(BaseDataTableBuilder TableBuilder)
        {
            this.m_Data = new List<T>();
            this.m_TableBuilder = TableBuilder;
        }
        /// <summary>
        /// 更新或者创建数据表
        /// </summary>
        public virtual void NotifyfreshDataTable()
        {
            NotifyClearTable();
            NotifyDestoryTable();

            m_ResultTable = m_TableBuilder.CreateDataTable();
            for (int index = 0; index < m_Data.Count; index++)
            {
                DataRow tempRow = m_ResultTable.NewRow();
                onCreateDataRow(ref tempRow,m_TableBuilder,index, m_Data[index]);
                m_ResultTable.Rows.Add(tempRow);
            }
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
        /// 移除指定索引的元素，该方法不会通知数据表更新
        /// </summary>
        /// <param name="index">指定索引</param>
        /// <returns>若发生异常，则返回T类型的默认值</returns>
        public T RemoveItem(int index)
        {
            T result = default(T);

            if (m_Data != null && m_Data.Count > index && index>=0)
            {
                result = m_Data[index];
                m_Data.RemoveAt(index);
            }
            return result;
        }
        /// <summary>
        /// 添加元素，该方法会引起数据表更新
        /// </summary>
        /// <param name="item">待添加的元素</param>
        public void AddItem(T item)
        {
            if (m_Data != null)
            {
                m_Data.Add(item);
                NotifyfreshDataTable();
            }
        }
        /// <summary>
        /// 添加元素，该方法不会引起数据表更新
        /// </summary>
        /// <param name="item">待添加的元素</param>
        public void InsertItem(T item)
        {
            if (m_Data == null)
                m_Data = new List<T>();
            m_Data.Add(item);
        }

        /// <summary>
        /// 元素索引，获取或设置指定序号的索引
        /// </summary>
        /// <param name="index">索引序号（越界不引发异常，只是设置无效）</param>
        /// <returns></returns>
        public T this[int index] 
        {
            get
            {
                T result = default(T);
                if (m_Data != null && m_Data.Count > index && index >= 0)
                {
                    result = m_Data[index];
                    //m_Data.RemoveAt(index);
                    //NotifyfreshDataTable();
                }
                return result;
            }
            set
            {
                if (m_Data != null && m_Data.Count > index && index >= 0)
                {
                    m_Data[index] = value;
                    NotifyfreshDataTable();
                }
            }
        }

        public IList<T> DataList { get { return m_Data; } }

        public T First(Func<T, bool> predicate)
        {
            return m_Data.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获得当前数据表的元素个数
        /// </summary>
        public int ItemCount { get { return m_Data.Count; } }
        /// <summary>
        /// 创建数据表中的一行记录
        /// </summary>
        /// <param name="tempRow">待创建的一行记录</param>
        /// <param name="builder">当前Adapter所使用的表结构构造器</param>
        /// <param name="RowIndex">当前索引序号</param>
        /// <param name="t">当前元素元素</param>
        public abstract void onCreateDataRow(ref DataRow tempRow, BaseDataTableBuilder builder,int RowIndex,T t);
    
        
    }
    /// <summary>
    /// GridControl的适配器基类2,支持创建Row的处理(借口方式实现)
    /// </summary>
    /// <typeparam name="T">数据的类型</typeparam>
    public abstract class BaseGridControlAdapter2<T> : BaseGridControlAdapter<T>
    {
        //中间变量
        protected IDictionary<string,object> m_TempParams;
        /// <summary>
        /// 添加中间变量，若tag对应的值已经存在，则先移除
        /// </summary>
        /// <param name="tag">中间变量标签</param>
        /// <param name="param">中间变量值</param>
        protected void AddTempParams(string tag,object param)
        {
            if (m_TempParams == null)
                m_TempParams = new Dictionary<string, object>();
            if (m_TempParams.Keys.Contains(tag))
                m_TempParams.Remove(tag);
            m_TempParams.Add(tag, param);
        }

        /// <summary>
        /// 设置获取创建行时的事件回调
        /// </summary>
        public ICreateRowCallBack CreateRowCallBack { get; set; }

        /// <summary>
        /// 更新或者创建数据表，并支持回调
        /// </summary>
        public override void NotifyfreshDataTable()
        {
            NotifyClearTable();
            NotifyDestoryTable();


            m_ResultTable = m_TableBuilder.CreateDataTable();

            if (CreateRowCallBack != null)
                CreateRowCallBack.onPrepareCreated(m_ResultTable, m_TableBuilder);

            for (int index = 0; index < m_Data.Count; index++)
            {
                if (CreateRowCallBack != null)
                    CreateRowCallBack.onBeforeRowCreated(m_TableBuilder, index);
                DataRow tempRow = m_ResultTable.NewRow();
                onCreateDataRow(ref tempRow, m_TableBuilder, index, m_Data[index]);
                m_ResultTable.Rows.Add(tempRow);
                if (CreateRowCallBack != null)
                    CreateRowCallBack.onAfterRowCreated(tempRow, m_TableBuilder, index,m_TempParams);
            }

            if(CreateRowCallBack!=null)
                CreateRowCallBack.onFinishRowsCreated(m_ResultTable);
        }
    }
    /// <summary>
    /// 创建行时的事件回调
    /// </summary>
    public interface ICreateRowCallBack
    {
        /// <summary>
        /// 行创建后的事件回调
        /// </summary>
        /// <param name="tempRow">已经创建的行变量</param>
        /// <param name="m_TableBuilder">当前adapter对应的builder</param>
        /// <param name="index">行序号</param>
        /// <param name="m_TempParams">临时变量</param>
        void onAfterRowCreated(DataRow tempRow, BaseDataTableBuilder m_TableBuilder, int index, IDictionary<string, object> m_TempParams);
        /// <summary>
        /// 行创建前的事件回调
        /// </summary>
        /// <param name="m_TableBuilder">当前adapter对应的builder</param>
        /// <param name="index">行序号</param>
        void onBeforeRowCreated(BaseDataTableBuilder m_TableBuilder, int index); 
        /// <summary>
        /// 当所有行都创建完成后执行的回调
        /// </summary>
        /// <param name="m_ResultTable">完成构建的数据表</param>
        void onFinishRowsCreated(DataTable m_ResultTable);

        void onPrepareCreated(DataTable m_ResultTable, BaseDataTableBuilder m_TableBuilder);
    }

    /// <summary>
    /// GridControl的适配器基类3,支持创建Row的处理(重写方法实现)
    /// </summary>
    /// <typeparam name="T">数据的类型</typeparam>
    public abstract class BaseGridControlAdapter3<T> : BaseGridControlAdapter<T>
    {
        //中间变量
        protected IDictionary<string, object> m_TempParams;
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
        /// 更新或者创建数据表，并支持回调
        /// </summary>
        public override void NotifyfreshDataTable()
        {
            NotifyClearTable();
            NotifyDestoryTable();

            m_ResultTable = m_TableBuilder.CreateDataTable();

            onPrepareCreated(m_ResultTable, m_TableBuilder);

            for (int index = 0; index < m_Data.Count; index++)
            {
                onBeforeRowCreated(m_TableBuilder, index);
                DataRow tempRow = m_ResultTable.NewRow();
                onCreateDataRow(ref tempRow, m_TableBuilder, index, m_Data[index]);
                m_ResultTable.Rows.Add(tempRow);
                onAfterRowCreated(tempRow, m_TableBuilder, index);
            }

            onFinishRowsCreated(m_ResultTable);
        }

        public virtual void onPrepareCreated(DataTable m_ResultTable, BaseDataTableBuilder m_TableBuilder) { }
        public virtual void onBeforeRowCreated(BaseDataTableBuilder m_TableBuilder, int index) { }
        public virtual void onFinishRowsCreated(DataTable m_ResultTable) { }
        public virtual void onAfterRowCreated(DataRow tempRow, BaseDataTableBuilder m_TableBuilder, int index) { }
    }
}
