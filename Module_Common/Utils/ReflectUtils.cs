using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class ReflectUtils
    {
        public static IList<string> ExtractNameSpace(string dllFilePath)
        {
            ISet<string> result = new HashSet<string>();
            Assembly assem = Assembly.UnsafeLoadFrom(dllFilePath);
            foreach (Type item in assem.GetTypes())
                result.Add(item.Namespace);
            return result.ToList();
        }
    }
}
