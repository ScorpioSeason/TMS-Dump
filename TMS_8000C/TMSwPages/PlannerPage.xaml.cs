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
using TMSwPages.Classes;

namespace TMSwPages
{
    /// <summary>
    /// Interaction logic for PlannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        static int selectedTab = -1;

        static FC_LocalContract Selected_Contract = new FC_LocalContract();

        public PlannerPage()
        {
            InitializeComponent();
            //selectedTab = 0;

            //List<Carrier> testCarriersList = new List<Carrier>();
            //Carrier testCarrier = new Carrier(42, "CarrierBob");
            //Carrier testCarrier2 = new Carrier(32, "CarrierFred");
            //Carrier testCarrier3 = new Carrier(1, "CarrierJoe");
            //testCarriersList.Add(testCarrier);
            //testCarriersList.Add(testCarrier2);
            //testCarriersList.Add(testCarrier3); 

            //List<CompleteNomination> test = new List<CompleteNomination>();
            //CompleteNomination testNomination = new CompleteNomination();
            //testNomination.ListOfCarriers = testCarriersList; 
            //test.Add(testNomination);

            //NomContractList.ItemsSource = test;

        }

        //public PlannerPage(SQL_Query_TMS validatedConnection)
        //{
        //    InitializeComponent();
        //    selectedTab = 0;
        //    NomContractList.Items.Refresh();

        //    // Load SQL Connection
        //    //admin.SetTMSConnection(validatedConnection);

        //    /// Bind to incoming log data.
        //    //this.DataContext = data;
        //}

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void LoadContracts_Click(object sender, RoutedEventArgs e)
        {
            NomContractList.ItemsSource = PlannerClass.GetNominatedContracts();
            NomContractList.Items.Refresh();
        }

        //private void SelectCarriers_Click(object sender, RoutedEventArgs e)
        //{
        //    if (NomContractList.SelectedItem != null)
        //    {
        //        PlannerCarriersWDepo newpage = new PlannerCarriersWDepo(this.NomContractList.SelectedItem);
        //        this.NavigationService.Navigate(newpage);
        //    }

        //}

        //private void NavCarriersDepots_Click(object sender, RoutedEventArgs e)
        //{
        //    if (NomContractList.SelectedItem != null)
        //    {
        //        FC_LocalContract t = (FC_LocalContract)NomContractList.SelectedCells[0].Item;

        //        SelectCarriersPage newpage = new SelectCarriersPage(t);
        //        this.NavigationService.Navigate(newpage);
        //    }
        //}
        

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

        private void NomContractList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.NomContractList.SelectedItem != null)
            {
                FC_LocalContract SelectedContract = (FC_LocalContract)this.NomContractList.SelectedItem;

                Selected_Contract = SelectedContract;

                CarriersList.ItemsSource = PlannerClass.GetNomCarriers_withDepot(SelectedContract);


                string query = "Select * from FC_TripTicket where Is_Complete = 0 and CurrentLocation = \"" + SelectedContract.Origin + "\";";

                FC_TripTicket t = new FC_TripTicket();
                List<FC_TripTicket> PossibleTickets = t.ObjToTable(SQL.Select(t, query));

                OtherTickets.ItemsSource = PossibleTickets;
            }
        }

        private void CreateChossenOrder(object sender, RoutedEventArgs e)
        {
            if(NomContractList.SelectedCells[0].Item != null)
            {
                AttatchTicketPage newpage = new AttatchTicketPage(this.NomContractList.SelectedItem);
                this.NavigationService.Navigate(newpage);


            }

            
        }

        private void DG5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (FC_TripTicket c in DG5.SelectedItems)
            {
                PlannerClass.ContractsPerTicket_Populate(c);
                PlannerClass.RoutSegsPerTicket_Populate(c);
            }
            DG6.ItemsSource = null;
            DG6.ItemsSource = PlannerClass.ContractsPerTicket;
            DG7.ItemsSource = null;
            DG7.ItemsSource = PlannerClass.RouteSegsPerTicket;
        }

        private void RefreshActiveTickets_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.TicketsWithStatus_Populate(1);
            DG5.ItemsSource = null;
            DG5.ItemsSource = PlannerClass.ActiveTickets;
        }
    }
}
