using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileNameFilter
{
    public interface IFileEntrySupport
    {
        IFileEntrySupport CreateFromFullPath(string FullPath);
    }

    public static class FileTravelUtils
    {
        public static string ErrorMessage { get; private set; }

        public static IList<string> LoadFileEntry(string FileFold, String[] extension)
        {
            ErrorMessage = "";
            IList<string> result = new List<string>();
            try
            {
                onTravleFold(FileFold, ref result, extension);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        private static void onTravleFold(string dirs, ref IList<string> result, String[] extensions)
        {
            DirectoryInfo dir = new DirectoryInfo(dirs);
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)
                    onTravleFold(fsinfo.FullName, ref result, extensions);
                else
                {
                    foreach (var ext in extensions)
                        if (fsinfo.FullName.EndsWith(ext, StringComparison.CurrentCultureIgnoreCase))
                        {
                            result.Add(fsinfo.FullName);
                        }
                }
            }
        }

        public static IList<T> LoadFileEntry<T>(string FileFold, String[] extension) where T : IFileEntrySupport
        {
            ErrorMessage = "";
            IList<T> result = new List<T>();
            try
            {
                onTravleFold(FileFold, ref result, extension);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        private static void onTravleFold<T>(string dirs, ref IList<T> result, String[] extensions) where T : IFileEntrySupport
        {
            DirectoryInfo dir = new DirectoryInfo(dirs);
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)
                    onTravleFold(fsinfo.FullName, ref result, extensions);
                else
                {
                    T temp = (T)typeof(T).Assembly.CreateInstance(typeof(T).FullName);
                    foreach (var ext in extensions)
                        if (fsinfo.FullName.EndsWith(ext, StringComparison.CurrentCultureIgnoreCase))
                        {
                            result.Add((T)temp.CreateFromFullPath(fsinfo.FullName));
                        }
                }
            }
        }
    }
}
