using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{
    /// <summary>
    /// UI修改定时器：用于完成包含UI修改的定时任务执行
    /// </summary>
    public class UITimerInvoker : BaseTimerInvoker
    {
        private delegate void DEL_onTimerElapsed(ElapsedEventArgs args);
        /// <summary>
        /// 容器控件
        /// </summary>
        public Control HostControl { get; protected set; }
        /// <summary>
        /// UI修改定时器,构造方法
        /// </summary>
        /// <param name="timespan">定时任务时间间隔</param>
        public UITimerInvoker(int timespan) : base(timespan) { }
        /// <summary>
        /// 注册定时任务
        /// </summary>
        /// <param name="hostControl">包含定时任务的宿主容器</param>
        /// <param name="callBack">定时任务回调(该回调的实现过程中可进行UI修改)</param>
        public void RegistElapsedEvent(Control hostControl, ITimerInvokeCallBack callBack)
        {
            HostControl = hostControl;
            TimerInvokeCallBack = callBack;

            mTimer.Enabled = true;
            mTimer.Elapsed += mTimer_Elapsed2;
        }

        private void mTimer_Elapsed2(object sender, ElapsedEventArgs e)
        {
            if (TimerInvokeCallBack != null)
                HostControl.Invoke(new DEL_onTimerElapsed(TimerInvokeCallBack.onTimerElapsed), e);
        }
    }
}
