using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Transport_Management_System_WPF
{
    class SQL_Query
    {
        public void ContractCalling()
        {
            string connetionString = "server=159.89.117.198;user=DevOSHT;database=cmp;port=3306;password=Snodgr4ss!";
            MySqlConnection connection = new MySqlConnection(connetionString);
            
            //SqlDataAdapter adapter = new SqlDataAdapter(); --- We don't need this line until we are updating the database
            //MySqlDataAdapter adapter = new MySqlDataAdapter(); //-- This is the class 
            try
            {
                Console.WriteLine("We is connecting bro");
                connection.Open();

                string sql = "SELECT * FROM Contract";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Console.WriteLine(reader[0]+"--"+reader[1]);
                }
                reader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            connection.Close();
            Console.WriteLine("Finto!");
        }
    }
}
//This can be deleted later when we know what we are doing
//This is Zena's Old Try Catch block that broke everything
                //try
                //{
                //    connection.Open();
                //    adapter.InsertCommand = new SqlCommand(sql, connection);
                //    adapter.InsertCommand.ExecuteNonQuery();
                //    MainWindow.SetOutput("Done !!");
                //}
                //catch (Exception ex)
                //{
                //    MainWindow.SetOutput((ex.ToString()));
                //}
