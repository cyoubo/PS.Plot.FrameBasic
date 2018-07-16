using PS.Plot.FrameBasic.Module_Common.Utils;
using PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.AppenderEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.Plot.FrameBasic.Module_SupportLibs.Log4Net.Utils.TemplateMethod
{
    public abstract class Log4NetTemplateMethod
    {
        public string ErrorMessage { get; protected set; }

        protected XDocument Document;

        protected IList<AppenderEntry> Entries;

        public Log4NetTemplateMethod()
        {
            Document = new XDocument();
            Entries = new List<AppenderEntry>();
        }

        public void AddAppendEntry(AppenderEntry appendEntry)
        {
            if (Entries.FindIndex(x => x.Name.Equals(appendEntry.Name)) == -1)
                Entries.Add(appendEntry);
        }

        public void RemoveAppendEntry(string AppendEntryName)
        {
            int index = Entries.FindIndex(x => x.Name.Equals(AppendEntryName));
            if (index >= 0)
                Entries.RemoveAt(index);
        }

        public bool WriteXML(string ExportFilePath)
        {
            bool result = false;
            try
            {
                XElement configSections = new XElement("configSections");
                XElement section = new XElement("section");
                section.SetAttributeValue("name", "log4net");
                section.SetAttributeValue("type", "log4net.Config.Log4NetConfigurationSectionHandler,log4net");
                configSections.Add(section);

                XElement log4net = new XElement("log4net");
                log4net.SetAttributeValue("debug", true);
                foreach (AppenderEntry entry in Entries)
                    log4net.Add(onCreateAppenderNode(entry));

                XElement root = new XElement("root");
                XElement level = new XElement("level");
                level.SetAttributeValue("value", "DEBUG");
                root.Add(level);
                foreach (AppenderEntry item in Entries)
                {
                    XElement appender_ref = new XElement("appender-ref");
                    appender_ref.SetAttributeValue("ref", item.Name);
                    root.Add(appender_ref);
                }


                XElement configuration = new XElement("configuration");
                configuration.Add(configSections);
                configuration.Add(log4net);
                configuration.Add(root);

                new FileUtils().DeleteFileIfExsit(ExportFilePath);

                Document.Add(configuration);
                Document.Save(ExportFilePath);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        protected XElement onCreateEmptyAppendNode(AppenderEntry entry)
        {
            XElement node = new XElement("appender");
            node.SetAttributeValue("name", entry.Name);
            node.SetAttributeValue("type", entry.Type);
            return node;
        }

        protected XElement onCreateValueNode(Enum name, object Value)
        {
            XElement result = new XElement(name.ToString());
            result.SetAttributeValue("value", Value);
            return result;
        }

        protected abstract XElement onCreateAppenderNode(AppenderEntry entry);
    }
}
