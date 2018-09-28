using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class ShareFoldUtils
    {
        public static string ErrorMessage { get; protected set; }

        public static bool CheckConnectable(string hostIP, string uesrName, string password,string foldName="")
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();

                string dosLine = string.Format(@"net use \\{0} {1}/user:{2}", hostIP, uesrName, password);
                if(string.IsNullOrEmpty(foldName) == false)
                    dosLine = string.Format(@"net use \\{0}\{3} {1}/user:{2}", hostIP, uesrName, password,foldName);
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                   ErrorMessage = errormsg;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }

        public static DirectoryInfo OpenShareFold(string hostIP, string FoldName)
        {
           return new DirectoryInfo(string.Format(@"\\{0}\{1}",hostIP,FoldName));
        }

        public static DirectoryInfo OpenShareFold(string hostIP)
        {
           return new DirectoryInfo(string.Format(@"\\{0}\{1}",hostIP));
        }

        public static bool Transfer2ShareFold(string srcFile, string HostIP, string FoldName)
        {
            FileStream inFileStream = null ;
            FileStream outFileStream = null;

            bool result = false;
            try
            {
                inFileStream = new FileStream(srcFile, FileMode.Open);
                DirectoryInfo targetFold = OpenShareFold(HostIP, FoldName);
                if (!targetFold.Exists)
                {
                    targetFold.Create();
                }
                string dst = System.IO.Path.Combine(targetFold.FullName, System.IO.Path.GetFileName(srcFile));
                if(File.Exists(dst))
                    File.Delete(dst);

                outFileStream = new FileStream(dst, FileMode.Create);
                byte[] buf = new byte[inFileStream.Length];
                int byteCount;
                while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
                {
                    outFileStream.Write(buf, 0, byteCount);
                }
                result = true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                inFileStream.Flush();
                inFileStream.Close();
                outFileStream.Flush();
                outFileStream.Close();
            }
            return result;
        }

        public static string CombineShareFoldPath(string srcFile, string HostIP, string FoldName)
        {
            return System.IO.Path.Combine(OpenShareFold(HostIP, FoldName).FullName, System.IO.Path.GetFileName(srcFile));
        }
    }
}
