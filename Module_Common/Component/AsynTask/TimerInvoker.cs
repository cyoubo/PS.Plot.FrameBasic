using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{

    /// <summary>
    /// 基础定时器类
    /// </summary>
    public abstract class BaseTimerInvoker
    {
        /// <summary>
        /// System.Timers 命名空间下的定期器
        /// </summary>
        protected Timer mTimer;

        /// <summary>
        /// 定时器任务回调
        /// </summary>
        public ITimerInvokeCallBack TimerInvokeCallBack { get; protected set; }

        /// <summary>
        /// 构造方法 
        /// </summary>
        /// <param name="timespan">定式任务时间间隔，单位毫秒</param>
        public BaseTimerInvoker(int timespan)
        {
            mTimer = new Timer();
            mTimer.Interval = timespan;
            mTimer.AutoReset = true;
        }
        /// <summary>
        /// 开始执行任务
        /// </summary>
        public void Start()
        {
            if (mTimer != null)
                mTimer.Start();
        }
        /// <summary>
        /// 终止指定任务
        /// </summary>
        public void End()
        {
            if (mTimer != null)
                mTimer.Stop();
        }
    }

    /// <summary>
    /// 定时器功能回调
    /// </summary>
    public interface ITimerInvokeCallBack
    {
        /// <summary>
        /// 定期器回调任务
        /// </summary>
        /// <param name="args">回调事件参数</param>
        void onTimerElapsed(ElapsedEventArgs args);
    }

    /// <summary>
    /// 简单定时器（用于非UI修改的情景）
    /// </summary>
    public class SimpleTimerInvoker : BaseTimerInvoker
    {
        /// <summary>
        /// 简单定时器,构造方法
        /// </summary>
        /// <param name="timespan">定时任务时间间隔</param>
        public SimpleTimerInvoker(int timespan) : base(timespan) { }
        /// <summary>
        /// 注册定时任务
        /// </summary>
        /// <param name="callBack">定时任务回调(该回调的实现过程中不能够进行UI修改)</param>
        public virtual void RegistElapsedEvent (ITimerInvokeCallBack callBack)
        {
            TimerInvokeCallBack = callBack;
            mTimer.Enabled = true;
            mTimer.Elapsed += mTimer_Elapsed;
        }

        private void mTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (TimerInvokeCallBack != null)
            {
                TimerInvokeCallBack.onTimerElapsed(e);
            }
        }
    }
}
