using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public delegate void NodeCheckStateChanged(TreeListNode node, CheckState state);

    public class CheckTreeListHelper : TreeListHelper
    {
        /// <summary>
        /// 节点选择后的事件监听
        /// </summary>
        public event NodeCheckStateChanged NodeCheckStateChangedEvent;

        /// <summary>
        /// 构造方法：由于构造包含复选框的树型控件帮助类
        /// </summary>
        /// <param name="treelist"></param>
        public CheckTreeListHelper(TreeList treelist) : base(treelist) 
        { 
            this.m_TreeList.OptionsView.ShowCheckBoxes = true;
        }

        /// <summary>
        /// 开启自动父-子节点单击事件处理
        /// </summary>
        public void AutoUpdateNodeCheckeState()
        {
            this.m_TreeList.BeforeCheckNode -= m_TreeList_BeforeCheckNode;
            this.m_TreeList.AfterCheckNode -= m_TreeList_AfterCheckNode;

            this.m_TreeList.BeforeCheckNode += m_TreeList_BeforeCheckNode;
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

        /// <summary>
        /// 全选所有树节点
        /// </summary>
        public void CheckedAllNode()
        {
            this.m_TreeList.CheckAll();
            for (int index = 0; index < this.m_TreeList.Nodes.Count; index++)
            {
                SetCheckedChildNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
                SetCheckedParentNodes(m_TreeList.Nodes[index], m_TreeList.Nodes[index].CheckState);
            }
        }
        /// <summary>
        /// 取消所有树节点的选择
        /// </summary>
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
        /// 设置是否只在子叶节点显示选择框
        /// </summary>
        /// <param name="isOnlyShow"></param>
        public void ShowCheckBoxInLeaftBox(bool isOnlyShow)
        { 
            if(isOnlyShow)
                this.m_TreeList.CustomDrawNodeCheckBox += m_TreeList_CustomDrawNodeCheckBox;
            else
                this.m_TreeList.CustomDrawNodeCheckBox -= m_TreeList_CustomDrawNodeCheckBox;
        }

        private void m_TreeList_CustomDrawNodeCheckBox(object sender, CustomDrawNodeCheckBoxEventArgs e)
        {
            if (e.Node.HasChildren)
                e.Handled = true;
        }

    }
}
