using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PS.Plot.FrameBasic.Module_Common.Component.Config
{
    public class XMLConfigInvoker : BaseConfigFileInvoker
    {
        private const string RootNodeName = "Configs";

        private XmlDocument document;
        private XmlNode rootNode;

        public XMLConfigInvoker(string ConfigFileName, string ConfigFilePath)
        {
            this.ConfigFileName = ConfigFileName;
            this.ConfigFilePath = ConfigFilePath;
        }

        public bool StartEdit()
        { 
            bool result = false;
            try
            {
                document = new XmlDocument();
                if (ExistConfigFile())
                {

                    document.Load(onCombineFullPath());
                    rootNode = document.SelectSingleNode(RootNodeName);
                    if (rootNode == null)
                    {
                        ErrorMessage = "没有找到Configs节点";
                        return result;
                    }
                }
                else
                {
                    XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
                    rootNode = document.CreateElement(RootNodeName);
                    document.AppendChild(declaration);
                    document.AppendChild(rootNode);
                }
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
			
        }

        public bool EndEdit()
        {
            bool result = false;
            try
            {
                if (document != null)
                {
                    document.Save(onCombineFullPath());
                }
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public void WriteValue(string ProperityName, string Value)
        {
            XmlNode node = document.SelectSingleNode("/" + RootNodeName + "/" + ProperityName);
            if (node == null)
            {
                node = this.document.CreateNode(XmlNodeType.Element, ProperityName, null);
                node.InnerText = Value;
                this.rootNode.AppendChild(node);
            }
            else
            {
                node.InnerText = Value;
            }
        }

        public string ReadValue(string ProperityName,string defalutResult="")
        {
            XmlNode node = document.SelectSingleNode("/" + RootNodeName + "/" + ProperityName);
            return node == null ? defalutResult : node.InnerText;
        }

        public bool ReadConfigParam(IXmlConfigParam param)
        {
            bool result = false;
            try
            {
                param.ReadConfig(this);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public bool SaveConfigParam(IXmlConfigParam param)
        {
            bool result = false;
            try
            {
                param.WriteConfig(this);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public bool ReadProperity<T>(ref T configParam)
        {
            bool result = false;
            try
            {
                Type type = configParam.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string value = ReadValue(propertyInfo.Name, "");
                    propertyInfo.SetValue(configParam, value);
                }
                result = true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public bool WriteProperity<T>(T configParam)
        {
            bool result = false;
            try
            {
                Type type = configParam.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    WriteValue(propertyInfo.Name, propertyInfo.GetValue(configParam).ToString());
                }
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }
    }

    public interface IXmlConfigParam
    {
        void ReadConfig(XMLConfigInvoker invoker);

        void WriteConfig(XMLConfigInvoker invoker);
    }
}
