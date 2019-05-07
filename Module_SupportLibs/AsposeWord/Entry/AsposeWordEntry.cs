using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Entry
{
    public class AsposeWordEntry
    {
        public string ExportPath { get; set; }

        private IDictionary<string, object> m_ContextDict = new Dictionary<string, object>();

        public object GetContext(string Tag)
        {
            if (m_ContextDict.Keys.Contains(Tag))
                return m_ContextDict[Tag];
            else
                return null;
        }

        public void AddContext(string Tag, object context)
        {
            if (m_ContextDict.Keys.Contains(Tag))
                m_ContextDict.Remove(Tag);
            m_ContextDict.Add(Tag, context);
        }

        public void ClearContext()
        {
            if (m_ContextDict != null)
                m_ContextDict.Clear();
        }
    }
}
