using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.DataBaseConnect
{
    public class SQLiteConnectParam : IConnectStrParam
    {
        public string DataSource { get; set; }

        public string Version { get; set; }

        public void Serialization(ConnectStrInvoker Invoker)
        {
            string temp = string.Format("{0} = {1};{2} = {3}", ConnectStrInvoker.Tag_DataSource, DataSource, ConnectStrInvoker.Tag_Version, Version);
            Invoker.ConnectStr = temp;
        }

        public void DeSerialization(ConnectStrInvoker Invoker)
        {
            string temp = Invoker.ConnectStr;
            if (string.IsNullOrEmpty(temp))
                return;

            foreach (string key in temp.Split(';'))
            {
                switch (key.Split('=')[0].Trim())
                {
                    case ConnectStrInvoker.Tag_DataSource: DataSource = key.Split('=')[1].Trim(); break;
                    case ConnectStrInvoker.Tag_Version: Version = key.Split('=')[1].Trim(); break;
                    default: break;
                }
            }
        }
    }
}
