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
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {
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
    }
}
