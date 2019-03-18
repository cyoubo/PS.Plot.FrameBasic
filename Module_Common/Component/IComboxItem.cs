using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component
{
    public class ComboxItem
    {
        public string Text { get; set; }

        public string Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public interface IComboxItemSupport
    {
        ComboxItem ConvertToComboxItem();
    }
}
