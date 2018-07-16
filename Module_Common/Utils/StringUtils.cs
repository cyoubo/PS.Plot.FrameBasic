using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    /// <summary>
    /// 字符串处理工具类
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// 用于判断，提取字符串尾数小数的正则表达式
        /// </summary>
        private readonly string m_TailNumberRegex = @"^(?<head>\w*?)(?<tail>\d+$)";
        /// <summary>
        /// 将数组对象拼接成字符串
        /// </summary>
        /// <param name="arrays">待拼接的数组</param>
        /// <param name="split">元素分隔符</param>
        /// <param name="prefix">结果字符串前缀</param>
        /// <param name="suffix">结果字符串后缀</param>
        /// <returns></returns>
        public string CombineArrays(Object[] arrays,String split=",",String prefix="",String suffix="")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(prefix);
            foreach (object element in arrays)
            {
                builder.Append(element.ToString());
                builder.Append(split);
            }
            if(builder.ToString().EndsWith(split))
                builder = builder.Remove(builder.Length - split.Count(), split.Count());
            return builder.Append(suffix).ToString();
        }

        public string CombineArrays(int[] arrays, String split = ",", String prefix = "", String suffix = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(prefix);
            foreach (object element in arrays)
            {
                builder.Append(element.ToString());
                builder.Append(split);
            }
            if (builder.ToString().EndsWith(split))
                builder = builder.Remove(builder.Length - split.Count(), split.Count());
            return builder.Append(suffix).ToString();
        }

        public string CombineArrays(double[] arrays, String split = ",", String prefix = "", String suffix = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(prefix);
            foreach (object element in arrays)
            {
                builder.Append(element.ToString());
                builder.Append(split);
            }
            if (builder.ToString().EndsWith(split))
                builder = builder.Remove(builder.Length - split.Count(), split.Count());
            return builder.Append(suffix).ToString();
        }

        /// <summary>
        /// 将ICustomToString接口数组对象拼接成字符串
        /// </summary>
        /// <param name="arrays">待拼接的数组</param>
        /// <param name="split">元素分隔符</param>
        /// <param name="prefix">结果字符串前缀</param>
        /// <param name="suffix">结果字符串后缀</param>
        /// <returns></returns>
        public string CombineArrays(ICustomToString[] arrays, String split = ",", String prefix = "", String suffix = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(prefix);
            foreach (ICustomToString element in arrays)
            {
                builder.Append(element.toCustomString());
                builder.Append(split);
            }
            if (builder.ToString().EndsWith(split))
                builder = builder.Remove(builder.Length - split.Count(), split.Count());
            return builder.Append(suffix).ToString();
        }
        /// <summary>
        /// 将hash表转换为指定格式的字符串
        /// </summary>
        /// <param name="HashMaps">待转换的hash表对象</param>
        /// <param name="split">键值对之间的分隔符</param>
        /// <returns></returns>
        public string ConvertHashMapToString(IDictionary<String, String> HashMaps, String split = ",")
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in HashMaps.Keys)
            {
                builder.Append("[");
                builder.Append(item);
                builder.Append("]=");
                builder.Append(HashMaps[item]);
                builder.Append(split);
            }
            builder = builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
        /// <summary>
        /// 判断指定字符是否是数字
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <param name="type">要判断的数字类型</param>
        /// <returns>如果是则返回true</returns>
        public bool IsNumber(string str, NumberType type)
        {
            String regexPatter = "";
            switch (type)
            {
                case NumberType.UnsignNumber: regexPatter = @"^?\d*[.]?\d*$"; break;
                case NumberType.Number: regexPatter = @"^[+-]?\d*[.]?\d*$";break;
                case NumberType.Integer: regexPatter= @"^[+-]?\d*$";break;
                case NumberType.UnsignInteger: regexPatter = @"^\d+$";break;
                case NumberType.Float: regexPatter = @"^[+-]?\d*\.\d*$";break;
                case NumberType.UnsignFloat: regexPatter = @"^\d*\.\d*$";break;
                default: regexPatter = @"^[+-]?\d*[.]?\d*$"; break;
            }
            return Regex.IsMatch(str, regexPatter);
        }
        /// <summary>
        /// 提取字符串后接的尾数
        /// </summary>
        /// <param name="str">待提取尾数的字符串(中间不能包含空格)</param>
        /// <returns></returns>
        public int ExtractTailIntegerNumber(string str)
        {
            Regex regex = new Regex(m_TailNumberRegex);
            Match result = regex.Match(str);
            if (result.Success)
                return Convert.ToInt32(result.Result("${tail}"));
            return 0;
        }
        /// <summary>
        /// 判断当前字符串是否以数字结尾
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <returns></returns>
        public bool IsEndWithIntegerNumber(string str)
        {
            return Regex.IsMatch(str, m_TailNumberRegex);
        }
        /// <summary>
        /// 字符串尾数加1
        /// </summary>
        /// <param name="str">待自增尾数的字符串</param>
        /// <returns>若尾数自增失败，则返回当前对象本身</returns>
        public string AddTailIntegerNumber(string str)
        {
            //前缀词采用非贪婪模式，保证尾数能够完全取出
            Regex regex = new Regex(m_TailNumberRegex);
            Match result = regex.Match(str);
            if (result.Success)
                return result.Result("${head}") + (Convert.ToInt32(result.Result("${tail}")) + 1);
            else
                return str;
        }

        /// <summary>    
        /// 字符串转为UniCode码字符串    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>    
        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("\\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        /// <summary>    
        /// Unicode字符串转为正常字符串    
        /// </summary>    
        /// <param name="srcText"></param>    
        /// <returns></returns>    
        public static string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }  


    }
    /// <summary>
    /// 忽略字符串大小的等值比较器
    /// </summary>
    public class StringEqualWithoutCase : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.ToUpper().Equals(y.ToUpper());
        }
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
    /// <summary>
    /// 自定义字符串持久化的接口
    /// </summary>
    public interface ICustomToString
    {
        string toCustomString();
    }
    /// <summary>
    /// 数字类型判断
    /// </summary>
    public enum NumberType
    {
        /// <summary>
        /// 所有数字，包括正负整数，正负小数
        /// </summary>
        Number,
        /// <summary>
        /// 所有整数，包括负数
        /// </summary>
        Integer,
        /// <summary>
        /// 所有非负整数，包括0
        /// </summary>
        UnsignInteger,
        /// <summary>
        /// 所有小数，保证负数小数
        /// </summary>
        Float,
        /// <summary>
        /// 所有非负小数，包括0
        /// </summary>
        UnsignFloat,
        /// <summary>
        /// 所有非负数字，包括整数、小数
        /// </summary>
        UnsignNumber
    }
}
