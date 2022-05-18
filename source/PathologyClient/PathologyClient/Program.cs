using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace PathologyClient
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    static class Program
    {
        public static Dictionary<string, Color> exam_status_dic = new Dictionary<string, Color>();
        public static Dictionary<string, Color> exam_status_name_dic = new Dictionary<string, Color>();
        //
        public static FrmMain frmMainins = null;
        //医院名称
        public static string HospitalName = "北京公司";
        public static string H_Pre_Char = "";
        //系统exe名称
        public static string sys_exe_name = "PathologyClient.exe";
        //操作人员编码
        public static string User_Code = "0000";
        //
        public static Mutex FFMPEGMutex;
        //应用程序执行路径
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        //Ftp服务器设置信息
        public static string FtpIP = "127.0.0.1";
        public static int FtpPort = 21;
        public static string FtpUser = "";
        public static string FtpPwd = "";
        //Webservice服务地址
        public static string WebServerUrl = "http://localhost:8888/ClientWebService.asmx";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //全局处理异常
            GlobalExceptionsHandler.ToAddHandler();
            FFMPEGMutex = new Mutex();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //本地化dotnetbar
            DevComponents.DotNetBar.LocalizationKeys.LocalizeString += new DevComponents.DotNetBar.DotNetBarManager.LocalizeStringEventHandler(LocalizeString);
            //删除7天前的日志
            LogLib.DeleteFolorAFile(-7);
            WebServerUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["ServicesUrl"], "ClientWebService.asmx");

            Application.Run(new FrmMain());

            FFMPEGMutex.Close();
            GC.KeepAlive(FFMPEGMutex);
            //移除异常处理
            GlobalExceptionsHandler.ToRemoveHandler();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        static void LocalizeString(object sender, DevComponents.DotNetBar.LocalizeEventArgs e)
        {

            if (e.Key == LocalizationKeys.MessageBoxOkButton)
            {

                e.LocalizedValue = "确认";

                e.Handled = true;

            }
            if (e.Key == LocalizationKeys.MessageBoxCancelButton)
            {

                e.LocalizedValue = "取消";

                e.Handled = true;

            }
            if (e.Key == LocalizationKeys.MonthCalendarTodayButtonText)
            {

                e.LocalizedValue = "今天";

                e.Handled = true;

            }
        }

        public static EntityModel.Exam_BlSqd GetSqdInfo(string exam_no)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("exam_no", exam_no);
            string RetStr = PublicBaseLib.PostWebService.PostCallWebServiceForTxt(Program.WebServerUrl, "GetPrintSqdInfo", parameters);
            EntityModel.Exam_BlSqd InsSqd = new EntityModel.Exam_BlSqd();
            InsSqd = PublicBaseLib.JsonHelper.JsonTo<EntityModel.Exam_BlSqd>(RetStr);
            return InsSqd;
        }
    }
}
