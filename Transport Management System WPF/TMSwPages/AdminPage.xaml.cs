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

            // Load the page components
            startDate.SelectedDate = (DateTime.Today.AddDays(-7));
            endDate.SelectedDate = DateTime.Today;
            searchTags.Focus(); 
            LogsList.ItemsSource = searchResults;
            LoadClick(null, null); 
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
        private void LoadClick(object sender, RoutedEventArgs e)
        {
            bool dateRange = false;
            string tempString = (searchTags.Text.Trim()).ToLower();

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

                    if ((l.logTime.Date >= startDate.SelectedDate) && (l.logTime.Date <= endDate.SelectedDate))
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
        private void ViewMoreClick(object sender, RoutedEventArgs e)
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
            string newLocationName = "";
            // View Save As File Dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

            // Set a text range using the textbox name
            //TextRange textRange = new TextRange(myTextbox.Document.ContentStart, myTextbox.Document.ContentEnd);

            if (saveFileDialog.ShowDialog() == true)
            {
                newLocationName = saveFileDialog.FileName; 

                //// Save work area to chosen file
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                //textRange.Save(fileStream, DataFormats.Rtf);

                //// Set unsaved flag to false
                //unsavedText = false;

            }
            
            //string nLoggerPath = "";
            // Copy over log from old location to new one
            // If successful, inform user, delete old log
            // If failed, inform user, keep old log  
        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }
    }
}
