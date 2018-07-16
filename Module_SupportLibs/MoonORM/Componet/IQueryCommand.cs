using Moon.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Componet
{
    public interface IQueryCommand
    {
        MQLBase CreateQueryMQL();

        string GetOrderFieldName();
    }

    public interface IQueryCommandProvider
    { 
        IQueryCommand QueryCommand{ get; }
    }
}
