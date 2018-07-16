using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    /// <summary>
    /// 数组、集合处理工具类
    /// </summary>
    public class ArraysUtils
    {
        public IList<T> Sort<T>(IList<T> arrays, IComparer<T> comparer=null)
        {
            if (comparer == null)
                return arrays.OrderBy(x => x).ToList();
            else
                return arrays.OrderBy(x => x, comparer).ToList();
        }

        public IList<T> Intersect<T>(IList<T> data1, IList<T> data2,IEqualityComparer<T> Ecomparer=null)
        {
            if (Ecomparer == null)
                return data1.Intersect(data2).ToList();
            else
                return data1.Intersect(data2,Ecomparer).ToList();
        }

        public IList<T> Union<T>(IList<T> data1, IList<T> data2, IEqualityComparer<T> Ecomparer = null)
        {
            if (Ecomparer == null)
                return data1.Union(data2).ToList();
            else
                return data1.Union(data2, Ecomparer).ToList();
        }

        public IList<T> Except<T>(IList<T> data1, IList<T> data2, IEqualityComparer<T> Ecomparer = null)
        {
            if (Ecomparer == null)
                return data1.Except(data2).ToList();
            else
                return data1.Except(data2, Ecomparer).ToList();
        }

        public IList<T> Concat<T>(IList<T> data1, IList<T> data2)
        {
            return data1.Concat(data2).ToList();
        }

        public bool IsArraysElementsEqual<T>(IList<T> data1, IList<T> data2)
        {
            //采用Linq表达式判断两个数组的内容是否一致
            var q = from a in data1 join b in data2 on a equals b select a;
            return data2.Count == data1.Count && q.Count() == data2.Count;
        }

        public bool IsArraysElementsEqual<T>(IList<IEquatable<T>> data1, IList<IEquatable<T>> data2)
        {
            //采用Linq表达式判断两个数组的内容是否一致
            var q = from a in data1 join b in data2 on a equals b select a;
            return data2.Count == data1.Count && q.Count() == data2.Count;
        }

        public IList<T> RemoveElementsAt<T>(IList<T> data, params int[] Indexs)
        {
            IList<T> result = new List<T>();
            int index = 0;
            foreach (var item in data)
            {
                if (Indexs.Contains(index) == false)
                    result.Add(item);
            }
            return result;
        }

        public IList<T> ExtractElementsAt<T>(IList<T> data, params int[] Indexs)
        {
            IList<T> result = new List<T>();
            int index = 0;
            foreach (var item in data)
            {
                if (Indexs.Contains(index++))
                    result.Add(item);
            }
            return result;
        }

        public bool HasDuplicates<T>(IList<T> data)
        {
            
            return data.GroupBy(i => i).Where(g => g.Count() > 1).Count() >= 1;
        }
    }
}
