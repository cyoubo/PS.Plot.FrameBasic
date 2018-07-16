using PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.AppenderEntries;
using PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.EntryEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.TemplateMethod
{
    public class SimpleFileAppendTemplate : Log4NetTemplateMethod
    {
        protected override XElement onCreateAppenderNode(AppenderEntries.AppenderEntry entry)
        {
            string DatePattern = "yyyy_MM_dd";
            string Extent = ".log";
            string ExportFold = "";
            FileAppenderEntry target = entry as FileAppenderEntry;
            XElement appender = onCreateEmptyAppendNode(target);
            if (target.RollingStyle == FileAppenderRollingStyle.Size)
                appender.Add(onCreateValueNode(EnumFileAppenderTag.file, target.FileName));
            else
            {
                int DotIndex = target.FileName.IndexOf(".");
                int SplitIndex = target.FileName.LastIndexOf("\\");
                if (SplitIndex != -1)
                {
                    ExportFold = target.FileName.Substring(0, SplitIndex);
                    DatePattern = target.FileName.Substring(SplitIndex + 1, DotIndex);
                }
                else
                {
                    DatePattern = target.FileName.Substring(0, DotIndex);
                }
                Extent = target.FileName.Substring(DotIndex);
                appender.Add(onCreateValueNode(EnumFileAppenderTag.file, ExportFold));
            }
            
            appender.Add(onCreateValueNode(EnumFileAppenderTag.appendToFile,true));
            appender.Add(onCreateValueNode(EnumFileAppenderTag.rollingStyle, target.RollingStyle));
            if (target.RollingStyle == FileAppenderRollingStyle.Size)
            {
                appender.Add(onCreateValueNode(EnumFileAppenderTag.maximumFileSize, target.MaximumFileSize + "MB"));
                appender.Add(onCreateValueNode(EnumFileAppenderTag.staticLogFileName,true));
            }
            else
            {
                appender.Add(onCreateValueNode(EnumFileAppenderTag.DatePattern, DatePattern+Extent));
                appender.Add(onCreateValueNode(EnumFileAppenderTag.staticLogFileName, false));
            }
            appender.Add(onCreateValueNode(EnumFileAppenderTag.maxSizeRollBackups, target.MaxSizeRollBackups));

            XElement layout = new XElement(EnumFileAppenderTag.layout.ToString());
            layout.SetAttributeValue("type", "log4net.Layout.PatternLayout");
            layout.Add(onCreateValueNode(EnumFileAppenderTag.conversionPattern, target.LayoutPattern));
            appender.Add(layout);

            return appender;
        }
    }
}
