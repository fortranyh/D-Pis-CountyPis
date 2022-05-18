using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using System;
using System.Configuration;
using System.Threading;

namespace WebServiceApp
{
    public class PublicModle
    {

        //接口服务设置信息
        public static DBHelper.Model.interface_set_info Interface_SetInfo = null;
        //创建日志记录组件实例
        public static log4net.ILog FileLog = log4net.LogManager.GetLogger("FileLog.Logging");
        public static void GetIneterfaceInfo()
        {
            //获取接口服务设置信息表
            if (Interface_SetInfo == null)
            {
                //获取接口服务设置信息
                DBHelper.BLL.interface_set_info InsInter = new DBHelper.BLL.interface_set_info();
                Interface_SetInfo = InsInter.GetInterfaceSetInfo();
            }
        }

        //1.异步主方法
        public static void DataSendThreadSCSModeFunc(string Tansmit_Info)
        {
            try
            {
                Thread thread = new Thread(() => AsynchronousClient(Tansmit_Info));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                PublicModle.FileLog.Error("DataSendThreadSCSModeFunc异常：" + ex.ToString());
            }
        }
        public static string AsynchronousClient(string Tansmit_Info)
        {
            if (Interface_SetInfo == null)
            {
                GetIneterfaceInfo();
            }
            if (Interface_SetInfo != null && !Tansmit_Info.Equals("") && Interface_SetInfo.enable_flag == 1)
            {
                try
                {
                    using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Interface_SetInfo.tcp_ip, Interface_SetInfo.tcp_port)))
                    {
                        using (var synchronizedMessenger = new SynchronizedMessenger<IScsClient>(client))
                        {
                            synchronizedMessenger.Start();
                            client.Connect();
                            synchronizedMessenger.SendMessage(new ScsTextMessage(Tansmit_Info));
                            var receivedMessage = synchronizedMessenger.ReceiveMessage<ScsTextMessage>();
                            return receivedMessage.Text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorStr = "Error|" + Tansmit_Info + "|" + ex.Message;
                    return errorStr;
                }
            }
            return "Off|" + Tansmit_Info;
        }
        //debug调试模式开关 
        public static string GetDebugMode()
        {
            return ConfigurationManager.AppSettings["Debug_Mode"];
        }

        public static String GetSysVersion()
        {
            string SysVersion = "";
            if (ConfigurationManager.AppSettings["SysVersion"] != null)
            {
                SysVersion = ConfigurationManager.AppSettings["SysVersion"];
            }
            return SysVersion;
        }
        public static String GetSystem_Code()
        {
            string System_Code = "";
            if (ConfigurationManager.AppSettings["System_Code"] != null)
            {
                System_Code = ConfigurationManager.AppSettings["System_Code"];
            }
            return System_Code;
        }
    }
}