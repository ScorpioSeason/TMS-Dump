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

            FC_Invoice invTemp = PlannerClass.GenerateInvoice(BuyerClass.SelectedForInvoice[0]);
            PlannerClass.InsertInvoice(invTemp, BuyerClass.SelectedForInvoice[0]);

            BuyerClass.SelectedForInvoice.Remove(BuyerClass.SelectedForInvoice[0]);
            
            foreach(FC_LocalContract c in BuyerClass.SelectedForInvoice)
            {
                PlannerClass.AddContractToInvoices(invTemp, c);
            }

            BuyerPage newpage = new BuyerPage();
            this.NavigationService.Navigate(newpage);
        }
    }
}
