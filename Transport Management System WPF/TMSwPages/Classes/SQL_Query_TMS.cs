﻿// ADMIN FILE HEADER COMMENT: =================================================================================
/*
 *  \file		SQL_Query_TMS.cs
 *  \ingroup	TMS
 *  \date		
 *  \author		8000 Cigarettes 
 *  \brief	     
 *  \see		
 *  \details    
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

//from https://www.codeproject.com/articles/43438/connect-c-to-mysql

namespace TMSwPages
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
        private bool isConnected;

        public MySqlConnection _connection { get { return connection; } }
        public string _server { get { return server; } }
        public string _database { get { return database; } }
        public string _uid { get { return uid; } }
        public string _password { get { return password; } }
        public bool _isConnected { get { return isConnected; } }

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

        // Alternate constructor
        public SQL_Query_TMS(string userType, string password)
        {
            isConnected = Initialize(userType, password);
        }

        public bool Initialize(string userType, string passKey)
        {
            server = "35.193.37.75";
            database = "duane_test";
            uid = userType.Trim().ToLower();
            password = passKey;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            bool returnValue = false; 

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                
            }
            catch (Exception e)
            {
                returnValue = false; 
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    returnValue = true; 
                }
            }

            return returnValue; 

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
        public bool OpenConnection()
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
                        TMSLogger.LogIt("Invalid username/password, please try again");
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
        public bool CloseConnection()
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
            string query = "SELECT * FROM Carrier;";

            //Create a list to store the result
            List<string>[] list = new List<string>[8];
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



        // Select list of 


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