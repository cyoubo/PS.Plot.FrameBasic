using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.IClauseConvert
{
    public interface ISQLDateFunctionCovert
    {
        string convertDateBetweenSQL(string colName, DateTime start, DateTime end,bool ContainsTime);

        string convertDateEqualSQL(string colName, DateTime Date, bool ContainsTime);
    }
}
