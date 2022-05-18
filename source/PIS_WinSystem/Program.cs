/*   
'                   _ooOoo_
'                  o8888888o
'                  88" . "88
'                  (| -_- |)
'                  O\  =  /O
'               ____/`---'\____
'             .'  \\|     |//  `.
'            /  \\|||  :  |||//  \
'           /  _||||| -:- |||||-  \
'           |   | \\\  -  /// |   |
'           | \_|  ''\---/''  |   |
'           \  .-\__  `-`  ___/-. /
'         ___`. .'  /--.--\  `. . __
'      ."" '<  `.___\_<|>_/___.'  >'"".
'     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
'     \  \ `-.   \_ __\ /__ _/   .-` /  /
'======`-.____`-.___\_____/___.-`____.-'======
'                   `=---='
'^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
'         佛祖保佑       永无BUG
'==============================================================================
'文件
'名称: 病理信息管理系统
'功能: 病理科业务系统信息化完整解决方案
'作者: peer
'日期: 2015.09.8
'修改:
'日期:
'备注:
'==============================================================================
*/
using DevComponents.DotNetBar;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys
{
    static class Program
    {
        //创建日志记录组件实例
        public static log4net.ILog FileLog = log4net.LogManager.GetLogger("FileLog.Logging");
        public static Mutex FFMPEGMutex;
        //冰冻是否单独编号
        public static Boolean BD_BHFun = false;
        //定义主窗体的实例
        public static FrmMain frmMainins;
        //医院名称
        public static string HospitalName = "医院名称";
        //科室名称
        public static string Dept_Name = "系统开发";
        //系统exe名称
        public static string sys_exe_name = "PIS_Sys.exe";
        //科室编码
        public static string Dept_Code = "000";
        //操作人员编码
        public static string User_Code = "0000";
        //操作人员姓名
        public static string User_Name = "admin";
        //操作人员密码
        public static string User_Pwd = "";
        //操作人员级别
        public static int user_role_code = 9;
        //电子病历连接串
        public static string EmrUrlStr = "";
        //PACS连接串
        public static string PACSUrlStr = "";
        //历史报告连接串
        public static string HistoryPIS = "";
        //系统编码
        public static string System_Code = "0001";
        public static string newversion = "";
        public static string oldversion = "";
        public static string update_date = "";
        public static string update_subDir = "";
        public static string process_name = "";
        public static string update_info = "";
        //系统管理员标记
        public static Boolean System_Admin = true;
        //接口服务设置信息
        public static DBHelper.Model.interface_set_info Interface_SetInfo = new DBHelper.Model.interface_set_info();
        //应用程序执行路径
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        //Ftp服务器设置信息
        public static string FtpIP = "127.0.0.1";
        public static int FtpPort = 21;
        public static string FtpUser = "peerct";
        public static string FtpPwd = "125353Ct";
        //工作站类型
        public static string workstation_type = ConfigurationManager.AppSettings["w_big_type"];
        //工作站类型数据库查询用字符串
        public static string workstation_type_db = ConfigurationManager.AppSettings["w_big_type_db"];
        //工作站类型名称
        public static string workstation_type_name = ConfigurationManager.AppSettings["w_big_type_name"];
        //取材工作站类别设置
        public static string workstation_QC_TYPE = ConfigurationManager.AppSettings["QC_TYPE"];
        //保持缩放比例
        public static int KeepAspectRatioFlag = 0;
        //数字摄像头类别配置(0通用1官方)
        public static int UsbCameraType = 0;
        //创建日志记录组件实例
        public static log4net.ILog FileLogIns = log4net.LogManager.GetLogger("FileLog.Logging");
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
            Delete7DaysLog();
            //删除一个月内的多媒体文件
            DeleteMonthFolor();
            //
            Application.Run(new FrmMain());
            FFMPEGMutex.Close();
            GC.KeepAlive(FFMPEGMutex);
            //移除异常处理
            GlobalExceptionsHandler.ToRemoveHandler();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            if ((pToDecrypt.Length % 4) != 0)//因为加密后是base64，所以用4来求余进行验证
            {
                return pToDecrypt;
            }
            if (pToDecrypt.Contains("Password"))//如果包含Password，表示没有加密
            {
                return pToDecrypt;
            }

            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// 计算报告及时率分钟数
        /// </summary>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">截止时间</param>
        /// <param name="skipFlag">是否跳过周六周日</param>
        /// <returns></returns>
        public static int CalculateMinutes(DateTime dtStart, DateTime dtEnd, int skipFlag)
        {
            int count = 0;
            int days = dtEnd.Date.Subtract(dtStart.Date).Days;
            if (days >= 0)
            {
                for (int i = 0; i <= days; i++)
                {
                    DateTime dtTemp = dtStart.AddDays(i);
                    //计算起始日分钟数
                    if (dtStart.Date == dtTemp.Date && dtEnd.Date != dtStart.Date)
                    {
                        //第二天的0点00分00秒
                        DateTime TwoDateTime = dtStart.AddDays(1).Date;
                        count += Convert.ToInt32(TwoDateTime.Subtract(dtStart).TotalMinutes);
                        continue;
                    }
                    if (dtEnd.Date == dtStart.Date)
                    {
                        count += Convert.ToInt32(dtEnd.Subtract(dtStart).TotalMinutes);
                        continue;
                    }
                    //跳过周六周日
                    if (skipFlag == 1)
                    {
                        if (dtTemp.DayOfWeek == DayOfWeek.Saturday || dtTemp.DayOfWeek == DayOfWeek.Sunday)
                        {
                            continue;
                        }
                    }
                    //计算截止日分钟数
                    if (dtEnd.Date == dtTemp.Date)
                    {
                        //当天的0点00分00秒
                        DateTime CurDateTime = dtEnd.Date;
                        count += Convert.ToInt32(dtEnd.Subtract(CurDateTime).TotalMinutes);
                        continue;
                    }
                    count += 1440;
                }
            }
            return count;
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

        /// <summary>
        /// 用递归方法删除文件夹目录及文件
        /// </summary>
        /// <param name="dir">带文件夹名的路径</param> 
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir, true); //删除已空文件夹                 
            }
        }

        //删除7天前的日志
        public static void Delete7DaysLog()
        {
            //日志目录
            string LogFolder = Program.APPdirPath + @"\log";
            if (Directory.Exists(LogFolder) == true)
            {
                DirectoryInfo dir = new DirectoryInfo(@LogFolder);
                foreach (FileInfo fi in dir.GetFiles())
                {
                    if (fi.CreationTime < DateTime.Today.AddDays(-7))
                        fi.Delete();
                }
            }
        }
        //删除一个月前的多媒体文件
        public static void DeleteMonthFolor()
        {
            //1视频路径
            string VideoFolder = Program.APPdirPath + @"\Pis_Cap_Video";
            if (Directory.Exists(VideoFolder) == true)
            {
                DeleteFolorAFile(VideoFolder);
            }
            //2图像路径
            string ImgFolder = Program.APPdirPath + @"\Pis_Cap_Image";
            if (Directory.Exists(ImgFolder) == true)
            {
                DeleteFolorAFile(ImgFolder);
            }
            //3录音路径
            string AudioFolder = Program.APPdirPath + @"\Pis_Cap_Audio";
            if (Directory.Exists(AudioFolder) == true)
            {
                DeleteFolorAFile(AudioFolder);
            }
            //4视频路径
            string VideoFolder4 = Program.APPdirPath + @"\Pis_Video";
            if (Directory.Exists(VideoFolder4) == true)
            {
                DeleteFolorAFile(VideoFolder4);
            }
            //5图像路径
            string ImgFolder5 = Program.APPdirPath + @"\Pis_Image";
            if (Directory.Exists(ImgFolder5) == true)
            {
                DeleteFolorAFile(ImgFolder5);
            }
            //6录音路径
            string AudioFolder6 = Program.APPdirPath + @"\Pis_Audio";
            if (Directory.Exists(AudioFolder6) == true)
            {
                DeleteFolorAFile(AudioFolder6);
            }
            //7报告图片路径
            string ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
            if (Directory.Exists(ReportFolder) == true)
            {
                DeleteFolorAFile(ReportFolder);
            }
        }

        public static void DeleteFolorAFile(string FolorStr)
        {
            DirectoryInfo di = new DirectoryInfo(@FolorStr);
            //获取子文件夹列表
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                if (dir.CreationTime < DateTime.Today.AddDays(-30))
                    DeleteFolder(@dir.FullName);
            }
        }


    }
}
