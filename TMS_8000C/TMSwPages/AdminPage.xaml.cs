// ADMINPAGE FILE HEADER COMMENT: ===============================================================================
/**
 *  \file		AdminPage.xaml.cs
 *  \ingroup	TMS
 *  \date		November 22, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the Interaction Logic for the Admin Page (code-behind)  
 *  \see		AdminPage.xaml
 *  \details    This file holds the functionality of the Admin Page. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
 *
 * =========================================================================================================== */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TMSwPages.Classes;

namespace TMSwPages
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		AdminPage
    *   \brief		This class runs the AdminPage UI functionality
    *   \details	This currently allows searching and updating of a local log list which is displayed 
    *               to the screen.  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public partial class AdminPage : Page
    {
        List<TMSLog> logSearchResults = new List<TMSLog>();
        List<TMSBackup> backupSearchResults = new List<TMSBackup>(); 
        static Admin admin = new Admin(); 
        static int selectedTab = -1;
        //TMSBackup backup = new TMSBackup(); 

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			public AdminPage()
        *	\brief		This is a constructor for the AdminPage user interface. 
        *	\details	This loads the logSearchResults TMSLogs into the LogsList data grid. 
        *	\return		Instantiates the AdminPage
        *
        * ---------------------------------------------------------------------------------------------------- */
        public AdminPage()
        {
            InitializeComponent();

            try
            {
                selectedTab = 0;

                LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                LogEndDate.SelectedDate = DateTime.Today;
                LogSearchTags.Focus();
                LogsList.ItemsSource = logSearchResults;
                LogLoadClick(null, null);

                LogsList.ItemsSource = logSearchResults;

                BackupsList.ItemsSource = backupSearchResults;


                //Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                //Route_TableList.ItemsSource = admin.DisplayRoutes();
                //Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();

            }
            catch (Exception e)
            {

            }
        }

        //public AdminPage(SQL_Query_TMS validatedConnection)
        //{
        //    try
        //    {
        //        InitializeComponent();

        //        // Load SQL Connection
        //        admin.SetTMSConnection(validatedConnection);

        //        selectedTab = 0;

        //        LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
        //        LogEndDate.SelectedDate = DateTime.Today;
        //        LogSearchTags.Focus();
        //        LogsList.ItemsSource = logSearchResults;
        //        LogLoadClick(null, null);
        //    }
        //    catch (Exception e)
        //    {

        //    }

        //    /// Bind to incoming log data.
        //    //this.DataContext = data;
        //}

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			private void LoadClick(object sender, RoutedEventArgs e)
        *	\brief		This adds logs to the search results.
        *	\details	This adds logs to the search results which match search strings entered. This will first
        *	            read from the Log file before it adds in any entries. This isn't done the most efficiently
        *	\exception	string.Contains() may throw an Argument Null exception
        *	\see		TMSLogger.LogIt()
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void LogLoadClick(object sender, RoutedEventArgs e)
        {
            bool dateRange = false;
            string tempString = (LogSearchTags.Text.Trim()).ToLower();

            /// This clears logSearchResults if the Log file is read in successfully
            if (TMSLogger.ReadExistingLogFile() == true)
            {
                logSearchResults.Clear();
            }
            try
            {
                foreach (TMSLog l in TMSLogger.logs)
                {
                    dateRange = false;

                    if ((l.logTime.Date >= LogStartDate.SelectedDate) && (l.logTime.Date <= LogEndDate.SelectedDate))
                    {
                        dateRange = true;
                    }

                    if ((dateRange == true) && (tempString != ""))
                    {
                        /// Compare search tags box to logs in the local list and add to logSearchResults list if matching
                        if ((l.logType.ToLower()).Contains(tempString) || ((l.logMessage.ToLower()).Contains(tempString)))
                        {
                            logSearchResults.Add(l);
                        }
                        else if ((l.logMethod.ToLower()).Contains(tempString) || ((l.logClass.ToLower()).Contains(tempString)))
                        {
                            logSearchResults.Add(l);
                        }
                    }
                    else if ((dateRange == true) && (tempString == ""))
                    {
                        logSearchResults.Add(l);
                    }

                }
            }
            /// Catch errors from Contains calls
            catch (Exception ex)
            {
                TMSLogger.LogIt("|"+ "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "LoadClick" + "|" + "Exception" + "|" + ex.Message + "|");

            }

            /// Refresh the UI data grid
            LogsList.Items.Refresh();

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			private void ViewMoreClick(object sender, RoutedEventArgs e)
        *	\brief		Sends the user to ViewLogDetails page using selected TMSLog item's details. 
        *	\details	Does not activate if nothing is selected in the datagrid.
        *	\see		NavigationService.Navigate()
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void LogViewMoreClick(object sender, RoutedEventArgs e)
        {
            if (LogsList.SelectedItem != null)
            {
                ViewLogDetails newpage = new ViewLogDetails(this.LogsList.SelectedItem);
                this.NavigationService.Navigate(newpage);
            }
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			void ChangeLogLocation() -- STUB
        *	\brief		This function will change where the log file is stored
        *	\details	This function allows the user to select a new location for the log file storage. Then
        *	            the program copies the current file to the new location. If the copy is successful, 
        *	            the old file is deleted. Otherwise the old file is kept. 
        *	\exception	From FileStream and StreamReader / StreamWriter
        *	\see		
        *	\return		Void
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void ChangeLogLocation(object sender, RoutedEventArgs e)
        {
            bool saveSuccess = true;
            string oldLogPath = TMSLogger.LogFilePath;
            string newLogPath = "";

            // View Save As File Dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

            // Set a text range using the textbox name
            // TextRange textRange = new TextRange(myTextbox.Document.ContentStart, myTextbox.Document.ContentEnd);

            if (saveFileDialog.ShowDialog() == true)
            {
                /// Get the new path
                newLogPath = (saveFileDialog.FileName); 

                /// Open both files to copy
                try
                {
                    /// Open file streams
                    FileStream newFile = new FileStream(newLogPath, FileMode.Create);
                    FileStream oldFile = new FileStream(oldLogPath, FileMode.Open);
                    StreamReader streamReader = new StreamReader(oldFile);
                    StreamWriter streamWriter = new StreamWriter(newFile);

                    /// Read any copy through the files
                    while (!streamReader.EndOfStream)
                    {
                        streamWriter.WriteLine(streamReader.ReadLine());
                        streamWriter.Flush(); 
                    }

                    /// Close all the streams
                    streamWriter.Close(); streamReader.Close();
                    newFile.Close(); oldFile.Close(); 

                }
                catch (Exception ex)
                {
                    TMSLogger.LogIt("|" + "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "ChangeLogLocation" + "|" + ex.GetType() + "|" + ex.Message + "|");
                    saveSuccess = false; 
                }
                
                if (saveSuccess == true)
                {
                    /// Set location of LogFilePath to new path
                    TMSLogger.LogFilePath = newLogPath;

                    /// Delete old file 
                    try
                    {
                        File.Delete(oldLogPath);
                    }
                    catch (Exception exc)
                    {
                        TMSLogger.LogIt("|" + "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "ChangeLogLocation" + "|" + exc.GetType() + "|" + exc.Message + "|");
                    }
                }

            }

        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void Carrier_DataLoadClick(object sender, RoutedEventArgs e)
        {
            FC_Carrier c = new FC_Carrier();

            Carrier_DataList.ItemsSource = c.ObjToTable(SQL.Select(c));
        }

        private void Route_TableLoadClick(object sender, RoutedEventArgs e)
        {
            

            Route_TableList.Items.Refresh(); 
        }

        private void Rate_Fee_TablesClick(object sender, RoutedEventArgs e)
        {
            FC_DepotCity d = new FC_DepotCity();

            Rate_Fee_TablesList.ItemsSource = d.ObjToTable(SQL.Select(d));
        }
        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedTab != tabber.SelectedIndex)
            {
                if ((tabber.SelectedIndex <= 4) && (tabber.SelectedIndex >= -1))
                {
                    selectedTab = tabber.SelectedIndex;

                    try
                    {
                        switch (selectedTab)
                        {
                            case (0):
                                LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                                LogEndDate.SelectedDate = DateTime.Today;
                                LogSearchTags.Focus();
                                LogsList.ItemsSource = logSearchResults;
                                LogLoadClick(null, null);
                                break;
                            case (1):
                                //backup
                                BackupsStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                                BackupsEndDate.SelectedDate = DateTime.Today;
                                
                                BackupsList.ItemsSource = backupSearchResults;
                                UpdateBackupsList();

                                break;
                            case (2):
                                //Carrier_Data.DataContext = admin.DisplayCarrier();
                                //Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                                Carrier_DataLoadClick(null, null);
                                break;
                            case (3):
                                //Route_Table.DataContext = admin.DisplayRoutes();
                                //Route_TableList.ItemsSource = admin.DisplayRoutes();
                                Route_TableLoadClick(null, null);
                                break;
                            case (4):
                                //Rate_Fee_Tables.DataContext = admin.DisplayFees();
                                //Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();
                                Rate_Fee_TablesClick(null, null);
                                break;
                            default:
                                break;
                        }
                        
                    }
                    catch (Exception ex)
                    {

                    }

                }
                
            }
        }

        private void RestoreSelected_Click(object sender, RoutedEventArgs e)
        {
            if (BackupsList.SelectedItem != null)
            {
                TMSBackup.RecoverRestorePoint((TMSBackup)BackupsList.SelectedItem);
            }

            UpdateBackupsList();
            // Restore from selected backup point
        }

        private void CreateRestore_Click(object sender, RoutedEventArgs e)
        {
            //backup.CreateRestorePoint();
            // Create a new restore point
            UpdateBackupsList();
        }

        private void ChangeDir_Click(object sender, RoutedEventArgs e)
        {
            //backup.ChangeBackupPath();
            //change the directory of the backup files
            UpdateBackupsList();
        }

        private void BackupsDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update search
            UpdateBackupsList(); 
        }

        private void UpdateBackupsList()
        {
            backupSearchResults.Clear();

            //backup.ReadInBackupsList(); // This probably should not be here
            foreach (TMSBackup b in TMSBackup.backupPoints)
            {
                if ((b.backupDate.Date <= BackupsEndDate.SelectedDate) && (b.backupDate.Date >= BackupsStartDate.SelectedDate))
                {
                    backupSearchResults.Add(b);
                }
            }

            //BackupsList.ItemsSource = backupSearchResults;
            BackupsList.Items.Refresh();
        }

        private void ChangeCUSLocation(object sender, RoutedEventArgs e)
        {
            // View Save As File Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv Files (*.csv)|*.csv";

            if(openFileDialog.ShowDialog() == true)
            {
                LoadCSV.SetNewCSVLocation(openFileDialog.FileName);
            }
        }

        private void LoadCsvIntoBD(object sender, RoutedEventArgs e)
        {
            //check if it is empty
            FC_Carrier c = new FC_Carrier();

            if(c.ObjToTable(SQL.Select(c)).Count == 0)
            {
                LoadCSV.Load();
            }

            Carrier_DataLoadClick(null, null);
        }
    }
}
