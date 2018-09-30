using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component
{
    public class Validator
    {
        public static void NotNullFinishValidate(string content, string contentName, ref string ErrorMessage)
        {
            if (string.IsNullOrEmpty(content))
                ErrorMessage += string.Format("请输入 {0} \n", contentName);
        }

        public static void EmptyDateValidate(DateTime dateTime, string contentName, ref string ErrorMessage)
        {
            if (dateTime.Equals(new DateTime()))
                ErrorMessage += string.Format("请输入 {0} \n", contentName);
        }

        public string ErrorMessage { get; protected set; }

        public void ClearValidateResult()
        {
            this.ErrorMessage = "";
        }

        public void NotNullFinishValidate(string content, string contentName)
        {
            if (string.IsNullOrEmpty(content))
                ErrorMessage += string.Format("请输入 {0} \n", contentName);
        }

        public void EmptyDateValidate(DateTime dateTime, string contentName)
        {
            if (dateTime.Equals(new DateTime()))
                ErrorMessage += string.Format("请输入 {0} \n", contentName);
        }

        public bool IsValidate { get { return string.IsNullOrEmpty(ErrorMessage); } }

        public void DateRangeValidate(DateTime dateTime1, DateTime dateTime2)
        {
            if (dateTime1.Equals(new DateTime()) == false && dateTime2.Equals(new DateTime()) == false)
            {
                if (dateTime1.CompareTo(dateTime2) > 0)
                    ErrorMessage += "日期范围错误 \n";
            }
        }



        public void DateGreaterTodayValidate(DateTime dateTime, string contentName)
        {
            if(dateTime.Date.CompareTo(DateTime.Now.Date) > 0)
                ErrorMessage += string.Format("{0} 不能大于本日日期 \n",contentName);
        }


    }
}
