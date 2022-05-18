using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
namespace DBHelper
{
    public class DBProcess
    {
        //当前数据库类型
        static string Str_DbType = "mysql";
        public static void SetDbType(string Str_Type)
        {
            Str_DbType = Str_Type;
        }
        //创建日志记录组件实例
        public static log4net.ILog FileLog = log4net.LogManager.GetLogger("FileLog.Logging");
        //定义数据库操作对象
        public static Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>(Str_DbType);

        //数据库连接测试
        public static int DbConnTest()
        {
            try
            {

                disposeDb();
                _db = EnterpriseLibraryContainer.Current.GetInstance<Database>(Str_DbType);
                System.Data.Common.DbConnection dbconn = _db.CreateConnection();
                dbconn.Open();
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                ShowException(ex, "数据库连接测试失败");
                return 0;
            }
        }
        #region 数据库错误日志
        public static void ShowException(Exception ex, string infos)
        {
            string exMessage = ex.Message;
            string exSource = ex.Source;
            string exStackTrace = ex.StackTrace;
            string exInnerMessage = string.Empty;
            string exInnerSource = string.Empty;
            string exInnerStackTrace = string.Empty;

            if (ex.InnerException != null)
            {
                exInnerMessage = ex.InnerException.Message;
                exInnerSource = ex.InnerException.Source;
                exInnerStackTrace = ex.InnerException.StackTrace;
            }
            string exInfoStr = "=======ex异常=======" + Environment.NewLine +
                            exMessage + Environment.NewLine +
                            exSource + Environment.NewLine +
                            exStackTrace + Environment.NewLine +
                            "=======exInner异常=======" + Environment.NewLine +
                            exInnerMessage + Environment.NewLine +
                            exInnerSource + Environment.NewLine +
                            exInnerStackTrace + Environment.NewLine +
                            "=======附加信息=======" + Environment.NewLine +
                            infos;
            //写日志
            System.Console.WriteLine("错误信息：" + exInfoStr);
            DBProcess.FileLog.Error(exInfoStr);
        }

        public static void disposeDb()
        {
            _db = null;
        }
        #endregion

    }
}
