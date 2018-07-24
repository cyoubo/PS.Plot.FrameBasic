using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class ControlAPI
    {

        public static void SelectedComboxItemByText(DevExpress.XtraEditors.ComboBoxEdit cmb, string target)
        {
            if (string.IsNullOrEmpty(target))
                cmb.SelectedIndex = -1;
            else
            {
                for (int index = 0; index < cmb.Properties.Items.Count; index++)
                {
                    if (target.Equals(cmb.Properties.Items[index].ToString()))
                    {
                        cmb.SelectedIndex = index;
                        return;
                    }
                }
                cmb.SelectedIndex = -1;
            }
        }

        internal static void SelectedRadioGroupByText(DevExpress.XtraEditors.RadioGroup rg_Catalog, string p)
        {
            throw new NotImplementedException();
        }

        public static void InsertTextInCursor(DevExpress.XtraEditors.TextEdit  textEdit ,string value)
        {
            string s = textEdit.Text;
            int idx = textEdit.SelectionStart;
            s = s.Insert(idx, value);
            textEdit.Text = s;
            textEdit.SelectionStart = idx + value.Length;
            textEdit.SelectionLength = 0;
            //textEdit.Focus();
        }

        public static void InsertTextBetweenCursor(DevExpress.XtraEditors.TextEdit textEdit, string value)
        {
            string s = textEdit.Text;
            int startIndex = textEdit.SelectionStart;
            s = s.Insert(startIndex, value);
            startIndex = startIndex + textEdit.SelectionLength + value.Length;
            s = s.Insert(startIndex, value);
            textEdit.Text = s;
            textEdit.SelectionStart = startIndex + value.Length;
            textEdit.SelectionLength = 0;
        }

        public static string[] GetCheckedComboxCheckedItemsText(DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit,char split=',')
        {
            string items = checkedComboBoxEdit.Properties.GetCheckedItems().ToString();
            List<string> result = new List<string>();

            if (items.Contains(split))
            {
                foreach (string i in items.Split(split))
                    result.Add(i);
            }
            else
                result.Add(items);
            return result.ToArray();
        }

        public static void SelectedRadioItemByText(DevExpress.XtraEditors.RadioGroup radioGroup, string content)
        {
            for (int index = 0; index < radioGroup.Properties.Items.Count; index++)
            {
                if (radioGroup.Properties.Items[index].Description.ToUpper().Equals(content.ToUpper()))
                {
                    radioGroup.SelectedIndex = index;
                    break;
                }
            }
        }

        public static void SelectedRadioItemByIndex(DevExpress.XtraEditors.RadioGroup radioGroup, long index)
        {
            radioGroup.SelectedIndex = (int)index;
        }

        public static void RemoveEmptyDate(DevExpress.XtraEditors.DateEdit dateEdit)
        {
            if (dateEdit.DateTime.Equals(new DateTime()))
                dateEdit.Text = "";
        }

        public static void InitialCombox(DevExpress.XtraEditors.ComboBoxEdit combox, IEnumerable<object> Elememets, int DefalutSelectedIndex = 0)
        {
            combox.Properties.Items.Clear();
            foreach (var item in Elememets)
                combox.Properties.Items.Add(item);
            if (combox.Properties.Items.Count > 0)
                combox.SelectedIndex = DefalutSelectedIndex;
        }
    }


}
