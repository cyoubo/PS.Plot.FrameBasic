using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class SQLStatementUtils
    {


        public static string Clause_IN(string colName, string[] values)
        {
            StringBuilder builder = new StringBuilder();

            string[] temp = new string[values.Count()];
            for (int index = 0; index < values.Count(); index++)
                temp[index] = string.Format("'{0}'", values[index]);
            builder.Append(string.Format("{0} in ( {1} )", colName,string.Join(" , ",temp)));

            return builder.ToString();
        }

        public static string Clause_IN(string colName, int[] values)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("{0} in ( {1} )", colName, string.Join(" , ", values)));
            return builder.ToString();
        }

        public static string Clause_IN_Plenty(string colName, int[] values)
        {
            IList<string> result = new List<string>();
            IList<string> temp = new List<string>();
            int startIndex = 0;
            int endIndex = 1000;
            int valuesCount = values.Count();
            while (endIndex < valuesCount)
            {
                for (int index = startIndex; index < endIndex; index++)
                    temp.Add(string.Format("{0}", values[index]));
                result.Add(string.Format("{0} in ( {1} )", colName, string.Join(",", temp)));

                startIndex += 1000;
                endIndex = valuesCount - startIndex > 1000 ? endIndex + 1000 : endIndex + valuesCount - startIndex;
                temp.Clear();
            }

            temp.Clear();

            if (result.Count() > 1)
                return string.Join(" or ", result);
            else
                return Clause_IN(colName, values);
        }

        public static string Clause_IN_Plenty(string colName, string[] values)
        {
            IList<string> result = new List<string>();
            IList<string> temp = new List<string>();
            int startIndex = 0;
            int endIndex = 1000;
            int valuesCount = values.Count();
            while (endIndex < valuesCount)
            {
                for (int index = startIndex; index < endIndex; index++)
                    temp.Add(string.Format("'{0}'", values[index]));
                result.Add(string.Format("{0} in ( {1} )", colName, string.Join(",", temp)));

                startIndex += 1000;
                endIndex = valuesCount - startIndex > 1000 ? endIndex + 1000 : endIndex + valuesCount - startIndex;
                temp.Clear();
            }

            temp.Clear();

            if (result.Count() > 1)
                return string.Join(" or ",result);
            else
                return Clause_IN(colName, values);
        }

        public static string Caluse_OR_Like(string[] colName, string value)
        {
            IList<string> result = new List<string>();
            foreach (var item in colName)
                result.Add(string.Format("{0} Like '%{1}%'", colName, value));
            return string.Join(" or ", result);

        }
    }
}
