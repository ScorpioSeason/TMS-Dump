// ADMIN FILE HEADER COMMENT: =================================================================================
/*
 *  \file		Admin.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the admin functionality	  
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
 *
 * =========================================================================================================== */

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
using TMSwPages;

//from https://www.codeproject.com/articles/43438/connect-c-to-mysql

namespace Transport_Management_System_WPF
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /*
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class SQL_Query_TMS
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Constructor
        public SQL_Query_TMS()
        {
            Initialize();
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Initialize values
        public void Initialize()
        {
            server = "35.193.37.75";
            database = "cigtms";
            uid = "test";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
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
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        .LogIt("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        
        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
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
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Select statement
        public List<string>[] Select_Carriers()
        {
            string query = "SELECT * FROM CUS;";

            //Create a list to store the result
            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

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
                    list[0].Add(dataReader["cName"] + "");
                    list[1].Add(dataReader["CityID"] + "");
                    list[2].Add(dataReader["FTLA"] + "");
                    list[3].Add(dataReader["LTLA"] + "");
                    list[4].Add(dataReader["FTLRate"] + "");
                    list[5].Add(dataReader["LTLRate"] + "");
                    list[7].Add(dataReader["reefCharge"] + "");
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