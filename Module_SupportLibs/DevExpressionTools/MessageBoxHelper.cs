using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_System.DevExpressionTools
{
    public class MessageBoxHelper
    {
        public static void ShowDialog(string title, string Message , MessageBoxButtons button = MessageBoxButtons.OK,MessageBoxIcon Icon =MessageBoxIcon.Information)
        {
            XtraMessageBox.Show(Message, title, button, Icon);
        }

        public static void ShowInputErrorDialog(string Message)
        {
            XtraMessageBox.Show(Message, "参数错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public static DialogResult ShowCreateStateDialog(bool IsSuccess)
        {
            string messeage= IsSuccess?"创建成功":"创建失败";
            MessageBoxIcon icon = IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            return XtraMessageBox.Show(messeage, "提示", MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowCreateStateDialog(bool IsSuccess, string ErrorMessage)
        {
            MessageBoxIcon icon = IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            if (IsSuccess)
                return XtraMessageBox.Show("创建成功", "提示", MessageBoxButtons.OK, icon);
            else
                return XtraMessageBox.Show(ErrorMessage, "创建失败", MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowDeleteStateDialog(bool IsSuccess)
        {
            string messeage = IsSuccess ? "删除成功" : "删除失败";
            MessageBoxIcon icon = IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            return XtraMessageBox.Show(messeage, "提示", MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowUpdateStateDialog(bool IsSuccess)
        {
            string messeage = IsSuccess ? "更新成功" : "更新失败";
            MessageBoxIcon icon = IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            return XtraMessageBox.Show(messeage, "提示", MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowDeleteInfoDialog()
        {
            return XtraMessageBox.Show("是否删除该条记录", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static DialogResult ShowQueryFailedStateDialog()
        {
            return XtraMessageBox.Show("查询失败", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }


        public static bool ShowYesOrNoDialog(string title, string content)
        {
            return XtraMessageBox.Show(content, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes ? true : false;
        }

        public static void ShowErrorDialog(string content,string title = "错误")
        {
            XtraMessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowSaveStateDialog(bool IsSuccess)
        {
            string messeage = IsSuccess ? "保存成功" : "保存失败";
            MessageBoxIcon icon = IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            return XtraMessageBox.Show(messeage, "提示", MessageBoxButtons.OK, icon);
        }
    }
}
