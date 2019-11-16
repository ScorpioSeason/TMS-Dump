using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftQualv1
{
    class Program
    {
        static void Main(string[] args)
        {

            GraphClass graphClass = new GraphClass();
            graphClass.getTravelData(1, 2, true);

            //MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            //conn_string.Server = "mysql7.000webhost.com";
            //conn_string.UserID = "a455555_test";
            //conn_string.Password = "a455555_me";
            //conn_string.Database = "xxxxxxxx";

            //using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            //using (MySqlCommand cmd = conn.CreateCommand())
            //{    //watch out for this SQL injection vulnerability below
            //    cmd.CommandText = string.Format("INSERT Test (lat, long) VALUES ({0},{1})",
            //                                   OSGconv.deciLat, OSGconv.deciLon);
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //}




            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = "Server='159.89.117.198:3306'; Database='CMP'; Uid='DevOSHT'; Pwd = 'Snodgr4ss!';";
            //conn.Open();

            //SqlCommand command = new SqlCommand("SELECT * FROM Contract;", conn);

            //conn.Close();
            //conn.Dispose();

        }
    }
}
