using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Component.DataBaseConnect
{
    public class MySQLConnectParam : IConnectStrParam
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DataBase { get; set; }
        public string Port { get; set; }
        public string Charset { get; set; }
        
        public void Serialization(ConnectStrInvoker Invoker)
        {
            IList<string> temp = new List<string>();
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_Server, Server));
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_User, User));
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_Password, Password));
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_Database, DataBase));
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_Port, Port));
            temp.Add(string.Format("{0} = {1}", ConnectStrInvoker.Tag_Charset, Charset));

            Invoker.ConnectStr = string.Join(";", temp);
        }

        public void DeSerialization(ConnectStrInvoker Invoker)
        {
            foreach (string item in Invoker.ConnectStr.Split(';'))
            {
                switch (item.Split('=')[0].Trim())
                {
                    case ConnectStrInvoker.Tag_Server: Server = item.Split('=')[0].Trim(); break;
                    case ConnectStrInvoker.Tag_User: Server = item.Split('=')[0].Trim(); break;
                    case ConnectStrInvoker.Tag_Password: Server = item.Split('=')[0].Trim(); break;
                    case ConnectStrInvoker.Tag_Database: Server = item.Split('=')[0].Trim(); break;
                    case ConnectStrInvoker.Tag_Port: Server = item.Split('=')[0].Trim(); break;
                    case ConnectStrInvoker.Tag_Charset: Server = item.Split('=')[0].Trim(); break;
                }
            }
        }
    }
}
