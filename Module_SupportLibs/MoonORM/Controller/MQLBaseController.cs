
using PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    public class MQLBaseController
    {
        protected IDataBaseFactory dbFactory;

        protected string m_ErrorMessage;

        public IDataBaseFactory DbFactory
        {
            set { dbFactory = value; }
        }

        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
        }
    }
}
