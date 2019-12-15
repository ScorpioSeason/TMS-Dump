using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public static class SQL
    {
        //variables
        public static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

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


        //This method will be used to initialize the connection
        public static void init()
        { 
            //This is a prototype application, so this data will not ever change
            server = "35.193.37.75";
            database = "duane_test";
            uid = "test";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            //set up the connection
            connection = new MySqlConnection(connectionString);
        }

        //This method will open up the connection to the database.
        public static bool open()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
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

        //this method will close the connection
        public static bool close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<object> SelectFromCMP(ParentTable tabletype)
        {
            SQL.close();
            int Use_Test_CMP = 0;

            string connectionString = string.Empty;

            if (Use_Test_CMP == 1)
            {
                server = "127.0.0.1";
                database = "Ivan_Test";
                uid = "root";
                password = "Conestoga1";
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            }
            else
            {
                //server = "";
                //database = "cmp";
                //uid = "DevOSHT";
                //password = "Snodgr4ss!";
                //put the real code here
                connectionString = "SERVER=" + "159.89.117.198" + ";" + "DATABASE=" + "cmp" + ";" + "UID=" + "DevOSHT" + ";" + "PASSWORD=" + "Snodgr4ss!" + ";";
            }
            //string connectionString;

            //set up the connection
            connection = new MySqlConnection(connectionString);

            SQL.open();

            List<object> RetrunedContracts = Select(tabletype, "SELECT * FROM Contract;");

            SQL.close();
            SQL.init();
            SQL.open();

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

            return inData;
        }

        //This select statement will take in a table type and specific select statement.
        //It will return a list of objects that can then be cast to the correct class type
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

            //return the list
            return outList;
        }

        //This select statement will take in a table type and a optional Id number for the table
        //It will return a list of objects that can then be cast to the correct class typeC:\Users\Ivan\Source\Repos\ScorpioSeason\TMS-Dump\TMS_8000C\TMSwPages\Classes\SQL.cs
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

            //return the list
            return outList;
        }

        //this method will be able to insert into any table that using the corresponding  ParentTable
        public static bool Insert(ParentTable input)
        {
            try
            {
                //get the insert statement
                string query = input.GetInsertStatment();

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                TMSBackup.WriteQueryToCurrentFile(new TMSBackupQuery(query));

                return true;
            }
            catch (Exception e)
            {
                //logit
                return false;
            }
        }

        //This method will be able to get the next available id for any table.
        //This will be used to ensure the uniqueness of primary keys
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

        //This method will be called if for a generic query.
        public static bool GenericFunction(string Query)
        {
            try
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(Query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //TMSBackup.WriteQueryToCurrentFile(new TMSBackupQuery(Query));

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void WipeEverything()
        {
            List<string> AllTableName = new List<string>();

            //AllTableName.Add("FC_DepotCity");
            //AllTableName.Add("FC_RouteSeg");
            //AllTableName.Add("FC_Invoice");
            //AllTableName.Add("FC_InvoiceContractLine");
            //AllTableName.Add("FC_Carrier");
            //AllTableName.Add("FC_TripTicket");
            //AllTableName.Add("FC_BuyerToPlannerContract");
            //AllTableName.Add("FC_CarrierNom");
            //AllTableName.Add("FC_LocalContract");
            //AllTableName.Add("FC_TripTicketLine");


            //foreach(string x in AllTableName)
            //{
            //    string query = "TRUNCATE TABLE " + x + ";";
            //    SQL.GenericFunction(query);
            //}

            string query = "DROP DATABASE IF EXISTS duane_test; CREATE DATABASE duane_test;";
            SQL.GenericFunction(query);

        }
    }
}
