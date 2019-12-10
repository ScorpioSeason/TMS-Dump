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

            NominatedCarrierDG.ItemsSource = PlannerClass.GetNomCarriers_withDepot(ReadInContract);


            string query = "Select * from FC_TripTicket where Is_Complete = 0 and CurrentLocation = \"" + ReadInContract.Origin + "\" and not Size_in_Palettes = 0;";

            FC_TripTicket t = new FC_TripTicket();
            List<FC_TripTicket> OtherTickets = t.ObjToTable(SQL.Select(t, query));

            PossibleTickets.ItemsSource = OtherTickets;
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

            if (this.NominatedCarrierDG.SelectedItem != null)
            {
                CarrierWithDepot_View t = (CarrierWithDepot_View)NominatedCarrierDG.SelectedCells[0].Item;
                FC_Carrier selCarrier = new FC_Carrier(t.FC_CarrierID, t.Carrier_Name);

                CreateTripInfo tripInfo = new CreateTripInfo(PassedInContract, selCarrier, SelectedTicket);

                ticketsFromScreen = new List<FC_TripTicket>();

                foreach (FC_TripTicket x in AllTickets.Items)
                {
                    if (x.FC_TripTicketID != SelectedTicket.FC_TripTicketID)
                    {
                        ticketsFromScreen.Add(x);
                    }
                }


                AllTickets.ItemsSource = ticketsFromScreen;
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
                    }
                }

                

            }

            
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            PlannerClass.DeleteNominations(PassedInContract);

            PlannerPage newpage = new PlannerPage();
            this.NavigationService.Navigate(newpage);
        }
    }
}
