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
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {
        static int selectedTab = -1;
        List<string> invoiceFiles = new List<string>(); 

        //public static string a = "";

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public BuyerPage()
        {
            InitializeComponent();
            BuyerClass.ParseContracts();
            DG1.ItemsSource = BuyerClass.Contracts;
            DG2.ItemsSource = BuyerClass.acceptedContracts;
            TMSLogger.LogStatusEvent += LogStatusEventHandler;
            Folder.ItemsSource = invoiceFiles; 
        }

        public void LogStatusEventHandler(TMSLog log)
        {
            // Handle the event (send it to the status bar)
            status.Text = "Status: " + log.logMessage;
        }

        //public BuyerPage(SQL_Query_TMS validatedConnection)
        //{
        //    InitializeComponent();

        //    // Load SQL Connection
        //    //admin.SetTMSConnection(validatedConnection);

        //    /// Bind to incoming log data.
        //    //this.DataContext = data;
        //}

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public void SetOutput(string outputString)
        {
            status.Text = outputString;
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void RedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            status.Text = "Yeet";
            //Megan.Append("Yeet");
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void Button_Click(object sender, RoutedEventArgs e)//load refresh button
        {
            DG1.ItemsSource = null;
            BuyerClass.ParseContracts();
            DG1.ItemsSource = BuyerClass.Contracts;
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DG2.Items.Refresh();
        }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /*
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        private void Button_AddContract(object sender, RoutedEventArgs e)
        {
            foreach (FC_ContractFromRuss c in DG1.SelectedItems)
            {
                BuyerClass.acceptedContracts.Add(c);
                BuyerClass.contracts.Remove(c);
            }
            DG1.ItemsSource = null;
            DG1.ItemsSource = BuyerClass.contracts;
            DG2.ItemsSource = null;
            DG2.ItemsSource = BuyerClass.acceptedContracts;
        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            SQL.close();

            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void DG2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (FC_ContractFromRuss c in DG2.SelectedItems)
            {
                BuyerClass.NominationView(c);
            }
            DG3.ItemsSource = null;
            DG3.ItemsSource = BuyerClass.tempCarriers;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            BuyerClass.Nominations();
            DG2.ItemsSource = null;
            DG3.ItemsSource = null;
        }


        private void BuyerTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedTab != BuyerTabs.SelectedIndex)
            {
                if ((BuyerTabs.SelectedIndex <= 4) && (BuyerTabs.SelectedIndex >= -1))
                {
                    selectedTab = BuyerTabs.SelectedIndex;

                    try
                    {
                        switch (selectedTab)
                        {
                            case (0):
                                // Initialize everything that you want on startup for the tab

                                //LogStartDate.SelectedDate = (DateTime.Today.AddDays(-7));
                                //LogEndDate.SelectedDate = DateTime.Today;
                                //LogSearchTags.Focus();
                                //LogsList.ItemsSource = searchResults;
                                //LogLoadClick(null, null);

                                break;
                            case (1):
                                //backup
                                break;

                            //case (2):
                            //    //Carrier_Data.DataContext = admin.DisplayCarrier();
                            //    Carrier_DataList.ItemsSource = admin.DisplayCarrier();
                            //    Carrier_DataLoadClick(null, null);
                            //    break;
                            //case (3):
                            //    //Route_Table.DataContext = admin.DisplayRoutes();
                            //    Route_TableList.ItemsSource = admin.DisplayRoutes();
                            //    Route_TableLoadClick(null, null);
                            //    break;
                            //case (4):
                            //    //Rate_Fee_Tables.DataContext = admin.DisplayFees();
                            //    //Rate_Fee_TablesList.ItemsSource = admin.DisplayFees();
                            //    Rate_Fee_TablesClick(null, null);
                            //    break;

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

        private void RefreshCustomers_2(object sender, RoutedEventArgs e)
        {
            BuyerClass.AllCustomers.Clear();

            Cust_Price cp = new Cust_Price();
            BuyerClass.AllCustomers = cp.ObjToTable(SQL.Select(cp));

            DGCustomers.ItemsSource = null;
            DGCustomers.ItemsSource = BuyerClass.AllCustomers;
        }

        private void DGCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BuyerClass.ContractsForCustomer.Clear();
            foreach (Cust_Price c in DGCustomers.SelectedItems)
            {
                BuyerClass.ContractsForCustomer = PlannerClass.ContractsByName_Populate(c.Client_Name);
            }

            DGContractsForCustomer.ItemsSource = BuyerClass.ContractsForCustomer;
        }

        

        private void SelectCustomer_Click(object sender, RoutedEventArgs e)
        {
            InvoiceSelection newpage = new InvoiceSelection();
            this.NavigationService.Navigate(newpage);
        }

        private void ReadFolder_Click(object sender, RoutedEventArgs e)
        {
            invoiceFiles.Clear(); 

            //FileInfo fold = new FileInfo(Directory.GetCurrentDirectory() + "")

            foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/Invoices/", "*.txt"))
            {
                FileInfo fi = new FileInfo(file);

                invoiceFiles.Add(fi.FullName);

            }

            Folder.Items.Refresh(); 
        }

        private void ChangeLogLocation(object sender, RoutedEventArgs e)
        {

        }

        private void ReacInvlocesFromDatabase(object sender, RoutedEventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"./Invoices/");//Assuming Test is your Folder

            //FileInfo fi = new FileInfo(d);
            //if (fi.Exists == false)
            //{
            //    Directory.CreateDirectory(invoiceDir);
            //}

            if (Folder.SelectedItem != null)
            {
                string viewInvoice = (string)Folder.SelectedItem;
                string invoiceText = "";

                ViewInvoice.Text = "";

                try
                {

                    /// Open the file stream to read from the file
                    FileStream fileStream = new FileStream((viewInvoice), FileMode.Open, FileAccess.Read);
                    StreamReader streamReader = new StreamReader(fileStream);

                    /// Fill the working list with lines from the file 
                    while (!streamReader.EndOfStream)
                    {
                        invoiceText += streamReader.ReadLine() + "\n";
                    }

                    ViewInvoice.Text = invoiceText;

                    /// Close the file
                    streamReader.Close(); fileStream.Close();
                }
                /// If an exception is thrown here, create a log for it. 
                catch (Exception ex)
                {
                    TMSLogger.LogIt("|" + "/BuyerPage.xaml.cs" + "|" + "BuyerPage" + "|" + "Folder_SelectionChanged" + "|" + ex.GetType().ToString() + "|" + ex.Message + "|");

                }


            }

           
        }
    }

    public class ViewInvoiceClass
    {
        public string theInvice { get; set; }
    }




}
