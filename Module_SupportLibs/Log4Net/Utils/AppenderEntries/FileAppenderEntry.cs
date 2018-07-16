using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.AppenderEntries
{
    public enum FileAppenderRollingStyle
    { 
        Date,Size,
    }

    public class FileAppenderEntry : AppenderEntry
    {
        public string FileName { get; set; }
        public FileAppenderRollingStyle RollingStyle { get; set; }
        public string LayoutPattern { get; set; }
        public int MaxSizeRollBackups { get; set; }
        public int MaximumFileSize { get; set; }

        public override void CreateDafault()
        {
            base.CreateDafault();
            Type = "log4net.Appender.RollingFileAppender";
            FileName = "ApplicationLog.log";
            RollingStyle = FileAppenderRollingStyle.Size;
            LayoutPattern = "[%date]  %thread -- %-5level -- %logger [%M] -- %message%newline";
            MaxSizeRollBackups = 10;
            MaximumFileSize = 2;
        }
    }
}
