using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Transport_Management_System_WPF
{
    static class SQL_Query
    {
        public static string sql = null;
        public static List<string> sqlReads = null; 
        public static void ContractCalling()
        {
            sql = null;
            string connetionString = null;
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connetionString = "Data Source=159.89.117.198,3306;Initial Catalog=cmp;User ID=DevOSHT;Password=Snodgr4ss!";
            connection = new SqlConnection(connetionString);
            sql = "SELECT * FROM Contract;";
            
            connection.Open();
            //adapter.InsertCommand = new SqlCommand(sql, connection);
            //adapter.InsertCommand.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand(sql, connection);
            using (SqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    string readString = reader.ToString().Trim();
                    sqlReads.Add(readString);
                }
            }
            return;
        }
    }
}