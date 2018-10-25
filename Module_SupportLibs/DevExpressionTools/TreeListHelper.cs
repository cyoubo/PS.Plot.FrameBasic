using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    /// <summary>
    /// 节点拖拽回调
    /// </summary>
    public interface INodeDragCallBack
    {
        void onNodeDragDroped(TreeListNode sourceNode, TreeListNode targetNode, DragEventArgs eventArgs);
        void onNodeDragOver(TreeListNode sourceNode, TreeListNode targetNode, DragEventArgs eventArgs);
    }
    /// <summary>
    /// 自定义节点渲染回调
    /// </summary>
    public abstract class CustomDrawNodeCommand
    {
        public abstract void ExecuteHighLight(TreeListHelper treelistHelper, CustomDrawNodeCellEventArgs e);
        public virtual string Tag { get { return this.GetType().Name; } }
    }
    /// <summary>
    /// TreeList空间的帮助类
    /// </summary>
    public class TreeListHelper
    {
        public INodeDragCallBack NodeDragCallBack { get; set; }
        public CustomDrawNodeCommand CustomDrawNodeCommand {get;set; }
        public TreeList TreeList { get { return m_TreeList; } }
        public DataTable DataSourceTable { get { return this.m_TreeList.DataSource as DataTable; } }

        protected TreeList m_TreeList;
        
        /// <summary>
        /// TreeList空间的帮助类
        /// </summary>
        /// <param name="TreeList">被操作的TreeList控件</param>
        public TreeListHelper(TreeList TreeList)
        {
            this.m_TreeList = TreeList;
        }

        /// <summary>
        /// 设置节点的可见性
        /// </summary>
        /// <param name="colName">节点列名</param>
        /// <param name="IsEditable">是否可编辑</param>
        /// <param name="IsVisual">是否可见</param>
        public void SetNodeVisualOption(string colName, bool IsEditable, bool IsVisual)
        {
            this.m_TreeList.Columns[colName].OptionsColumn.AllowEdit = IsEditable;
            this.m_TreeList.Columns[colName].Visible = IsVisual;
        }
        /// <summary>
        /// 设置界面列名标签
        /// </summary>
        /// <param name="colName">节点列名</param>
        /// <param name="Caption">列名标签</param>
        public void SetColNameCaption(string colName, string Caption)
        {
            this.m_TreeList.Columns[colName].Caption = Caption;
        }

        /// <summary>
        /// 设置树状图的分组关键字
        /// </summary>
        /// <param name="KeyField">自身节点编号</param>
        /// <param name="ParentField">双亲节点编号</param>
        public void SetKeyParentField(string KeyField, string ParentField)
        {
            this.m_TreeList.KeyFieldName = KeyField;
            this.m_TreeList.ParentFieldName = ParentField;
        }

        /// <summary>
        /// 设置是否支持节点移动（支持节点移动后，移动结果不会修改TreeList的双亲表）
        /// </summary>
        /// <param name="IsSupport">是否开启</param>
        public void SupportNodeDrop(bool IsSupport)
        {

            this.m_TreeList.DragDrop -= m_TreeList_DragDrop;
            this.m_TreeList.DragOver -= m_TreeList_DragOver;
            if (IsSupport)
            {
                this.m_TreeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
                this.m_TreeList.DragDrop += m_TreeList_DragDrop;
                this.m_TreeList.DragOver += m_TreeList_DragOver;
                
            }
            else
                this.m_TreeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;
        }

        void m_TreeList_DragOver(object sender, DragEventArgs e)
        {
            if (NodeDragCallBack != null)
            {
                TreeListNode dragNode, targetNode;
                TreeList tl = sender as TreeList;
                dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                targetNode = tl.CalcHitInfo(tl.PointToClient(new Point(e.X, e.Y))).Node;
                NodeDragCallBack.onNodeDragOver(dragNode, targetNode, e);
            }
        }

        void m_TreeList_DragDrop(object sender, DragEventArgs e)
        {
            if (NodeDragCallBack != null)
            {
                TreeListNode dragNode, targetNode;
                TreeList tl = sender as TreeList;
                dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                targetNode = tl.CalcHitInfo(tl.PointToClient(new Point(e.X, e.Y))).Node;
                NodeDragCallBack.onNodeDragDroped(dragNode, targetNode, e);
            }
        }

        /// <summary>
        /// 清除双清表并破坏表结构
        /// </summary>
        public void ClearTable()
        {
            DataTable table = this.m_TreeList.DataSource as DataTable;
            if (table != null)
            {
                table.Rows.Clear();
                table.Columns.Clear();
            }
        }
        /// <summary>
        /// 清除双亲表
        /// </summary>
        public void ClearTableRow()
        {
            DataTable table = this.m_TreeList.DataSource as DataTable;
            if (table != null)
            {
                table.Rows.Clear();
            }
        }

        /// <summary>
        /// 利用Tag值获取ValueFieldName对应的值
        /// </summary>
        /// <param name="tag">标签值</param>
        /// <param name="ValueFieldName">待获取值的列名</param>
        /// <param name="tagFieldName">标签值列名（默认是SelfID）</param>
        /// <returns>若返回失败则是int.MinValue</returns>
        public int getIntValueByTag(string tag, string ValueFieldName, string tagFieldName = "SelfID")
        {
            EnumerableRowCollection<DataRow> rows = DataSourceTable.AsEnumerable().Where(x => x.Field<string>(tagFieldName).ToString().Equals(tag));
            if (rows.Count() > 0)
                return int.Parse(rows.ToArray()[0][ValueFieldName].ToString());
            else
                return int.MinValue;
        }
        /// <summary>
        /// 获取指定节点的指定属性值
        /// </summary>
        /// <param name="node">指定节点</param>
        /// <param name="ValueFieldName">属性值名</param>
        /// <returns>若返回失败则返回int.MinValue</returns>
        public int getIntValueByTag(TreeListNode node, string ValueFieldName)
        {
            string temp = node.GetValue(ValueFieldName).ToString();
            return string.IsNullOrEmpty(temp) ? int.MinValue : int.Parse(temp);
        }

        /// <summary>
        /// 利用Tag值获取ValueFieldName对应的值
        /// </summary>
        /// <param name="tag">标签值</param>
        /// <param name="ValueFieldName">待获取值的列名</param>
        /// <param name="tagFieldName">标签值列名（默认是SelfID）</param>
        /// <returns>若返回失败则是空字符串</returns>
        public string getStringValueByTag(string tag, string ValueFieldName, string tagFieldName = "SelfID")
        {
            try
            {
                EnumerableRowCollection<DataRow> rows = DataSourceTable.AsEnumerable().Where(x => x.Field<string>(tagFieldName).ToString().Equals(tag));
                if (rows.Count() > 0)
                    return rows.ToArray()[0][ValueFieldName].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
            }
            return "";
        }

        /// <summary>
        /// 获取指定节点的指定属性值
        /// </summary>
        /// <param name="node">指定节点</param>
        /// <param name="ValueFieldName">属性值名</param>
        /// <returns>若返回失败则返回空字符串</returns>
        public string getStringValueByTag(TreeListNode node, string ValueFieldName)
        {
            string temp = node.GetValue(ValueFieldName).ToString();
            return string.IsNullOrEmpty(temp) ? "" : temp;
        }

        /// <summary>
        /// 依据着色命令对树节点行进行着色
        /// </summary>
        /// <param name="command">着色命令</param>
        public void CustomDrawCellByCommand(CustomDrawNodeCommand command)
        {
            if (command == null)
                return;

            //如果没有设置高亮filter
            if (this.CustomDrawNodeCommand == null)
            {
                this.CustomDrawNodeCommand = command;
                this.m_TreeList.CustomDrawNodeCell+= m_TreeList_CustomDrawNodeCell;

                foreach (TreeListNodes item in this.TreeList.Nodes)
                {
                    foreach(TreeListNode item2 in item)
                        this.m_TreeList.RefreshNode(item2);
                }
            }
            else
            {
                //判断是否和当前filter一致
                if (this.CustomDrawNodeCommand.Tag.Equals(command.Tag) == false)
                {
                    this.m_TreeList.CustomDrawNodeCell -= m_TreeList_CustomDrawNodeCell;
                    this.m_TreeList.CustomDrawNodeCell += m_TreeList_CustomDrawNodeCell;
                   

                    foreach (TreeListNodes item in this.TreeList.Nodes)
                    {
                        foreach (TreeListNode item2 in item)
                            this.m_TreeList.RefreshNode(item2);
                    }
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
                this.CustomDrawNodeCommand = null;
                this.m_TreeList.CustomDrawNodeCell -= m_TreeList_CustomDrawNodeCell;
            }
            catch (Exception) { }
            finally
            {
                foreach (TreeListNodes item in this.TreeList.Nodes)
                {
                    foreach (TreeListNode item2 in item)
                        this.m_TreeList.RefreshNode(item2);
                }
            }
        }

        private void m_TreeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            this.CustomDrawNodeCommand.ExecuteHighLight(this, e);
        }

        
    }
}
