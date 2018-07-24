using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.DataBaseConnect
{
    /// <summary>
    /// 数据库连接字符串合成、解析执行器 
    /// </summary>
    public class ConnectStrInvoker
    {
        public const string Tag_DataSource = "Data Source";
        public const string Tag_Version = "Verison";
        public const string Tag_Server = "Server";
        public const string Tag_User = "User";
        public const string Tag_Password = "Password";
        public const string Tag_Database = "Database";
        public const string Tag_Port = "Port";
        public const string Tag_Charset = "Charset";

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectStr { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// 合成数据库连接字符串
        /// </summary>
        /// <param name="param">数据库连接信息对象</param>
        public void SerializationConnectStr(IConnectStrParam param)
        {
            param.Serialization(this);
        }
        /// <summary>
        /// 解析数据库连接字符串
        /// </summary>
        /// <param name="param">数据库连接信息对象(out)</param>
        public void DeSerializationConnectStr(IConnectStrParam param)
        {
            if (string.IsNullOrEmpty(ConnectStr) == false)
                param.DeSerialization(this);
        }
        /// <summary>
        /// 合成数据库连接字符串,并输出到项目的AppConfig文件中
        /// </summary>
        /// <param name="param">数据库连接信息对象</param>
        /// <param name="ConnectName">连接节点名</param>
        /// <param name="ProviderName">连接提供者</param>
        /// <returns></returns>
        public bool SerializationConnectStrToAppConfig(IConnectStrParam param, string ConnectName, string ProviderName)
        {
            bool result = false;
            try
            {
                
                SerializationConnectStr(param);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.ConnectionStrings.ConnectionStrings[ConnectName] != null)
                {
                    config.ConnectionStrings.ConnectionStrings[ConnectName].ProviderName = ProviderName;
                    config.ConnectionStrings.ConnectionStrings[ConnectName].ConnectionString = ConnectStr;
                }
                else
                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(ConnectName, ConnectStr, ProviderName));
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("ConnectionStrings");
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return result;
        }
        /// <summary>
        /// 解析数据库连接字符串,并输出到项目的AppConfig文件中
        /// </summary>
        /// <param name="param">数据库连接信息对象(out)</param>
        /// <param name="ConnectName">连接节点名</param>
        /// <returns></returns>
        public bool DeSerializationConnectStrFromAppConfig(IConnectStrParam param, string ConnectName)
        {
            bool result = false;
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.ConnectionStrings.ConnectionStrings[ConnectName] != null)
                {
                    ConnectStr = config.ConnectionStrings.ConnectionStrings[ConnectName].ConnectionString;
                    DeSerializationConnectStr(param);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return result;
        }

    }

    /// <summary>
    /// 数据库连接信息对象 统一接口
    /// </summary>
    public interface IConnectStrParam
    {
        /// <summary>
        /// 执行数据库连接字符串合成
        /// </summary>
        /// <param name="Invoker">用于记录合成的连接字符串</param>
        void Serialization(ConnectStrInvoker Invoker);
        /// <summary>
        /// 执行数据库连接字符串解析
        /// </summary>
        /// <param name="Invoker">用于提供合成的连接字符串</param>
        void DeSerialization(ConnectStrInvoker Invoker);
    }
}
