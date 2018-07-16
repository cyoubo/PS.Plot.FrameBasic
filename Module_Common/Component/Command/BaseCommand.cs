using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Command
{
    /// <summary>
    /// 基础命令接口
    /// </summary>
    public interface IBaseCommand
    {
        /// <summary>
        /// 命令执行前操作，command内部的操作
        /// </summary>
        /// <param name="conductor">当前命令的指挥者</param>
        void onBeforeExcuteCommand(BaseConductor conductor);
        /// <summary>
        /// 命令具体操作
        /// </summary>
        /// <param name="conductor"></param>
        void onExcuteCommand(BaseConductor conductor);
        /// <summary>
        /// 命令执行后操作，command内部的操作
        /// </summary>
        /// <param name="conductor">当前命令的指挥者</param>
        void onAfterExcuteCommand(BaseConductor conductor);
    
    }
    /// <summary>
    /// 基础命令类
    /// </summary>
    public abstract class BaseCommand : IBaseCommand
    {
        /// <summary>
        /// 中间临时变量
        /// </summary>
        protected IDictionary<string, object> m_TempParams;
        /// <summary>
        /// 指挥者者
        /// </summary>
        protected BaseConductor conductor;
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

        public abstract void onExcuteCommand(BaseConductor conductor);
        /// <summary>
        /// 命令执行前操作，command内部的操作，重载时要调用父类实现
        /// </summary>
        /// <param name="conductor">当前命令的指挥者</param>
        public virtual void onBeforeExcuteCommand(BaseConductor conductor)
        {
            this.conductor = conductor;
        }
        /// <summary>
        /// 命令执行后操作，command内部的操作，重载时要调用父类实现
        /// </summary>
        /// <param name="conductor">当前命令的指挥者</param>
        public virtual void onAfterExcuteCommand(BaseConductor conductor)
        {
            ClearTemParams();
        }
    }
}
