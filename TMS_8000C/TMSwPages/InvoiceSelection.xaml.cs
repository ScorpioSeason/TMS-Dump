using System;
using System.Collections.Generic;
using System.IO;
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

            if(BuyerClass.SelectedForInvoice != null)
            {
                FC_Invoice invTemp = PlannerClass.GenerateInvoice(BuyerClass.SelectedForInvoice[0]);
                PlannerClass.InsertInvoice(invTemp, BuyerClass.SelectedForInvoice[0]);

                BuyerClass.SelectedForInvoice.Remove(BuyerClass.SelectedForInvoice[0]);

                foreach (FC_LocalContract c in BuyerClass.SelectedForInvoice)
                {
                    PlannerClass.AddContractToInvoices(invTemp, c);
                }
                
                // sql read of the invoice 

                // Write the invoice to a file

                try
                {
                    string invoiceDir = Directory.GetCurrentDirectory() + "/Invoices";
                    Directory.CreateDirectory(invoiceDir);

                    string filePath = "invoice_" + invTemp.FC_InvoiceID + ".txt";

                    /// Open the filestream to append to the file. 
                    FileStream fileStream = new FileStream(invoiceDir + filePath, FileMode.Create, FileAccess.Write);
                    StreamWriter fileWriter = new StreamWriter(fileStream);

                    /// Add each log entry from the working list to the file as a BSV
                    fileWriter.WriteLine(invTemp);
                    fileWriter.Flush();

                    /// Close the file
                    fileWriter.Close(); fileStream.Close();
                }
                /// If an exception is thrown here, catch it
                catch (Exception ex)
                {
                    TMSLogger.LogIt("|" + "/InvoiceSelection.xaml.cs" + "|" + "InvoiceSelection" + "|" + "ConfirmInvoice_Click" + "|" + ex.GetType().ToString() + "|" + ex.Message + "|");   
                }
                BuyerPage newpage = new BuyerPage();
                this.NavigationService.Navigate(newpage);
            }

            
        }
    }
}
