using PS.Plot.FrameBasic.Module_Common.Component.TreeTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileOperator
{
    public class FileStructionTreeCheckNodeCommand : BaseBuildTreeCheckNodeCommand
    {
        private string RootPath;
        private Stack<int> stack;
        private string FileExtention;
        private int m_FileSelfID = -2;

        /// <summary>
        /// 用于完成将文件夹树状结构创建成TreeList控件可识别的树状结构的Command
        /// </summary>
        /// <param name="RootPath">根目录全路径</param>
        public FileStructionTreeCheckNodeCommand(string RootPath, string FileExtention = "*.*")
        {
            this.RootPath = RootPath;
            this.FileExtention = FileExtention;
            this.stack = new Stack<int>();
        }

        public override void onCreateTreeTable(TreeTableConductor invorker)
        {
            int selfID = 0;
            int parentID = -1;
            TravelFold2(RootPath, ref selfID, ref parentID);
        }
        //递归遍历
        private void TravelFold2(string Root, ref int selfID, ref int ParentID)
        {
            //访问节点
            DirectoryInfo root = new DirectoryInfo(Root);
            onAddTableRow(selfID, root.Name, ParentID);
            //遍历目录下的第
            foreach (var item in root.GetFiles(FileExtention,SearchOption.TopDirectoryOnly))
                onAddTableRow(m_FileSelfID--, item.Name, selfID);
            //将当前SelfID压入堆栈，以便作为下一层的ParentID
            stack.Push(selfID);
            //完成节点访问后，SelfID自增以标识
            selfID++;
            foreach (DirectoryInfo item in root.GetDirectories())
            {
                //进入下一层之前，获取栈顶值，作为默认ParentID,注意此处栈顶不出站
                ParentID = stack.Peek();
                //进入下一层
                TravelFold2(item.FullName, ref selfID, ref ParentID);
            }
            //返回到上一层或者当前节点没有子节点了，栈顶出站
            stack.Pop();
        }


    }
}
