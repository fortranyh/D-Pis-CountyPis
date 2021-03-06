using System;
using System.Threading;
using System.Windows.Forms;

namespace PIS_Sys
{

    public class GlobalExceptionsHandler
    {
        public static void ToAddHandler()
        {
            System.AppDomain.CurrentDomain.UnhandledException += ConsoleThreadException;
            Application.ThreadException += UIThreadException;
        }
        public static void ToRemoveHandler()
        {
            System.AppDomain.CurrentDomain.UnhandledException -= ConsoleThreadException;
            Application.ThreadException -= UIThreadException;
        }
        public static void UIThreadException(Object sender, ThreadExceptionEventArgs e)
        {
            string errorMSG = "【病理信息管理系统】发生错误，请结合以下错误信息联系系统服务商，错误信息: " + e.Exception.Message + "追踪错误堆栈信息:" + e.Exception.StackTrace;
            Program.FileLog.Error("UIThreadException(), " + errorMSG);
        }
        public static void ConsoleThreadException(Object sender, System.UnhandledExceptionEventArgs e)
        {

            System.Exception ex = e.ExceptionObject as System.Exception;
            string errorMSG = "【病理信息管理系统】发生错误，请结合以下错误信息联系系统服务商[風雲工作室(QQ:90988176)]错误信息: " + ex.Message + "追踪错误堆栈信息:" + ex.StackTrace;
            Program.FileLog.Error("UIThreadException(), " + errorMSG);
        }
    }
}
