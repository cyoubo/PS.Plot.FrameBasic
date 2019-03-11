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
    public interface IMutiSetsConfigProperties
    {
        string ConfigFileName { get; set; }
        string RootNodeName { get; set; }
        string NodeName { get; set; }
        string InnerTextPropertyName { get; set; }
        string RootNodeDes { get; set; }
    }

    public class MutiSetsConfigProperties : IMutiSetsConfigProperties
    {
        public virtual string ConfigFileName {  get; set; }
        public virtual string RootNodeName { get; set; }
        public virtual string NodeName { get; set; }
        public virtual string InnerTextPropertyName { get; set; }
        public virtual string RootNodeDes { get; set; }
    }

    /// <summary>
    /// 规则型配置文件读取器
    /// </summary>
    public class MutiSetsConfigInvoker
    {
        private readonly string Description = "Description";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; protected set; }
        /// <summary>
        /// 读取器参数
        /// </summary>
        public IMutiSetsConfigProperties Properties { get; protected set; }
        /// <summary>
        /// 获得读取后的根节点描述
        /// </summary>
        public string RootNodeDescription { get; protected set; }
        /// <summary>
        /// 规则型配置文件读取器
        /// </summary>
        /// <param name="properties">读取参数</param>
        public MutiSetsConfigInvoker(IMutiSetsConfigProperties properties)
        {
            this.Properties = properties;
        }
        /// <summary>
        /// 规则型配置文件读取器，需要通过Properties属性对操作参数进行设置
        /// </summary>
        public MutiSetsConfigInvoker()
        {
            this.Properties = new MutiSetsConfigProperties();
        }

        /// <summary>
        /// 将规则列表输出到目标文件
        /// </summary>
        /// <typeparam name="E">规则实体类型</typeparam>
        /// <param name="ConfigSets">规则实体记录</param>
        /// <returns>若输出成功则返回true</returns>
        public bool Write<E>(IList<E> ConfigSets)
        {
            bool result = false;
            try
            {
                FileInfo configFile = new FileInfo(Properties.ConfigFileName);
                if (configFile.Exists)
                    configFile.Delete();

                XmlDocument document = new XmlDocument();
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
                XmlNode rootNode = document.CreateElement(Properties.RootNodeName);

                if (string.IsNullOrEmpty(Properties.RootNodeDes) == false)
                {
                    XmlAttribute attr = document.CreateAttribute(Description);
                    attr.Value = Properties.RootNodeDes;
                    rootNode.Attributes.Append(attr);
                }
                
                document.AppendChild(declaration);
                document.AppendChild(rootNode);

                foreach (E item in ConfigSets)
                {
                    
                    XmlNode node = document.CreateNode(XmlNodeType.Element, Properties.NodeName, null);
                    Type type = typeof(E);
                    foreach (PropertyInfo prop in type.GetProperties())
	                {
                        if (prop.Name.Equals(Properties.InnerTextPropertyName))
                            node.InnerText = prop.GetValue(item).ToString();
                        else
                        {
                            XmlAttribute attr = document.CreateAttribute(prop.Name);
                            attr.Value = prop.GetValue(item).ToString();
                            node.Attributes.Append(attr);
                        }
	                } 
                    rootNode.AppendChild(node);
                }

                document.Save(Properties.ConfigFileName);

                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 从目标文件中读取规则列表
        /// </summary>
        /// <typeparam name="E">规则实体类型</typeparam>
        /// <returns>若读取失败则返回长度为0的列表</returns>
        public  IList<E> Read<E>()
        {
            IList<E> result = new List<E>();
            try
            {
                FileInfo configFile = new FileInfo(Properties.ConfigFileName);
                if (configFile.Exists == false)
                {
                    ErrorMessage = "配置文件不存在";
                    return result;
                }

                XmlDocument document = new XmlDocument();
                document.Load(Properties.ConfigFileName);
                XmlNode rootNode = document.SelectSingleNode("/" + Properties.RootNodeName);
                if (rootNode == null)
                {
                    ErrorMessage = "配置文件格式不正确";
                    return result;
                }

                try
                {
                   RootNodeDescription = rootNode.Attributes[Description].ToString();
                }
                catch(Exception){ }

                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    if (node.Name.Equals(Properties.NodeName))
                    {
                        Type type = typeof(E);
                        object item = type.Assembly.CreateInstance(type.FullName);
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            if (prop.Name.Equals(Properties.InnerTextPropertyName))
                                prop.SetValue(item, node.InnerText);
                            else
                            {
                                try
                                {
                                    prop.SetValue(item, node.Attributes[prop.Name].Value);
                                }
                                catch (Exception)
                                {
                                    prop.SetValue(item, null);
                                }
                            }
                        }
                        result.Add((E)item);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
