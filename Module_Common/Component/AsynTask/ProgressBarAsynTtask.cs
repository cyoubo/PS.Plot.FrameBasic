
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_Common.Component.AsynTask
{
    /// <summary>
    /// 带有.Net进度条的异步类基类
    /// </summary>
    public abstract class ProgressBarAsynTtask : BackgroudTask
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Progressbar">Windows系统中的进度条对象</param>
        public ProgressBarAsynTtask(ProgressBar Progressbar)
        {
            //此处用了命令模式，以实现对象操作的反转，以便更好的与其他类型进度条控件集成
            BarChangedCommand = new ProgressBarUpdateCommand(Progressbar);
        }
        /// <summary>
        /// .Net进度条的更新命令
        /// </summary>
        public class ProgressBarUpdateCommand : ProgressBarChangedCommand
        {
            //具体的Net更新命令执行者
            private ProgressBar mProgressBar;
            /// <summary>
            /// 构造方法
            /// </summary>
            /// <param name="Progressbar">待更新的Net进度条</param>
            public ProgressBarUpdateCommand(ProgressBar Progressbar)
            {
                mProgressBar = Progressbar;
            }
            /// <summary>
            /// 进度条更新方法
            /// </summary>
            /// <param name="Percent"></param>
            /// <param name="Value"></param>
            public void onProgressBarChanged(int Percent, object Value = null)
            {
                mProgressBar.Value = Percent;
            }
        }
    }
}
