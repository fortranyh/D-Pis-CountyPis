using MySql.Data.MySqlClient;
using System;

namespace DBHelper.BLL
{
    public class GetUidKey
    {
        public UInt64 InsertAndGetLastId(ref int IRowCount, string query)
        {
            UInt64 uid = 0;
            using (var connection = new MySqlConnection(DBProcess._db.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    IRowCount = command.ExecuteNonQuery();
                }

                const string queryForLastId = "SELECT LAST_INSERT_ID() AS LastId;";
                using (var command = new MySqlCommand(queryForLastId, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            uid = Convert.ToUInt64(reader["LastId"]);
                        }
                    }
                }
            }
            return uid;
        }

        public int ExecuteNonQuery(string query)
        {
            using (var connection = new MySqlConnection(DBProcess._db.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

    }
}
