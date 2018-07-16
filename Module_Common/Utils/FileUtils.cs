using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class FileUtils
    {
        public string ErrorMessage { get; set; }

        public string[] splitFileFullPath(string FilePath)
        {
            List<string> result = new List<string>();
            FileInfo info = new FileInfo(FilePath);
            if (info.Exists)
            {
                result.Add(info.DirectoryName);
                result.Add(info.Name);
                result.Add(info.Extension);
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
                if(dirInfo.Exists)
                {
                    result.Add(dirInfo.Parent.FullName);
                    result.Add(dirInfo.Name);
                }
            }
            return result.ToArray();
        }

        public void DeleteDirectory(string FoldPath)
        {
            DirectoryInfo dir = new DirectoryInfo(FoldPath);
            foreach (FileInfo file in dir.GetFiles())
                file.Delete();
            foreach (var subDirs in dir.GetDirectories())
                DeleteDirectory(subDirs.FullName);
            dir.Delete();
        }


        public string AutoRenameFold(string FilePath)
        {
            string[] temps = splitFileFullPath(FilePath);
            int index = 1;
            do
            {
              string tempName =  temps[0]+"\\"+temps[1]+ string.Format("({0})", index);
              if (new DirectoryInfo(tempName).Exists)
                  index++;
              else
                  return tempName;
            } while (true);
        }

        public void GetSubFiles(string SourceFold, IList<string> OutFilePath, string searchPattern)
        {
            DirectoryInfo dir = new DirectoryInfo(SourceFold);
            foreach (var item in dir.GetFiles(searchPattern, SearchOption.TopDirectoryOnly))
                OutFilePath.Add(item.FullName);
        }

        public void GetSubDirectories(string SourceFold, IList<string> OutFilePath, string searchPattern = null)
        {
            if(SourceFold.EndsWith(":"))
                SourceFold = SourceFold + "\\";
            DirectoryInfo dir = new DirectoryInfo(SourceFold);
            if (string.IsNullOrEmpty(searchPattern) == false)
            {
                foreach (var item in dir.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly))
                    OutFilePath.Add(item.FullName);
            }
            else
            {
                foreach (var item in dir.GetDirectories())
                    OutFilePath.Add(item.FullName);
            }
        }

        public string[] ExtractFileName(IList<string> RasterPathInMap)
        {
            string[] result = new string[RasterPathInMap.Count];
            int index = 0;
            foreach (var item in RasterPathInMap)
            {
                result[index++] = splitFileFullPath(item)[1];   
            }
            return result;
        }

        internal void DeleteFileIfExsit(string ExportFilePath)
        {
            FileInfo info = new FileInfo(ExportFilePath);
            if (info.Exists)
                info.Delete();
        }


    }
}
