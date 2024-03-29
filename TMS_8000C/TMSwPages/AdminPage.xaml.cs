﻿// ADMINPAGE FILE HEADER COMMENT: ===============================================================================
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
using System.Text.RegularExpressions;
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
            TMSLogger.LogStatusEvent += LogStatusEventHandler;

            try
            {
                selectedTab = 0;

                LoadLogTab();
                LoadBackupTab();
                LoadCarrierDataTab();
                LoadRouteTab();
                LoadRateFeeTab(); 

            }
            catch (Exception e)
            {

            }
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
                                LoadLogTab();
                                break;
                            case (1):
                                LoadBackupTab();
                                break;
                            case (2):
                                LoadCarrierDataTab();
                                break;
                            case (3):
                                LoadRouteTab();
                                break;
                            case (4):
                                LoadRateFeeTab();
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

        // MENU FUNCTIONS ======================================================================================

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            SQL.close();

            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void ChangeCSVLocation(object sender, RoutedEventArgs e)
        {
            // View Save As File Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                LoadCSV.SetNewCSVLocation(openFileDialog.FileName);
                LoadCSV.Load();
            }
        }

        private void LoadCsvIntoBD(object sender, RoutedEventArgs e)
        {
            //check if it is empty
            FC_Carrier c = new FC_Carrier();

            if (c.ObjToTable(SQL.Select(c)).Count == 0)
            {
                LoadCSV.Load();
            }

            Carrier_DataLoadClick(null, null);
        }

        private void ResetTheDataBase(object sender, RoutedEventArgs e)
        {
            SQL.WipeEverything();

        }

        private void UseLocalClick(object sender, RoutedEventArgs e)
        {
            SQL.SetCMPIP(1);
        }

        private void OriginalClick(object sender, RoutedEventArgs e)
        {
            SQL.SetCMPIP(2);
        }

        // LOG TAB FUNCTIONS ===================================================================================
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
        private void LogSearchClick(object sender, RoutedEventArgs e)
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
                TMSLogger.LogIt("|" + "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "LoadClick" + "|" + ex.GetType().ToString() + "|" + ex.Message + "|");

            }

            /// Refresh the UI data grid
            LogsList.Items.Refresh();

        }

        private void LogDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LogSearchClick(null, null);
        }

        private void LoadLogTab()
        {
            LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
            LogEndDate.SelectedDate = DateTime.Today;
            LogSearchTags.Focus();
            LogsList.ItemsSource = logSearchResults;
            LogsList.Items.Refresh();
            LogSearchClick(null, null);

        }


        public void LogStatusEventHandler(TMSLog log)
        {
            // Handle the event (send it to the status bar)
            status.Text = "Status: " + log.logMessage;
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
                    TMSLogger.LogIt("|" + "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "ChangeLogLocation" + "|" + ex.GetType().ToString() + "|" + ex.Message + "|");
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
                        TMSLogger.LogIt("|" + "/AdminPage.xaml.cs" + "|" + "AdminPage" + "|" + "ChangeLogLocation" + "|" + exc.GetType().ToString() + "|" + exc.Message + "|");
                    }
                }

            }

        }

        // BACKUP TAB FUNCTIONS ================================================================================
        private void LoadBackupTab()
        {
            BackupsStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
            BackupsEndDate.SelectedDate = DateTime.Today;
            BackupsList.ItemsSource = backupSearchResults;
            BackupsList.Items.Refresh();
            UpdateBackupsList();
        }

        private void RestoreSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BackupsList.SelectedItem != null)
                {
                    TMSBackup.RecoverRestorePoint((TMSBackup)BackupsList.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                TMSLogger.LogIt(" | " + "AdminPage.xaml.cs" + " | " + "AdminPage" + " | " + "RestoreSelected_Click" + " | " + ex.GetType().ToString() + " | " + ex.Message + " | ");
            }

            UpdateBackupsList();

        }

        private void CreateRestore_Click(object sender, RoutedEventArgs e)
        {
            TMSBackup.CreateRestorePoint();
            // Create a new restore point
            UpdateBackupsList();
        }

        private void ChangeDir_Click(object sender, RoutedEventArgs e)
        {
            TMSBackup.ChangeBackupPath();
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

            TMSBackup.ReadInBackupsList(); // This probably should not be here
            foreach (TMSBackup b in TMSBackup.backupPoints)
            {
                if ((b.backupDate.Date <= BackupsEndDate.SelectedDate) && (b.backupDate.Date >= BackupsStartDate.SelectedDate))
                {
                    backupSearchResults.Add(b);
                }
            }

            BackupsList.Items.Refresh();
        }

        // CARRIER TAB FUNCTIONS ===============================================================================
        private void LoadCarrierDataTab()
        {
            //Carrier_Data.DataContext = admin.DisplayCarrier();
            //Carrier_DataList.ItemsSource = admin.DisplayCarrier();
            FC_Carrier c = new FC_Carrier();

            Carrier_DataList.ItemsSource = c.ObjToTable(SQL.Select(c));
        }

        private void Carrier_DataLoadClick(object sender, RoutedEventArgs e)
        {
            LoadCarrierDataTab(); 
        }

        // ROUTE TAB FUNCTIONS =================================================================================
        private void LoadRouteTab()
        {
            //Route_Table.DataContext = admin.DisplayRoutes();
            //Route_TableList.ItemsSource = admin.DisplayRoutes();
            Route_TableList.Items.Refresh();
        }

        private void Route_TableLoadClick(object sender, RoutedEventArgs e)
        {
            LoadRouteTab(); 
        }

        // RATE FEE TAB FUNCTIONS ==============================================================================
        private void Rate_Fee_TablesClick(object sender, RoutedEventArgs e)
        {
            LoadRateFeeTab(); 
            
        }

        private void LoadRateFeeTab()
        {
            //Rate_Fee_Tables.DataContext = admin.DisplayFees();
            //Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();

            FC_DepotCity d = new FC_DepotCity();
            Rate_Fee_TablesList.ItemsSource = d.ObjToTable(SQL.Select(d));
            Rate_Fee_TablesList.Items.Refresh(); 
        }

        private void Rate_Fee_TablesList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (Rate_Fee_TablesList.SelectedItem != null)
            {
                FC_DepotCity temp = (FC_DepotCity)Rate_Fee_TablesList.SelectedItem;
                IList<DataGridCellInfo> cellInfo = Rate_Fee_TablesList.SelectedCells; 

                // The cell contents are changed. Update the specified depot city. 
            }
        }

        public FC_DepotCity selectedCityForEditing;

        private void Rate_Fee_TablesList_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            selectedCityForEditing = (FC_DepotCity)Rate_Fee_TablesList.SelectedItem;
        }


        private void Rate_Fee_TablesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Rate_Fee_TablesList.SelectedItem != null)
            {
                selectedCityForEditing = (FC_DepotCity)Rate_Fee_TablesList.SelectedItem;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FromCB = AvalCB.Text;

                Regex CheckReg = new Regex("[^0-9.]");

                string FromTB = NewDataValueTB.Text;

                if (FromTB != null && !CheckReg.IsMatch(FromTB) && FromCB != "")
                {
                    string query = "update FC_DepotCity set " + FromCB + " = "+ FromTB + " where FC_CarrierID = " + selectedCityForEditing.FC_CarrierID.ToString() + " and CityName = '" + selectedCityForEditing.CityName + "';";
                    SQL.GenericFunction(query);

                    NewDataValueTB.Text = "";
                    AvalCB.Text = "";

                    LoadRateFeeTab();
                }
            }
            catch (Exception ex)
            {
                TMSLogger.LogIt(" | " + "AdminPage.xaml.cs" + " | " + "AdminPage" + " | " + "ConfirmButton_Click" + " | " + ex.GetType().ToString() + " | " + ex.Message + " | ");
            }
            
        }
    }
}
