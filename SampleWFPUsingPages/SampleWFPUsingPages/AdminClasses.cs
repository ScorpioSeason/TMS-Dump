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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SampleWFPUsingPages
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
        // This is actually mostly run in the AdminPage and its code-behind... this class may be redundant... 

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
        static public List<TMSLog> logs = new List<TMSLog>();

        // Add to internal Log list AND append to the external file
        static public void LogIt(string newLogString)
        {
            TMSLog myLog = new TMSLog(newLogString);
            logs.Add(myLog);
            AppendLogFile(myLog);

        }

        // Only add to the internal log list (i.e. read in from file)
        static public void NewLog(string newLogString)
        {
            TMSLog myLog = new TMSLog(newLogString);
            logs.Add(myLog);
        }

        // Read logs in from a file
        static public bool ReadExistingLogFile()
        {
            bool readSuccess = true;
            LoggerPath = Environment.CurrentDirectory;

            // Clear out the working list 
            logs.Clear(); 

            try
            {
                // Open the file stream to read from the file
                FileStream fileStream = new FileStream((LoggerPath + "/TMSLogger.txt"), FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);

                // Fill the working list with lines from the file 
                while (!streamReader.EndOfStream)
                {
                    string lineString = streamReader.ReadLine();
                    NewLog(lineString);
                }

                // Close the file
                streamReader.Close(); fileStream.Close();
            }
            // If an exception is thrown here, create a log for it. 
            catch ( Exception e)
            {
                LogIt("|" + LoggerPath + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "ReadExistingLogFile" + "|" + "Exception" + "|" + e.Message + "|");
                readSuccess = false;
            }

            return readSuccess;

        }

        static public bool AppendLogFile(TMSLog newLog)
        {
            bool appendSuccess = true; 
            LoggerPath = Environment.CurrentDirectory;

            try
            {
                // Open the filestream to append to the file. 
                FileStream fileStream = new FileStream((LoggerPath + "/TMSLogger.txt"), FileMode.Append, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);

                // Add each log entry from the working list to the file as a BSV
               
                fileWriter.WriteLine(newLog.BSV);
                fileWriter.Flush(); 

                // Close the file
                fileWriter.Close();  fileStream.Close(); 
            }
            // If an exception is thrown here, catch it
            catch (Exception e)
            {
                // This could become problematic as it calls itself
                LogIt("|" + LoggerPath + "/AdminClasses.cs" + "|" + "TMSLogger" + "|" + "AppendLogFile" + "|" + "Exception" + "|" + e.Message + "|");
                appendSuccess = false; 
            }

            return appendSuccess; 

        }

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


            //string nLoggerPath = "";
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
        public string BSV { set; get; } // Bar separated values
        public DateTime logTime { set; get; }

        public TMSLog(string nUnparsed)
        {
            // Used in Logger / LogIt call
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
            // Used in reading in from a file
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
            // Used in formatting error
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
