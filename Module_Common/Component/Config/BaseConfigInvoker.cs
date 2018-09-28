using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Config
{
    /// <summary>
    /// 基础配置文件执行器
    /// </summary>
    public class BaseConfigFileInvoker
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// 配置文件名（包含文件后缀）
        /// </summary>
        public string ConfigFileName { set; get; }

        /// <summary>
        /// 配置文件所在文件夹
        /// </summary>
        public string ConfigFilePath { set; get; }

        /// <summary>
        /// 判断当前文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistConfigFile()
        {
            return File.Exists(onCombineFullPath());
        }

        /// <summary>
        /// 合成配置文件全路劲
        /// </summary>
        /// <returns></returns>
        protected virtual string onCombineFullPath()
        {
            return System.IO.Path.Combine(ConfigFilePath, ConfigFileName);
        }
    }
}
