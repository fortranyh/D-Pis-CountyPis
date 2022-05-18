using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class Exam_Items
    {

        public Boolean InsertExamItems(string EXAM_NO, int EXAM_ITEM_NO, string EXAM_ITEM, string EXAM_ITEM_CODE, float COSTS)
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into exam_items(EXAM_NO,EXAM_ITEM_NO,EXAM_ITEM,EXAM_ITEM_CODE,COSTS)values(@EXAM_NO,@EXAM_ITEM_NO,@EXAM_ITEM,@EXAM_ITEM_CODE,@COSTS)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@EXAM_NO", DbType.String, EXAM_NO);
                DBProcess._db.AddInParameter(cmd, "@EXAM_ITEM_NO", DbType.Int16, EXAM_ITEM_NO);
                DBProcess._db.AddInParameter(cmd, "@EXAM_ITEM", DbType.String, EXAM_ITEM);
                DBProcess._db.AddInParameter(cmd, "@EXAM_ITEM_CODE", DbType.String, EXAM_ITEM_CODE);
                DBProcess._db.AddInParameter(cmd, "@COSTS", DbType.Single, COSTS);
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
                DBProcess.ShowException(ex, "InsertExamItems 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
    }
}
