using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{
    /// <summary>
    /// 简单异步任务类：一般用于不处理进度条信息的异步任务类
    /// </summary>
    /// <typeparam name="TArgument">启动参数类型</typeparam>
    /// <typeparam name="TProgress">过程参数类型</typeparam>
    /// <typeparam name="TResult">结果参数类型</typeparam>
    public abstract class SimpleBackgroundTask<TArgument, TProgress, TResult> : AbstractBackgroudTask 
    {
        public SimpleTaskStateChangedUpdateCallBack<TProgress, TResult> SimpleTaskStateChangedUpdateCallBack { get; set; }

        public override void Start(object StartParams = null)
        {
            //如果是扩展类型回调，则先转换并调用其前置处理方法
            if (SimpleTaskStateChangedUpdateCallBack is SimpleTaskStateChangedUpdateCallBack2<TProgress, TResult>)
                ((SimpleTaskStateChangedUpdateCallBack2<TProgress, TResult>)SimpleTaskStateChangedUpdateCallBack).onBeforeStartAsynTask();
            //开始异步任务
            base.Start(StartParams);
        }

        public SimpleBackgroundTask(): base()
        {
            this.mWorker.DoWork += backgroundWorker1_DoWork;
            this.mWorker.ProgressChanged += backgroundWorker1_ProgressChanged;
            this.mWorker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TResult Result = (TResult)e.Result;
            TArgument Argument = (TArgument)e.Argument;
            onAsynTaskExcuting(Argument, e.Cancel, ref Result);
            e.Result = Result;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (SimpleTaskStateChangedUpdateCallBack != null)
            {
                TProgress prgress = (TProgress)e.UserState;
                SimpleTaskStateChangedUpdateCallBack.onAsynTaskDateChanged(prgress);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SimpleTaskStateChangedUpdateCallBack != null)
            {
                TResult Result = (TResult)e.Result;
                if (e.Error != null)
                    SimpleTaskStateChangedUpdateCallBack.onAsynTaskError(e.Error, Result);
                else
                    SimpleTaskStateChangedUpdateCallBack.onAsynTaskSucceed(Result);
            }
        }
        /// <summary>
        /// 发送异步更新请求
        /// </summary>
        /// <param name="precent">完成百分比1-100</param>
        /// <param name="Params">其他更新参数</param>
        protected void NotifyDataChanged(TProgress Params)
        {
            this.mWorker.ReportProgress(0, Params);
        }

        /// <summary>
        /// 在子线程中执行的方法(该方法内不能够修改UI等主线程对象)
        /// </summary>
        /// <param name="Argument">启动参数</param>
        /// <param name="Cancl">是否取消</param>
        /// <param name="Result">传出参数，返回异步处理记过</param>
        public abstract void onAsynTaskExcuting(TArgument Argument, bool Cancl, ref TResult Result);
    }


    /// <summary>
    /// 异步任务状态更新的回调(一般由被更新的UI窗体实现)
    /// </summary>
    public interface SimpleTaskStateChangedUpdateCallBack<TProgress, TResult>
    {
        /// <summary>
        /// 任务成功的回调
        /// </summary>
        /// <param name="Result">异步方法的结果</param>
        void onAsynTaskSucceed(TResult Result); 
        /// <summary>
        /// 任务异常的回调
        /// </summary>
        /// <param name="e">引起任务错误的异常</param>
        /// <param name="Result">异步方法的结果</param>
        void onAsynTaskError(Exception e, TResult Result);
        /// <summary>
        /// 发送状态更新信息
        /// </summary>
        /// <param name="Percent">百分比</param>
        /// <param name="Paras">其他参数</param>
        void onAsynTaskDateChanged(TProgress Paras);
    }
    /// <summary>
    /// 异步任务状态更新的回调2，支持前置事件处理(一般由被更新的UI窗体实现)
    /// </summary>
    public interface SimpleTaskStateChangedUpdateCallBack2<TProgress, TResult> : SimpleTaskStateChangedUpdateCallBack<TProgress, TResult>
    {
        /// <summary>
        /// 在异步任务开启前进行的操作
        /// </summary>
        void onBeforeStartAsynTask();
    }
}
