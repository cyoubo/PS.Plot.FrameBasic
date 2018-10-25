using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    /// <summary>
    /// 注册表读写帮助类:(使用此类时，请保证当前程序有管理员运行权限)
    /// </summary>
    public class RegistryUtils
    {
        public const string Tag_CustomNodeKeyName = "software\\RunningTimeConfig";
        public const string Tag_CustomKey_LastFoldBrowserPath = "LastFoldBrowserPath";
        /// <summary>
        /// 读取配置表值
        /// </summary>
        /// <param name="NodeKey">节点键名</param>
        /// <param name="Name">键名</param>
        /// <returns>若读取失败则返回空字符串</returns>
        public static string ReadRegistryKey(string NodeKey,string Name)
        {
            string result = "";
            try
            {
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software = key.CreateSubKey(NodeKey);
                result = software.GetValue(Name, "").ToString();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public static void WriteRegistryKey(string NodeKey, string Name,object value)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software = key.CreateSubKey(NodeKey);
                software.SetValue(Name, value);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public static void DeleteRegistryKey(string NodeKey, string Name)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine;
                key.CreateSubKey(NodeKey).DeleteSubKey(Name);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        /// <summary>
        /// 存入上一次文件夹浏览器所选择的路径
        /// </summary>
        /// <param name="path"></param>
        public static void WriteLastFoldBrowserPath(string path)
        {
            WriteRegistryKey(Tag_CustomKey_LastFoldBrowserPath, Tag_CustomNodeKeyName, path);
        }

        /// <summary>
        /// 获得上一次文件夹浏览器所选择的路径
        /// </summary>
        /// <returns>如果读取失败则返回空字符串</returns>
        public static string ReadLastFoldBrowserPath()
        {
            return ReadRegistryKey(Tag_CustomKey_LastFoldBrowserPath, Tag_CustomNodeKeyName);
        }
    }
}
