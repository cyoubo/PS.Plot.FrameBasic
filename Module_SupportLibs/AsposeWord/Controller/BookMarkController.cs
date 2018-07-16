using PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Component;
using PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Controller
{
    /// <summary>
    /// 执行书签输出的控制器
    /// </summary>
    /// <typeparam name="T">被操作的输出的数据实体</typeparam>
    public abstract class BookMarkController<T> : BaseController<T> where T : AsposeWordTemplateEntry, new()
    {

        public override bool Excute()
        {
            bool result = true;
            try
            {
                //删除已存在的文档
                if (System.IO.File.Exists(Entry.ExportPath))
                    System.IO.File.Delete(Entry.ExportPath);
                //拷贝模板word
                System.IO.File.Copy(Entry.GetTemplateFilePath(), Entry.ExportPath, true);
                //开启编辑
                mHelper = new WorkHelper(Entry.ExportPath);
                //执行自定义写入方法
                onWriteCustomContext(Entry);
                //保存
                mHelper.Save();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 实现具体的输出业务逻辑
        /// </summary>
        /// <param name="Entry">待输出的数据实体</param>
        protected abstract void onWriteCustomContext(T Entry);
    }
}
