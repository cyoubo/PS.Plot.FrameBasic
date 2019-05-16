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
        public const string RootNodeName = "Configs";

        public XmlDocument document { get; protected set; }
        public XmlNode rootNode { get; protected set; }

        public XMLConfigInvokerUtils InvokerUtils { get; private set; }

        public XMLConfigInvoker(string ConfigFileName, string ConfigFilePath)
        {
            this.ConfigFileName = ConfigFileName;
            this.ConfigFilePath = ConfigFilePath;
            
        }

        public XMLConfigInvoker(string ConfigFileFullName)
        {
            this.ConfigFileName = Path.GetFileName(ConfigFileFullName);
            this.ConfigFilePath = Path.GetDirectoryName(ConfigFileFullName);
        }

        public bool StartEdit(bool isOverrive = false)
        { 
            bool result = false;
            try
            {
                document = new XmlDocument();
                if (ExistConfigFile())
                {
                    if (isOverrive == false)
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
                        File.Delete(onCombineFullPath());
                        XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
                        rootNode = document.CreateElement(RootNodeName);
                        document.AppendChild(declaration);
                        document.AppendChild(rootNode);
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
                //构造工具类
                InvokerUtils = new XMLConfigInvokerUtils(this);
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

        //public bool ReadProperity<T>(ref T configParam)
        //{
        //    bool result = false;
        //    try
        //    {
        //        Type type = configParam.GetType();
        //        PropertyInfo[] propertyInfos = type.GetProperties();
        //        foreach (PropertyInfo propertyInfo in propertyInfos)
        //        {
        //            string value = ReadValue(propertyInfo.Name, "");
        //            propertyInfo.SetValue(configParam, value);
        //        }
        //        result = true;
        //    }
        //    catch(Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}

        //public bool WriteProperity<T>(T configParam)
        //{
        //    bool result = false;
        //    try
        //    {
        //        Type type = configParam.GetType();
        //        PropertyInfo[] propertyInfos = type.GetProperties();
        //        foreach (PropertyInfo propertyInfo in propertyInfos)
        //        {
        //            WriteValue(propertyInfo.Name, propertyInfo.GetValue(configParam).ToString());
        //        }
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}
    }

    public class XMLConfigInvokerUtils
    {
        private XMLConfigInvoker invoker;

        public XMLConfigInvokerUtils(XMLConfigInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void WriteValue(string ProperityName, string Value)
        {
            XmlNode node = invoker.document.SelectSingleNode("/" + XMLConfigInvoker.RootNodeName + "/" + ProperityName);
            if (node == null)
            {
                node = invoker.document.CreateNode(XmlNodeType.Element, ProperityName, null);
                node.InnerText = Value;
                invoker.rootNode.AppendChild(node);
            }
            else
            {
                node.InnerText = Value;
            }
        }

        public void WriteValue(string ProperityName, string Value, XmlNode parentNode)
        {
            XmlNode node = invoker.document.SelectSingleNode("/" + XMLConfigInvoker.RootNodeName + "/"  +ProperityName);
            if (node == null)
            {
                node = invoker.document.CreateNode(XmlNodeType.Element, ProperityName, null);
                node.InnerText = Value;
                parentNode.AppendChild(node);
            }
            else
            {
                node.InnerText = Value;
            }
        }

        public string ReadValue(string ProperityName, string defalutResult = "")
        {
            XmlNode node = invoker. document.SelectSingleNode("/" + XMLConfigInvoker.RootNodeName + "/" + ProperityName);
            return node == null ? defalutResult : node.InnerText;
        }

        public void ConvertToNodeAttribute<T>(XmlNode sign, T item, PropertyInfo[] propertyInfos, string[] IgnorePropNames = null)
        {
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (IgnorePropNames == null ||IgnorePropNames.Contains(propertyInfo.Name))
                    continue;
                XmlAttribute attr = invoker.document.CreateAttribute(propertyInfo.Name);
                object valueObj = propertyInfo.GetValue(item);
                attr.Value = valueObj == null ? "" : propertyInfo.GetValue(item).ToString();
                sign.Attributes.Append(attr);
            }
        }

        public XmlNode CreateSimpleNode(string nodeName, object value)
        {
            XmlNode result = invoker.document.CreateElement(nodeName);
            result.InnerText = value.ToString();
            return result;
        }
    }

    public interface IXmlConfigParam
    {
        void ReadConfig(XMLConfigInvoker invoker);

        void WriteConfig(XMLConfigInvoker invoker);
    }
}
