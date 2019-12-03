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
    /// Interaction logic for PlannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        static int selectedTab = -1;

        public PlannerPage()
        {
            InitializeComponent();
            selectedTab = 0;

        }

        public PlannerPage(SQL_Query_TMS validatedConnection)
        {
            InitializeComponent();
            selectedTab = 0;

            // Load SQL Connection
            //admin.SetTMSConnection(validatedConnection);

            /// Bind to incoming log data.
            //this.DataContext = data;
        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void LoadContracts_Click(object sender, RoutedEventArgs e)
        {
            NomContractList.Items.Refresh();
        }

        private void SelectCarriers_Click(object sender, RoutedEventArgs e)
        {
            //if (NomContractList.SelectedItem != null)
            //{
            //    PlannerCarriersWDepo newpage = new PlannerCarriersWDepo(this.NomContractList.SelectedItem);
            //    this.NavigationService.Navigate(newpage);
            //}

            PlannerCarriersWDepo newpage = new PlannerCarriersWDepo();
            this.NavigationService.Navigate(newpage); 

        }
        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedTab != PlannerTabs.SelectedIndex)
            {
                if ((PlannerTabs.SelectedIndex <= 4) && (PlannerTabs.SelectedIndex >= -1))
                {
                    selectedTab = PlannerTabs.SelectedIndex;

                    try
                    {
                        switch (selectedTab)
                        {
                            case (0):
                                //LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                                //LogEndDate.SelectedDate = DateTime.Today;
                                //LogSearchTags.Focus();
                                //LogsList.ItemsSource = searchResults;
                                //LogLoadClick(null, null);
                                break;
                            case (1):
                                //backup
                                break;
                            case (2):
                                //Carrier_Data.DataContext = admin.DisplayCarrier();
                                //Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                                //Carrier_DataLoadClick(null, null);
                                break;
                            case (3):
                                //Route_Table.DataContext = admin.DisplayRoutes();
                                //Route_TableList.ItemsSource = admin.DisplayRoutes();
                                //Route_TableLoadClick(null, null);
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
