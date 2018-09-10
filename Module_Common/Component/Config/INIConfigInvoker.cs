using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Config
{
    public class INIConfigInvoker
    {
        public string ErrorMessage { get; protected set; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string ConfigFileName {set;get;}

        public string ConfigFilePath { set; get; }

        public INIConfigInvoker(string ConfigFileName, string ConfigFilePath)
        {
            this.ConfigFileName = ConfigFilePath;
            this.ConfigFilePath = ConfigFileName;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.onCombineFullPath());
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.onCombineFullPath());
            return temp.ToString();
        }

        public bool ExistINIFile()
        {
            return File.Exists(onCombineFullPath());
        }

        private string onCombineFullPath()
        {
            return System.IO.Path.Combine(ConfigFilePath, ConfigFileName);
        }

        public bool readConfigParam(ref IIniConfigParam param)
        {
            bool result = false;
            try
            {
                param.ReadIniConfig(this);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public bool saveConfigParam(IIniConfigParam param)
        {
            bool result = false;
            try
            {
                param.WriteIniConfig(this);
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
        void ReadIniConfig(INIConfigInvoker invoker);

        void WriteIniConfig(INIConfigInvoker invoker);
    }
}
