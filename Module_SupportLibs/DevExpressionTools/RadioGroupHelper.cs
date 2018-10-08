using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PS.Plot.FrameBasic.Module_Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class RadioGroupHelper
    {
        public RadioGroup Radiogroup { get; protected set; }

        public RadioGroupHelper(RadioGroup radioGroup)
        {
            this.Radiogroup = radioGroup;
        }

        public void FillRadioButtonByEnum<T>(bool isShowInOneLine = true)
        {
            this.Radiogroup.Properties.Items.Clear();
            Type EnumType = typeof(T);
            EnumUtils utils = new EnumUtils();
            foreach (var temp in Enum.GetValues(EnumType))
            {
                RadioGroupItem item = new RadioGroupItem();
                item.Tag = Enum.GetName(EnumType, temp);
                item.Value = int.Parse(Enum.Format(EnumType, temp, "d"));
                item.Description = utils.GetEnumdescriptionByName(EnumType,item.Tag.ToString());
                this.Radiogroup.Properties.Items.Add(item);
            }

            if(isShowInOneLine)
                this.Radiogroup.Properties.Columns = this.Radiogroup.Properties.Items.Count;
        }

        public void FillRadioButtonByEnum(Enum defalutSelectItem, bool isShowInOneLine = true)
        {
            this.Radiogroup.Properties.Items.Clear();
            Type EnumType = defalutSelectItem.GetType();
            EnumUtils utils = new EnumUtils();

            int index = 0;
            foreach (var temp in Enum.GetValues(EnumType))
            {
                RadioGroupItem item = new RadioGroupItem();
                item.Tag = Enum.GetName(EnumType, temp);
                item.Value = int.Parse(Enum.Format(EnumType, temp, "d"));
                item.Description = utils.GetEnumdescriptionByName(EnumType, item.Tag.ToString());
                this.Radiogroup.Properties.Items.Add(item);
                if (Enum.GetName(EnumType, temp).Equals(Enum.GetName(EnumType, defalutSelectItem)))
                    Radiogroup.SelectedIndex = index;
                index++;
            }

            if (isShowInOneLine)
                this.Radiogroup.Properties.Columns = this.Radiogroup.Properties.Items.Count;
        }

        public T GetSelectItemAsEnum<T>()
        {
            string name = this.Radiogroup.Properties.Items[this.Radiogroup.SelectedIndex].Tag.ToString();
            return (T)Enum.Parse(typeof(T), name);
        }

        public string GetSelectItemDescription()
        {
            return this.Radiogroup.Properties.Items[this.Radiogroup.SelectedIndex].Description;
        }

        public int GetSelectItemValue()
        {
            return int.Parse(this.Radiogroup.Properties.Items[this.Radiogroup.SelectedIndex].Value.ToString());
        }

        public void SetRadioGroupColumnNums(int cols)
        {
            this.Radiogroup.Properties.Columns = cols;
        }

        public void SelectItemByEnumValue(long enumvalue)
        {
            Radiogroup.SelectedIndex = -1;
            for (int index = 0; index < this.Radiogroup.Properties.Items.Count; index++)
            {
                if (long.Parse(Radiogroup.Properties.Items[index].Value.ToString()) == enumvalue)
                    Radiogroup.SelectedIndex = index;
            }
        }

        public void SelectItemByEnumDescription(string enumvalue)
        {
            Radiogroup.SelectedIndex = -1;
            for (int index = 0; index < this.Radiogroup.Properties.Items.Count; index++)
            {
                if (Radiogroup.Properties.Items[index].Description.ToString().Equals(enumvalue))
                    Radiogroup.SelectedIndex = index;
            }
        }
    }

    
}
