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
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    /// 

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public partial class AdminPage : Page
    {
        List<TMSLog> searchResults = new List<TMSLog>();

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public AdminPage()
        {
            InitializeComponent();
            LogsList.ItemsSource = searchResults;
        }

        //public AdminPage(object data) : this()
        //{
        //    // Bind to expense report data.
        //    this.DataContext = data;
        //}

        // This adds logs which match search strings to the displayed list

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void LoadClick(object sender, RoutedEventArgs e)
        {
            // This is just test logs.
            if (TMSLogger.ReadExistingLogFile())
            {
                searchResults.Clear();
            }

            // This actually populates the logs in the UI
            //if ()
            //{
            // Search by time/date is currently incomplete
            //}

            if (searchTags.Text.Trim() != "")
            {
                foreach (TMSLog l in TMSLogger.logs)
                {
                    if ((l.logType).Contains(searchTags.Text) || (l.logMessage).Contains(searchTags.Text))
                    {
                        searchResults.Add(l);
                    }
                    else if ((l.logClass).Contains(searchTags.Text) || (l.logMethod).Contains(searchTags.Text))
                    {
                        searchResults.Add(l);
                    }
                }
            }
            else
            {
                foreach (TMSLog l in TMSLogger.logs)
                {
                    searchResults.Add(l);
                }
            }

            LogsList.Items.Refresh();
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        // This navigates to a new page where the details of the selected log are listed
        private void ViewMoreClick(object sender, RoutedEventArgs e)
        {
            if (LogsList.SelectedItem != null)
            {
                ViewLogDetails newpage = new ViewLogDetails(this.LogsList.SelectedItem);
                this.NavigationService.Navigate(newpage);
            }
        }

    }
}
