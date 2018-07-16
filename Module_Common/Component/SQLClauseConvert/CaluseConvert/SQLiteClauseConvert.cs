using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.CaluseConvert
{
    public class SQLiteClauseConvert : BaseClauseConvert
    {

        public override string convertDateBetweenSQL(string colName, DateTime start, DateTime end, bool ContainsTime)
        {
            throw new NotImplementedException();
        }

        public override string convertDateEqualSQL(string colName, DateTime Date, bool ContainsTime)
        {
            throw new NotImplementedException();
        }
    }
}
