using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileNameFilter
{
    public class ContainsFileNameFilter : BaseFileNameFilter
    {
        public override bool OnReceive(string MetaFileName)
        {
            if (string.IsNullOrEmpty(this.Location))
            {
                return true;
            }
            else if (this.Location.Equals(FileNameFilterInvoker.Location_Head))
            {
                return SplitFullName(MetaFileName)[1].StartsWith(KeyWord);
            }
            else if (this.Location.Equals(FileNameFilterInvoker.Location_Tail))
            {
                return SplitFullName(MetaFileName)[1].EndsWith(KeyWord);
            }
            else
            {
                return SplitFullName(MetaFileName)[1].Contains(KeyWord);
            }
        }
    }
}
