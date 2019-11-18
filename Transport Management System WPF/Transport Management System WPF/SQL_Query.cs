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
    class SQL_Query
    {
        public static List<string> sqlReads = null;     //List to hold returns 

        public static void ContractCalling()            //This is contract calling from the contract marketplace
        {
            MySqlConnection connection = new MySqlConnection("database = 'cmd'; server = 159.89.117.198; port = 3306; user id = 'DevOSHT'; Password = 'Snodgr4ss!;'");

            var queryString = "SELECT * From Contract";      //Query text string 

            //connection.CommandType = System.Data.CommandType.Text;  //I want my connection string to be text
            
            //connection.CommandText = "SELECT * From Contract";      //Query text string 

            var command = new MySqlConnection(queryString, connection);     //This string might be better
            
            MySqlCommand command = connection.CreateCommand();      //Create the mySQL command
            try                         //"Try" :p
            {
                connection.Open();      //Open Connection
                MySqlDataReader reader = command.ExecuteReader();   //Call reader
             
                while (reader.Read())   //While there is something to read
                {
                    sqlReads.Add(reader.GetString(0));
                    sqlReads.Add(reader.GetString(1));
                    sqlReads.Add(reader.GetString(2));
                    sqlReads.Add(reader.GetString(3));
                    sqlReads.Add(reader.GetString(4));
                    sqlReads.Add(reader.GetString(5));
                }
                reader.Close();     //Close the reader - This means you need to properly open the reader duuuhhhhhhh
                
                connection.Close();     //Close Connection
            }
            catch (Exception ex)        //Exception: Gross!
            {
                MessageBox.Show(ex.Message);  //This will be used to log exception
                //Console.WriteLine(ex.Message);  //This is for Megan for debugging purposes - It can be deleted later
            }
        }
    }
}

