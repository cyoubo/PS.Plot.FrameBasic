using PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Component;
using PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Controller
{
    /// <summary>
    /// AsposeWord操作word的统一接口
    /// </summary>
    /// <typeparam name="T">操作的实体类型</typeparam>
    public abstract class BaseController<T> where T : AsposeWordEntry, new()
    {
        /// <summary>
        /// AsposeWord操作帮助类
        /// </summary>
        protected WorkHelper mHelper;
        /// <summary>
        /// 获取执行过程中的异常
        /// </summary>
        public string ErrorMessage { get; protected set; }
        /// <summary>
        /// 被操作的数据实体
        /// </summary>
        public T Entry { get; protected set; }

        public BaseController()
        {
            Entry = new T();
        }
        /// <summary>
        /// 具体业务逻辑的执行方法接口
        /// </summary>
        /// <returns>若执行失败则返回false</returns>
        public abstract bool Excute();
    }
}
