using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileOperator
{
    /// <summary>
    /// 文件输入输出命令执行器
    /// </summary>
    public class FileIOInvoker
    {
        /// <summary>
        /// 文件全路径
        /// </summary>
        private string FileFullName;
        /// <summary>
        /// 文件读取器
        /// </summary>
        public StreamWriter Writer { get; protected set; }
        /// <summary>
        /// 文件输出器
        /// </summary>
        public StreamReader Readar { get; protected set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; protected set; }

        private FileIOInvoker (string fullPath)
        {
            this.FileFullName = fullPath;
        }
        /// <summary>
        /// 依据绝对路径创建执行器
        /// </summary>
        /// <param name="FileFullName">绝对路径</param>
        /// <returns></returns>
        public static FileIOInvoker CreateFromAbusoluteFullPath(string FileFullName)
        {
            return new FileIOInvoker(FileFullName);
        }
        /// <summary>
        /// 依据相对路径创建执行器
        /// </summary>
        /// <param name="FileFullName"></param>
        /// <returns></returns>
        public static FileIOInvoker CreateFromRelativeFullPath(string FileFullName)
        {
            return CreateFromAbusoluteFullPath(Path.GetFullPath(FileFullName));
        }
        /// <summary>
        /// 执行读取命令
        /// </summary>
        /// <param name="command">读取命令</param>
        /// <param name="IsOverride">是否覆盖已存在的目标文件</param>
        /// <returns>若输出失败则返回false</returns>
        public bool ExecuteWriteCommand(IFileWriteCommand command,bool IsOverride = true)
        { 
            bool result = false;
            try
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FilePath);
                if (dir.Exists == false)
                    dir.Create();

                FileStream file = new FileStream(FileFullName, IsOverride ? FileMode.OpenOrCreate : FileMode.Append, FileAccess.Write);
                Writer = new StreamWriter(file);
                command.WriteToFile(this);
                Writer.Close();
                file.Close();
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// 执行读取命令
        /// </summary>
        /// <param name="command">读取命令</param>
        /// <returns>若读取失败在返回false</returns>
        public bool ExecuteReadCommand(IFileReadCommand command)
        {
            bool result = false;
            try
            {
                FileStream file = new FileStream(FileFullName,FileMode.Open, FileAccess.Read);
                Readar = new StreamReader(file);
                command.ReadFromFile(this);
                Readar.Close();
                file.Close();
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return result;
        }

        public string FilePath { get { return System.IO.Path.GetDirectoryName(FileFullName); } }
        public string FileName { get { return System.IO.Path.GetFileNameWithoutExtension(FileFullName); } }
        public string FileExtentsion { get { return System.IO.Path.GetExtension(FileFullName); } }
    }

    /// <summary>
    /// 文件输出命令
    /// </summary>
    public interface IFileWriteCommand
    {
       /// <summary>
       /// 具体的输出操作
       /// </summary>
       /// <param name="invoker">执行器</param>
       void WriteToFile(FileIOInvoker invoker);
    }
    /// <summary>
    /// 文件读取命令
    /// </summary>
    public interface IFileReadCommand
    {
        /// <summary>
        /// 具体的读取命令
        /// </summary>
        /// <param name="invoker">执行器</param>
        void ReadFromFile(FileIOInvoker invoker);
    }
}
