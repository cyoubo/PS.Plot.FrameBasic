using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.Adapter
{
    /// <summary>
    /// DataTable数据反序列化执行器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseTableDeserializion<T>
    {
        /// <summary>
        /// 获取反序列化过程中的错误信息
        /// </summary>
        public string ErrorMessage { get; private set; }
        
        /// <summary>
        /// 反序列化结果
        /// </summary>
        private IList<T> deserializeResult;

        /// <summary>
        /// 获得反序列化结果
        /// </summary>
        public IList<T> DeserializeResult
        {
            get 
            {
                if (deserializeResult == null)
                    deserializeResult = new List<T>();
                return deserializeResult; 
            }
        }
        /// <summary>
        /// 外部方法：执行具体的反序列化过程
        /// </summary>
        /// <param name="builder">当前table的builder</param>
        /// <param name="table">待反序列化的table</param>
        /// <param name="otherParam">其他需要存入反序列化中的参数</param>
        /// <returns></returns>
        public bool ExcuteDesrialize(BaseDataTableBuilder builder, DataTable table, params object[] otherParam)
        {
            bool result = false;

            if (deserializeResult == null)
                deserializeResult = new List<T>();
            deserializeResult.Clear();

            try
            {
                foreach (DataRow item in table.Rows)
                    deserializeResult.Add(onDesrialize(builder, item, otherParam));
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 功能方法：执行具体的反序列化过程
        /// </summary>
        /// <param name="builder">当前table的builder</param>
        /// <param name="row">待反序列化的一行数据</param>
        /// <param name="otherParam">其他需要存入反序列化中的参数</param>
        /// <returns></returns>
        public abstract T onDesrialize(BaseDataTableBuilder builder,DataRow row, params object[] otherParam);
    }
}
