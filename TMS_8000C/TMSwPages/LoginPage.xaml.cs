using System.Windows;
using System.Windows.Controls;

namespace TMSwPages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            ShowsNavigationUI = false;
            /* If ShowsNavigationUI is true it will show the arrows in the top left corner. 
             * I want to avoid this and simply make a back button on each page where applicable. 
               from a UI perspective, it provides a cleaner interface and gives the impression 
               that if we DID use log-in security it would be easier to implement. 
             
               We can implement navigation as seen below in AdminClick OR using the 
               NavigationService object's methods (this.NavigationService.GoBack());
              
             */

            InitializeComponent();
            //TMSLogger.SetDefaultLogFilePath(); // Initialize logger location when app opens
        }

        private void ValidateLogIn(object sender, RoutedEventArgs e)
        {
            bool loginSuccess = false;
            //SQL_Query_TMS loginConnection = null; 
            // Check that values have been entered first
            if (/*(password.Password != "") &&*/ (username.SelectedIndex != -1))
            {
                // Get login connection
                if (loginSuccess == false)
                {
                    //loginConnection = new SQL_Query_TMS(username.Text, password.Password.ToString()); // pass login to each page for access privileges
                    //loginSuccess = loginConnection._isConnected;

                    //password.Password = "";

                    loginSuccess = true;

                }
                // If the connection credentials were validated, redirect to page with set connection
                if (loginSuccess == true)
                {

                    //duane messed with this just for testing

                    if (username.Text == "Admin")
                    {
                        AdminPage newpage = new AdminPage();
                        this.NavigationService.Navigate(newpage);

                        //if (loginConnection != null)
                        //{
                        //    AdminPage newpage = new AdminPage(loginConnection);
                        //    this.NavigationService.Navigate(newpage);
                        //}

                    }
                    else if (username.Text == "Buyer")
                    {

                        BuyerPage newpage = new BuyerPage();
                        this.NavigationService.Navigate(newpage);

                        //if (loginConnection != null)
                        //{
                        //    BuyerPage newpage = new BuyerPage(loginConnection);
                        //    this.NavigationService.Navigate(newpage);
                        //}
                    }
                    else if (username.Text == "Planner")
                    {

                        PlannerPage newpage = new PlannerPage();
                        this.NavigationService.Navigate(newpage);

                        //if (loginConnection != null)
                        //{
                        //    PlannerPage newpage = new PlannerPage(loginConnection);
                        //    this.NavigationService.Navigate(newpage);
                        //}
                    }
                }

            }
        }
    }

}   

