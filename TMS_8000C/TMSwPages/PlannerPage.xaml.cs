using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            TMSLogger.LogStatusEvent += LogStatusEventHandler;
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

        public void LogStatusEventHandler(TMSLog log)
        {
            // Handle the event (send it to the status bar)
            status.Text = "Status: " + log.logMessage;
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
            SQL.close();

            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void LoadContracts_Click(object sender, RoutedEventArgs e)
        {
            NomContractList.ItemsSource = PlannerClass.GetNominatedContracts();
            NomContractList.Items.Refresh();
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


        private void CreateChossenOrder(object sender, RoutedEventArgs e)
        {
            if (NomContractList.SelectedCells != null)
            {
                AttatchTicketPage newpage = new AttatchTicketPage(this.NomContractList.SelectedItem);
                this.NavigationService.Navigate(newpage);
            }
        }

        private void DG5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlannerClass.ContractsPerTicket.Clear();

            foreach (FC_TripTicket_WProgress c in DG5.SelectedItems)
            {
                PlannerClass.ContractsPerTicket = PlannerClass.ContractsPerTicket_Populate(c.instance);

                foreach(FC_LocalContract x in PlannerClass.ContractsPerTicket)
                {
                    string query = "select * from FC_TripTicketLine where FC_TripTicketID = " + c.instance.FC_TripTicketID.ToString() + " and FC_LocalContractID =  " + x.FC_LocalContractID.ToString() + " ;";

                    FC_TripTicketLine t = new FC_TripTicketLine();
                    List<FC_TripTicketLine> theTicketLine = t.ObjToTable(SQL.Select(t, query));

                    x.Quantity = theTicketLine[0].PalletsOnTicket;
                }

                PlannerClass.RoutSegsPerTicket_Populate(c.instance);
            }

            DG6.ItemsSource = null;
            DG6.ItemsSource = PlannerClass.ContractsPerTicket;
            DG7.ItemsSource = null;
            DG7.ItemsSource = PlannerClass.RouteSegsPerTicket;
        }


        private void RefreshActiveTickets_Click(object sender, RoutedEventArgs e)
        {
            progresses.Clear();
            PlannerClass.ActiveTickets.Clear();
            PlannerClass.ActiveTickets = PlannerClass.TicketsWithStatus_Populate(1);

            foreach (FC_TripTicket c in PlannerClass.ActiveTickets)
            {
                FC_TripTicket_WProgress temp = new FC_TripTicket_WProgress(c);
                temp.GetTicketProgress(temp.instance);
                progresses.Add(temp);
            }
            DG5.ItemsSource = null;
            DG5.ItemsSource = progresses;
            
        }
        public static List<FC_TripTicket_WProgress> progresses = new List<FC_TripTicket_WProgress>();
        private void DGActiveContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            foreach (FC_LocalContract c in DGActiveContracts.SelectedItems)
            {
                PlannerClass.ConnectedTickets_Populate(c);
                
            }
            DGConnectedTickets.ItemsSource = null;
            DGConnectedTickets.ItemsSource = PlannerClass.ConnectedTickets;
        }

        private void RefreshActiveContracts_Click(object sender, RoutedEventArgs e)
        {
            
            PlannerClass.ActiveContracts.Clear();
            PlannerClass.ActiveContracts = PlannerClass.ContractsByStatus_Populate(1);
            DGActiveContracts.ItemsSource = null;
            DGActiveContracts.ItemsSource = PlannerClass.ActiveContracts;
            DGConnectedTickets.ItemsSource = null;
        }

        private void RefeshPendingTickets_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.PendingTickets.Clear();
            PlannerClass.PendingTickets = PlannerClass.TicketsWithStatus_Populate(0);//0 is pending
            DGPendingTickets.ItemsSource = null;
            DGPendingTickets.ItemsSource = PlannerClass.PendingTickets;
        }

        private void StartSelectedTicket_Click(object sender, RoutedEventArgs e)
        {
            foreach(FC_TripTicket x in PlannerClass.PendingTickets)
            {
                PlannerClass.UpdateTicketState(x, 1);
                x.Is_Complete = 1;
            }

            RefeshPendingTickets_Click(null, null);
        }

        private void MakeTimePass(object sender, RoutedEventArgs e)
        {
            TimePass.IncrementOneDay();
        }

        private void SetOrderToComplete_Click(object sender, RoutedEventArgs e)
        {
            foreach(FC_LocalContract c in DGActiveContracts.SelectedItems)
            {
                PlannerClass.UpdateContratState(c, 2);
            }

            PlannerClass.ActiveContracts.Clear();
            PlannerClass.ActiveContracts = PlannerClass.ContractsByStatus_Populate(1);
            DGActiveContracts.ItemsSource = null;
            DGActiveContracts.ItemsSource = PlannerClass.ActiveContracts;
            DGConnectedTickets.ItemsSource = null;
        }

        private void RefreshConfirmed_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.ToBeConfirmedContracts.Clear();
            PlannerClass.ToBeConfirmedContracts = PlannerClass.ContractsByStatus_Populate(2);
            DGConfirmCompletion.ItemsSource = null;
            DGConfirmCompletion.ItemsSource = PlannerClass.ToBeConfirmedContracts;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            foreach (FC_LocalContract c in DGConfirmCompletion.SelectedItems)
            {
                PlannerClass.UpdateContratState(c, 3);
            }

            PlannerClass.ConfirmedContracts.Clear();
            PlannerClass.ConfirmedContracts = PlannerClass.ContractsByStatus_Populate(2);
            DGConfirmCompletion.ItemsSource = null;
            DGConfirmCompletion.ItemsSource = PlannerClass.ConfirmedContracts;
        }

        private void ViewAllButton_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDG.ItemsSource = PlannerClass.GetAllInvoices();
            ContractsPreInvouce.ItemsSource = null;
        }

        private void VeiwTwoWeekButton_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDG.ItemsSource = PlannerClass.TwoWeekInvoices();
            ContractsPreInvouce.ItemsSource = null;
        }

        private void InvoiceDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InvoiceDG.SelectedItem != null)
            {
                FC_Invoice SelectedInvoice = (FC_Invoice)InvoiceDG.SelectedItem;

                ContractsPreInvouce.ItemsSource = PlannerClass.GetContracts_PreInvoice(SelectedInvoice);
            }
        }

        private void RefreshActiveTickets_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
