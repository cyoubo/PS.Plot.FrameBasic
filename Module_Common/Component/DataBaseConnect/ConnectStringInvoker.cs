using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.DataBaseConnect
{
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

        public string ConnectStr { get; set; }
        public string ErrorMessage { get; protected set; }

        public void SerializationConnectStr(IConnectStrParam param)
        {
            param.Serialization(this);
        }

        public void DeSerializationConnectStr(IConnectStrParam param)
        {
            if (string.IsNullOrEmpty(ConnectStr) == false)
                param.DeSerialization(this);
        }

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

    public interface IConnectStrParam
    {
        void Serialization(ConnectStrInvoker Invoker);

        void DeSerialization(ConnectStrInvoker Invoker);
    }
}
