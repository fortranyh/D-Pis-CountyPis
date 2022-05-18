using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
    public class xbx_templet
    {
        public DataTable GetTreeXbx_Templet(string docno)
        {
            string sqlstr = "select autoid, id,parentid,title,content,DocNo,TreeLevel,Clicked,flag from xbx_templet where DocNo='" + docno + "'";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTreeXbx_Templet 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取所有子节点
        public DataTable GetChildTreeXbx_Templet(string docno, string id)
        {
            string sqlstr = "select id from Xbx_templet where docno='" + docno + "' and parentid='" + id + "'";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetChildTreeXbx_Templet 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        public Boolean DelChildTreeXbx_Templet(string docno, string id)
        {
            string sqlstr = "delete from  Xbx_templet where docno='" + docno + "' and  id='" + id + "'";

            try
            {
                DBProcess._db.ExecuteNonQuery(CommandType.Text, sqlstr);
                return true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelChildTreeXbx_Templet 执行语句：" + sqlstr);

            }
            return false;
        }

        public Model.bgmb_templet GetOneTemplet(string docno, string id)
        {
            Model.bgmb_templet ins = null;
            string sqlstr = "select id,parentid,title,content,DocNo,TreeLevel,Clicked,flag from Xbx_templet where DocNo='" + docno + "' and id='" + id + "'"; ;

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
            string sqlstr = "insert into Xbx_templet(id,parentid,title,content,DocNo,TreeLevel,Clicked,flag)values(@id,@parentid,@title,@content,@DocNo,@TreeLevel,@Clicked,@flag)";
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



        public void InsertXbxtempletRoot(string docno, string uid, string suid)
        {
            string sqlstr = "insert into xbx_templet(id,parentid,title,content,DocNo,TreeLevel,Clicked,flag)values(@id,@parentid,@title,@content,@DocNo,@TreeLevel,@Clicked,@flag)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, uid);
                DBProcess._db.AddInParameter(cmd, "@parentid", DbType.String, "-1");
                DBProcess._db.AddInParameter(cmd, "@title", DbType.String, "公共模版");
                DBProcess._db.AddInParameter(cmd, "@content", DbType.String, "");
            
                DBProcess._db.AddInParameter(cmd, "@DocNo", DbType.String, docno);
                DBProcess._db.AddInParameter(cmd, "@TreeLevel", DbType.Int16, 1);
                DBProcess._db.AddInParameter(cmd, "@Clicked", DbType.Int32, 0);
                DBProcess._db.AddInParameter(cmd, "@flag", DbType.Int16, 1);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    cmd.Parameters.Clear();
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@id", DbType.String, suid);
                    DBProcess._db.AddInParameter(cmd, "@parentid", DbType.String, "-1");
                    DBProcess._db.AddInParameter(cmd, "@title", DbType.String, "私有模版");
                    DBProcess._db.AddInParameter(cmd, "@content", DbType.String, "");
                 
                    DBProcess._db.AddInParameter(cmd, "@DocNo", DbType.String, docno);
                    DBProcess._db.AddInParameter(cmd, "@TreeLevel", DbType.Int16, 1);
                    DBProcess._db.AddInParameter(cmd, "@Clicked", DbType.Int32, 0);
                    DBProcess._db.AddInParameter(cmd, "@flag", DbType.Int16, 0);
                    DBProcess._db.ExecuteNonQuery(cmd);
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertXbxtempletRoot 执行语句异常：" + sqlstr);
            }
        }

        public int updateXbxtempletTitle(string docno, string id, string title)
        {
            string sqlstr = "update xbx_templet set title=@title where docno=@docno and id=@id";
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
                DBProcess.ShowException(ex, "updateXbxtempletTitle 执行语句异常：" + sqlstr);
            }
            return 0;
        }

        public int updateXbxtempletContent(string docno, string id, string content)
        {
            string sqlstr = "update Xbx_templet set content=@content where docno=@docno and id=@id";
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
                DBProcess.ShowException(ex, "updateXbxtempletContent 执行语句异常：" + sqlstr);
            }
            return 0;
        }


        public Model.bgnr_templet GetContent(string id, string docno)
        {
            Model.bgnr_templet result = null;
            string sqlstr = "select content from  Xbx_templet where DocNo='" + docno + "' and id='" + id + "'";
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
    }
}
