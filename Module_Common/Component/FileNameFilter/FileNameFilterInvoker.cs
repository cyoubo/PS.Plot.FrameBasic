using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.FileNameFilter
{
    public interface IFileNameSupport
    {
        string FileFullPath { get; }
    }

    public delegate BaseFileNameFilter onCreateOtherTypeFileNameFilter(string typeName,BaseFileNameFilter baseFilter);
    

    public class FileNameFilterInvoker
    {
        public const string Location_Head = "前缀";
        public const string Location_Tail = "后缀";
        public const string Location_Middie = "中部";

        public const string Type_Contains = "包含";
        public const string Type_Except = "排除";

        public event onCreateOtherTypeFileNameFilter CreateOtherTypeFileNameFilterEvent;

        public IList<BaseFileNameFilter> Filters { get; protected set; }

        public void InitialFilter(string defalutJson)
        {
            if (Filters != null)
                Filters.Clear();
            Filters = new List<BaseFileNameFilter>();
            if (string.IsNullOrEmpty(defalutJson) == false)
            {
                var arrdata = Newtonsoft.Json.Linq.JArray.Parse(defalutJson);
                foreach (var item in arrdata.ToObject<List<BaseFileNameFilter>>())
                    onCreateFilter(item);
            }
        }
        
        protected void onCreateFilter(BaseFileNameFilter baseFilter)
        {
            if (baseFilter.Type.Equals(Type_Contains))
                Filters.Add(new ContainsFileNameFilter() { KeyWord = baseFilter.KeyWord, Location = baseFilter.Location, Type = baseFilter.Type });
            else 
                Filters.Add(new ExceptFileNameFilter() { KeyWord = baseFilter.KeyWord, Location = baseFilter.Location, Type = baseFilter.Type });
        }

        public void AddFilter(BaseFileNameFilter filter)
        { 
            if (Filters == null)
                Filters = new List<BaseFileNameFilter>();
            Filters.Add(filter);
        }

        public IList<T> ExcuteFilter<T>(IList<T> FileNames) where T : IFileNameSupport
        {
            IList<T> result = new List<T>();
            foreach (var metaFile in FileNames)
            {
                bool flag = true;
                foreach (var filter in Filters)
                {
                    flag = filter.OnReceive(metaFile.FileFullPath) && flag;
                    if (flag == false)
                        break;
                }

                if (flag)
                    result.Add((T)metaFile);
            }
            return result;
        }

        public IList<string> ExcuteFilter(IList<string> metaFiles)
        {
            IList<string> result = new List<string>();
            foreach (var metaFile in metaFiles)
            {
                bool flag = true;
                foreach (var filter in Filters)
                {
                    flag = filter.OnReceive(metaFile) && flag;
                    if (flag == false)
                        break;
                }

                if (flag)
                    result.Add(metaFile);
            }
            return result;
        }

        public string SerializeFilter()
        {
            if (Filters != null && Filters.Count > 0)
                return JsonConvert.SerializeObject(Filters, Formatting.Indented);
            else
                return "";
        }

        public static string[] TravelLocation()
        {
            return new string[] { Location_Head, Location_Middie, Location_Tail };
        }

        public static string[] TravelType()
        {
            return new string[] { Type_Contains, Type_Except };
        }

        public static string SerializeFilter(IList<BaseFileNameFilter> list)
        {
            if (list != null && list.Count > 0)
                return JsonConvert.SerializeObject(list, Formatting.Indented);
            else
                return "";
        }
    }
}
