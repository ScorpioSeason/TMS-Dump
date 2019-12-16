using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TMSwPages.Classes;

namespace TMSwPages.Classes
{
     // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class      SQL
    *   \brief      This is a class used for connecting to the database and running SQL querries
    * -------------------------------------------------------------------------------------------------------- */
    public static class SQL
    {
        //variables
        public static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        private const string ConstCmpIP = "159.89.117.198";

        private static string CMPserver = ConstCmpIP;
        private static string CMPdatabase;
        private static string CMPuid;
        private static string CMPpassword;

        
        public static void SetCMPIP(int inMode)
        {
            if(inMode == 1)
            {
                CMPserver = "127.0.0.1";
            }
            else
            {
                CMPserver = ConstCmpIP;
            }
        }

        public static void SetPassWord(string inString)
        {
            password = inString;
        }

        public static void SetDatabaseName(string inString)
        {
            database = inString;
        }

        public static void SetUserID(string inString)
        {
            uid = inString;
        }

        public static void SetServer(string inString)
        {
            server = inString;
        }




        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn         init    
        *   \brief      This method will be used to initialize the connection
        *   \param[in]  None     
        *   \param[out] None    
        *   \return     Void    
        * ---------------------------------------------------------------------------------------------------- */
        public static void init()
        { 
            //This is a prototype application, so this data will not ever change
            server = "35.193.37.75";
            database = "duane_test";
            //uid = "test";
            //password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            //set up the connection
            connection = new MySqlConnection(connectionString);

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "init" + " | " + "Confirmation" + " | " + "SQL connection initialized" + " | ");
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn         open    
        *   \brief      This method will open up the connection to the database
        *   \param[in]  none     
        *   \param[out] none    
        *   \return     bool    
        * ---------------------------------------------------------------------------------------------------- */
        public static bool open()
        {
            try
            {
                connection.Open();
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Open" + " | " + "Confirmation" + " | " + "SQL connection opened" + " | ");
                return true;
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Open" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                return false;
            }
        }

        public static void UpdateDepotAvalibility(int carrierID, string inCityName, int FTForLTL, int NewValue)
        {
            string avalType = "LTL";

            if (FTForLTL == 0)
            {
                avalType = "FTL";
            }
            
            
            string query = "update FC_DepotCity set " + avalType + "_Availibility = " + NewValue.ToString() +
                " where CityName = \'" + inCityName + "\' and FC_CarrierID = " + carrierID.ToString() + ";";

            SQL.GenericFunction(query);

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn         close    
        *   \brief      this method will close the connection
        *   \param[in]  none     
        *   \param[out] none    
        *   \return     bool    
        * ---------------------------------------------------------------------------------------------------- */
        public static bool close()
        {
            try
            {
                connection.Close();
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "close" + " | " + "Confirmation" + " | " + "SQL connection closed" + " | ");
                return true;
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "close" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                return false;
            }
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn         SelectFromCMP    
        *   \brief      This will connection to the contract market place, query, and store the results in a 
        *               list
        *   \param[in]  ParentTable tabletyp     
        *   \param[out] None    
        *   \return     List<object>     
        * ---------------------------------------------------------------------------------------------------- */
        public static List<object> SelectFromCMP(ParentTable tabletype)
        {
            SQL.close();
            int Use_Test_CMP = 0;

            string connectionString = string.Empty;

            if (Use_Test_CMP == 0)
            {
                connectionString = "SERVER=" + "127.0.0.1" + ";" + "DATABASE=" + "Ivan_Test" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "Conestoga1" + ";";
            }
            else 
            {

                //CMPserver = "159.89.117.198";
                CMPdatabase = "cmp";
                CMPuid = "DevOSHT";
                CMPpassword = "Snodgr4ss!";
                connectionString = "SERVER=" + CMPserver + ";" + "DATABASE=" + CMPdatabase + ";" + "UID=" + CMPuid + ";" + "PASSWORD=" + CMPpassword + ";";
            }
            //string connectionString;

            //set up the connection
            connection = new MySqlConnection(connectionString);

            SQL.open();

            List<object> RetrunedContracts = Select(tabletype, "SELECT * FROM Contract;");

            SQL.close();
            SQL.init();
            SQL.open();

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "SelectFromCMP" + " | " + "Confirmation" + " | " + "Data selected from CMP" + " | ");

            return RetrunedContracts;
        }


        public static List<CustomerName> GetAllCustomerNames()
        {
            string query = "select DISTINCT client_name from FC_LocalContract;";

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            List<CustomerName> inData = new List<CustomerName>();       
            
            while (dataReader.Read())
            {
                CustomerName temp = new CustomerName();
                temp.CustName = dataReader["client_name"] + "";
                inData.Add(temp);
            }

            dataReader.Close();

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "GetAllCustomerNames" + " | " + "Confirmation" + " | " + "Customer names loaded" + " | ");

            return inData;
        }

         // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn            Select 
        *   \brief         This select statement will take in a table type and specific select statement
        *                  It will return a list of objects that can then be cast to the correct class type
        *   \param[in]     ParentTable tabletype, String InputStatment  
        *   \param[out]    None 
        *   \return        List<object> 
        * ---------------------------------------------------------------------------------------------------- */
        public static List<object> Select(ParentTable tabletype, String InputStatment)
        {
            //Create an array of lists to store the result
            List<string>[] list = new List<string>[tabletype.GetColoumInt()];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<string>();
            }

            //set up the command and reader
            MySqlCommand cmd = new MySqlCommand(InputStatment, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //get all the column names in the table
            List<string> ColomNames = tabletype.GetColoumNames();

            //Read the data and store them in the array of lists
            while (dataReader.Read())
            {
                for (int i = 0; i < list.Length; i++)
                {
                    list[i].Add(dataReader[ColomNames[i]] + "");
                }
            }

            //close Data Reader
            dataReader.Close();

            //take the array of lists, and convert it to a list of objects
            List<object> outList = tabletype.PackageClasses(list);

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Select" + " | " + "Confirmation" + " | " + "Data selected with SQL" + " | ");

            //return the list
            return outList;
        }

         // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn             Select
        *   \brief          This select statement will take in a table type and a optional Id number for the table
        *                   It will return a list of objects that can then be cast to the correct class type
        *   \param[in]      ParentTable tabletype, int TableID = -1 
        *   \param[out]     None
        *   \return         List<object>
        * ---------------------------------------------------------------------------------------------------- */
        public static List<object> Select(ParentTable tabletype, int TableID = -1)
        {
            //get the select statement for the table
            string query = tabletype.GetSelectStatment();

            if (TableID != -1)
            { 
                //if a id was passed in, add a where class to the select statement
                string sufix = " where " + tabletype.GetTableName() + "ID = " + TableID.ToString() + ";";
                query = query.Replace(";", sufix);
            }

            //Create as array list to store the result
            List<string>[] list = new List<string>[tabletype.GetColoumInt()];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<string>();
            }

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //get the column names from the 
            List<string> ColomNames = tabletype.GetColoumNames();

            //Read the data and store them in the array of lists
            while (dataReader.Read())
            {
                for (int i = 0; i < list.Length; i++)
                {
                    list[i].Add(dataReader[ColomNames[i]] + "");
                }
            }

            //close Data Reader
            dataReader.Close();

            //take the array of lists, and convert it to a list of objects
            List<object> outList = tabletype.PackageClasses(list);

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Select" + " | " + "Confirmation" + " | " + "Data selected with SQL" + " | ");

            //return the list
            return outList;
        }
        
        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn            Insert 
        *   \brief         this method will be able to insert into any table that using the corresponding 
        *                  ParentTable
        *   \param[in]     ParentTable input  
        *   \param[out]    none 
        *   \return        bool 
        * ---------------------------------------------------------------------------------------------------- */
        public static bool Insert(ParentTable input)
        {
            try
            {
                //get the insert statement
                string query = input.GetInsertStatment();

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                if (cmd.ExecuteNonQuery() > 0 )
                {
                    TMSBackup.WriteQueryToCurrentFile(new TMSBackupQuery(query));
                }

                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Insert" + " | " + "Confirmation" + " | " + "Data inserted into SQL database" + " | ");
                return true;
            }
            catch (Exception e)
            {
                //logit
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "Insert" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                return false;
            }
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn           GetNextID  
        *   \brief        This method will be able to get the next available id for any table
        *                 This will be used to ensure the uniqueness of primary keys
        *   \param[in]    string TableName   
        *   \param[out]   none  
        *   \return       int  
        * ---------------------------------------------------------------------------------------------------- */
        public static int GetNextID(string TableName)
        {
            //setup the select statement
            string query = "SELECT * FROM " + TableName + ";";

            //Create a list to store the result
            List<string>[] list = new List<string>[1];
            list[0] = new List<string>();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader[(TableName + "ID")] + "");
            }

            //close Data Reader
            dataReader.Close();

            List<int> inIDs = new List<int>();

            //convert the read in data to ints
            for (int i = 0; i < list[0].Count; i++)
            {
                inIDs.Add(int.Parse(list[0][i]));
            }

            int currentIndex = -1;

            foreach (int x in inIDs)
            {
                if (x > currentIndex)
                {
                    currentIndex = x;
                }
            }

            if (currentIndex == -1)
            {
                currentIndex++;
            }

            //return the next available id in the table
            return ++currentIndex;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *   \fn          GenericFunction   
        *   \brief       This method will be called if for a generic query
        *   \param[in]   string Query    
        *   \param[out]  none   
        *   \return      bool   
        * ---------------------------------------------------------------------------------------------------- */
        public static bool GenericFunction(string Query)
        {
            try
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(Query, connection);

                //Execute command
                if (cmd.ExecuteNonQuery() > 0 )
                {
                    TMSBackup.WriteQueryToCurrentFile(new TMSBackupQuery(Query));
                }

                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "GenericFunction" + " | " + "Confirmation" + " | " + "Query executed successfully" + " | ");
                return true;
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "GenericFunction" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                return false;
            }
        }

        public static void WipeEverything()
        {
            List<string> AllTableName = new List<string>();

            string query = "DROP DATABASE IF EXISTS duane_test; CREATE DATABASE duane_test;";
            SQL.GenericFunction(query);

            // View Save As File Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "sql Files (*.sql)|*.sql";

            if (openFileDialog.ShowDialog() == true)
            {
                string text = System.IO.File.ReadAllText(openFileDialog.FileName);
                SQL.GenericFunction(text);
            }

            TMSLogger.LogIt(" | " + "SQL.cs" + " | " + "SQL" + " | " + "WipeEverything" + " | " + "Confirmation" + " | " + "Database was wiped" + " | ");
        }
    }
}
