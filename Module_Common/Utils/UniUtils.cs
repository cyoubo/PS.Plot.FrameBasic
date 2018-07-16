using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class UniUtils
    {
       
        public string CreateFileDialogFileter(string[] description, string[] extents)
        {
            int count = description.Length > extents.Length ? description.Length : extents.Length;
            StringBuilder buider = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                buider.Append(String.Format("{0}|*.{1}", description[i], extents[i]));
                buider.Append("|");
            }
            return buider.Remove(buider.Length - 1, 1).ToString();
        }

        public string CreateFileDialogFileter(string description, string extents)
        {
            return String.Format("{0}|*.{1}", description, extents);
        }


    }
}
