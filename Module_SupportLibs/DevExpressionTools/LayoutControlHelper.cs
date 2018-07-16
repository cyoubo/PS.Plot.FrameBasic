using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools
{
    public class LayoutControlHelper
    {
        public LayoutControl RootLayoutControl { private set; get; }
        public LayoutControlGroup RootLayoutGroup { private set; get; }

        private int LayoutControlItemStartHeight = 0;

        public LayoutControlHelper(LayoutControl rootLayout)
        {
            this.RootLayoutControl = rootLayout;
            this.RootLayoutGroup = rootLayout.Root;
        }

        //public void StartEdit()
        //{
        //    this.RootLayoutControl.BeginUpdate();
        //}

        //public void StopEdit()
        //{
        //    this.RootLayoutControl.EndUpdate();
        //}

        public void AddUserControl(string title, XtraUserControl control,string tag = "")
        {
            
            RootLayoutControl.Controls.Add(control);

            try
            {
                LayoutControlItem controlItem = new LayoutControlItem();
                controlItem.Control = control;
                controlItem.Location = new System.Drawing.Point(0, LayoutControlItemStartHeight);
                controlItem.Name = string.IsNullOrEmpty(tag) ? title : tag;
                controlItem.Size = new System.Drawing.Size(RootLayoutGroup.Width - RootLayoutGroup.Padding.Left - RootLayoutGroup.Padding.Right, control.Height);
                controlItem.Text = title;
                controlItem.TextSize = new System.Drawing.Size(72, 22);
                RootLayoutGroup.Items.AddRange(new BaseLayoutItem[] { controlItem });

                LayoutControlItemStartHeight += controlItem.Height;
            }
            catch (Exception ex)
            {
                string messa = ex.Message;
            }
        }

        public void RemoveUserControl(string tag)
        {
            BaseLayoutItem currentItem =  RootLayoutGroup.Items.FirstOrDefault(x => x.Name.Equals(tag));
            if (currentItem != null)
            {
                LayoutControlItem controlItem = currentItem as LayoutControlItem;
                RootLayoutControl.Controls.Remove(controlItem.Control);
                RootLayoutGroup.Remove(currentItem);
            }
        }

        public void ClearUserControl()
        {
            RootLayoutGroup.Clear();
            RootLayoutControl.Controls.Clear();
        }

        public void HideUserControl(string tag)
        {
            LayoutControlItem currentItem = RootLayoutGroup.Items.FirstOrDefault(x => x.Name.Equals(tag)) as LayoutControlItem;
            if (currentItem != null)
                currentItem.ContentVisible = false;  
        }

        public void ShowUserControl(string tag)
        {
            LayoutControlItem currentItem = RootLayoutGroup.Items.FirstOrDefault(x => x.Name.Equals(tag)) as LayoutControlItem;
            if (currentItem != null)
                currentItem.ContentVisible = true;  
        }

        public IDictionary<string, T> ExtractDataFromUserControl<T>(params string[] targetTags) where T : UserControlData
        {
            IDictionary<string, T> result = new Dictionary<string, T>();
            foreach (BaseLayoutItem item in targetTags == null || targetTags.Count() == 0 ? RootLayoutGroup.Items : RootLayoutGroup.Items.Where(x=>targetTags.Contains(x.Name)))
            {
                LayoutControlItem controlitem = item as LayoutControlItem;
                IUserControlDataPrivider<T> privder = controlitem.Control as IUserControlDataPrivider<T>;
                if (privder == null)
                {
                    UserControlData data = new UserControlData();
                    data.Tag = (item as LayoutControlItem).Name;
                    data.ResultText = controlitem.Control.Text;
                    result.Add(item.Name, data as T);
                }
                else
                    result.Add(item.Name, privder.ExtractData());
            }
            return result;
        }

        public IDictionary<string, UserSQLControlData> ExtractDataFromUserSQLControl(params string[] targetTags)
        {
            IDictionary<string, UserSQLControlData> result = new Dictionary<string, UserSQLControlData>();
            foreach (BaseLayoutItem item in targetTags == null || targetTags.Count() == 0 ? RootLayoutGroup.Items : RootLayoutGroup.Items.Where(x => targetTags.Contains(x.Name)))
            {
                LayoutControlItem controlitem = item as LayoutControlItem;
                IUserSQLControlDataPrivider privder = controlitem.Control as IUserSQLControlDataPrivider;
                if (privder != null)
                     result.Add(item.Name, privder.ExtractData());
                   
            }
            return result;
        
        }

        public void SendBroadcastToAllUserControl(BroadcastMessage message)
        {
            foreach (BaseLayoutItem item in RootLayoutGroup.Items)
            {
                LayoutControlItem controlitem = item as LayoutControlItem;
                IUserControlReceiver receiver = controlitem.Control as IUserControlReceiver;
                if (receiver != null)
                {
                    receiver.RespondBroadcast(message);
                }
            }
        
        }

        public void SendBroadcastToTargetUserControl(BroadcastMessage message, params string[] targetTags)
        {
            foreach (BaseLayoutItem item in RootLayoutGroup.Items.Where(x => targetTags.Contains(x.Name)))
            {
                LayoutControlItem controlitem = item as LayoutControlItem;
                IUserControlReceiver receiver = controlitem.Control as IUserControlReceiver;
                if (receiver != null)
                {
                    receiver.RespondBroadcast(message);
                }
            }        
        }
    }

    public class UserControlData
    {
        public string Tag { get; set; }

        public string ResultText { get; set; }

        public bool IsEmpty { get; set; }
    }


    public interface IUserControlDataPrivider<T> where T : UserControlData
    {
        T ExtractData(params object[] Params);
        void SetUserControlDataTag(string tag);
    }

    public class UserSQLControlData :  UserControlData
    {
        public string ColName { get; set; }

        public bool HasError { get; set; }
    }


    public interface IUserSQLControlDataPrivider
    {
        UserSQLControlData ExtractData(params object[] Params);
        void SetUserControlDataTag(string tag);
        void SetDataBaseColName(string colName);
    }



    public class BroadcastMessage
    {
        public object Message{get;set;}
    }

    public interface IUserControlReceiver
    {
        void RespondBroadcast(BroadcastMessage Message);
    }
}
