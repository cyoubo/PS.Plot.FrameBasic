using PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.IClauseConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.CaluseConvert
{
    public abstract class BaseClauseConvert : ISQLDateFunctionCovert
    {
        public abstract string convertDateBetweenSQL(string colName, DateTime start, DateTime end, bool ContainsTime);

        public abstract string convertDateEqualSQL(string colName, DateTime Date, bool ContainsTime);
    }
}