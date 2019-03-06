using DevExpress.XtraGrid;
using PS.Plot.FrameBasic.Module_Common.Component.Adapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.GridControlViewState
{
    public abstract class GridControlViewStateProperties<E>
    {
        public delegate void HostControlUpdateHandler(object param);
        
        public event HostControlUpdateHandler HostControlUpdate;

        /// <summary>
        /// 辅助变量，判断当前状态是否为第一次执行
        /// </summary>
        public IDictionary<E, bool> IsStateFirstLoad { get; protected set; }

        public BaseDataTableBuilder TableBuilder { get; protected set; }

        public void Initial(BaseDataTableBuilder tableBuilder)
        {
            this.TableBuilder = tableBuilder;
            
            if (IsStateFirstLoad != null)
                IsStateFirstLoad.Clear();
            IsStateFirstLoad = new Dictionary<E, bool>();
            foreach (E state in Enum.GetValues(typeof(E)))
                IsStateFirstLoad.Add(state, true);
        }

        /// <summary>
        /// 通知更新
        /// </summary>
        /// <param name="param"></param>
        public void NotifyUpdateHostControl(object param)
        {
            if (HostControlUpdate != null)
                HostControlUpdate.Invoke(param);
        }
       
    }

    public abstract class GridControlViewStateContext<E>
    {
        protected abstract GridControlBaseViewState<E> onCreateStartStateComponet();

        public  GridControl HostControl { get; protected set; }

        public E CurrentState { get; protected set; }

        public GridControlBaseViewState<E> CurrentStateComponent { get; set; }

        public GridControlViewStateProperties<E> Properties { get; set; }

        public void ShiftState(E targetstate)
        {
            CurrentState = targetstate;
        }

        public virtual void Initial(GridControl gridControl, E initialState,GridControlViewStateProperties<E> properites)
        {
            this.HostControl = gridControl;
            this.CurrentState = initialState;
            this.Properties = properites;
            this.CurrentStateComponent = onCreateStartStateComponet();
        }

        public  void ExecuteStateShift()
        {
            if (CurrentStateComponent != null)
                CurrentStateComponent.OnShiftState(this);
        }

        public void FillData(DataTable table,bool notifyState = true)
        {
            this.HostControl.DataSource = table;
            if (notifyState)
                ExecuteStateShift();
        }
        
    }
}
