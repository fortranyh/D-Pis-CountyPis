using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
    public class blzd_templet
    {

        public DataTable GetTreeBlzd_Templet(string docno)
        {
            string sqlstr = "select autoid, id,parentid,title,content,DocNo,TreeLevel,Clicked,flag from blzd_templet where DocNo='" + docno + "'";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTreeBlzd_Templet 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取所有子节点
        public DataTable GetChildTreeBlzd_Templet(string docno, string id)
        {
            string sqlstr = "select id from blzd_templet where docno='" + docno + "' and parentid='" + id + "'";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetChildTreeBlzd_Templet 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        public Boolean DelChildTreeBlzd_Templet(string docno, string id)
        {
            string sqlstr = "delete from  blzd_templet where docno='" + docno + "' and id='" + id + "'";

            try
            {
                DBProcess._db.ExecuteNonQuery(CommandType.Text, sqlstr);
                return true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelChildTreeBlzd_Templet 执行语句：" + sqlstr);

            }
            return false;
        }

        public Model.bgmb_templet GetOneTemplet(string docno, string id)
        {
            Model.bgmb_templet ins = null;
            string sqlstr = "select id,parentid,title,content,DocNo,TreeLevel,Clicked,flag from blzd_templet where DocNo='" + docno + "' and id='" + id + "'"; ;

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ins = new Model.bgmb_templet();
                    ins.id = dt.Rows[0]["id"].ToString();
                    ins.parentid = dt.Rows[0]["parentid"].ToString();
                    ins.title = dt.Rows[0]["title"].ToString();
                    ins.content = dt.Rows[0]["content"].ToString();
                  
                    ins.DocNo = dt.Rows[0]["DocNo"].ToString();
                    ins.TreeLevel = Convert.ToInt32(dt.Rows[0]["TreeLevel"]);
                    ins.Clicked = Convert.ToInt32(dt.Rows[0]["Clicked"]);
                    ins.flag = Convert.ToInt32(dt.Rows[0]["flag"]);
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetOneTemplet 执行语句：" + sqlstr);

            }
            return ins;
        }


        public Boolean InsertOneTemplet(Model.bgmb_templet ins)
        {
            string sqlstr = "insert into blzd_templet(id,parentid,title,content,DocNo,TreeLevel,Clicked,flag)values(@id,@parentid,@title,@content,@DocNo,@TreeLevel,@Clicked,@flag)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, ins.id);
                DBProcess._db.AddInParameter(cmd, "@parentid", DbType.String, ins.parentid);
                DBProcess._db.AddInParameter(cmd, "@title", DbType.String, ins.title);
                DBProcess._db.AddInParameter(cmd, "@content", DbType.String, ins.content);
                
                DBProcess._db.AddInParameter(cmd, "@DocNo", DbType.String, ins.DocNo);
                DBProcess._db.AddInParameter(cmd, "@TreeLevel", DbType.Int16, ins.TreeLevel);
                DBProcess._db.AddInParameter(cmd, "@Clicked", DbType.Int32, ins.Clicked);
                DBProcess._db.AddInParameter(cmd, "@flag", DbType.Int16, ins.flag);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertOneTemplet 执行语句：" + sqlstr);
            }
            return false;
        }



        public void InsertBLtempletRoot(string docno, string uid)
        {
            string sqlstr = "insert into blzd_templet(id,parentid,title,content,DocNo,TreeLevel,Clicked,flag)values(@id,@parentid,@title,@content,@DocNo,@TreeLevel,@Clicked,@flag)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, uid);
                DBProcess._db.AddInParameter(cmd, "@parentid", DbType.String, "-1");
                DBProcess._db.AddInParameter(cmd, "@title", DbType.String, "病理诊断模版");
                DBProcess._db.AddInParameter(cmd, "@content", DbType.String, "");
               
                DBProcess._db.AddInParameter(cmd, "@DocNo", DbType.String, docno);
                DBProcess._db.AddInParameter(cmd, "@TreeLevel", DbType.Int16, 1);
                DBProcess._db.AddInParameter(cmd, "@Clicked", DbType.Int32, 0);
                DBProcess._db.AddInParameter(cmd, "@flag", DbType.Int16, 0);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertBLtempletRoot 执行语句异常：" + sqlstr);
            }
        }

        public int updateBLtempletTitle(string docno, string id, string title)
        {
            string sqlstr = "update blzd_templet set title=@title where docno=@docno and id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@title", DbType.String, title);
                DBProcess._db.AddInParameter(cmd, "@docno", DbType.String, docno);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "updateBLtempletTitle 执行语句异常：" + sqlstr);
            }
            return 0;
        }

        public int updateBLtempletContent(string docno, string id, string content)
        {
            string sqlstr = "update blzd_templet set content=@content where docno=@docno and id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@content", DbType.String, content);

                DBProcess._db.AddInParameter(cmd, "@docno", DbType.String, docno);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "updateBLtempletContent 执行语句异常：" + sqlstr);
            }
            return 0;
        }


        public Model.bgnr_templet GetMContent(string id, string docno)
        {
            Model.bgnr_templet result = null;
            string sqlstr = "select content from  blzd_templet where DocNo='" + docno + "' and id='" + id + "'";
            try
            {
                DataTable dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    result = new Model.bgnr_templet();
                    result.content = dt.Rows[0][0].ToString();
                 
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetContent 执行语句：" + sqlstr);
            }
            return result;
        }
        public string GetContent(string id, string docno)
        {
            string result = "";
            string sqlstr = "select content from  blzd_templet where DocNo='" + docno + "' and id='" + id + "'";
            try
            {
                result = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetContent 执行语句：" + sqlstr);
            }
            return result;
        }


        public DataTable GetParentInfo(string id, string DocNo)
        {
            string sqlstr = "select ParentID,title,content,DocNo,TreeLevel,Clicked,flag from blzd_templet  where Id =@id and DocNo=@DocNo  order by TreeLevel,autoid";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                DBProcess._db.AddInParameter(cmd, "@docno", DbType.String, DocNo);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetParentInfo 执行语句：" + sqlstr);
            }
            return dt;
        }
        public int GetCount(string id, string DocNo)
        {
            string sqlstr = "select count(*) as icount from blzd_templet  where Id =@id and DocNo=@DocNo";
            int result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                DBProcess._db.AddInParameter(cmd, "@docno", DbType.String, DocNo);
                result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetCount 执行语句：" + sqlstr);
            }
            return result;
        }
    }
}
