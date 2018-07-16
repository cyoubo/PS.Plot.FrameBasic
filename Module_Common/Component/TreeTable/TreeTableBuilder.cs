using PS.Plot.FrameBasic.Module_Common.Component.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.TreeTable
{
    /// <summary>
    /// 树状控件内容表构建器
    /// </summary>
    public class TreeTableBuilder : BaseDataTableBuilder
    {
        public readonly string SelfID = "SelfID";
        public readonly string Value = "Value";
        public readonly string Tag = "Tag";
        public readonly string ParentID = "ParentID";

        protected override void AddDataColumn()
        {
            onCreateDataColumn(SelfID, typeof(string));
            onCreateDataColumn(Value, typeof(string));
            onCreateDataColumn(Tag, typeof(string));
            onCreateDataColumn(ParentID, typeof(string));
        }
    }

    /// <summary>
    /// 树状控件内容表构建器
    /// </summary>
    public class TreeCheckTableBuilder : TreeTableBuilder
    {
        public readonly string IsChecked = "IsChecked";

        protected override void AddDataColumn()
        {
            base.AddDataColumn();
            this.onCreateDataColumn(IsChecked, typeof(string));
        }
    }
}
