using PS.Plot.FrameBasic.Module_Common.Component.TreeTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.TreeTable
{
    /// <summary>
    /// 分组字段提取委托
    /// </summary>
    /// <typeparam name="T">与SelfCatalogTreeNodeCommand泛型一致的实体类</typeparam>
    /// <param name="data">实体对象</param>
    /// <returns>提取的结果字段</returns>
    public delegate string Del_ExtractGroupFieldValue<T>(T data);
    /// <summary>
    /// 分组字段提取器接口
    /// </summary>
    /// <typeparam name="T">与SelfCatalogTreeNodeCommand泛型一致的实体类</typeparam>
    public interface IGroupFieldExtractor<T>
    {
        /// <summary>
        /// 提取分组字段
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <returns>提取的结果字段</returns>
        string onExtractGroupFieldValue(T data);
    }

    /// <summary>
    /// 临时节点类实体
    /// </summary>
    public class TempNodeEntry
    {
        /// <summary>
        /// 双清表标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 双亲表值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 双亲表其他属性
        /// </summary>
        public string DataSerialization { get; set; }
    }

    /// <summary>
    /// 自属性分类双亲表构建器
    /// </summary>
    /// <typeparam name="T">属性实体</typeparam>
    public abstract class SelfCatalogTreeNodeCommand<T> : BaseBuildTreeNodeCommand
    {
        /// <summary>
        /// 标识符：设置当前Command是采用提取器还是提取事件
        /// </summary>
        public bool IsUseEctarctEvent { get; set; }
        /// <summary>
        /// 待填充的数据
        /// </summary>
        public IList<T> DataList { get; set; }

        protected IList<IGroupFieldExtractor<T>> Extractors { get; set; }

        protected event Del_ExtractGroupFieldValue<T> ExtractEvents;

        public override void onCreateTreeTable(TreeTableConductor invorker)
        {
            int selfID = 1;
            ISet<string> sets = new HashSet<string>();
            Dictionary<int, List<TempNodeEntry>> tempDict2 = new Dictionary<int, List<TempNodeEntry>>();
            //判别使用的是分组事件还是分组接口
            int groupCount = 0;
            if (IsUseEctarctEvent)
            {
                //添加分组字段提取器事件
                if (ExtractEvents != null)
                {
                    foreach (Delegate d in ExtractEvents.GetInvocationList())
                        ExtractEvents -= d as Del_ExtractGroupFieldValue<T>;
                }
                onBlindExtractEvent();
                groupCount = ExtractEvents.GetInvocationList().Count();
            }
            else
            {
                //添加分组字段提取器接口
                if (Extractors != null)
                    Extractors.Clear();
                Extractors = new List<IGroupFieldExtractor<T>>();
                onAddDataExtract(Extractors);
                groupCount = Extractors.Count();
            }
            
            TreeTableUtils tableUtils = invorker.convertToTreeTableUtils();
            //开始分等等级遍历
            for (int NodeLevel = 0; NodeLevel < groupCount; NodeLevel++)
            {
                //按照指定的分组方法，对每一各分组字段进行获取
                foreach (T item in DataList)
                {
                    string[] tags = new string[NodeLevel + 1];
                    if (IsUseEctarctEvent)
                    {
                        for (int index = 0; index < tags.Length; index++)
                            tags[index] = ExtractEvents.GetInvocationList()[index].DynamicInvoke(item).ToString();
                    }
                    else
                    {
                        for (int index = 0; index < tags.Length; index++)
                            tags[index] = Extractors[index].onExtractGroupFieldValue(item);
                    }

                    string tag = onFormatTag(tags);
                    int parentID = Tag_RootIndex;

                    if (NodeLevel != 0)
                    {
                        DataRow temp = tableUtils.FindNodeByTag(onFormatTag(tags.Take(NodeLevel).ToArray()));
                        if (temp == null)
                            continue;
                        parentID = int.Parse(temp[invorker.TableBuilder.SelfID].ToString());
                    }

                    if (tempDict2.Keys.Contains(parentID) == false)
                        tempDict2.Add(parentID, new List<TempNodeEntry>());

                    if (sets.Contains(tag) == false)
                    {
                        TempNodeEntry entry = new TempNodeEntry();
                        entry.Tag = tag;
                        entry.Value = tags[NodeLevel];
                        onCustomTempNodeEntry(ref entry, item, NodeLevel);
                        tempDict2[parentID].Add(entry);
                        sets.Add(tag);
                    }
                }

                foreach (int Key_parentID in tempDict2.Keys)
                {
                    foreach (var value_entry in SortTempNodeEntryBeforeAddToTree(tempDict2[Key_parentID], NodeLevel, Key_parentID))
                    {
                        DataRow tempRow = Conductor.ResultTable.NewRow();
                        tempRow[Conductor.TableBuilder.SelfID] = selfID++;
                        tempRow[Conductor.TableBuilder.Value] = value_entry.Value;
                        tempRow[Conductor.TableBuilder.ParentID] = Key_parentID;
                        tempRow[Conductor.TableBuilder.Tag] = value_entry.Tag;
                        //支持有其他属性的添加
                        onAddNodeEntryToDataRow(Conductor.TableBuilder, NodeLevel, tempRow, value_entry);
                        //添加到数据表
                        Conductor.ResultTable.Rows.Add(tempRow);
                    }
                }
                sets.Clear();
                tempDict2.Clear();
            }
        }
        protected string onFormatTag(params string[] tags)
        {
            return string.Join("#", tags);
        }
        /// <summary>
        /// 添加分组字段提取器
        /// </summary>
        /// <param name="Extracts"></param>
        public abstract void onAddDataExtract(IList<IGroupFieldExtractor<T>> Extracts);
        /// <summary>
        /// 绑定分组字段提取事件
        /// </summary>
        public abstract void onBlindExtractEvent();
        /// <summary>
        /// 依据item对临时节点对象entry进行调整
        /// 1. 默认是将item.toString值赋给entry.DataSerialization字段
        /// 2. 原则上除了最后一级节点，不要修改entry的Tage和value值
        /// </summary>
        /// <param name="entry">临时节点对象</param>
        /// <param name="item">填充元素</param>
        /// <param name="NodeLevel">分组等级（从0开始）</param>
        public virtual void onCustomTempNodeEntry(ref TempNodeEntry entry, T item, int NodeLevel)
        {
            entry.DataSerialization = item.ToString();
        }
        /// <summary>
        /// 添加TempNode到双亲表(默认情况是将entry.DataSerialization赋给tempRow[Conductor.TableBuilder.DataSerialization]列)
        /// </summary>
        /// <param name="treeTableBuilder">双亲表Builder</param>
        /// <param name="NodeLevel">分组等级（从0开始）</param>
        /// <param name="tempRow">双亲表目标行</param>
        /// <param name="entry">待添加TempNode对象</param>
        public virtual void onAddNodeEntryToDataRow(TreeTableBuilder treeTableBuilder, int NodeLevel, DataRow tempRow, TempNodeEntry entry)
        {
            tempRow[Conductor.TableBuilder.DataSerialization] = entry.DataSerialization;
        }
        /// <summary>
        /// 对待添加到树节点的前的TempNode对象进行排序(默认按照entry.value进行默认linq排序)
        /// </summary>
        /// <param name="entries">待排序的TempNode</param>
        /// <param name="NodeLevel"></param>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public virtual IList<TempNodeEntry> SortTempNodeEntryBeforeAddToTree(IList<TempNodeEntry> entries, int NodeLevel, int ParentID)
        {
            return entries.OrderBy(x => x.Value).ToList();
        }
    }
}
