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

namespace SampleWFPUsingPages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<TMSLog> searchResults = new List<TMSLog>();

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

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            // This is just test logs.
            TMSLogger.LogIt("THIs |is| a |thing?|");
            TMSLogger.LogIt("THIs is another different thing?");
            TMSLogger.LogIt("|THIs |is |another |third |different |thing?");

            searchResults.Clear();

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
                    /*  logPath = nLogPath;
                        logClass = nLogClass;
                        logMethod = nLogMethod;
                        logType = nLogType;
                        logMessage = nLogMessage;
                        logTime = DateTime.Now; */
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

        private void ViewMoreClick(object sender, RoutedEventArgs e)
        {
            if (LogsList.SelectedItem != null)
            {
                ViewLogDetailsPage newpage = new ViewLogDetailsPage(this.LogsList.SelectedItem);
                this.NavigationService.Navigate(newpage);
            }
        }
    }
}
