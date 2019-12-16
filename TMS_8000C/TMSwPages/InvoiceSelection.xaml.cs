using System.Windows;
using System.Windows.Controls;
using TMSwPages.Classes;

namespace TMSwPages
{
    /// <summary>
    /// Interaction logic for InvoiceSelection.xaml
    /// </summary>
    public partial class InvoiceSelection : Page
    {
        public InvoiceSelection()
        {
            InitializeComponent();
            DGContractsForCustomer.ItemsSource = BuyerClass.ContractsForCustomer;
        }

        private void AddSelectedToInvoice_Click(object sender, RoutedEventArgs e)
        {
            BuyerClass.SelectedForInvoice.Clear();
            DGSelectForInvoice.ItemsSource = null;
            foreach (FC_LocalContract c in DGContractsForCustomer.SelectedItems)
            {
                BuyerClass.ContractsForCustomer.Remove(c);
                BuyerClass.SelectedForInvoice.Add(c);
            }
            DGContractsForCustomer.ItemsSource = null;
            DGContractsForCustomer.ItemsSource = BuyerClass.ContractsForCustomer;
            DGSelectForInvoice.ItemsSource = BuyerClass.SelectedForInvoice;

        }

        private void ConfirmInvoice_Click(object sender, RoutedEventArgs e)
        {
            if(BuyerClass.SelectedForInvoice != null)
            {
                FC_Invoice invTemp = PlannerClass.GenerateInvoice(BuyerClass.SelectedForInvoice[0]);
                PlannerClass.InsertInvoice(invTemp, BuyerClass.SelectedForInvoice[0]);
                PlannerClass.UpdateContratState(BuyerClass.SelectedForInvoice[0], 4);

                BuyerClass.SelectedForInvoice.Remove(BuyerClass.SelectedForInvoice[0]);

                foreach (FC_LocalContract c in BuyerClass.SelectedForInvoice)
                {
                    PlannerClass.AddContractToInvoices(invTemp, c);
                    PlannerClass.UpdateContratState(c, 4);
                }

                BuyerPage newpage = new BuyerPage();
                this.NavigationService.Navigate(newpage);
            }
        }
    }
}
