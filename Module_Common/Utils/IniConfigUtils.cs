using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class IniConfigUtils
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string ConfigFileName {set;get;}

        public string ConfigFilePath { set; get; }

        public IniConfigUtils(string ConfigFileName, string ConfigFilePath)
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

        public virtual bool ReadInI() { return false; }

        public virtual void WriteInI() { }

        private string onCombineFullPath()
        {
            return string.Format("{0}\\{1}", ConfigFilePath, ConfigFileName);
        }
    }
}
