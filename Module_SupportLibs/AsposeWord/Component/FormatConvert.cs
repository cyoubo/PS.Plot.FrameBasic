using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Component
{
    public class FormatConvert
    {
        private string wordFullPath;

        public FormatConvert(string wordFullPath)
        {
            this.wordFullPath = wordFullPath;
        }

        public bool ConvertToPDF(string ExportPath)
        {
            bool result = false;
            try
            {
                //读取doc文档
                Document doc = new Document(wordFullPath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                doc.Save(ExportPath, SaveFormat.Pdf);
                result = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public bool ConvertToDoc(string ExportPath)
        {
            bool result = false;
            try
            {
                //读取doc文档
                Document doc = new Document(wordFullPath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                doc.Save(ExportPath, SaveFormat.Doc);
                result = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public bool ConvertToDocx(string ExportPath)
        {
            bool result = false;
            try
            {
                //读取doc文档
                Document doc = new Document(wordFullPath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                doc.Save(ExportPath, SaveFormat.Docx);
                result = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }
    }
}
