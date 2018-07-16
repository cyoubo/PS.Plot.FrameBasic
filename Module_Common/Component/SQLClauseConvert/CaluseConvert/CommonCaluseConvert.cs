using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.CaluseConvert
{
    public class CommonCaluseConvert : BaseClauseConvert
    {
        public override string convertDateBetweenSQL(string colName, DateTime start, DateTime end, bool ContainsTime = false)
        {
            string format = ContainsTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd";
            return string.Format("{0} between '{1}' and '{2}'", colName, start.ToString(format), end.ToString(format));
        }

        public override string convertDateEqualSQL(string colName, DateTime Date, bool ContainsTime = false)
        {
            string format = ContainsTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd";
            return string.Format("{0} = '{1}' ", colName, Date.ToString(format));
        }
    }
}
