// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		SQL_Query.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan,Ivan,Zena,Duane
 *  \brief	    This file contains the SQL Querys to the cmp.  
 *  \see		
 *  \details    This file holds the functionality to communicate withe the cmp database through MySQL.                                       
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

namespace TMSwPages
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		SQL_Query
    *   \brief		This class does all of the querys dealing with the cmp db.
    *   \details	public class.  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class SQL_Query
    {
        //variables
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			void SQL_Query()
        *	\brief		Constructor.
        *	\details	Constructor.
        *	\param[in]	none
        *	\param[out]	none
        *	\exception	none
        *	\see		Initialize()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Constructor
        public SQL_Query()
        {
            Initialize();
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			void Initialize()
        *	\brief		Sets all values for the sql connection.
        *	\details	Sets all values for the sql connection.
        *	\param[in]	none
        *	\param[out]	none
        *	\exception  none should be thrown
        *	\see		none
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Initialize values
        public void Initialize()
        {
            server = "159.89.117.198";
            database = "cmp";
            uid = "DevOSHT";
            password = "Snodgr4ss!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			bool OpenConnection()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	none
        *	\param[out]	none
        *	\exception	MySqlException ex, 0: Cannot connect to server.  Contact administrator / 1: Invalid username/password, please try again
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
                        TMSLogger.LogIt("Cannot connect to server.  Contact administrator");
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
        *	\fn			bool CloseConnection()
        *	\brief		Closes the connection to the SQL database server.
        *	\details	Closes the connection to the SQL database server and outputs an exeption to the log and to the screen if a exception is thrown.
        *	\param[in]	none
        *	\param[out]	none
        *	\exception	MySqlException
        *	\see		connection.Close()
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
                TMSLogger.LogIt(ex.Message);
                return false;
            }
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			Select_Contracts()
        *	\brief		public function that pulls all contracts from database for the buyer.
        *	\details	Pulls all contracts from database usinhg mysql querys and populates a list which and returns the list.
        *	\param[in]	void
        *	\param[out]	void
        *	\exception	no exceptions should be thrown.
        *	\see		BuyerClass Class
        *	\return		List<string>[]
        *
        * ---------------------------------------------------------------------------------------------------- */
        //Select statement
        public List<string>[] Select_Contracts()
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

}