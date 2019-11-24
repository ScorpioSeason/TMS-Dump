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

namespace TMSwPages
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class	    Admin
    *   \brief		This class runs the Admin UI functionality. 
    *   \details	The AdminPage codebehind should make calls to this. This holds functions that the user can 
    *   choose to run. For example button presses and UI drawingRight now this class does nothing.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    class Admin
    {
        //TMSLogger adminLogger = null;
        BackupTMS adminBackup = null;
        AlterTables adminAlter = null;

        Admin()
        {
            //adminLogger = new TMSLogger();
            adminBackup = new BackupTMS();
            adminAlter = new AlterTables();
        }

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
        static public string LoggerPath { set; get; }               /// Stores location of the log file
        static public List<TMSLog> logs = new List<TMSLog>();       /// This is a list of logs stored locally

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
            LoggerPath = Environment.CurrentDirectory;

            try
            {
                /// Clear out the working list 
                logs.Clear();

                /// Open the file stream to read from the file
                FileStream fileStream = new FileStream((LoggerPath + "/TMSLogger.txt"), FileMode.Open, FileAccess.Read);
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
                LogIt("|" + LoggerPath + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "ReadExistingLogFile" + "|" + "Exception" + "|" + e.Message + "|");
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
            LoggerPath = Environment.CurrentDirectory;

            try
            {
                /// Open the filestream to append to the file. 
                FileStream fileStream = new FileStream((LoggerPath + "/TMSLogger.txt"), FileMode.Append, FileAccess.Write);
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
                LogIt("|" + LoggerPath + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "AppendLogFile" + "|" + "Exception" + "|" + e.Message + "|");
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
        public string logPath { set; get; }
        public string logClass { set; get; }
        public string logMethod { set; get; }
        public string logType { set; get; }
        public string logMessage { set; get; }
        public string BSV { set; get; }                     /// Bar separated values
        public DateTime logTime { set; get; }

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
                logPath = temp[1];
                logClass = temp[2];
                logMethod = temp[3];
                logType = temp[4];
                logMessage = temp[5];
                logTime = DateTime.UtcNow;
            }
            /// Used in reading in from a file
            else if ((Regex.Matches(nUnparsed, "\\|").Count) == 7)
            {
                string[] temp = nUnparsed.Split('|');
                logTime = DateTime.Parse(temp[1]);
                logPath = temp[2];
                logClass = temp[3];
                logMethod = temp[4];
                logType = temp[5];
                logMessage = temp[6];
            }
            /// Used in formatting error
            else
            {
                logPath = "AdminClasses.cs";
                logClass = "TMSLog";
                logMethod = "Constructor";
                logType = "LogParseError";
                logMessage = "The log message did not enter as the correct string format";
                logTime = DateTime.UtcNow;
                nUnparsed = "|" + logPath + "|" + logClass + "|" + logMethod + "|" + logType + "|" + logMessage + "|";
            }

            BSV = "|" + logTime + nUnparsed;
        }

    }

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		BackupTMS -- STUB
    *   \brief		This class executes the Admin functions related to Backup. 
    *   \details	This class can initiate the backup of the TMS database and change where the backup is 
    *               stored.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    class BackupTMS
    {
        // Read store location
        // Choose new store location
        // Copy files from old location to new location
        // If error, inform user, do not delete old copy. 
        // Else if success, inform user, delete old copy

        // Initiate backup (button) 
        // Write to location
        // If successful write, delete old copy of backup (do not immediately overwrite)
    }

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		AlterTables -- STUB
    *   \brief		This class executes functions related to Altering Tables
    *   \details	This class should be able to access, view, and change data in the Carrier Data Table, Route
    *   Table, and Rate / Fee Table
    *   
    * -------------------------------------------------------------------------------------------------------- */
    class AlterTables
    {
        // Access tables
        // Carrier Data Table
        // Route Table
        // Rate / Fee Table

        // Draw tables
        // Carrier Data Table
        // Route Table
        // Rate / Fee Table

        // Change table data 
        // Carrier Data Table
        // Route Table
        // Rate / Fee Table
    }
}
