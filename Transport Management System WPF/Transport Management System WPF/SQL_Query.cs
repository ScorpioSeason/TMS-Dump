using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

//from https://www.codeproject.com/articles/43438/connect-c-to-mysql


namespace Transport_Management_System_WPF
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        public void Initialize()
        {
            server = "159.89.117.198";
            database = "cmp";
            uid = "DevOSHT";
            password = "Snodgr4ss!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);




        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM Contract;";

            //Create a list to store the result
            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();



            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["Client_Name"] + "");
                    list[1].Add(dataReader["Job_Type"] + "");
                    list[2].Add(dataReader["Quantity"] + "");
                    list[3].Add(dataReader["Origin"] + "");
                    list[4].Add(dataReader["Destination"] + "");
                    list[5].Add(dataReader["Van_Type"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }


        ////Insert statement
        //public void Insert()
        //{
        //}

        ////Update statement
        //public void Update()
        //{
        //}

        ////Delete statement
        //public void Delete()
        //{
        //}

        ////Select statement
        //public List<string>[] Select()
        //{
        //}

        ////Count statement
        //public int Count()
        //{
        //}

        ////Backup
        //public void Backup()
        //{
        //}

        ////Restore
        //public void Restore()
        //{
        //}
    }


    //class SQL_Query
    //{
    //    public string sql = null;
    //    public void ContractCalling()
    //    {
    //        sql = null;
    //        string connetionString = null;
    //        SqlConnection connection;
    //        SqlDataAdapter adapter = new SqlDataAdapter();
    //        connetionString = "Data Source=159.89.117.198,3306;Initial Catalog=cmp;User ID=DevOSHT;Password=Snodgr4ss!";
    //        connection = new SqlConnection(connetionString);
    //        sql = "SELECT * FROM Contract;";

    //        connection.Open();
    //        //adapter.InsertCommand = new SqlCommand(sql, connection);
    //        //adapter.InsertCommand.ExecuteNonQuery();

    //        SqlCommand cmd = new SqlCommand(sql, connection);
    //        using (SqlDataReader reader = cmd.ExecuteReader()) 
    //        {
    //            while (reader.Read())
    //            {  
    //              string returnString = reader.GetValue().ToString().Trim();  
    //            }
    //        }
    //    }
    //}
}