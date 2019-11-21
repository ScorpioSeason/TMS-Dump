// ADMIN FILE HEADER COMMENT: =================================================================================
/**
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

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
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

        // This holds functions that the user can choose to run. For example button presses and UI drawing

        // If changes location, move ALL current log files to there, change write location to there. 

        //This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
        //specified by time period, view details of specific logs, alter where the log files are stored,
        //initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier
        // Data Table, the Route Table, and the Rate / Fee Tables.
    }

    static public class TMSLogger
    {
        static string LoggerPath { set; get; }                                          // Stored location of the log file
        static Dictionary<DateTime, TMSLog> logs = new Dictionary<DateTime, TMSLog>();  // To allow searching by time

        // Create Log
        static public void LogIt(string newLogString)
        {
            TMSLog myLog = new TMSLog(newLogString);
            logs.Add(myLog.logTime, myLog);
        }

        // Draw logs

        // Select Log

        // Save Log

        // Move logs
        static void ChangeLogLocation()
        {
            // Select a new location from the popup box
            //private void SaveAs_Click(object sender, RoutedEventArgs e)
            //{
            //    // View Save As File Dialog
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

            //    // Set a text range using the textbox name
            //    TextRange textRange = new TextRange(myTextbox.Document.ContentStart, myTextbox.Document.ContentEnd);

            //    if (saveFileDialog.ShowDialog() == true)
            //    {
            //        // Save work area to chosen file
            //        FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
            //        textRange.Save(fileStream, DataFormats.Rtf);

            //        // Set unsaved flag to false
            //        unsavedText = false;

            //    }

            //}


            string nLoggerPath = ""; 
            // Copy log from old location to new one
                // If successful, inform user, delete old log
                // If failed, inform user, keep old log

        }

        // Search logs (by tags / time)

        // Draw log details

        // Change stored location 

    }

    public class TMSLog
    {
        // Data for each individual log (not exactly protected)
        public string logPath { set; get; }
        public string logClass { set; get; }
        public string logMethod { set; get; }
        public string logType { set; get; }
        public string logMessage { set; get; }
        public string unparsed { set; get; }
        public DateTime logTime { set; get; }

        // Default constructor at least sets time
        //public TMSLog()
        //{
        //    logTime = DateTime.Now;
        //}

        // Creates a log from an unparsed string

        public TMSLog(string nUnparsed)
        {
            unparsed = nUnparsed;
            // Set parsed using unparsed

            string[] temp = unparsed.Split('|');
            logPath = temp[0];
            logClass = temp[1];
            logMethod = temp[2];
            logType = temp[3];
            logMessage = temp[4];
            logTime = DateTime.Now;
        }

        //// Creates a log object from parsed information
        //public TMSLog(string nLogPath, string nLogClass, string nLogMethod, string nLogType, string nLogMessage)
        //{
        //    logPath = nLogPath;
        //    logClass = nLogClass;
        //    logMethod = nLogMethod;
        //    logType = nLogType;
        //    logMessage = nLogMessage;
        //    logTime = DateTime.Now;
        //    unparsed = "|" + nLogPath + "|" + nLogClass + "|" + nLogMethod + "|" + nLogType + "|" + nLogMessage + "|"; 
        //}

    }

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
