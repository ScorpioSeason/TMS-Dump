using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Diagnostics;

namespace Transport_Management_System_WPF
{
    static class SQL_Query
    {
        public static string sql = null;
        public static List<string> sqlReads = null; 
        public static void ContractCalling()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("database = c,d; server = 159.89.117.198; port = 3306; user id = DevOSHT; Password = Snodgr4ss!;");
                

                con.Open();
                MySqlCommand com = con.CreateCommand();

                com.CommandType = System.Data.CommandType.Text;
                com.CommandText = "SELECT * From Contract";
                MySql.Data.MySqlClient.MySqlDataReader reader = com.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sqlReads.Add(reader.GetString(0));
                        sqlReads.Add(reader.GetString(1));
                        sqlReads.Add(reader.GetString(2));
                        sqlReads.Add(reader.GetString(3));
                        sqlReads.Add(reader.GetString(4));
                        sqlReads.Add(reader.GetString(5));
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            //sql = null;
            //string connectionString = null;
            //SqlConnection connection;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //connectionString = "server=159.89.117.198,3306;database=cmp;uid=DevOSHT;pwd=Snodgr4ss!;";
            //connection = new SqlConnection(connectionString);
            //sql = "SELECT * FROM Contract;";

            //connection.Open();
            ////adapter.InsertCommand = new SqlCommand(sql, connection);
            ////adapter.InsertCommand.ExecuteNonQuery();

            //SqlCommand cmd = new SqlCommand(sql, connection);
            //using (SqlDataReader reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        string readString = reader.ToString().Trim();
            //        sqlReads.Add(readString);
            //    }
            //}
            //connection.Close();
            //return;
        }
    }
}