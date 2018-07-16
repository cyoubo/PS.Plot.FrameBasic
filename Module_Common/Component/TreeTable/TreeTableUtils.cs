using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.TreeTable
{
    public delegate bool IsNeedAdd(DataRow node, TreeTableBuilder builder);
    /// <summary>
    /// 基于双亲表示法结构的树状帮助类
    /// </summary>
    public class TreeTableUtils
    {
        private DataTable table;
        private TreeTableBuilder builder;
        /// <summary>
        /// 基于双亲表示法结构的树状帮助类
        /// </summary>
        /// <param name="table">双亲表</param>
        /// <param name="builder">表结构</param>
        public TreeTableUtils(DataTable table,TreeTableBuilder builder)
        {
            this.table = table;
            this.builder = builder;
        }
        /// <summary>
        /// 通过Tag找到指定节点
        /// </summary>
        /// <param name="Tag">待查找的Tag值</param>
        /// <returns>若查找失败则返回null</returns>
        public DataRow FindNodeByTag(string Tag)
        {
            foreach (DataRow item in table.Rows)
            {
                if (item[builder.Tag].ToString().Equals(Tag))
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 通过Self找到指定节点
        /// </summary>
        /// <param name="Tag">待查找的Self值</param>
        /// <returns>若查找失败则返回null</returns>
        public DataRow FindNodeBySelfID(string SelfID)
        {
            foreach (DataRow item in table.Rows)
            {
                if (item[builder.SelfID].ToString().Equals(SelfID))
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 从指定SelfID的节点回溯到目标节点
        /// </summary>
        /// <param name="SelfID">起始节点SelfID</param>
        /// <returns>若查找失败则返回长度为0的列表</returns>
        public IList<string> TraceValueToTop(string StartSelfID,string EndSelfID = "-1")
        {
            Stack<string> m_tempStack = new Stack<string>();
            //递归访问
            onTrace(StartSelfID, builder.Value, m_tempStack, EndSelfID);
            return m_tempStack.ToList();
        }

        /// <summary>
        /// 从指定SelfID的节点回溯到目标节点
        /// </summary>
        /// <param name="SelfID">起始节点SelfID</param>
        /// <returns>若查找失败则返回长度为0的列表</returns>
        public IList<string> TraceValueToTop(string StartSelfID, string colName ,string EndSelfID = "-1")
        {
            if (table.Columns.Contains(colName) == false)
                return new List<string>();

            Stack<string> m_tempStack = new Stack<string>();
            //递归访问
            onTrace(StartSelfID, colName, m_tempStack, EndSelfID);
            return m_tempStack.ToList();
        }

        private void onTrace(string SelfID, string FindColName, Stack<string> resullt, string EndSelfID)
        {
            DataRow temp = FindNodeBySelfID(SelfID);
            resullt.Push(temp[FindColName].ToString());
            if (temp[builder.ParentID].ToString().Equals(EndSelfID) == false)
                onTrace(FindNodeBySelfID(temp[builder.ParentID].ToString())[builder.SelfID].ToString(), FindColName, resullt, EndSelfID);
        }
        /// <summary>
        /// 获取指定SelfID的第一代孩子
        /// </summary>
        /// <param name="SelfID">起始节点SelfID</param>
        /// <returns>查找失败则返回长度为0的列表</returns>
        public IList<DataRow> GetDirectChirldrenBySelfID(string SelfID)
        {
            IList<DataRow> result = new List<DataRow>();
            DataRow current = FindNodeByTag(SelfID);
            if (current != null)
            {
                var collections =  table.AsEnumerable().Where(x => x.Field<string>(builder.ParentID).ToString().Equals(current[builder.SelfID].ToString()));
                if(collections!=null)
                {
                    foreach(DataRow temp in collections)
                        result.Add(temp);
                }
            }
            return result;
        }
        /// <summary>
        /// 查找通路时判断是否添加该通路的条件
        /// </summary>
        public event IsNeedAdd RouteNodeAddFilter;

        /// <summary>
        /// 从指定起始节点一直访问到叶子节点,对每条通路上的节点value值通过split连接
        /// </summary>
        /// <param name="StartSelfID">起始ID编号</param>
        /// <param name="split">节点value连接符号</param>
        /// <returns></returns>
        public IList<string> RouteValueToLeftNode(string StartSelfID,string split="/")
        {
            IList<string> result = new List<string>();
            IList<string> queue = new List<string>();
            //递归查找通路
            onRoute(StartSelfID, queue, result, split);
            return result;
        }

        private void onRoute(string SelfID, IList<string> queue, IList<string> result, string split)
        {
            DataRow temp = FindNodeBySelfID(SelfID);
            queue.Add(temp[builder.Value].ToString());
            IList<DataRow> children = GetDirectChirldrenBySelfID(SelfID);
            if(children.Count==0 )
            {
                //若有过滤条件则进行过滤
                if (RouteNodeAddFilter != null)
                {
                    if (RouteNodeAddFilter.Invoke(temp, builder))
                    {
                        result.Add(string.Join(split, queue.ToList()));
                        queue.RemoveAt(queue.Count - 1);
                    }
                        
                }
                else
                {
                    result.Add(string.Join(split, queue.ToList()));
                    queue.RemoveAt(queue.Count-1);
                }
            }
            else
            {
                foreach (var item in children)
                    onRoute(item[builder.SelfID].ToString(), queue, result, split);
            }
        }
    }
}
