using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    /// <summary>
    /// 导航栏的帮助类
    /// </summary>
    public class NavigatorHelper
    {
        public  const string Tag_First = "First";
        public  const string Tag_Pre = "Pre";
        public  const string Tag_Next = "Next";
        public  const string Tag_Last = "Last";

        //被操作的导航栏控件
        private DataNavigator m_Navigator;
        //导航栏按钮监听事件
        private INavigatorClickCallBack m_CallBack;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="Navigator">被操作的导航栏控件</param>
        public NavigatorHelper(DataNavigator Navigator)
        {
            this.m_Navigator = Navigator;
        }
        /// <summary>
        /// 界面定制化显示
        /// </summary>
        /// <param name="callBack">导航栏事件监听</param>
        public void InitialUI(INavigatorClickCallBack callBack)
        {
            this.m_Navigator.Buttons.Append.Visible = false;
            this.m_Navigator.Buttons.CancelEdit.Visible = false;
            this.m_Navigator.Buttons.EndEdit.Visible = false;
            this.m_Navigator.Buttons.First.Visible = false;
            this.m_Navigator.Buttons.Last.Visible = false;
            this.m_Navigator.Buttons.Next.Visible = false;
            this.m_Navigator.Buttons.NextPage.Visible = false;
            this.m_Navigator.Buttons.Prev.Visible = false;
            this.m_Navigator.Buttons.PrevPage.Visible = false;
            this.m_Navigator.Buttons.Remove.Visible = false;
            if (this.m_Navigator.CustomButtons.Count == 0)
            {
                this.m_Navigator.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[]
                {
                    new DevExpress.XtraEditors.NavigatorCustomButton(-1, 0, true, true, "", Tag_First),
                    new DevExpress.XtraEditors.NavigatorCustomButton(-1, 1, true, true, "", Tag_Pre),
                    new DevExpress.XtraEditors.NavigatorCustomButton(-1, 4, true, true, "", Tag_Next),
                    new DevExpress.XtraEditors.NavigatorCustomButton(-1, 5, true, true, "", Tag_Last)
                });
            }

            this.m_Navigator.TextLocation = NavigatorButtonsTextLocation.Center;

            //注册监听事件
            if (this.m_CallBack == null)
            {
                this.m_CallBack = callBack;
                this.m_Navigator.ButtonClick += m_Navigator_ButtonClick;
            }
        }
        /// <summary>
        /// 更新导航栏提示信息
        /// </summary>
        /// <param name="currentPage">当前页码</param>
        /// <param name="countPage">总计页码</param>
        public void UpdateText(int currentPage, int countPage)
        {
            this.m_Navigator.TextStringFormat = string.Format("{0} / {1} 页 ", currentPage, countPage);
        }

        private void m_Navigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (m_CallBack != null)
            {
 	            switch(e.Button.Tag.ToString())
                {
                    case Tag_First: m_CallBack.onFirstButtonClick(); break;
                    case Tag_Last: m_CallBack.onLastButtonClick(); break;
                    case Tag_Pre: m_CallBack.onPreButtonClick(); break;
                    case Tag_Next: m_CallBack.onNextButtonClick(); break;
                }
            }
        }
    }

    /// <summary>
    /// 导航栏事件监听回调
    /// </summary>
    public interface INavigatorClickCallBack
    {
        void onFirstButtonClick();
        void onPreButtonClick();
        void onNextButtonClick();
        void onLastButtonClick();
    }
}
