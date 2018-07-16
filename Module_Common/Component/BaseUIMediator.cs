using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component
{
    /// <summary>
    /// UI控件的调停者
    /// </summary>
    public class BaseUIMediator
    {
        #region 基础信息
        private IDictionary<string, object> mActors;

        public BaseUIMediator()
	    {
             mActors = new Dictionary<string, object>();
	    }

        public void AddActor(string tag, object actor)
        {
            
            if (mActors.Keys.Contains(tag))
                mActors.Remove(tag);
            mActors.Add(tag, actor);
        }

        public object GetActor(string tag)
        {
            object result = null;
            if (mActors.Keys.Contains(tag))
            {
                result = mActors[tag];
            }
            return result;
        }

        public object RemoveActor(string tag)
        {
            object result = null;
            if (mActors.Keys.Contains(tag))
            {
                result = mActors[tag];
                mActors.Remove(tag);
            }
            return result;
        }

        public void ClearActors()
        {
            if (mActors != null)
                mActors.Clear();
        }
        #endregion

        
    }
}
