using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
   public class exam_charge
    {
       //新建病人插入检查项目
       public Boolean Process_exam_charge(string charge_code, string exam_no,string doctor_code,string doctor_name,int fy_num)
       {
           Boolean Zx_Result = false;
           string sqlstr = "select * from charge_dict where charge_code=@charge_code";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@charge_code", DbType.String, charge_code);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               cmd.Parameters.Clear();
               if (ds != null && ds.Tables[0].Rows.Count == 1)
               {
                       //插入
                   sqlstr = "insert into exam_charge(exam_no,charge_code,charge_name,costs,doctor_code,doctor_name,fy_num)values(@exam_no,@charge_code,@charge_name,@costs,@doctor_code,@doctor_name,@fy_num)";
                       cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                       DBProcess._db.AddInParameter(cmd, "@charge_code", DbType.String, charge_code);
                       DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                       DBProcess._db.AddInParameter(cmd, "@charge_name", DbType.String, ds.Tables[0].Rows[0]["charge_name"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@costs", DbType.Single, (fy_num * Convert.ToSingle(ds.Tables[0].Rows[0]["costs"])));
                       DBProcess._db.AddInParameter(cmd, "@doctor_code", DbType.String, doctor_code);
                       DBProcess._db.AddInParameter(cmd, "@doctor_name", DbType.String, doctor_name);
                       DBProcess._db.AddInParameter(cmd, "@fy_num", DbType.Int16, fy_num);
                       if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                       {
                           Zx_Result = true;
                          
                       }
                       else
                       {
                           Zx_Result = false;
                          
                       }
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Process_exam_charge 执行语句异常：" + sqlstr);
               Zx_Result = false;
              
           }
           return Zx_Result;

       }
        //更新检查单的费用总额
       public Boolean Process_ExamCosts(string exam_no, ref string Str_Result)
       {
           Boolean Zx_Result = true;
           string sqlstr = "select sum(costs) as value from exam_charge where exam_no='" + exam_no + "'";
           try
           {
                   var costs =DBhleper.DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr);
                   if (costs is DBNull)
                   {
                       costs = 0;
                   }
                   sqlstr = "update exam_master set costs=" + Convert.ToSingle(costs).ToString() + " where exam_no='" + exam_no + "'";
                   if (DBProcess._db.ExecuteNonQuery(CommandType.Text,sqlstr) == 1)
                   {
                       Zx_Result = true;
                       Str_Result = "更新检查费用总额成功！";
                   }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Process_ExamCosts 执行语句异常：" + sqlstr);
               Zx_Result = false;
               Str_Result = "更新检查费用总额异常：" + ex.ToString();
           }
           return Zx_Result;
       }

       public DataTable GetExamChargeInfo(string exam_no)
       {
           string sqlstr = "select charge_code,charge_name from exam_charge where  exam_no=@exam_no";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               cmd.Parameters.Clear();
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetExamChargeInfo 执行语句异常：" + sqlstr);
           }
           return null;
       }
       //删除指定申请单下的所有费用信息
       public void DelCharges(string exam_no)
       {
           string sqlstr = "delete from exam_charge where  exam_no=@exam_no";
           try
           {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelCharges 执行语句：" + sqlstr);
           }
       }
       //删除指定id费用信息
       public int DelChargesFromId(int id)
       {
           int col = 0;
           string sqlstr = "delete from exam_charge where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
              col= DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelChargesFromId 执行语句：" + sqlstr);
           }
           return col;
       }
       //获取费用信息
       public DataTable GetFyInfo(string exam_no)
       {
           DataTable dt = null;
           string sqlstr = "select id,charge_code,charge_name,costs,doctor_code,doctor_name,fy_num, date_format(create_datetime,'%Y-%m-%d %H:%i:%s') as  create_datetime from exam_charge where exam_no=@exam_no";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
               dt=  DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetFyInfo 执行语句：" + sqlstr);
           }
           return dt;
       }
       

    }
}
