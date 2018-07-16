using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class EnumUtils
    {
        /// <summary>
        /// 遍历枚举值
        /// </summary>
        /// <param name="enumElememt">待遍历的枚举值的某一个具体实例</param>
        /// <returns>枚举值名称所组成的列表</returns>
        public IList<string> TraverseEnum(Enum enumElememt)
        {
            IList<string> result = new List<string>();
            foreach (var name in Enum.GetNames(enumElememt.GetType()))
                result.Add(name);
            return result;
        }

        public string GetEnumdescription(Enum enumElememt)
        {
            Type type = enumElememt.GetType();
            MemberInfo[] memInfo = type.GetMember(enumElememt.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumElememt.ToString();
        }

        public string GetEnumdescriptionByName(Type EnumType, String enumString)
        {
            System.Enum enumElememt = (System.Enum)System.Enum.Parse(EnumType, enumString);
            Type type = enumElememt.GetType();
            MemberInfo[] memInfo = type.GetMember(enumElememt.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumElememt.ToString();
        }

        public IList<string> GetEnumdescriptions(Enum enumElememt)
        {
            IList<string> result = new List<string>();
            Type type = enumElememt.GetType();
            foreach (var item in TraverseEnum(enumElememt))
            {
                MemberInfo[] memInfo = type.GetMember(item);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                        result.Add(((DescriptionAttribute)attrs[0]).Description);
                }
                else
                    result.Add(item);

            }
            return result;
        }
    }
}
