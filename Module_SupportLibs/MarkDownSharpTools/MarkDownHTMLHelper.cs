using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MarkDownSharpTools
{
    public class MarkDownHTMLHelper
    {
        private Markdown markDownTool;

        private StringBuilder buider;
        private string bodyContent;
        private string headContent;

        public string MarkDownBodyStelyClass { get; private set; }

        public string CreateHTMLCode(string Content,bool IsNeedTranslate)
        {
            if (markDownTool == null)
                markDownTool = new Markdown();

            bodyContent = IsNeedTranslate ? markDownTool.Transform(Content) : Content;

            onProcessNewLineChar(ref bodyContent);

            buider = new StringBuilder();
            buider.Append("<!DOCTYPE html>");
            buider.Append("<html>");
            if (string.IsNullOrEmpty(headContent) == false)
            {
                buider.Append("<head><style type=\"text/css\">");
                buider.Append(headContent);
                buider.Append("</style><meta charset=\"utf-8\"/><title></title></head>");
            }
            if(string.IsNullOrEmpty(bodyContent) ==false)
                buider.Append(string.Format("<body class = \"{0}\">{1}</body>\n",MarkDownBodyStelyClass,bodyContent));
            buider.Append(@"</html>");
            return buider.ToString();
        }

        public void AddCss(string CssFile,string markDownBodyStelyClass ="markdown-body",bool isCssContent = false)
        {
            this.MarkDownBodyStelyClass = markDownBodyStelyClass;
            
            if (isCssContent == false)
                this.headContent = File.ReadAllText(CssFile);
            else
                this.headContent = CssFile;
        }

        private void onProcessNewLineChar(ref string content)
        {
            content = content.Replace("\n", "<br>").Replace("<br><br>", "<br>");
            Regex pattern = new Regex("><br>");
            content = pattern.Replace(content, ">");
        }
    }
}
