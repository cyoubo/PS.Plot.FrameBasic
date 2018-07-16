using PS.Plot.FrameBasic.Module_Common.Component.TreeTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileOperator
{
    /// <summary>
    /// 文件目录树结构构建器
    /// </summary>
    public class FoldConstructor
    {
        private TreeCheckTableBuilder builder = new TreeCheckTableBuilder();

        public TreeCheckTableBuilder TableBuilder
        {
            get { return builder; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; private set;}
        /// <summary>
        /// 基础路径
        /// </summary>
        public string BasePath { get; set; }
        /// <summary>
        /// 根目录文件夹
        /// </summary>
        public string RootPath { get; set; }
        /// <summary>
        /// 创建文件夹结构
        /// </summary>
        /// <param name="table">与TreeCheckTableBuilder所构建相同结构的文件结构表</param>
        /// <returns>若创建成功则返回true</returns>
        public bool CreateFolds(DataTable table)
        {
            bool result = false;
            try
            {
                foreach (DataRow tempRow in table.Rows)
                {
                    Stack<string> stack = new Stack<string>();
                    onGetFullPath(stack, tempRow, table);
                    StringBuilder strBuilder = new StringBuilder();
                    foreach (var item in stack)
                    {
                        strBuilder.Append(item);
                        strBuilder.Append("\\");
                    }
                    string FullPath = BasePath + "\\" + RootPath + "\\" + strBuilder;
                    System.IO.Directory.CreateDirectory(FullPath);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 创建文件夹结构
        /// </summary>
        /// <param name="struction">文件目录构建命令</param>
        /// <returns>若创建成功则返回true</returns>
        public bool CreateFolds(IFoldsStructCommand struction)
        {
           DataTable table =  builder.CreateDataTable();
           struction.onCreateFoldStructionTable(this ,ref table);
           return CreateFolds(table);
        }

        /// <summary>
        /// 递归获取文件结构
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="tempRow"></param>
        /// <param name="table"></param>
        private void onGetFullPath(Stack<string> stack, DataRow tempRow, DataTable table)
        {
            if (Boolean.Parse(tempRow[builder.IsChecked].ToString()))
                stack.Push(tempRow[builder.Value].ToString());

            if (int.Parse(tempRow[builder.ParentID].ToString()) == -1)
                return;
            else
            {
                DataRow row = table.AsEnumerable().First(x => x.Field<string>(builder.SelfID).Equals(tempRow[builder.ParentID]));
                onGetFullPath(stack, row, table);
            }
        }
    }

    /// <summary>
    /// 用于创建文件目录的命令(接口)
    /// </summary>
    public interface IFoldsStructCommand
    {
        /// <summary>
        /// 创建一个用于FoldConstructor使用的文件夹结构表
        /// </summary>
        /// <param name="constructor">文件目录构建器(调用者)</param>
        /// <param name="table">存储待创建的文件目录的组织形式(双亲表示法)</param>
        void onCreateFoldStructionTable(FoldConstructor constructor, ref DataTable table);
    }

    /// <summary>
    /// 用于创建文件目录的命令(基类)
    /// </summary>
    public abstract class BaseFoldsStructCommand : IFoldsStructCommand
    {
        /// <summary>
        /// 创建一个用于FoldConstructor使用的文件夹结构表
        /// </summary>
        /// <param name="constructor">文件目录构建器(调用者)</param>
        /// <param name="table">存储待创建的文件目录的组织形式(双亲表示法)</param>
        public abstract void onCreateFoldStructionTable(FoldConstructor constructor, ref DataTable table);
        
        /// <summary>
        /// 向文件夹目录组织表中添加一个文件节点
        /// </summary>
        /// <param name="constructor">文件目录构建器(调用者)</param>
        /// <param name="table">存储待创建的文件目录的组织形式(双亲表示法)</param>
        /// <param name="selfID">当前节点ID</param>
        /// <param name="FoldName">文件夹名称</param>
        /// <param name="ParentID">父节点ID</param>
        /// <param name="IsChecked">是否选中</param>
        public virtual void onAddFoldNode(FoldConstructor constructor, ref DataTable table, int selfID, string FoldName, int ParentID, bool IsChecked = true)
        {
            DataRow tempRow = table.NewRow();
            tempRow[constructor.TableBuilder.SelfID] = selfID;
            tempRow[constructor.TableBuilder.Value] = FoldName;
            tempRow[constructor.TableBuilder.ParentID] = ParentID;
            tempRow[constructor.TableBuilder.IsChecked] = IsChecked;
            table.Rows.Add(tempRow);
        }
    }
}
