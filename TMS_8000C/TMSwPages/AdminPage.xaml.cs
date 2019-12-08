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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        List<TMSLog> searchResults = new List<TMSLog>();
        static Admin admin = new Admin(); 
        static int selectedTab = -1; 

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			public AdminPage()
        *	\brief		This is a constructor for the AdminPage user interface. 
        *	\details	This loads the searchResults TMSLogs into the LogsList data grid. 
        *	\return		Instantiates the AdminPage
        *
        * ---------------------------------------------------------------------------------------------------- */
        public AdminPage()
        {
            InitializeComponent();

            try
            {
                // Load SQL Connection
                admin.SetTMSConnection(new SQL_Query_TMS());

                selectedTab = 0;

                LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                LogEndDate.SelectedDate = DateTime.Today;
                LogSearchTags.Focus();
                LogsList.ItemsSource = searchResults;
                LogLoadClick(null, null);

                LogsList.ItemsSource = searchResults; 
                Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                Route_TableList.ItemsSource = admin.DisplayRoutes();
                Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();

            }
            catch (Exception e)
            {

            }
        }

        public AdminPage(SQL_Query_TMS validatedConnection)
        {
            try
            {
                InitializeComponent();

                // Load SQL Connection
                admin.SetTMSConnection(validatedConnection);

                selectedTab = 0;

                LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                LogEndDate.SelectedDate = DateTime.Today;
                LogSearchTags.Focus();
                LogsList.ItemsSource = searchResults;
                LogLoadClick(null, null);

            }
            catch (Exception e)
            {
               
            }
            
            /// Bind to incoming log data.
            //this.DataContext = data;
        }

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

            /// This clears searchResults if the Log file is read in successfully
            if (TMSLogger.ReadExistingLogFile() == true)
            {
                searchResults.Clear();
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
                        /// Compare search tags box to logs in the local list and add to searchResults list if matching
                        if ((l.logType.ToLower()).Contains(tempString) || ((l.logMessage.ToLower()).Contains(tempString)))
                        {
                            searchResults.Add(l);
                        }
                        else if ((l.logMethod.ToLower()).Contains(tempString) || ((l.logClass.ToLower()).Contains(tempString)))
                        {
                            searchResults.Add(l);
                        }
                    }
                    else if ((dateRange == true) && (tempString == ""))
                    {
                        searchResults.Add(l);
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
            Carrier_DataList.Items.Refresh(); 
        }

        private void Route_TableLoadClick(object sender, RoutedEventArgs e)
        {
            Route_TableList.Items.Refresh();
        }

        private void Rate_Fee_TablesClick(object sender, RoutedEventArgs e)
        {
            Rate_Fee_TablesList.Items.Refresh();
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
                                LogsList.ItemsSource = searchResults;
                                LogLoadClick(null, null);
                                break;
                            case (1):
                                //backup
                                break;
                            case (2):
                                //Carrier_Data.DataContext = admin.DisplayCarrier();
                                Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                                Carrier_DataLoadClick(null, null);
                                break;
                            case (3):
                                //Route_Table.DataContext = admin.DisplayRoutes();
                                Route_TableList.ItemsSource = admin.DisplayRoutes();
                                Route_TableLoadClick(null, null);
                                break;
                            case (4):
                                //Rate_Fee_Tables.DataContext = admin.DisplayFees();
                                Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();
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
    }
}