using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.AppenderEntries
{
    public class AppenderEntry
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual void CreateDafault()
        {
            if (string.IsNullOrEmpty(Name))
                Name = Guid.NewGuid().ToString();
        }
    }
}
