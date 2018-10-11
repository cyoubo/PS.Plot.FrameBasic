using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{
    /// <summary>
    /// 后台异步执行任务类
    /// </summary>
    public abstract class BackgroudTask 
    {
        /// <summary>
        /// Net 框架中用于执行UI级异步的帮助类对象
        /// </summary>
        protected BackgroundWorker mWorker;
        /// <summary>
        /// 进度条更新的命令
        /// </summary>
        public ProgressBarChangedCommand BarChangedCommand{get;set;}
        /// <summary>
        /// 任务状态更新的回调
        /// </summary>
        public TaskStateChangedUpdateCallBack TaskStateChangedUpdateCallBack { get; set; }

        /// <summary>
        /// 默认构造方法：用于构造异步组件
        /// </summary>
        public BackgroudTask()
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.ProgressChanged += backgroundWorker1_ProgressChanged;
            mWorker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            mWorker.DoWork += backgroundWorker1_DoWork;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            object Result = e.Result;
            onAsynTaskExcuting(e.Argument, e.Cancel, ref Result);
            e.Result = Result;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (TaskStateChangedUpdateCallBack != null)
                TaskStateChangedUpdateCallBack.onAsynTaskDateChanged(e.ProgressPercentage, e.UserState);
            if(BarChangedCommand!=null)
                BarChangedCommand.onProgressBarChanged(e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (TaskStateChangedUpdateCallBack != null)
            {
                if (e.Error != null)
                    TaskStateChangedUpdateCallBack.onAsynTaskError(e.Error, e.Result);
                else
                    TaskStateChangedUpdateCallBack.onAsynTaskSucceed(e.Result);
            }
        }

        /// <summary>
        /// 开始执行异步类
        /// </summary>
        /// <param name="StartParams">启动参数</param>
        public void Start(object StartParams=null)
        {
            if(StartParams==null)
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
        /// <summary>
        /// 发送异步更新请求
        /// </summary>
        /// <param name="precent">完成百分比1-100</param>
        /// <param name="Params">其他更新参数</param>
        protected void NotifyDataChanged(int precent,object Params)
        {
            this.mWorker.ReportProgress(precent, Params);
        }

        protected void NotifyDataChanged(int current,int total, object Params)
        {
            double precent = current * 1.0 / total * 1.0;
            this.mWorker.ReportProgress((int)precent, Params);
        }

        /// <summary>
        /// 在子线程中执行的方法(该方法内不能够修改UI等主线程对象)
        /// </summary>
        /// <param name="Argument">启动参数</param>
        /// <param name="Cancl">是否取消</param>
        /// <param name="Result">传出参数，返回异步处理记过</param>
        public abstract void onAsynTaskExcuting(object Argument, bool Cancl, ref object Result);
    }
    /// <summary>
    /// 进度条更新命令接口(一般由被更新的进度条型控件实现)
    /// </summary>
    public interface ProgressBarChangedCommand
    {
        /// <summary>
        /// 通知进度条更新的方法
        /// </summary>
        /// <param name="Percent">进度条的百分比</param>
        /// <param name="Value">其他参数</param>
        void onProgressBarChanged(int Percent,object Value=null);
    }
    /// <summary>
    /// 异步任务状态更新的回调(一般由被更新的UI窗体实现)
    /// </summary>
    public interface TaskStateChangedUpdateCallBack
    {
        /// <summary>
        /// 任务成功的回调
        /// </summary>
        /// <param name="Result">异步方法的结果</param>
        void onAsynTaskSucceed(object Result); 
        /// <summary>
        /// 任务异常的回调
        /// </summary>
        /// <param name="e">引起任务错误的异常</param>
        /// <param name="Result">异步方法的结果</param>
        void onAsynTaskError(Exception e, object Result);
        /// <summary>
        /// 发送状态更新信息
        /// </summary>
        /// <param name="Percent">百分比</param>
        /// <param name="Paras">其他参数</param>
        void onAsynTaskDateChanged(int Percent, object Paras);
    }
}
