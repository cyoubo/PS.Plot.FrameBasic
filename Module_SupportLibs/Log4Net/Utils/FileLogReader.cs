using log4net;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils
{
    public class FileLogReader
    {
        public static string ReadLogFile(ILog log,int loggerIndex = 0)
        {
            //获取文件日志输出器的对象
            RollingFileAppender append = log.Logger.Repository.GetAppenders()[loggerIndex] as RollingFileAppender;
            if (append != null)
            {
                //以非占用的形式打开
                using (FileStream fs = new FileStream(append.File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sw = new StreamReader(fs, Encoding.Default))
                    {
                        return  sw.ReadToEnd();
                    }
                }
            }
            return "不能打开日志文件";
        }

        public static string ReadLogFile(ILog log, Encoding readEncoding, int loggerIndex = 0)
        {
            //获取文件日志输出器的对象
            RollingFileAppender append = log.Logger.Repository.GetAppenders()[loggerIndex] as RollingFileAppender;
            if (append != null)
            {
                //以非占用的形式打开
                using (FileStream fs = new FileStream(append.File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sw = new StreamReader(fs, readEncoding))
                    {
                        return sw.ReadToEnd();
                    }
                }
            }
            return "不能打开日志文件";
        }
    }
}
