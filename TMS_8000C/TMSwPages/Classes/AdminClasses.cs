// ADMINCLASSES FILE HEADER COMMENT: ===========================================================================
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
using System.Windows.Forms;

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
        
        ////SQL_Query_TMS adminTMSConnection = null;
        //string query = "";


        //// public List<Carrier> carrierTable = new List<Carrier>();
        ////public List<Route> routeTable = new List<Route>();
        ////public List<Fee> feeTable = new List<Fee>();

        ////public void SetTMSConnection(SQL_Query_TMS validConnection)
        ////{
        ////    adminTMSConnection = validConnection;
        ////}
        ////public SQL_Query_TMS GetTMSConnection()
        ////{
        ////    return adminTMSConnection;
        ////}

        //public List<string>[] DisplayCarrier() {

        //    int numColumns = 0;
        //    List<string> columnNames = new List<string>();
        //    List<string>[] columns = null; 

        //    try
        //    {
        //        query = "SELECT * FROM Carrier;";
        //        columnNames.Clear();
        //        columnNames.Add("CarrierID");
        //        columnNames.Add("Carrier_Name");
        //        numColumns = columnNames.Count();
                
        //        columns = new List<string>[numColumns];

        //        //if (adminTMSConnection.OpenConnection() == true)
        //        //{
        //        //    MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
        //        //    MySqlDataReader dataReader = command.ExecuteReader();

        //        //    while (dataReader.Read())
        //        //    {
        //        //        int i = 0;

        //        //        foreach (string s in columnNames)
        //        //        {
        //        //            columns[i].Add(dataReader[s] + "");
        //        //            i++;
        //        //        }

        //        //    }

        //        //    dataReader.Close();

        //        //    adminTMSConnection.CloseConnection();
        //        //}

        //        return columns;
        //    }
        //    catch (Exception e)
        //    {
        //        return columns;
        //    }
            
        //}

        //public List<string>[] DisplayRoutes()
        //{
        //    int numColumns = 0;
        //    List<string> columnNames = new List<string>();
        //    List<string>[] columns = null;

        //    try
        //    {
        //        query = "SELECT * FROM RouteData;";
        //        columnNames.Clear();
        //        columnNames.Add("RouteDataID");
        //        columnNames.Add("CityA");
        //        columnNames.Add("CityB");
        //        columnNames.Add("PickUpTime");
        //        columnNames.Add("DropOffTime");
        //        columnNames.Add("LtlTime");
        //        columnNames.Add("DrivenTime");
        //        numColumns = columnNames.Count();
                
        //        columns = new List<string>[numColumns];

        //        //if (adminTMSConnection.OpenConnection() == true)
        //        //{
        //        //    MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
        //        //    MySqlDataReader dataReader = command.ExecuteReader();

        //        //    while (dataReader.Read())
        //        //    {
        //        //        int i = 0;

        //        //        foreach (string s in columnNames)
        //        //        {
        //        //            columns[i].Add(dataReader[s] + "");
        //        //            i++;
        //        //        }

        //        //    }

        //        //    dataReader.Close();

        //        //    adminTMSConnection.CloseConnection();
        //        //}

        //        return columns;
        //    }
        //    catch (Exception e)
        //    {
        //        return columns;
        //    }
        //}

        ////public List<CarrierDepot> DisplayFees()
        ////{
        ////    List<string>[] columns = new List<string>[7];
        ////    List<CarrierDepot> ReadInData = new List<CarrierDepot>();

        ////    for (int i = 0; i < 7; i++)
        ////    {
        ////        columns[i] = new List<string>();
        ////    }
            
        ////    try
        ////    {
        ////        query = "SELECT * FROM CarrierDepot;";
                       
        ////        if (adminTMSConnection.OpenConnection() == true)
        ////        {
        ////            MySqlCommand command = new MySqlCommand(query, adminTMSConnection._connection);
        ////            MySqlDataReader dataReader = command.ExecuteReader();

        ////            while (dataReader.Read())
        ////            {
        ////                columns[0].Add(dataReader["CityName"] + "");
        ////                columns[1].Add(dataReader["CarrierID"] + "");
        ////                columns[2].Add(dataReader["FTL_Availibility"] + "");// spelled wrong in database
        ////                columns[3].Add(dataReader["LTL_Availibility"] + "");
        ////                columns[4].Add(dataReader["FTL_Rate"] + "");
        ////                columns[5].Add(dataReader["LTL_Rate"] + "");
        ////                columns[6].Add(dataReader["Reefer_Charge"] + "");

        ////            }

        ////            dataReader.Close();
                    
        ////            for (int i = 0; i < columns[0].Count(); i++)
        ////            {
        ////                CarrierDepot current = new CarrierDepot();

        ////                current.CityName = columns[0][i];
        ////                current.CarrierID = Int32.Parse(columns[1][i]);
        ////                current.FTL_Availibility = Int32.Parse(columns[2][i]);
        ////                current.LTL_Availibility = Int32.Parse(columns[3][i]);
        ////                current.FTL_Rate = Double.Parse(columns[4][i]);
        ////                current.LTL_Rate = Double.Parse(columns[5][i]);
        ////                current.Reefer_Charge = Double.Parse(columns[6][i]);

        ////                ReadInData.Add(current); 
        ////            }

        ////            adminTMSConnection.CloseConnection();

        ////        }

        ////        return ReadInData;
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        return ReadInData;
        ////    }

            
        ////}
        
        //public void SelectData() { }
        //public void DeleteSelection() { }
        //public void AddNew()
        //{
        //    // column
        //    // row
        //    // cell
        //}

    }

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
        static public string LogFilePath;                               /// Stores location of the log file
        static public List<TMSLog> logs = new List<TMSLog>();           /// This is a list of logs stored locally
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
                TMSLogger.NewLog(" | " + "AdminClasses.cs" + " | " + "TMSLogger" + " | " + "SetDefaultLogFilePath" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
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

            try
            {
                LogStatusEvent(myLog);
            }
            catch(Exception e)
            {
                TMSLogger.NewLog(" | " + "AdminClasses.cs" + " | " + "TMSLogger" + " | " + "LogIt" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                //megan look here
            }


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
                /// This could become problematic if it calls itself
                LogIt("|" + thisFileDir + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "AppendLogFile" + "|" + "Exception" + "|" + e.Message + "|");
                appendSuccess = false;
            }

            return appendSuccess;

        }


        static public event LogStatusDelegate LogStatusEvent;

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

    public class TMSBackup 
    {
        // Overall List of restore point files found
        public static List<TMSBackup> backupPoints = new List<TMSBackup>(); 
        static public string thisFileDir { get; set; }

        public string filePath { get; set; }
        public static string writeFilePath = thisFileDir + "/TMStempBackup.sql";
        public DateTime backupDate { get; set; }

        public TMSBackup() { }
        public TMSBackup(string fp, DateTime bd)
        {
            filePath = fp;
            backupDate = bd;
        }

        static public bool WriteQueryToCurrentFile(TMSBackupQuery bq)
        {
            // writeQuery to file
            bool appendSuccess = true;

            try
            {
                /// Open the file stream to append to the file. 
                FileStream fileStream = new FileStream((writeFilePath), FileMode.Append, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);

                fileWriter.WriteLine("\n/*" + bq.BackupDate.ToString() + "*/\n");
                fileWriter.WriteLine(bq.queryString);

                fileWriter.Flush();

                /// Close the file
                fileWriter.Close(); fileStream.Close();
            }
            /// If an exception is thrown here, catch it
            catch (Exception e)
            {
                /// This could become problematic as it calls itself
                TMSLogger.LogIt("|" + "/AdminClasses.cs" + "|" + "TMSBackup" + "|" + "WriteQuery" + "|" + "Exception" + "|" + e.Message + "|");
                appendSuccess = false;
            }

            return appendSuccess;

        }

        static public bool CreateRestorePoint()
        {
            // Copy current file to a new one with the restore date. 
            // Change the write file path to the new file
            // Start appending to the new file. 
            
            bool saveSuccess = true;

            // View Save As File Dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.sql";
            saveFileDialog.DefaultExt = ".sql";
            saveFileDialog.InitialDirectory = thisFileDir;
            saveFileDialog.FileName = "RestoreTo_" + DateTime.Now.ToString().Replace(':', '-').Replace(' ', '_');

            // Set a text range using the textbox name
            // TextRange textRange = new TextRange(myTextbox.Document.ContentStart, myTextbox.Document.ContentEnd);
            DialogResult res = saveFileDialog.ShowDialog();

            if ((res == DialogResult.OK) && (string.IsNullOrWhiteSpace(saveFileDialog.FileName.ToString()) == false))
            {
                /// Get the new path
                string newFilePath = (saveFileDialog.FileName);

                /// Open both files to copy
                try
                {
                    /// Open file streams
                    FileStream newFile = new FileStream(newFilePath, FileMode.Create);
                    FileStream oldFile = new FileStream(writeFilePath, FileMode.Open);
                    StreamReader streamReader = new StreamReader(oldFile);
                    StreamWriter streamWriter = new StreamWriter(newFile);

                    /// Read and copy through the files
                    while (!streamReader.EndOfStream)
                    {
                        streamWriter.WriteLine(streamReader.ReadLine());
                        streamWriter.Flush();
                    }

                    /// Close all the streams
                    streamWriter.Close(); streamReader.Close();
                    newFile.Close(); oldFile.Close();

                    // Set the write file path to the new path
                    writeFilePath = newFilePath;
                }
                catch (Exception ex)
                {
                    TMSLogger.LogIt(" | " + "AdminClasses.cs" + " | " + "TMSBackup" + " | " + "CreateRestorePoint" + " | " + ex.GetType().ToString() + " | " + ex.Message + " | ");
                    saveSuccess = false;
                }

                if (saveSuccess == true)
                {
                    /// Set location of LogFilePath to new path
                    FileInfo tmpfi = new FileInfo(newFilePath);

                    backupPoints.Add(new TMSBackup(newFilePath, tmpfi.CreationTime)); 
                    
                }

            }

            return true;
        }

        static public bool RecoverRestorePoint(TMSBackup b)
        {
            bool readSuccess = true;

            try
            {
                FileInfo restoreFile = new FileInfo(b.filePath);
                if (restoreFile.Exists == true)
                {
                    List<string> AllTableName = new List<string>();

                    // Reset database
                    string query = "DROP DATABASE IF EXISTS duane_test; CREATE DATABASE duane_test;";
                    SQL.GenericFunction(query);

                    // Run table builder string
                    SQL.GenericFunction(BuildTables.tableBuilder); 

                    // Run CSV!

                    // Run all queries since the database started
                    string restoreQueries = System.IO.File.ReadAllText(b.filePath);
                    SQL.GenericFunction(restoreQueries); 

                }

            }
            /// If an exception is thrown here, create a log for it. 
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "AdminClasses.cs" + " | " + "TMSBackup" + " | " + "RecoverRestorePoint" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
                readSuccess = false;
            }

            return readSuccess;
        }

        static public void ChangeBackupPath()
        {
            string oldFileDir = thisFileDir;
            string copyFileName = "";
            bool saveSuccess = true; 

            // Open a folder browser to choose the new folder
            FolderBrowserDialog browser = new FolderBrowserDialog(); 
            DialogResult res = browser.ShowDialog();
            
            if (res == DialogResult.OK && (string.IsNullOrWhiteSpace(browser.SelectedPath.ToString()) == false))
            {
                thisFileDir = browser.SelectedPath;
            }

            // Copy over the newest restore point to the new folder 
            try
            {
                /// Open file streams
                FileStream newFile = new FileStream((thisFileDir + "/TMStempBackup.sql"), FileMode.Create);
                FileStream oldFile = new FileStream(writeFilePath, FileMode.Open);
                StreamReader streamReader = new StreamReader(oldFile);
                StreamWriter streamWriter = new StreamWriter(newFile);

                /// Read and copy through the files
                while (!streamReader.EndOfStream)
                {
                    streamWriter.WriteLine(streamReader.ReadLine());
                    streamWriter.Flush();
                }

                /// Close all the streams
                streamWriter.Close(); streamReader.Close();
                newFile.Close(); oldFile.Close();

                // Set the write file path to the new path
                writeFilePath = thisFileDir + copyFileName;

            }
            catch (Exception ex)
            {
                TMSLogger.LogIt(" | " + "AdminClasses.cs" + " | " + "TMSBackup" + " | " + "ChangeBackupPath" + " | " + ex.GetType().ToString() + " | " + ex.Message + " | ");
                saveSuccess = false;
            }

        }

        static public void ReadInBackupsList()
        {
            try
            {

                TMSBackup.backupPoints.Clear();
                foreach (string file in Directory.EnumerateFiles(thisFileDir, "*.sql"))
                {
                    FileInfo fi = new FileInfo(file);

                    backupPoints.Add(new TMSBackup(fi.FullName, fi.CreationTime));

                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "AdminClasses.cs" + " | " + "TMSBackup" + " | " + "ReadInBackupsList" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
               
        }

        static public void SetDefaultBackupFilePath()
        {
            try
            {
                (writeFilePath) = Environment.CurrentDirectory + "/TMStempBackup.sql";
                thisFileDir = Environment.CurrentDirectory;
            }
            catch (Exception e)
            {
                TMSLogger.LogIt("|" + "/AdminClasses.cs" + "|" + "TMSBackup" + "|" + "SetDefaultBackupFilePath" + "|" + "Exception" + "|" + e.Message + "|");
            }

        }
    }

    public class TMSBackupQuery
    {
        public TMSBackupQuery() { }

        public TMSBackupQuery(string iq)
        {
            BackupDate = DateTime.Now;
            queryString = iq;
        }

        public DateTime BackupDate{ get; set; }
        public string queryString{ get; set; }
        
    }

}

   
