using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Interface
{
    /// <summary>
    /// 宿主窗体关闭时的操作事件
    /// 1. 内容控件发起事件
    /// 2. 宿主窗体执行操作
    /// </summary>
    public delegate void OwnerClosedNotifer();
    
    /// <summary>
    /// 内容容器关闭的回调
    /// 1. 内容控件需要实现该接口
    /// 2. 宿主窗体需要该接口的属性并调用
    /// </summary>
    public interface IContentClosedCallBack
    {
        /// <summary>
        /// 相当于内容控件的Closed事件
        /// </summary>
        /// <remarks>特别注意该方法IContentClosedCallBack.NotifyOwnerClose的循环调用</remarks>
        void onOwnerNotifyClosed();
    }
}
