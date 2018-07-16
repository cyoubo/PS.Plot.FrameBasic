using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class DynamicCompilingUtils
    {
        public string ErrorMessage { get; protected set; }
        public string ClassName { get; protected set; }
        public string NameSpace { get; protected set; }
        public IList<string> SupportDLL { get; protected set; }
        public string CompilleContent { get; protected set; }

        
        public DynamicCompilingUtils()
        {
            SupportDLL = new List<string>();
            SupportDLL.Add("System.dll");
            SupportDLL.Add("System.Core.dll");
        }


        public object Compilling(string Content,bool isFilePath = false)
        {

            this.CompilleContent = isFilePath ? onExtractContentFromFile(Content):Content;

            object result = null;
            if (string.IsNullOrEmpty(CompilleContent))
            {
                ErrorMessage = "未指定编译内容";
                return result;
            }

            if (onExtractNameSpace())
            {
                ErrorMessage = "提取命名空间失败";
                return result;
            }

            if (onExtractClassName())
            {
                ErrorMessage = "提取命名类名失败";
                return result;
            }


            //创建编译器
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            //有警告错误
            //ICodeCompiler objICodeCompiler = new CSharpCodeProvider().CreateCompiler();

            //添加编译参数
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;
            foreach (var item in SupportDLL)
            {
                objCompilerParameters.ReferencedAssemblies.Add(item);
            }
            
            //编译代码
            CompilerResults cr = provider.CompileAssemblyFromSource(objCompilerParameters, CompilleContent);
            if (cr.Errors.HasErrors)
                ErrorMessage = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
            else
            {
                
                result = cr.CompiledAssembly.CreateInstance(NameSpace + "." + ClassName);
                if (result == null)
                    ErrorMessage = "反射构建对象失败";
            }
            return result;
        
        }

        private bool onExtractClassName()
        {
            Regex reg = new Regex(@"class (?<cn>\w*)");
            var matches = reg.Matches(CompilleContent);
            if (matches.Count != 1)
                return false;
            else
                ClassName = matches[0].Result("${cn}");
            return string.IsNullOrEmpty(ClassName);
        }

        private bool onExtractNameSpace()
        {
            Regex reg = new Regex(@"namespace (?<ns>[A-za-z0-9.]*)");
            var matches = reg.Matches(CompilleContent);
            if (matches.Count != 1)
                return false;
            else
                NameSpace = matches[0].Result("${ns}");
            return string.IsNullOrEmpty(NameSpace);
        }

        private string onExtractContentFromFile(string FilePath)
        {
            return File.ReadAllText(FilePath);
        }
    }
}
