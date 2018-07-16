using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.TreeTable
{
    /// <summary>
    /// 构建TreeList空间的父子关系表的命令
    /// </summary>
    public interface IBuildTreeNodeCommand
    {
        string CommandName { get; }
        void onCreateTreeTable(TreeTableConductor invorker);
        void onBeforeCreateTreeTable(TreeTableConductor invorker);
        void onAfterCreateTreeTable(TreeTableConductor invorker);
    }

    /// <summary>
    /// 普通TreeList的节点构建命令
    /// </summary>
    public abstract class BaseBuildTreeNodeCommand : IBuildTreeNodeCommand
    {
        /// <summary>
        /// 中间临时变量
        /// </summary>
        protected IDictionary<string, object> m_TempParams;
        /// <summary>
        /// 根节点索引号
        /// </summary>
        protected const int Tag_RootIndex = -1;
        /// <summary>
        /// 树结构构建命令的指挥者
        /// </summary>
        protected TreeTableConductor Conductor;

        /// <summary>
        /// 构建命令的名称
        /// </summary>
        public virtual string CommandName
        {
            get { return "未知的Folds构建方案"; }
        }

        /// <summary>
        /// 工具方法：添加一个树节点
        /// </summary>
        /// <param name="selfID">自身编号</param>
        /// <param name="value">自身值</param>
        /// <param name="ParentID">父节点编号</param>
        protected void onAddTableRow(int selfID, string value, int ParentID)
        {
            DataRow tempRow = Conductor.ResultTable.NewRow();
            tempRow[Conductor.TableBuilder.SelfID] = selfID;
            tempRow[Conductor.TableBuilder.Value] = value;
            tempRow[Conductor.TableBuilder.ParentID] = ParentID;
            tempRow[Conductor.TableBuilder.Tag] = selfID;
            Conductor.ResultTable.Rows.Add(tempRow);
        }
        /// <summary>
        /// 工具方法：添加一个树节点
        /// </summary>
        /// <param name="selfID">自身编号</param>
        /// <param name="tag">节点标签</param>
        /// <param name="value">自身值</param>
        /// <param name="ParentID">父节点编号</param>
        protected void onAddTableRow(int selfID, string tag, string value, int ParentID)
        {
            DataRow tempRow = Conductor.ResultTable.NewRow();
            tempRow[Conductor.TableBuilder.SelfID] = selfID;
            tempRow[Conductor.TableBuilder.Value] = value;
            tempRow[Conductor.TableBuilder.ParentID] = ParentID;
            tempRow[Conductor.TableBuilder.Tag] = tag;
            Conductor.ResultTable.Rows.Add(tempRow);
        }
        /// <summary>
        ///  通过值标签获取SelfID编号
        /// </summary>
        /// <param name="table">目标结果表</param>
        /// <param name="Tag">"></param>
        /// <returns>若查找失败则返回-1</returns>
        protected int onFindSelfIDbyTag(string Tag)
        {
            int result = -1;
            for (int index = 0; index < this.Conductor.ResultTable.Rows.Count; index++)
            {
                string targetTag = this.Conductor.ResultTable.Rows[index].Field<string>(Conductor.TableBuilder.Tag);
                if (targetTag.Equals(Tag))
                {
                    result = Int32.Parse(this.Conductor.ResultTable.Rows[index].Field<string>(Conductor.TableBuilder.SelfID));
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断指定Tag的节点是否是最末端的子节点
        /// </summary>
        /// <param name="Tag">待判定的Tag</param>
        /// <returns></returns>
        protected bool IsLastChildren(string Tag)
        {
            int selfID = onFindSelfIDbyTag(Tag);
            for (int index = 0; index < this.Conductor.ResultTable.Rows.Count; index++)
            {
                string targetTag = this.Conductor.ResultTable.Rows[index].Field<string>(Conductor.TableBuilder.ParentID);
                if (targetTag.Equals(""+selfID))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 工具方法：添加临时中间变量
        /// </summary>
        /// <param name="key">变量标签</param>
        /// <param name="value">变量值</param>
        protected void AddTempParams(string key, object value)
        {
            if (m_TempParams == null)
                m_TempParams = new Dictionary<string, object>();
            m_TempParams.Add(key, value);
        }
        /// <summary>
        /// 工具方法，清除所有的中间变量
        /// </summary>
        protected void ClearTemParams()
        {
            if (m_TempParams != null)
                m_TempParams.Clear();
        }
        /// <summary>
        /// 功能方法：具体的树结构构建方案
        /// </summary>
        /// <param name="invorker">当前构建命令的指挥者</param>
        public abstract void onCreateTreeTable(TreeTableConductor invorker);
        /// <summary>
        /// 功能方法：onCreateTreeTable方法执行前进行的操作
        /// </summary>
        /// <param name="invorker">当前构建命令的指挥者</param>
        public virtual void onBeforeCreateTreeTable(TreeTableConductor invorker) 
        {
            this.Conductor = invorker;
        }
        /// <summary>
        /// 功能方法：onCreateTreeTable方法执行后进行的操作
        /// </summary>
        /// <param name="invorker">当前构建命令的指挥者</param>
        public virtual void onAfterCreateTreeTable(TreeTableConductor invorker) { }
    }


    /// <summary>
    /// 包含选择框的树节点构建命令
    /// </summary>
    public abstract class BaseBuildTreeCheckNodeCommand : BaseBuildTreeNodeCommand
    {
        protected void onAddTableRow(int selfID, string FoldName, int ParentID, bool IsChecked)
        {
            DataRow tempRow = Conductor.ResultTable.NewRow();
            tempRow[Conductor.TableBuilder.SelfID] = selfID;
            tempRow[Conductor.TableBuilder.Value] = FoldName;
            tempRow[Conductor.TableBuilder.ParentID] = ParentID;
            tempRow[Conductor.TableBuilder.Tag] = selfID;
            tempRow[(Conductor.TableBuilder as TreeCheckTableBuilder).IsChecked] = IsChecked;
            Conductor.ResultTable.Rows.Add(tempRow);
        }

        protected void onAddTableRow(int selfID, string tag ,string FoldName, int ParentID, bool IsChecked)
        {
            DataRow tempRow = Conductor.ResultTable.NewRow();
            tempRow[Conductor.TableBuilder.SelfID] = selfID;
            tempRow[Conductor.TableBuilder.Value] = FoldName;
            tempRow[Conductor.TableBuilder.ParentID] = ParentID;
            tempRow[(Conductor.TableBuilder as TreeCheckTableBuilder).IsChecked] = IsChecked;
            tempRow[Conductor.TableBuilder.Tag] = tag;
            Conductor.ResultTable.Rows.Add(tempRow);
        }
    
    }
}
