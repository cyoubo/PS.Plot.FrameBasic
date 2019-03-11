using DevExpress.XtraGrid.Views.Base;
using PS.Plot.FrameBasic.Module_Common.Component.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.GridControlViewState
{
    public abstract class GridControlBaseViewState<E>
    {
        public abstract void OnShiftState(GridControlViewStateContext<E> context);

        public abstract E GetCurrentState();

        public GridControlViewStateProperties<E> ContextProperties { get; protected set; }

        /// <summary>
        /// 从HostControl中获得指定类型的视图对象
        /// </summary>
        /// <typeparam name="T">视图类型</typeparam>
        /// <param name="context">上下文环境</param>
        /// <param name="result">返回结果</param>
        /// <param name="name">视图名称(视图变量名)</param>
        /// <returns>当视图类型和视图变量名两个条件都满足时，返回true</returns>
        protected BaseView GetTargetView(GridControlViewStateContext<E> context,  string name)
        {
            for (int index = 0; index < context.HostControl.ViewCollection.Count; index++)
            {
                if (context.HostControl.ViewCollection[index].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return context.HostControl.ViewCollection[index];
            }
            return null;
        }

        /// <summary>
        /// 判断指定视图是否是当前gridControl的主视图
        /// </summary>
        /// <typeparam name="T">视图类型</typeparam>
        /// <param name="context">上下文环境</param>
        /// <param name="name">视图名称(视图变量名)</param>
        /// <returns></returns>
        protected bool IsMainViewTarget(GridControlViewStateContext<E> context, string name)
        {
            return context.HostControl.MainView.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        protected void onSaveContextProperties(GridControlViewStateProperties<E> properties, bool isoverride = false)
        {
            if (ContextProperties == null || isoverride)
                this.ContextProperties = properties;
        }

        protected void NotifyHostControlUpdate(object param)
        {
            if (this.ContextProperties != null)
                this.ContextProperties.NotifyUpdateHostControl(param);
        }
    }

    public abstract class GridControlBaseViewState2<E, V> : GridControlBaseViewState<E> where V : BaseView
    {
        protected V TargetView { get; set; }

        protected string TargetViewName { get; set; }

        public abstract GridControlBaseViewState<E> GetNextStateComponet();

        public abstract void onAfterMainViewUpdate(GridControlViewStateContext<E> context);

        public abstract string onGetCurrentViewName(GridControlViewStateProperties<E> properties);
        
        public virtual void onBeforeMainViewUpdate(GridControlViewStateContext<E> context) { }

        public virtual void onPrepareMainViewUpdate(GridControlViewStateContext<E> context) { }

        public virtual bool IsContextTargetState(GridControlViewStateContext<E> context)
        {
            return context.CurrentState.Equals(GetCurrentState());
        }

        public virtual B ConvertToTargetBuilder<B>() where B : BaseDataTableBuilder
        {
            return this.ContextProperties.TableBuilder as B;
        }

        public virtual P ConvertToTargetProperties<P>() where P : GridControlViewStateProperties<E>
        {
            return this.ContextProperties as P;
        }

        protected bool ExtractTagetView(GridControlViewStateContext<E> context, string name)
        {
            for (int index = 0; index < context.HostControl.ViewCollection.Count; index++)
            {
                if (context.HostControl.ViewCollection[index].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    TargetView = (V)context.HostControl.ViewCollection[index];
                    return true;
                }
            }
            return false;
        }

        public override void OnShiftState(GridControlViewStateContext<E> context)
        {
            
            if (IsContextTargetState(context) == false)
            {
                context.CurrentStateComponent = GetNextStateComponet();
                context.ExecuteStateShift();
            }
            else
            {
                onSaveContextProperties(context.Properties);
                onPrepareMainViewUpdate(context);
                TargetViewName = onGetCurrentViewName(context.Properties);
                if (IsMainViewTarget(context, TargetViewName) == false)
                {
                    if (ExtractTagetView(context, TargetViewName) && this.TargetView != null)
                    {
                        onBeforeMainViewUpdate(context);
                        context.HostControl.MainView = TargetView;
                        onAfterMainViewUpdate(context);
                        context.Properties.IsStateFirstLoad[GetCurrentState()] = false;
                    }
                }
                else
                {
                    if (context.Properties.IsStateFirstLoad[GetCurrentState()])
                    {
                        if (ExtractTagetView(context, TargetViewName) && this.TargetView != null)
                        {
                            onBeforeMainViewUpdate(context);
                            context.HostControl.MainView = TargetView;
                            onAfterMainViewUpdate(context);
                            context.Properties.IsStateFirstLoad[GetCurrentState()] = false;
                        }
                    }
                }
            }
        }
    }


    public abstract class GridControlBaseViewState3<E, V, B ,P> : GridControlBaseViewState2<E , V> where V : BaseView where B : BaseDataTableBuilder where P : GridControlViewStateProperties<E>
    {
        protected B TargetBuilder;

        protected P TargetProperties;

        public override void onPrepareMainViewUpdate(GridControlViewStateContext<E> context)
        {
            base.onPrepareMainViewUpdate(context);
            TargetBuilder = ConvertToTargetBuilder<B>();
            TargetProperties = ConvertToTargetProperties<P>();
        }
    }
}
