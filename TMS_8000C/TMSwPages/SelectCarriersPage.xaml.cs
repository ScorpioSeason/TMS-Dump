﻿using System;
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
    public partial class SelectCarriersPage : Page
    {
        static int selectedTab = -1;

        FC_LocalContract PassedInContract = null;

        public SelectCarriersPage()
        {
            InitializeComponent();

            //DG5.ItemsSource = 
        }

        public SelectCarriersPage(object data) : this()
        {
            FC_LocalContract ReadInContract = (FC_LocalContract)data;
            PassedInContract = ReadInContract;

            string query = "select ca.FC_CarrierID, ca.Carrier_Name, dc.CityName, dc.FTL_Availibility, dc.LTL_Availibility, dc.FTL_Rate, dc.LTL_Rate, dc.Reefer_Charge " +
                           "from FC_BuyerToPlannerContract as bp " +
                           "left join FC_CarrierNom as CN on CN.FC_BuyerToPlannerContractID = bp.FC_BuyerToPlannerContractID " +
                           "left join FC_Carrier as ca on ca.FC_CarrierID = CN.FC_CarrierID " +
                           "left join FC_DepotCity as dc on dc.FC_CarrierID = ca.FC_CarrierID " +
                           "where bp.FC_LocalContractID = " + ReadInContract.FC_LocalContractID.ToString() + " and dc.CityName = \"" + ReadInContract.Origin + "\";";

            CarrierWithDepot_View c = new CarrierWithDepot_View();
            List<CarrierWithDepot_View> NominatedCarriers = c.ObjToTable(SQL.Select(c, query));

            CarriersList.ItemsSource = NominatedCarriers;

            query = "Select * from FC_TripTicket where Is_Complete = 0 and CurrentLocation = \"" + ReadInContract.Origin + "\";";

            FC_TripTicket t = new FC_TripTicket();
            List<FC_TripTicket> PossibleTickets = t.ObjToTable(SQL.Select(t, query));

            OtherTickets.ItemsSource = PossibleTickets;
        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void LoadContracts_Click(object sender, RoutedEventArgs e)
        {
            string query = "select LC.FC_LocalContractID, LC.Client_Name, LC.Job_type, LC.Quantity, LC.Origin, LC.Destination, LC.Van_type " +
               "from FC_BuyerToPlannerContract as bp " +
               "left join FC_LocalContract as LC on LC.FC_LocalContractID = bp.FC_LocalContractID;";

            FC_LocalContract n = new FC_LocalContract();
            List<FC_LocalContract> NominatedContracts = n.ObjToTable(SQL.Select(n, query));
        }

        private void SelectCarriers_Click(object sender, RoutedEventArgs e)
        {
            FC_Carrier t = (FC_Carrier)CarriersList.SelectedCells[0].Item;

            CreateTripInfo tripInfo = new CreateTripInfo(PassedInContract, t);
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

        private void CarriersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DG5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (FC_TripTicket c in DG5.SelectedItems)
            {
                PlannerClass.ContractsPerTicket_Populate(c);
            }
            DG6.ItemsSource = null;
            DG6.ItemsSource = PlannerClass.ContractsPerTicket;
        }

        private void RefreshActiveTickets_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.TicketsWithStatus_Populate(1);
            DG5.ItemsSource = null;
            DG5.ItemsSource = PlannerClass.ActiveTickets;
        }
    }
}
