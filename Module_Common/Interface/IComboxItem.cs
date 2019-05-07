using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Interface
{
    interface IComboxItem
    {
        string DisplayText { get; set; }

        object Tag { get; set; }
    }
}
