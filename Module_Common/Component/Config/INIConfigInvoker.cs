using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Config
{
    /// <summary>
    /// ini 配置文件读写执行器
    /// </summary>
    public class INIConfigInvoker : BaseConfigFileInvoker
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ConfigFileName">配置文件文件名(包含文件后缀)</param>
        /// <param name="ConfigFilePath">配置文件所在路径</param>
        public INIConfigInvoker(string ConfigFileName, string ConfigFilePath)
        {
            this.ConfigFileName = ConfigFileName;
            this.ConfigFilePath = ConfigFilePath;
        }


        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.onCombineFullPath());
        }

        public string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.onCombineFullPath());
            return temp.ToString();
        }

        public bool ReadConfigParam(IIniConfigParam param)
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

        public bool SaveConfigParam(IIniConfigParam param)
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

        public bool ReadProperity<T>(ref T configParam,string Section)
        {
            bool result = false;
            try
            {
                Type type = configParam.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string value = ReadValue(Section, propertyInfo.Name);
                    propertyInfo.SetValue(configParam, value);
                }
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        
        }

        public bool WriteProperity<T>(T configParam, string Section)
        {
            bool result = false;
            try
            {
                Type type = configParam.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    WriteValue(Section, propertyInfo.Name, propertyInfo.GetValue(configParam).ToString());
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

    public interface IIniConfigParam
    {
        void ReadConfig(INIConfigInvoker invoker);

        void WriteConfig(INIConfigInvoker invoker);
    }
}
