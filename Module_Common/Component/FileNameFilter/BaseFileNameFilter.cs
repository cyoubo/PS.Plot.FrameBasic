using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileNameFilter
{
    public class BaseFileNameFilter
    {
        public String Location { get; set; }

        public String Type { get; set; }

        public String KeyWord { get; set; }

        public virtual bool OnReceive(string MetaFileName) { return true; }

        protected string[] SplitFullName(string MetaFileName)
        {
            string[] result = new string[3];
            result[0] = System.IO.Path.GetDirectoryName(MetaFileName);
            result[1] = System.IO.Path.GetFileNameWithoutExtension(MetaFileName);
            result[2] = System.IO.Path.GetExtension(MetaFileName);
            return result;
        }
    }
}
