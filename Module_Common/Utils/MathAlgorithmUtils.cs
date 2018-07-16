using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    /// <summary>
    /// 基础数学算法帮助类
    /// </summary>
    public class MathAlgorithmUtils
    {
        public double Mean(IList<double> data)
        {
            return Double.Parse(data.Sum(x=>x).ToString()) / data.Count;
        }
        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="data">待计算方差的数据</param>
        /// <param name="IsSample">是否为样本方差，即总个数是否-1</param>
        /// <returns></returns>
        public double Var(IList<double> data, bool IsSample=true)
        {
            double mean = Mean(data);
            double sum = data.Sum(x => (x - mean) * (x - mean));
            return IsSample ? sum / (data.Count - 1) : sum / data.Count;
        }

        public double CoVar(IList<double> data1, IList<double> data2)
        {
            if (data1.Count != data2.Count)
                return Double.NaN;

            double mean1 = Mean(data1);
            double mean2 = Mean(data2);
            double sum = 0;
            for (int i = 0; i < data2.Count; i++)
                sum += (data2[i] * data1[i]);
            return sum / data2.Count - mean1 * mean2;
        }

        public double Coefficient(IList<double> data1, IList<double> data2)
        {
            double var1 = Var(data1, false);
            double var2 = Var(data2, false);
            double coVar = CoVar(data1, data2);
            return coVar / Math.Sqrt(var1 * var2);
        }

        public IList<double> convert(IList<int> data)
        {
            IList<double> result = new List<double>();
            foreach (var item in data)
                result.Add(item * 1.0);
            return result;
        }
    }
}
