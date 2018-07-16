using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Entry
{
    /// <summary>
    /// 提供模板文档的AsposeWord数据实体
    /// </summary>
    public class AsposeWordTemplateEntry : AsposeWordEntry
    {
        /// <summary>
        /// 提供模板文件所在的路径
        /// </summary>
        /// <returns>默认返回运行目录下的Template.docx</returns>
        public virtual string GetTemplateFilePath() { return "Template.docx"; }
    }
}
