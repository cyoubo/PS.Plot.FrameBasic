using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Command
{
    /// <summary>
    /// 基础命令执行的指挥者
    /// </summary>
    public class BaseConductor
    {
        /// <summary>
        /// 获得执行命令过程中的异常信息
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// 执行具体的命令
        /// </summary>
        /// <param name="command">待执行的命令</param>
        /// <returns>若命令执行成功则返回true</returns>
        public bool Execute(IBaseCommand command)
        {
            bool result = true;
            try
            {
                onBeforeExcute();
                command.onBeforeExcuteCommand(this);
                command.onExcuteCommand(this);
                command.onAfterExcuteCommand(this);
                onAfterExcute();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 命令执行前指挥者本身执行的操作，基类为空实现
        /// </summary>
        protected virtual void onBeforeExcute(){ }
        /// <summary>
        /// 命令执行完成后指挥者本身执行的操作，基类为空实现
        /// </summary>
        protected virtual void onAfterExcute() { }
    }
}
