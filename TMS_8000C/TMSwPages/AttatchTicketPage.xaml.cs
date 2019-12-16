using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TMSwPages.Classes;

namespace TMSwPages
{
    /// <summary>
    /// Interaction logic for AttatchTicketPage.xaml
    /// </summary>
    public partial class AttatchTicketPage : Page
    {

        public FC_TripTicket SelectedTicket;
        public FC_LocalContract PassedInContract;

        public AttatchTicketPage()
        {
            InitializeComponent();
        }

        public AttatchTicketPage(object data) : this()
        {
            FC_LocalContract ReadInContract = (FC_LocalContract)data;
            PassedInContract = ReadInContract;

            List<FC_TripTicket> ContractsTickets = PlannerClass.CreateTicketsFromContract(ReadInContract);
            AllTickets.ItemsSource = ContractsTickets;

            RefreshNomCarriers();
            RefreshPossibleTickets();
        }

        public void RefreshPossibleTickets()
        {

            string query = "Select * from FC_TripTicket where Is_Complete = 0 and CurrentLocation = \"" + PassedInContract.Origin + "\" and not Size_in_Palettes = 0;";

            FC_TripTicket t = new FC_TripTicket();
            List<FC_TripTicket> OtherTickets = t.ObjToTable(SQL.Select(t, query));
            List<FC_TripTicket> ValidatedTickets = new List<FC_TripTicket>();

            foreach (FC_TripTicket x in OtherTickets)
            {
                List<FC_LocalContract> ContractsForTicket = PlannerClass.ContractsPerTicket_Populate(x);

                bool matchfound = false;

                foreach (FC_LocalContract y in ContractsForTicket)
                {
                    if (y.FC_LocalContractID == PassedInContract.FC_LocalContractID)
                    {
                        matchfound = true;
                    }

                    if(y.Van_type != PassedInContract.Van_type)
                    {
                        matchfound = true;
                    }
                }

                if (!matchfound)
                {
                    ValidatedTickets.Add(x);
                }
            }

            PossibleTickets.ItemsSource = ValidatedTickets;
        }

        private void RefreshNomCarriers()
        {
            List<CarrierWithDepot_View> PotentialDepots = PlannerClass.GetNomCarriers_withDepot(PassedInContract);
            List<CarrierWithDepot_View> OutDepots = new List<CarrierWithDepot_View>();

            foreach (CarrierWithDepot_View x in PotentialDepots)
            {
                if (PassedInContract.Job_type == 0)
                {
                    if (x.FTL_Availibility > 0)
                    {
                        OutDepots.Add(x);
                    }
                }
                else
                {
                    if (x.LTL_Availibility > 0)
                    {
                        OutDepots.Add(x);
                    }
                }
            }

            NominatedCarrierDG.ItemsSource = OutDepots;
        }

        private void AllTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.AllTickets.SelectedItem != null)
            {
                FC_TripTicket selected = (FC_TripTicket)this.AllTickets.SelectedItem;

                SelectedTicket = selected;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            List<FC_TripTicket> ticketsFromScreen = new List<FC_TripTicket>();

            if(SelectedTicket != null)
            {
                if (this.NominatedCarrierDG.SelectedItem != null && SelectedTicket != null)
                {
                    CarrierWithDepot_View t = (CarrierWithDepot_View)NominatedCarrierDG.SelectedCells[0].Item;
                    FC_Carrier selCarrier = new FC_Carrier(t.FC_CarrierID, t.Carrier_Name);

                    CreateTripInfo tripInfo = new CreateTripInfo(PassedInContract, selCarrier, SelectedTicket);

                    int AvalType = t.LTL_Availibility - 1;

                    if(SelectedTicket.Size_in_Palettes == 0)
                    {
                        AvalType = t.FTL_Availibility - 1;
                    }

                    SQL.UpdateDepotAvalibility(t.FC_CarrierID, t.CityName, SelectedTicket.Size_in_Palettes, AvalType);

                    ticketsFromScreen = new List<FC_TripTicket>();

                    foreach (FC_TripTicket x in AllTickets.Items)
                    {
                        if (x.FC_TripTicketID != SelectedTicket.FC_TripTicketID)
                        {
                            ticketsFromScreen.Add(x);
                        }
                    }

                    AllTickets.ItemsSource = ticketsFromScreen;

                    if (ticketsFromScreen.Count == 0)
                    {
                        Complete.IsEnabled = true;
                        ExitMessage.Visibility = Visibility.Hidden;
                    }
                }
                else if (this.PossibleTickets.SelectedItem != null)
                {
                    FC_TripTicket ExcistingTicket = (FC_TripTicket)this.PossibleTickets.SelectedItem;

                    int PalletesAddedToTicket = PlannerClass.AddContractToTicket(ExcistingTicket, SelectedTicket, PassedInContract);

                    if (PalletesAddedToTicket != -1)
                    {
                        SelectedTicket.Size_in_Palettes -= PalletesAddedToTicket;

                        foreach (FC_TripTicket x in AllTickets.Items)
                        {
                            if (x.FC_TripTicketID == SelectedTicket.FC_TripTicketID)
                            {
                                x.Size_in_Palettes = SelectedTicket.Size_in_Palettes;

                                if (x.Size_in_Palettes > 0)
                                {
                                    ticketsFromScreen.Add(x);
                                }
                            }
                        }

                        AllTickets.ItemsSource = ticketsFromScreen;

                        if (ticketsFromScreen.Count == 0)
                        {
                            Complete.IsEnabled = true;
                            ExitMessage.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }

            this.NominatedCarrierDG.UnselectAll();
            this.PossibleTickets.UnselectAll();

            RefreshNomCarriers();
            RefreshPossibleTickets();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.DeleteNominations(PassedInContract);
            PlannerClass.UpdateContratState(PassedInContract, 1);

            PlannerPage newpage = new PlannerPage();
            this.NavigationService.Navigate(newpage);
        }

        private void PossibleTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NominatedCarrierDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
