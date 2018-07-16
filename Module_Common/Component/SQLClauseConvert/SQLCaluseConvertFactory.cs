
using PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.CaluseConvert;
using PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert.IClauseConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.SQLClauseConvert
{
    public class SQLCaluseConvertFactory
    {
        public static ISQLDateFunctionCovert CreateDataFunctionConvert(string DBTypeName)
        {
            switch (DBTypeName.Clone().ToString().ToLower())
            {
                case "oracle": return new OracleClauseConvert(); 
                case "mysql": return new MySQLClauseConvert(); 
                case "sqlite": return new SQLiteClauseConvert();
                default: return new CommonCaluseConvert() ;
            }
        }
    }
}
