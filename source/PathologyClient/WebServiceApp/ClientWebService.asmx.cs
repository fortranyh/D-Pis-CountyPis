using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;

namespace WebServiceApp
{
    /// <summary>
    /// WebServiceAppAppoint 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Description = "<center><strong>病理诊断中心接口系统</strong></center><br><center>是一个通用且可灵活配置的系统。通过此系统可以达到我们的病理诊断中心解决方案与客户不同业务系统间数据交互的目的。</center>")]
    [System.ComponentModel.ToolboxItem(false)]
    // 允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务
    [System.Web.Script.Services.ScriptService]
    public class ClientWebService : System.Web.Services.WebService
    {
        [WebMethod(Description = "测试此接口系统搭建是否成功方法", EnableSession = false)]
        public string HelloWorld()
        {
            return "Hello World，系统版本号：" + PublicModle.GetSysVersion();
        } 
        [WebMethod(Description = "测试此接口系统搭建是否成功方法,直接返回字符串", EnableSession = false)]
        public void HelloWorldStr()
        {
            Context.Response.ContentType = "text/plain; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write("你好");
            Context.Response.End();
        }
        [WebMethod(Description = "获取第三方医院XML列表", EnableSession = false)]
        public void GetThirdHospitalXmlStr(string hospital_code)
        {
            //获取当前医院信息
            DBhleper.BLL.sys_info InsSys = new DBhleper.BLL.sys_info();
            DataTable DtSys = InsSys.GetThirdData(hospital_code);
            string RetStr =PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(DtSys, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取检查时间轴信息", EnableSession = false)]
        public void GetSjzXmlStr(string exam_no)
        {
            //执行查询
            DBhleper.BLL.exam_master ins = new DBhleper.BLL.exam_master();
            DataTable dt = ins.GetDt("select exam_type,exam_status,study_no,patient_source,req_dept,req_physician,date_format(req_date_time,'%Y-%m-%d %H:%i:%s') AS req_date_time,date_format(received_datetime,'%Y-%m-%d %H:%i:%s') AS  received_datetime,receive_doctor_name,date_format(qucai_datetime,'%Y-%m-%d %H:%i:%s') AS  qucai_datetime,qucai_doctor_name,date_format(baomai_datetime,'%Y-%m-%d %H:%i:%s') AS   baomai_datetime,baomai_doctor_name,date_format(zhipian_datetime,'%Y-%m-%d %H:%i:%s') AS zhipian_datetime,zhipian_doctor_name,date_format(tuoshui_datetime,'%Y-%m-%d %H:%i:%s') as tuoshui_datetime from exam_master where exam_no='" + exam_no + "'");
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取检查状态字典", EnableSession = false)]
        public void GetExamStatusDic()
        {
            //加载检查状态
            DBhleper.BLL.exam_status exam_status_ins = new DBhleper.BLL.exam_status();
            DataTable dt = exam_status_ins.GetExamStatusDic();
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "测试链接病理数据库是否成功", EnableSession = false)]
        public void TestDbConn()
        {
            string ReturnStr = "不成功！";
            Context.Response.ContentType = "text/plain; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            //告诉浏览器，这是数据，就不要缓存我了
            Context.Response.Buffer = true;
            Context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";
            Context.Response.AppendHeader("Pragma", "No-Cache");
            try
            {
                if (DBhleper.DBProcess.DbConnTest() == 1)
                {
                    ReturnStr = "成功！";
                }
            }
            catch(Exception ex)
            {
                 ReturnStr = ex.ToString();
            }
            Context.Response.Clear();
            Context.Response.Flush();
            Context.Response.Write(ReturnStr);
            Context.Response.End();
        }
       
        private static readonly object SequenceLock = new object();
        [WebMethod(Description = "控制并发执行获取识别号", EnableSession = false)]
        public string GetUidInfo(string msg)
        {
            string returnValue = "";
            // 这里用锁的机制，提高并发控制能力
            lock (SequenceLock)
            {
                try
                {
                    DBhleper.BLL.GetUidKey ins = new DBhleper.BLL.GetUidKey();
                    int IRowCount=0;
                    UInt64 RetInt = ins.InsertAndGetLastId(ref IRowCount, "insert into uid_key(msg) values('" + msg + "')");
                    if (IRowCount == 1 && RetInt!=0)
                    {
                        returnValue = RetInt.ToString();
                    }
                }
                catch
                {
                    returnValue = "";
                }
            }
            return returnValue;
        }
        [WebMethod(Description = "验证申请单号是否存在", EnableSession = false)]
        public int verifySqd(string SqdStr)
        {
            DBhleper.BLL.exam_master exam_Bll_masterIns = new DBhleper.BLL.exam_master();
            int sl = exam_Bll_masterIns.GetExamNoCount(SqdStr);
            return sl;
        }
        [WebMethod(Description = "验证申病人ID号是否存在", EnableSession = false)]
        public int verifyPid(string PidStr)
        {
            DBhleper.BLL.exam_pat_mi ins = new DBhleper.BLL.exam_pat_mi();
            int sl = ins.GetExamPatCount(PidStr);
            return sl;
        }

        [WebMethod(Description = "验证用户登录", EnableSession = false)]
        public void LoginSys(string user_code,string user_pwd, string hospital_code)
        {
            DBhleper.BLL.sys_user ins = new DBhleper.BLL.sys_user();
            int RetInt = ins.sysThirdLoginCheck(user_code, user_pwd, hospital_code);
            Context.Response.ContentType = "text/plain; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetInt);
            Context.Response.End();
        }
        [WebMethod(Description = "民族字典", EnableSession = false)]
        public void GetMzDicXML()
        {
            DBhleper.BLL.exam_nation exam_nation_ins = new DBhleper.BLL.exam_nation();
            DataSet ds = exam_nation_ins.GetDsExam_Nation();
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(ds.Tables[0], "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取检查列表", EnableSession = false)]
        public void GetExamMasterXML(string tjStr)
        {
            DBhleper.BLL.exam_report ins = new DBhleper.BLL.exam_report();
            DataTable dt = ins.GetThirdReportList(tjStr);
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "检查类别字典", EnableSession = false)]
        public void GetExamTypeXML(int flag)
        {
            DBhleper.BLL.exam_type exam_type_ins = new DBhleper.BLL.exam_type();
            DataSet ds = exam_type_ins.GetDsExam_Type(flag, false, "");
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(ds.Tables[0], "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "处理病人基本信息", EnableSession = false)]
        public void Process_Patmi(string JsonStr)
        {
            string faild_info = "";
            EntityModel.exam_pat_mi patmi_Ins = PublicBaseLib.JsonHelper.JsonTo<EntityModel.exam_pat_mi>(JsonStr);
            DBhleper.BLL.exam_pat_mi Patmi_Dll_ins = new DBhleper.BLL.exam_pat_mi();
            Boolean ResultDb_Flag = false;
            ResultDb_Flag = Patmi_Dll_ins.Process_Patmi(patmi_Ins, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "处理妇科信息", EnableSession = false)]
        public void Process_Obstetric(string JsonStr)
        {
            string faild_info = "";
            EntityModel.exam_obstetric exam_obs_Ins = PublicBaseLib.JsonHelper.JsonTo<EntityModel.exam_obstetric>(JsonStr);
            Boolean ResultDb_Flag = false;
            DBhleper.BLL.exam_obstetric exam_B_Ins = new DBhleper.BLL.exam_obstetric();
            ResultDb_Flag = exam_B_Ins.Process_exam_obstetric(exam_obs_Ins, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }

        [WebMethod(Description = "处理肿瘤信息", EnableSession = false)]
        public void Process_Tumour(string JsonStr)
        {
            string faild_info = "";
            EntityModel.exam_tumour exam_tuIns = PublicBaseLib.JsonHelper.JsonTo<EntityModel.exam_tumour>(JsonStr);
            Boolean ResultDb_Flag = false;
            DBhleper.BLL.exam_tumour tum_B_Ins = new DBhleper.BLL.exam_tumour();
            ResultDb_Flag = tum_B_Ins.Process_exam_tumour(exam_tuIns, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }

        [WebMethod(Description = "处理送检标本信息", EnableSession = false)]
        public void Process_Specimens(string sjbbStr, int Icount, string exam_no, string doctor_code, string doctor_name)
        {
            string faild_info = "";
            Boolean ResultDb_Flag = false;
            DBhleper.BLL.exam_specimens speci_Ins = new DBhleper.BLL.exam_specimens();
            ResultDb_Flag = speci_Ins.Process_exam_specimens(sjbbStr, Icount, exam_no, doctor_code, doctor_name, false, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }

        [WebMethod(Description = "处理申请单描述信息", EnableSession = false)]
        public void Process_Requisition(string JsonStr)
        {
            string faild_info = "";
            EntityModel.exam_requisition exam_req_ins = PublicBaseLib.JsonHelper.JsonTo<EntityModel.exam_requisition>(JsonStr);
            Boolean ResultDb_Flag = false;
            DBhleper.BLL.exam_requisition exam_B_req_Ins = new DBhleper.BLL.exam_requisition();
            ResultDb_Flag = exam_B_req_Ins.Process_exam_requisition(exam_req_ins, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }

        [WebMethod(Description = "处理申请单信息", EnableSession = false)]
        public void Process_Master(string JsonStr)
        {
            string faild_info = "";
            EntityModel.exam_master examMas_Ins = PublicBaseLib.JsonHelper.JsonTo<EntityModel.exam_master>(JsonStr);
            Boolean ResultDb_Flag = false;
            DBhleper.BLL.exam_master exam_Bll_masterIns = new DBhleper.BLL.exam_master();
            ResultDb_Flag = exam_Bll_masterIns.Process_ExamMaster(examMas_Ins, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }

        [WebMethod(Description = "作废申请单", EnableSession = false)]
        public void ZfExamMaster(string exam_no,string user_code)
        {
            string faild_info = "";
            DBhleper.BLL.exam_master ins = new DBhleper.BLL.exam_master();
            Boolean ResultDb_Flag = ins.Third_ZF_ExamMaster(exam_no, user_code, ref faild_info);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<RESULTS>");
            if (!ResultDb_Flag)
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "0");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "失败！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", faild_info);
                sb.Append("</ROW>");
            }
            else
            {
                sb.Append("<ROW>");
                sb.AppendFormat("<RESULT_CODE><![CDATA[{0}]]></RESULT_CODE>", "1");
                sb.AppendFormat("<RESULT_MSG><![CDATA[{0}]]></RESULT_MSG>", "成功！");
                sb.AppendFormat("<INFO><![CDATA[{0}]]></INFO>", "");
                sb.Append("</ROW>");
            }
            sb.Append("</RESULTS>");
            string RetStr = sb.ToString();
            sb.Clear();
            sb = null;
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }



        [WebMethod(Description = "取检查大类编码", EnableSession = false)]
        public string GetExamTypeCode(string modality)
        {
            string big_type = "PL";
            //取大类
            DBhleper.BLL.exam_type insType = new DBhleper.BLL.exam_type();
            big_type = insType.GetBigType(modality);
            return big_type;
        }

        [WebMethod(Description = "修改登录密码", EnableSession = false)]
        public string UpdateLoginPwd(string user_code, string user_pwd, string hospital_code)
        {
            string RetStr = "0";
            DBhleper.BLL.sys_user ins = new DBhleper.BLL.sys_user();
            Boolean flag = ins.UpdateThirdUserPwd(user_code, user_pwd, hospital_code);
            if (flag)
            {
                RetStr = "1";
            }
            return RetStr;
        }

        [WebMethod(Description = "获取病理整体报告是否存在", EnableSession = false)]
        public string GetReportImgFlag(string exam_no,int img_type)
        {
            string RetStr = "0";
            DBhleper.BLL.exam_report ins = new DBhleper.BLL.exam_report();
            string ReportPath = ins.GetReportImgPath(exam_no, img_type);
            string RootDir = System.Web.Configuration.WebConfigurationManager.AppSettings["rvdir"];
            string ReportUrl = RootDir + @ReportPath;
            if (System.IO.File.Exists(ReportUrl))
            {
                RetStr = "1";
            }
            return RetStr;
        }

        [WebMethod(Description = "获取病理整体报告图像流", EnableSession = false)]
        public void GetReportImgStream(string exam_no, int img_type)
        {
            DBhleper.BLL.exam_report ins = new DBhleper.BLL.exam_report();
            string ReportPath = ins.GetReportImgPath(exam_no, img_type);
            string RootDir = System.Web.Configuration.WebConfigurationManager.AppSettings["rvdir"];
            string ReportUrl = RootDir + @ReportPath;
            if (System.IO.File.Exists(ReportUrl))
            {
                Context.Response.ContentType = "image/jpeg";
                Context.Response.TransmitFile(@ReportUrl);
            }
            else
            {
                Context.Response.End();
            }
        }
        [WebMethod(Description = "获取病理整体报告图像流", EnableSession = false)]
        public void GetReportImgBinary(string exam_no, int img_type)
        {
            DBhleper.BLL.exam_report ins = new DBhleper.BLL.exam_report();
            string ReportPath = ins.GetReportImgPath(exam_no, img_type);
            string RootDir = System.Web.Configuration.WebConfigurationManager.AppSettings["rvdir"];
            string ReportUrl = RootDir + @ReportPath;
            if (System.IO.File.Exists(ReportUrl))
            {
                //以字符流的形式下载文件   
                FileStream fs = new FileStream(@ReportUrl, FileMode.Open);   
                byte[] bytes = new byte[(int)fs.Length];    
                fs.Read(bytes, 0, bytes.Length);        
                fs.Close();
                Context.Response.ContentType = "application/octet-stream";   
                //通知浏览器下载文件而不是打开     
                Context.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(@ReportUrl, System.Text.Encoding.UTF8));
                Context.Response.BinaryWrite(bytes);
                Context.Response.Flush();
            }
            Context.Response.End();
        }

        [WebMethod(Description = "获取申请单打印所需信息", EnableSession = false)]
        public void GetPrintSqdInfo(string exam_no)
        {
            EntityModel.Exam_BlSqd SqdIns = new EntityModel.Exam_BlSqd();
            //获取申请单信息 
            DBhleper.Model.exam_requisition insModel = new DBhleper.Model.exam_requisition();
            DBhleper.BLL.exam_requisition insm = new DBhleper.BLL.exam_requisition();
            insModel = insm.GetRequisitionInfo(exam_no);
            if (insModel != null)
            {
                SqdIns.lczd = insModel.clinical_diag;
                SqdIns.bs = insModel.history_note;
                SqdIns.szsj = insModel.ops_note;
            }
            DBhleper.BLL.exam_master insM = new DBhleper.BLL.exam_master();
            DataTable dtPat = insM.GetPatInfoFromExamNo(exam_no);
            if (dtPat != null && dtPat.Rows.Count == 1)
            {
                SqdIns.pat_name = dtPat.Rows[0]["patient_name"].ToString();
                SqdIns.sex = dtPat.Rows[0]["sex"].ToString();
                SqdIns.age = dtPat.Rows[0]["age"].ToString();
                SqdIns.pho = dtPat.Rows[0]["phone_number"].ToString();
                SqdIns.hospital_name = dtPat.Rows[0]["submit_unit"].ToString();
                SqdIns.sqks = dtPat.Rows[0]["req_dept"].ToString();
                SqdIns.sqys = dtPat.Rows[0]["req_physician"].ToString();
                SqdIns.parts = dtPat.Rows[0]["parts"].ToString();
                SqdIns.sqrq = dtPat.Rows[0]["received_datetime"].ToString();
                SqdIns.exam_no = exam_no;
            }
            string RetStr = PublicBaseLib.JsonHelper.ToJson(SqdIns);
            Context.Response.ContentType = "text/plain; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取蜡块信息", EnableSession = false)]
        public void GetDrawMeterialsXML(string study_no)
        {
            DBhleper.BLL.exam_draw_meterials ins = new DBhleper.BLL.exam_draw_meterials();
            DataTable dt = ins.GetDtHdMeterials(study_no);
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取切片信息", EnableSession = false)]
        public void GetFilmMakingXML(string study_no)
        {
            DBhleper.BLL.exam_filmmaking ins = new DBhleper.BLL.exam_filmmaking();
            DataTable dt = ins.GetDtFilmMakingPj(study_no);
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取特检医嘱", EnableSession = false)]
        public void GetTjyzXML(string study_no)
        {
            //展示特检医嘱信息
            DBhleper.BLL.exam_tjyz ins = new DBhleper.BLL.exam_tjyz();
            DataTable dt = ins.GetDataTjyz(study_no);
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dt, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取大体描述", EnableSession = false)]
        public void GetDtmsXML(string exam_no)
        {
            DBhleper.BLL.exam_specimens insSpe = new DBhleper.BLL.exam_specimens();
            DataTable dtdtms = insSpe.GetDtmsInfo(exam_no);
            string RetStr = PublicBaseLib.PostWebService.ConvertDataTableToXML_CDATA(dtdtms, "RESULTS", "ROW");
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(RetStr);
            Context.Response.End();
        }
        [WebMethod(Description = "获取当前检查状态", EnableSession = false)]
        public string GetExamStatus(string study_no)
        {
            DBhleper.BLL.exam_master insM = new DBhleper.BLL.exam_master();
            int status = insM.GetStudyExam_Status(study_no);
            return status.ToString();
        }
        [WebMethod(Description = "上传文件到服务器的接口:参数fs文件的byte[]；path上传文件的路径；fileName上传文件名字；examno申请单号。", EnableSession = false)]
        public bool UploadFile(byte[] fs, string path, string fileName,string examno)
        {
            bool flag = false;
            try
            {
                //获取上传案例图片路径
                path = Server.MapPath(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //定义并实例化一个内存流，以存放提交上来的字节数组。
                MemoryStream m = new MemoryStream(fs);
                //定义实际文件对象，保存上载的文件。
                FileStream f = new FileStream(path + @"\" + fileName, FileMode.Create);
                //把内存里的数据写入物理文件
                m.WriteTo(f);
                m.Close();
                f.Close();
                f = null;
                m = null;
                flag = true;
            }
            catch (Exception ex)
            {
                PublicModle.FileLog.Error(ex.ToString());
                flag = false;
            }
            return flag;
        }

        [WebMethod(Description = "下载服务器文件到本地的接口；strFilePath为文件相对路径；path服务器上的路径根目录；examno申请单号。", EnableSession = false)]
        public byte[] DownloadFile(string strFilePath, string path,string examno)
        {
            FileStream fs = null;
            string CurrentUploadFolderPath = HttpContext.Current.Server.MapPath(path);

            string CurrentUploadFilePath = CurrentUploadFolderPath + @"\" + strFilePath;
            if (File.Exists(CurrentUploadFilePath))
            {
                try
                {
                    ///打开现有文件以进行读取。
                    fs = File.OpenRead(CurrentUploadFilePath);
                    int b1;
                    System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                    while ((b1 = fs.ReadByte()) != -1)
                    {
                        tempStream.WriteByte(((byte)b1));
                    }
                    return tempStream.ToArray();
                }
                catch
                {
                    return new byte[0];
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            {
                return new byte[0];
            }
        }

        [WebMethod(Description = "上传文件到服务器的接口.base64Str文件的base64编码字符串；path上传文件的路径；fileName上传文件名字；examno申请单号。", EnableSession = false)]
        public void UploadFileBase64Str(string base64Str, string path, string fileName, string examno)
        {
            bool flag = false;
            try
            {
                //获取上传案例图片路径
                path = Server.MapPath(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                base64Str=HttpUtility.UrlDecode(base64Str, System.Text.Encoding.GetEncoding(65001));
                byte[] bytes = Convert.FromBase64String(base64Str);
                //定义并实例化一个内存流，以存放提交上来的字节数组。
                MemoryStream m = new MemoryStream(bytes);
                //定义实际文件对象，保存上载的文件。
                FileStream f = new FileStream(path + @"\" + fileName, FileMode.Create);
                //把内存里的数据写入物理文件
                m.WriteTo(f);
                m.Close();
                f.Close();
                f = null;
                m = null;
                bytes = null;
                flag = true;
            }
            catch (Exception ex)
            {
                PublicModle.FileLog.Error(ex.ToString());
                flag = false;
            }
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Write(flag);
            Context.Response.End();
        }

        [WebMethod(Description = "下载服务器文件到本地的接口；strFilePath为文件相对路径；path服务器上的路径根目录；examno申请单号。", EnableSession = false)]
        public void DownloadFileBase64Str(string strFilePath, string path, string examno)
        {
            string base64Str = "";
            FileStream fs = null;
            string CurrentUploadFolderPath = Server.MapPath(path);
            string CurrentUploadFilePath = CurrentUploadFolderPath + @"\" + strFilePath;
            if (File.Exists(CurrentUploadFilePath))
            {
                try
                {
                    ///打开现有文件以进行读取。
                    fs = new FileStream(CurrentUploadFilePath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    fs.Flush();
                    fs.Close();
                    base64Str = Convert.ToBase64String(bytes);
                }
                catch (Exception ex)
                {
                    base64Str="";
                    PublicModle.FileLog.Error(ex.ToString());
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            {
                base64Str="";
            }
            Context.Response.ContentType = "application/xml; charset=utf-8";
            Context.Response.Charset = Encoding.UTF8.ToString(); //设置字符集类型
            Context.Response.ContentEncoding = Encoding.UTF8;
            base64Str = HttpUtility.UrlEncode(HttpUtility.UrlEncode(base64Str, System.Text.Encoding.GetEncoding(65001)));
            Context.Response.Write(base64Str);
            Context.Response.End();
        }

    }
}