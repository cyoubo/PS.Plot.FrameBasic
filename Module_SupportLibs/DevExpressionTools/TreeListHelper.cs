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
    /// TreeList空间的帮助类
    /// </summary>
    public class TreeListHelper
    {
        /// <summary>
        /// 节点选择后的事件监听
        /// </summary>
        public event NodeCheckStateChanged NodeCheckStateChangedEvent;

        public event NodeDragDroped NodeDragDropedEvent;

        private TreeList m_TreeList;
        
        /// <summary>
        /// TreeList空间的帮助类
        /// </summary>
        /// <param name="TreeList">被操作的TreeList控件</param>
        public TreeListHelper(TreeList TreeList)
        {
            this.m_TreeList = TreeList;
        }
        /// <summary>
        /// 是否显示选择复选框
        /// </summary>
        /// <param name="isAllow">是否显示选择复选框</param>
        public void SetShowNodeCheckBox(bool isAllow)
        {
            this.m_TreeList.OptionsView.ShowCheckBoxes = isAllow;
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
        /// 开启自动父-子节点单击事件处理
        /// </summary>
        public void AutoUpdateNodeCheckeState()
        {
            this.m_TreeList.BeforeCheckNode -= m_TreeList_BeforeCheckNode;
            this.m_TreeList.BeforeCheckNode += m_TreeList_BeforeCheckNode;

            this.m_TreeList.AfterCheckNode -= m_TreeList_AfterCheckNode;
            this.m_TreeList.AfterCheckNode += m_TreeList_AfterCheckNode;
        }



        private void m_TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void m_TreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        }

        private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }

            if (NodeCheckStateChangedEvent != null)
                NodeCheckStateChangedEvent.Invoke(node, check);
        }

        private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                if (b)
                {
                    node.ParentNode.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    node.ParentNode.CheckState = check;
                }
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        public void CheckedAllNode()
        {
            this.m_TreeList.CheckAll();
            for (int index = 0; index < this.m_TreeList.Nodes.Count; index++)
            {
                SetCheckedChildNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
                SetCheckedParentNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
            }
        }

        public void UnCheckedAllNode()
        {
            this.m_TreeList.UncheckAll();
            for (int index = 0; index < this.m_TreeList.Nodes.Count; index++)
            {
                SetCheckedChildNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
                SetCheckedParentNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
            }
        }
        /// <summary>
        /// 设置是否支持节点移动（支持节点移动后，移动结果会修改TreeList的双亲表）
        /// </summary>
        /// <param name="IsSupport">是否开启</param>
        public void SupportNodeDrop(bool IsSupport)
        {

            this.m_TreeList.DragDrop -= m_TreeList_DragDrop;
            if (IsSupport)
            {
                this.m_TreeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
                this.m_TreeList.DragDrop += m_TreeList_DragDrop;
                
            }
            else
                this.m_TreeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;
        }

        void m_TreeList_DragDrop(object sender, DragEventArgs e)
        {
            if(NodeDragDropedEvent!=null)
            {
                TreeListNode dragNode, targetNode;
                TreeList tl = sender as TreeList;
                dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                targetNode = tl.CalcHitInfo(tl.PointToClient(new Point(e.X, e.Y))).Node;
                NodeDragDropedEvent.Invoke(dragNode, targetNode);
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
    }

    public delegate void NodeCheckStateChanged(TreeListNode node, CheckState state);
    public delegate void NodeDragDroped(TreeListNode sourceNode,TreeListNode targetNode);
}
