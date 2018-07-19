using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class DateUtils
    {
        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        public DateTime MonthFirstDay(DateTime dateTime)
        {
            return dateTime.AddDays(-dateTime.Day).AddDays(1);
        }

        public DateTime MonthLastDay(DateTime dateTime)
        {
            return dateTime.AddMonths(1).AddDays(-dateTime.Day);
        }

    }
}
