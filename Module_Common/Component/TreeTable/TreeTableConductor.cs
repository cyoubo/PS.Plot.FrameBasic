using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.TreeTable
{
    /// <summary>
    /// 普通TreeList的树结构内容构建命令的指挥者
    /// </summary>
    public class TreeTableConductor
    {
        protected string m_ErrorMessage;

        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
        }

        private DataTable mResultTable;
        
        public DataTable ResultTable
        {
            get { return mResultTable; }
        }

        public TreeTableBuilder TableBuilder { get; set; }

        public bool Excute(IBuildTreeNodeCommand Command)
        {
            bool result = false;

            //清除上一次的表记录
            ClearTable();

            try
            {
                mResultTable = TableBuilder.CreateDataTable();
                Command.onBeforeCreateTreeTable(this);
                Command.onCreateTreeTable(this);
                Command.onAfterCreateTreeTable(this);
                result = true;
            }
            catch (Exception ex)
            {
                m_ErrorMessage = ex.Message;
            }
            return result;
        }

        public void ClearTable()
        {
            if (mResultTable != null)
            {
                mResultTable.Rows.Clear();
                mResultTable.Columns.Clear();
                mResultTable.Clear();
            }
        }

        public TreeTableUtils convertToTreeTableUtils()
        {
            return new TreeTableUtils(ResultTable, TableBuilder);
        }
        
    }

    /// <summary>
    /// 包含多选框的TreeList的树结构内容构建命令的指挥者
    /// </summary>
    public class TreeCheckTableConductor : TreeTableConductor
    {
        /// <summary>
        /// 更新指定SelfID的节点的选择状态
        /// </summary>
        /// <param name="selfID"></param>
        /// <param name="IsChecked"></param>
        public void UpdateNodeCheckedState(int selfID, bool IsChecked)
        {
            try
            {
                ResultTable.AsEnumerable().Where(x => x.Field<string>(TableBuilder.SelfID).Equals("" + selfID)).First()[IsCheckColName] = IsChecked;
            }
            catch (Exception ex)
            {
                m_ErrorMessage = ex.Message;
            }
        }

        public string IsCheckColName
        {
            get
            { 
                return (TableBuilder as TreeCheckTableBuilder).IsChecked;
            }
        }


    
    }
}
