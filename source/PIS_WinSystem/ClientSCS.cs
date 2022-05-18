using DevComponents.DotNetBar;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys
{
    public class ClientSCS
    {
        //=======================================第一种调用，事件模式====================================================
        private static ManualResetEvent Message_Done = new ManualResetEvent(false);
        public static void Process_Client(string Interface_Info)
        {
            if (Program.Interface_SetInfo != null && !Interface_Info.Equals(""))
            {
                try
                {
                    Message_Done.Reset();
                    var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Program.Interface_SetInfo.tcp_ip, Program.Interface_SetInfo.tcp_port));
                    //注册接口服务响应消息处理程序
                    client.MessageReceived += Client_MessageReceived;
                    //连接到接口服务
                    client.Connect();
                    //发送接口消息给接口服务
                    client.SendMessage(new ScsTextMessage(Interface_Info));
                    //等待关闭信号量到来
                    Message_Done.WaitOne();
                    //关闭接口连接
                    client.Disconnect();
                }
                catch
                {

                }

            }
        }
        static void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            //设置关闭信号量
            Message_Done.Set();
            //获取接口服务响应消息
            var message = e.Message as ScsTextMessage;
            if (message == null)
            {
                return;
            }
            string ResponseMessageTxt = message.Text;
            //处理接口服务返回消息
            Frm_TJInfo("提示", ResponseMessageTxt);
        }
        //=======================================第2种调用，应答模式====================================================
        public static void RequestReplyStyleClient(string Interface_Info)
        {
            if (Program.Interface_SetInfo != null && !Interface_Info.Equals(""))
            {
                try
                {
                    using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Program.Interface_SetInfo.tcp_ip, Program.Interface_SetInfo.tcp_port)))
                    {
                        using (var requestReplyMessenger = new RequestReplyMessenger<IScsClient>(client))
                        {
                            requestReplyMessenger.Start();
                            client.Connect();
                            var response = requestReplyMessenger.SendMessageAndWaitForResponse(new ScsTextMessage(Interface_Info));
                            string ResponseMessageTxt = ((ScsTextMessage)response).Text;
                            //处理接口服务返回消息
                            Frm_TJInfo("提示", ResponseMessageTxt);
                        }
                    }
                }
                catch
                {

                }

            }
        }

        //=======================================第3种调用,同步模式====================================================
        public static string ZSynchronizedClient(string Interface_Info)
        {
            string ResponseMessageTxt = "";
            if (Program.Interface_SetInfo != null && !Interface_Info.Equals(""))
            {
                try
                {
                    using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Program.Interface_SetInfo.tcp_ip, Program.Interface_SetInfo.tcp_port)))
                    {
                        using (var synchronizedMessenger = new SynchronizedMessenger<IScsClient>(client))
                        {
                            synchronizedMessenger.Start();
                            client.Connect();
                            synchronizedMessenger.SendMessage(new ScsTextMessage(Interface_Info));
                            var receivedMessage = synchronizedMessenger.ReceiveMessage<ScsTextMessage>();
                            ResponseMessageTxt = receivedMessage.Text;

                        }
                    }

                }
                catch
                {
                    ResponseMessageTxt = "";

                }

            }
            return ResponseMessageTxt;
        }

        //=======================================第4种调用,异步模式====================================================
        //1.异步主方法
        public static string AsynchronousClient(string Interface_Info)
        {
            if (Program.Interface_SetInfo != null && !Interface_Info.Equals(""))
            {
                try
                {
                    using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Program.Interface_SetInfo.tcp_ip, Program.Interface_SetInfo.tcp_port)))
                    {
                        using (var synchronizedMessenger = new SynchronizedMessenger<IScsClient>(client))
                        {
                            synchronizedMessenger.Start();
                            client.Connect();
                            synchronizedMessenger.SendMessage(new ScsTextMessage(Interface_Info));
                            var receivedMessage = synchronizedMessenger.ReceiveMessage<ScsTextMessage>();
                            return receivedMessage.Text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorStr = "Error|" + Interface_Info + "|" + ex.Message;
                    return errorStr;
                }
            }
            return "Off|" + Interface_Info;
        }
        //2.建立一个委托,以便这个委托可以异步调用方法
        //委托与需要代表的方法具有相同的参数和返回类型
        public delegate string AsynchronousClientDel(string Interface_Info);
        //3.初始化委托并开始异步调用,BeginInvoke会将自身结果(IAsyncResult类型)当作参数传递给它的回调函数,而且BeginInvoke的最后一个参数将作为ar.AsyncState(Object类型)传递给回调函数
        //这里BeginInvoke有几个参数,前面的参数是和所要代理的打印方法一致的参数,最后2个参数:一个是表明
        //当异步调用结束时回调哪个方法(CallbackMethod),最后一个是回调函数将要用到的信息,这里直接传递代理
        //对象AsynchronousClientDel del,作用是在回调函数中可以让这个del结束异步调用,即del.EndInvoke()
        public static void SynchronizedClient(string Interface_Info)
        {
            if (Program.Interface_SetInfo != null && !Interface_Info.Equals(""))
            {
                try
                {
                    AsynchronousClientDel del = new AsynchronousClientDel(AsynchronousClient);
                    IAsyncResult ar = del.BeginInvoke(Interface_Info, new AsyncCallback(CallbackMethod), del);
                }
                catch (Exception ex)
                {
                    Program.FileLog.Error(ex.Message);
                }
            }
        }
        //4.在回调方法中执行完成后的操作(显示成功的信息)
        public static void CallbackMethod(IAsyncResult ar)
        {
            AsynchronousClientDel del = (AsynchronousClientDel)ar.AsyncState;
            string msg = del.EndInvoke(ar);
            Program.FileLog.Info(msg);
        }
        //提示窗体
        public static void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(FrmMain.mdiClient);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
    }
}
