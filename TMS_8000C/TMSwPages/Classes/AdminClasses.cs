﻿// ADMINCLASSES FILE HEADER COMMENT: ===========================================================================
/**
 *  \file		AdminClasses.cs
 *  \ingroup	TMS
 *  \date		November 22, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the admin functionality	  
 *  \details    This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
 *
 * =========================================================================================================== */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using TMSwPages.Classes;

namespace TMSwPages.Classes
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class	    Admin
    *   \brief		This class runs the Admin UI functionality. 
    *   \details	The AdminPage codebehind should make calls to this. This holds functions that the user can 
    *   choose to run. For example button presses and UI drawingRight now this class does nothing.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class Admin
    {
        SQL_Query_TMS adminTMSConnection = null;
        string query = ""; 
       // public List<Carrier> carrierTable = new List<Carrier>();
        //public List<Route> routeTable = new List<Route>();
        //public List<Fee> feeTable = new List<Fee>();

        public void SetTMSConnection(SQL_Query_TMS validConnection)
        {
            adminTMSConnection = validConnection;
        }
        public SQL_Query_TMS GetTMSConnection()
        {
            return adminTMSConnection;
        }
        
        public List<string>[] DisplayCarrier() {

            int numColumns = 0;
            List<string> columnNames = new List<string>();
            List<string>[] columns = null; 

            try
            {
                query = "SELECT * FROM Carrier;";
                columnNames.Clear();
                columnNames.Add("CarrierID");
                columnNames.Add("Carrier_Name");
                numColumns = columnNames.Count();
                
                columns = new List<string>[numColumns];

                if (adminTMSConnection.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int i = 0;

                        foreach (string s in columnNames)
                        {
                            columns[i].Add(dataReader[s] + "");
                            i++;
                        }

                    }

                    dataReader.Close();

                    adminTMSConnection.CloseConnection();
                }

                return columns;
            }
            catch (Exception e)
            {
                return columns;
            }
            
        }

        public List<string>[] DisplayRoutes()
        {
            int numColumns = 0;
            List<string> columnNames = new List<string>();
            List<string>[] columns = null;

            try
            {
                query = "SELECT * FROM RouteData;";
                columnNames.Clear();
                columnNames.Add("RouteDataID");
                columnNames.Add("CityA");
                columnNames.Add("CityB");
                columnNames.Add("PickUpTime");
                columnNames.Add("DropOffTime");
                columnNames.Add("LtlTime");
                columnNames.Add("DrivenTime");
                numColumns = columnNames.Count();
                
                columns = new List<string>[numColumns];

                if (adminTMSConnection.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int i = 0;

                        foreach (string s in columnNames)
                        {
                            columns[i].Add(dataReader[s] + "");
                            i++;
                        }

                    }

                    dataReader.Close();

                    adminTMSConnection.CloseConnection();
                }

                return columns;
            }
            catch (Exception e)
            {
                return columns;
            }
        }

        public List<CarrierDepot> DisplayFees()
        {
            List<string>[] columns = new List<string>[7];
            List<CarrierDepot> ReadInData = new List<CarrierDepot>();

            for (int i = 0; i < 7; i++)
            {
                columns[i] = new List<string>();
            }
            
            try
            {
                query = "SELECT * FROM CarrierDepot;";
                       
                if (adminTMSConnection.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        columns[0].Add(dataReader["CityName"] + "");
                        columns[1].Add(dataReader["CarrierID"] + "");
                        columns[2].Add(dataReader["FTL_Availibility"] + "");// spelled wrong in database
                        columns[3].Add(dataReader["LTL_Availibility"] + "");
                        columns[4].Add(dataReader["FTL_Rate"] + "");
                        columns[5].Add(dataReader["LTL_Rate"] + "");
                        columns[6].Add(dataReader["Reefer_Charge"] + "");

                    }

                    dataReader.Close();
                    
                    for (int i = 0; i < columns[0].Count(); i++)
                    {
                        CarrierDepot current = new CarrierDepot();

                        current.CityName = columns[0][i];
                        current.CarrierID = Int32.Parse(columns[1][i]);
                        current.FTL_Availibility = Int32.Parse(columns[2][i]);
                        current.LTL_Availibility = Int32.Parse(columns[3][i]);
                        current.FTL_Rate = Double.Parse(columns[4][i]);
                        current.LTL_Rate = Double.Parse(columns[5][i]);
                        current.Reefer_Charge = Double.Parse(columns[6][i]);

                        ReadInData.Add(current); 
                    }

                    adminTMSConnection.CloseConnection();

                }

                return ReadInData;
            }
            catch (Exception e)
            {
                return ReadInData;
            }

            
        }
        
        public void SelectData() { }
        public void DeleteSelection() { }
        public void AddNew()
        {
            // column
            // row
            // cell
        }

    }



    // Backup Option 1: PHP
    /*" <?php
        include 'config.php';
        include 'opendb.php';

        $tableName  = 'mypet';
        $backupFile = 'backup/mypet.sql';
        $query      = "SELECT * INTO OUTFILE '$backupFile' FROM $tableName";
        $result = mysql_query($query);

        include 'closedb.php';
        ?> 

    // Restore
        <?php
        include 'config.php';
        include 'opendb.php';

        $tableName  = 'mypet';
        $backupFile = 'mypet.sql';
        $query      = "LOAD DATA INFILE 'backupFile' INTO TABLE $tableName";
        $result = mysql_query($query);

        include 'closedb.php';
        ?>
     "*/

    // Backup Option 2: Command Line
    /*"
        Change to the bin subdirectory in the directory where MySQL is installed.

        For instance, type cd c:Program FilesMySQLMySQL Server 5.0bin into the command prompt.

        Type the following:

        mysqldump --user=accountname --password=password databasename >pathbackupfilename

    "*/

    // Backup Option 3: Flush
    /*"
     Making Backups by Copying Table Files
    For storage engines that represent each table using its own files, tables can be backed up by 
    copying those files. For example, MyISAM tables are stored as files, so it is easy to do a backup 
    by copying files (*.frm, *.MYD, and *.MYI files). To get a consistent backup, stop the server or 
    lock and flush the relevant tables:

    FLUSH TABLES tbl_list WITH READ LOCK;
    You need only a read lock; this enables other clients to continue to query the tables while you are
    making a copy of the files in the database directory. The flush is needed to ensure that the all 
    active index pages are written to disk before you start the backup. See Section 13.3.5, “LOCK 
    TABLES and UNLOCK TABLES Statements”, and Section 13.7.6.3, “FLUSH Statement”.

    You can also create a binary backup simply by copying all table files, as long as the server 
    isn't updating anything. (But note that table file copying methods do not work if your database 
    contains InnoDB tables. Also, even if the server is not actively updating data, InnoDB may still 
    have modified data cached in memory and not flushed to disk.)
    "*/

    // Backup Option 4: Delimited-Text file Backups 
    /*
     "Making Delimited-Text File Backups
    To create a text file containing a table's data, you can use 
    SELECT * INTO OUTFILE 'file_name' FROM tbl_name. The file is created on the MySQL server host, 
    not the client host. For this statement, the output file cannot already exist because permitting 
    files to be overwritten constitutes a security risk. See Section 13.2.9, “SELECT Statement”. 
    This method works for any kind of data file, but saves only table data, not the table structure.

    Another way to create text data files (along with files containing CREATE TABLE statements for the
    backed up tables) is to use mysqldump with the --tab option. See Section 7.4.3, “Dumping Data in 
    Delimited-Text Format with mysqldump”.

    To reload a delimited-text data file, use LOAD DATA or mysqlimport.
    "*/

    // Backup Option 5: Binary Logs
    /*"
     Making Incremental Backups by Enabling the Binary Log
     MySQL supports incremental backups: You must start the server with the --log-bin option to enable 
     binary logging; see Section 5.4.4, “The Binary Log”. The binary log files provide you with the 
     information you need to replicate changes to the database that are made subsequent to the point 
     at which you performed a backup. At the moment you want to make an incremental backup (containing 
     all changes that happened since the last full or incremental backup), you should rotate the binary 
     log by using FLUSH LOGS. This done, you need to copy to the backup location all binary logs which 
     range from the one of the moment of the last full or incremental backup to the last but one. These 
     binary logs are the incremental backup; at restore time, you apply them as explained in Section 7.5, 
     “Point-in-Time (Incremental) Recovery Using the Binary Log”. The next time you do a full backup, 
     you should also rotate the binary log using FLUSH LOGS or mysqldump --flush-logs. See Section 4.5.4, 
     “mysqldump — A Database Backup Program”.

   "*/

    // Backup Notes: Recovering Corrupt tables
    /*"Recovering Corrupt Tables
       If you have to restore MyISAM tables that have become corrupt, try to recover them using REPAIR TABLE 
       or myisamchk -r first. That should work in 99.9% of all cases. If myisamchk fails, see Section 7.6, 
       “MyISAM Table Maintenance and Crash Recovery”.

    "*/


    // Read store location
    // Choose new store location
    // Copy files from old location to new location
    // If error, inform user, do not delete old copy. 
    // Else if success, inform user, delete old copy

    // Initiate backup (button) 
    // Write to location
    // If successful write, delete old copy of backup (do not immediately overwrite)

    /*DROP TABLE IF EXISTS 'Carrier';
         CREATE TABLE 'Carrier' (
         'Carrier_Name' VARCHAR(100),
         'FTL_Rate' DOUBLE(5, 4),
         'LTL_Rate' DOUBLE(5, 4),
         'Reefer_Charge' DOUBLE(5, 4),
         'FTL_Availability' INT,
         'LTL_Availability' INT,
         PRIMARY KEY('Carrier_Name')
         )
         */

    /*
    DROP TABLE IF EXISTS 'RouteData';
    CREATE TABLE 'RouteData' (
    'RouteDataID' VARCHAR(100),
    'CityA' VARCHAR(100),
    'CityB' VARCHAR(100),
    'PickUpTime' DOUBLE(2, 2),
    'DropOffTime' DOUBLE(2, 2),
    'LtlTime' DOUBLE(2, 2),
    'DrivenTime' DOUBLE(2, 2),
    PRIMARY KEY('RouteDataID'),
    FOREIGN KEY('CityA') REFERENCES Location('CityID'),
    FOREIGN KEY('CityB') REFERENCES Location('CityID')) 
     */

    /*
    cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge
    Planet Express,Windsor,50,640,5.21,0.3621,0.08
    ,Hamilton,50,640,5.21,0.3621,0.08
    ,Oshawa,50,640,5.21,0.3621,0.08
    ,Belleville,50,640,5.21,0.3621,0.08
    ,Ottawa,50,640,5.21,0.3621,0.08
    Schooner's,London,18,98,5.05,0.3434,0.07
    ,Toronto,18,98,5.05,0.3434,0.07
    ,Kingston,18,98,5.05,0.3434,0.07
    Tillman Transport,Windsor,24,35,5.11,0.3012,0.09
    ,London,18,45,5.11,0.3012,0.09
    ,Hamilton,18,45,5.11,0.3012,0.09
    We Haul,Ottawa,11,0,5.2,0,0.065
    ,Toronto,11,0,5.2,0,0.065
    */




    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		TMSLogger
    *   \brief		This class runs the logging functionality for all files in this solution
    *   \details	This is a static class containing a list of logs and the methods to create new logs. 
    *               The main function of this class is to call the TMSLog class. 
    *   
    * -------------------------------------------------------------------------------------------------------- */
    static public class TMSLogger
    {
        static public string LogFilePath;                                  /// Stores location of the log file
        static public List<TMSLog> logs = new List<TMSLog>();       /// This is a list of logs stored locally
        static public string thisFileDir;

        static public void SetDefaultLogFilePath()
        {
            try
            {
                LogFilePath = Environment.CurrentDirectory + "/TMSLogger.txt";
                thisFileDir = Environment.CurrentDirectory;
            }
            catch (Exception e)
            {
                // error finding/starting logger file. 
            }

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			static public void LogIt(string newLogString)
        *	\brief		This creates a new log object
        *	\details	This creates a new log object, adds it to the local list, then appends it to the 
        *	external file
        *	\param[in]	string  newLogString    This is the unparsed BSV string to create the file. 
        *	\see		TMSLog(), logs.Add(), AppendLogFile()
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        static public void LogIt(string newLogString)
        {
            TMSLog myLog = new TMSLog(newLogString);
            logs.Add(myLog);
            if (AppendLogFile(myLog) == false)
            {
                /// Error writing to Log File: Try once again
                AppendLogFile(myLog);
            };

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			static public void NewLog(string newLogString)
        *	\brief		This creates a new log object
        *	\details	This creates a new log object, and adds it to the local list. This exists so that the 
        *	function ReadExistingLogFile doesn't call recursively. 
        *	\param[in]	string  newLogString    This is the unparsed BSV string to create the file. 
        *	\see		TMSLog(), logs.Add()
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        static public void NewLog(string newLogString)
        {
            TMSLog myLog = new TMSLog(newLogString);
            logs.Add(myLog);
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			static public bool ReadExistingLogFile()
        *	\brief		Reads logs in from a file
        *	\details	This clears the local log list then reads logs into it from the file specified by the 
        *	            loggerPath variable. If there is an error, it throws an exception to a new log.
        *	\exception	From FileStream and StreamReader
        *	\see		logs.Clear(), NewLog(), LogIt()
        *	\return		bool    readSuccess    A value indicating whether the file read threw an exception. 
        *
        * ---------------------------------------------------------------------------------------------------- */
        static public bool ReadExistingLogFile()
        {
            bool readSuccess = true;

            try
            {
                /// Clear out the working list 
                logs.Clear();

                /// Open the file stream to read from the file
                FileStream fileStream = new FileStream((TMSLogger.LogFilePath), FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);

                /// Fill the working list with lines from the file 
                while (!streamReader.EndOfStream)
                {
                    string lineString = "";
                    if ((lineString = streamReader.ReadLine()).Trim() != "")
                    {
                        NewLog(lineString);
                    }

                }

                /// Close the file
                streamReader.Close(); fileStream.Close();
            }
            /// If an exception is thrown here, create a log for it. 
            catch (Exception e)
            {
                LogIt("|" + thisFileDir + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "ReadExistingLogFile" + "|" + "Exception" + "|" + e.Message + "|");
                readSuccess = false;
            }

            return readSuccess;

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			static public bool AppendLogFile()
        *	\brief		Writes new logs into a file
        *	\details	This writes individual logs into the file specified by the loggerPath variable. 
        *	If there is an error, it throws an exception to a new log.
        *	\exception	From FileStream and StreamWriter
        *	\see		LogIt()
        *	\return		bool    appendSuccess    A value indicating whether the file write threw an exception. 
        *
        * ---------------------------------------------------------------------------------------------------- */
        static public bool AppendLogFile(TMSLog newLog)
        {
            bool appendSuccess = true;

            try
            {
                /// Open the filestream to append to the file. 
                FileStream fileStream = new FileStream(TMSLogger.LogFilePath, FileMode.Append, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);

                /// Add each log entry from the working list to the file as a BSV
                fileWriter.WriteLine(newLog.BSV);
                fileWriter.Flush();

                /// Close the file
                fileWriter.Close(); fileStream.Close();
            }
            /// If an exception is thrown here, catch it
            catch (Exception e)
            {
                /// This could become problematic as it calls itself
                LogIt("|" + thisFileDir + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "AppendLogFile" + "|" + "Exception" + "|" + e.Message + "|");
                appendSuccess = false;
            }

            return appendSuccess;

        }

    }

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		TMSLog
    *   \brief		This class instantiates and stores a TMSLog object. 
    *   \details	This is mainly just called by the TMSLogger class.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class TMSLog
    {
        /// Data for each individual log (not currently protected)
        string _logPath;
        string _logClass;
        string _logMethod;
        string _logType;
        string _logMessage;
        string _BSV;                  /// Bar separated values
        DateTime _logTime;

        // Public accessors 
        public string logPath { get { return _logPath; } }
        public string logClass { get { return _logClass; } }
        public string logMethod { get { return _logMethod; } }
        public string logType { get { return _logType; } }
        public string logMessage { get { return _logMessage; } }
        public string BSV { get { return _BSV; } }                  /// Bar separated values
        public DateTime logTime { get { return _logTime; } }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			TMSLog(string nUnparsed)
        *	\brief		Constructor for the TMSLog class
        *	\details	This takes in a specifically formatted string of BSV (Bar separated values) and parses 
        *	it to create a new log object. If the string isn't parsed correctly, a new log is created which 
        *	acknowledges this. 
        *	\param[in]	string  nUnparsed   An incoming value meant to be parsed into a log object
        *	\see		Regex.Matches(), nUnparsed.Split(), DateTime.Parse()
        *	\return		Creates a TMSLog object
        *
        * ---------------------------------------------------------------------------------------------------- */
        public TMSLog(string nUnparsed)
        {
            /// Used in Logger / LogIt call
            if ((Regex.Matches(nUnparsed, "\\|").Count) == 6)
            {
                string[] temp = nUnparsed.Split('|');
                _logPath = temp[1];
                _logClass = temp[2];
                _logMethod = temp[3];
                _logType = temp[4];
                _logMessage = temp[5];
                _logTime = DateTime.Now;
            }
            /// Used in reading in from a file
            else if ((Regex.Matches(nUnparsed, "\\|").Count) == 7)
            {
                string[] temp = nUnparsed.Split('|');
                _logTime = DateTime.Parse(temp[1]);
                _logPath = temp[2];
                _logClass = temp[3];
                _logMethod = temp[4];
                _logType = temp[5];
                _logMessage = temp[6];
            }
            /// Used in formatting error
            else
            {
                _logPath = TMSLogger.thisFileDir + "/AdminClasses.cs";
                _logClass = "TMSLog";
                _logMethod = "Constructor";
                _logType = "LogParseError";
                _logMessage = "The log message did not enter as the correct string format";
                _logTime = DateTime.Now;
                nUnparsed = "|" + _logPath + "|" + _logClass + "|" + _logMethod + "|" + _logType + "|" + _logMessage + "|";
            }

            _BSV = "|" + _logTime + nUnparsed;

        }

    }

}

   