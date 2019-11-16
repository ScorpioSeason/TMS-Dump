using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Transport_Management_System_WPF
{
    class SQL_Query
    {
        public static void ContractCalling()
        {
            string connetionString = null;
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = null;
            connetionString = "Data Source=159.89.117.198,3306;Initial Catalog=cmp;User ID=DevOSHT;Password=Snodgr4ss!";
            connection = new SqlConnection(connetionString);
            sql = "SELECT * FROM Contract;";
            try
            {
                connection.Open();
                adapter.InsertCommand = new SqlCommand(sql, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Done !!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
