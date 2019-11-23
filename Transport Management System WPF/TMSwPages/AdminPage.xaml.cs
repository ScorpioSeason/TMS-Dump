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

using System;
using System.Collections.Generic;
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
            LogsList.ItemsSource = searchResults;
            LoadClick(null, null); 
        }

        // public AdminPage(object data) : this()
        // {
        //    // Bind to expense report data.
        //    this.DataContext = data;
        // }

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
            /// This clears searchResults if the Log file is read in successfully
            if (TMSLogger.ReadExistingLogFile() == true)
            {
                searchResults.Clear();
            }

            try
            {
                /// Compare search tags box to logs in the local list and add to searchResults list if matching
                if (searchTags.Text.Trim() != "")
                {
                    string tempString = (searchTags.Text.Trim()).ToLower(); 

                    foreach (TMSLog l in TMSLogger.logs)
                    {
                        if ((l.logType.ToLower()).Contains(tempString) || (l.logMessage.ToLower()).Contains(tempString))
                        {
                            searchResults.Add(l);
                        }
                        else if ((l.logClass.ToLower()).Contains(tempString) || (l.logMethod.ToLower()).Contains(tempString))
                        {
                            searchResults.Add(l);
                        }
                    }
                }
                else
                {
                    /// If there are no search strings, add indescriminately
                    foreach (TMSLog l in TMSLogger.logs)
                    {
                        searchResults.Add(l);
                    }
                    
                }
            }
            /// Catch errors from Contains
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
        *	\fn			private void LogSettingsClick(object sender, RoutedEventArgs e) -- STUB
        *	\brief		
        *	\details	
        *	\see		
        *	\return		
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void LogSettingsClick(object sender, RoutedEventArgs e)
        {

        }

        /// METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			private void Time_Period_SelectionChanged(object sender, SelectionChangedEventArgs e) -- STUB
        *	\brief		
        *	\details	
        *	\see		
        *	\return		
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void Time_Period_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) -- STUB
        *	\brief		
        *	\details	
        *	\see		
        *	\return		
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
