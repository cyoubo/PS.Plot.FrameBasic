using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{
    /// <summary>
    /// 异步任务类基类，一般用于基础任务类的扩充，不建议直接作为业务异步任务类的基类
    /// </summary>
    public abstract class  AbstractBackgroudTask
    {
        /// <summary>
        /// Net 框架中用于执行UI级异步的帮助类对象
        /// </summary>
        protected BackgroundWorker mWorker;
        /// <summary>
        /// 默认构造方法：异步任务类基类，一般用于基础任务类的扩充，不建议直接作为业务异步任务类的基类
        /// </summary>
        public AbstractBackgroudTask()
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
        }
        /// <summary>
        /// 开始执行异步类
        /// </summary>
        /// <param name="StartParams">启动参数</param>
        public virtual void Start(object StartParams = null)
        {
            if (StartParams == null)
                mWorker.RunWorkerAsync();
            else
                mWorker.RunWorkerAsync(StartParams);
        }
        /// <summary>
        /// 取消异步类执行(该方法只是重置取消标识,并不停止任务执行)
        /// </summary>
        public void Cancl()
        {
            mWorker.CancelAsync();
        }
        /// <summary>
        /// 获取当前取消的标识符
        /// </summary>
        protected bool IsCancl { get { return mWorker.CancellationPending; } }
    }
}
