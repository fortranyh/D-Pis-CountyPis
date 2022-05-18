using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class icd10dict
    {
        public Boolean Inserticd10dict(string big_type_name, string icd_code, string icd_name, string icd_pinyin)
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into icd10_dict(big_type_name,icd_code,icd_name,icd_pinyin)values(@big_type_name,@icd_code,@icd_name,@icd_pinyin)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@big_type_name", DbType.String, big_type_name);
                DBProcess._db.AddInParameter(cmd, "@icd_code", DbType.String, icd_code);
                DBProcess._db.AddInParameter(cmd, "@icd_name", DbType.String, icd_name);
                DBProcess._db.AddInParameter(cmd, "@icd_pinyin", DbType.String, icd_pinyin);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    Zx_Result = true;
                }
                else
                {
                    Zx_Result = false;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Inserticd10dict 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }

        public DataSet GetDsICD10Py(string py)
        {
            DataSet ds = null;
            string sqlstr = "select big_type_name,icd_code,icd_name,icd_pinyin from icd10_dict where icd_enable=1 ";
            if (!py.Equals(""))
            {
                sqlstr += " and icd_pinyin like '%" + py + "%'";
            }
            sqlstr += " limit 300";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsICD10Py 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataSet GetDsICD10TypeDict()
        {
            DataSet ds = null;
            string sqlstr = "select big_type_name from icd10_dict group by big_type_name ";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsICD10TypeDict 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataSet GetDsICD10Type(string big_type_name)
        {
            DataSet ds = null;
            string sqlstr = "select big_type_name,icd_code,icd_name,icd_pinyin from icd10_dict where icd_enable=1 ";
            if (!big_type_name.Equals(""))
            {
                sqlstr += " and big_type_name = '" + big_type_name + "'";
            }
            sqlstr += " limit 300";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsICD10Type 执行语句：" + sqlstr);
            }
            return ds;
        }


    }
}
