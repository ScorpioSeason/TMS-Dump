using System.Windows;
using System.Windows.Controls;
using TMSwPages.Classes;

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
        }

        private void ValidateLogIn(object sender, RoutedEventArgs e)
        {
            bool loginSuccess = false;

            if ((password.Password != "") && (username.SelectedIndex != -1))
            {
                // Get login connection
                if (loginSuccess == false)
                {
                    SQL.SetUserID(username.Text.ToLower());
                    SQL.SetPassWord(password.Password.ToString());

                    SQL.init();
                    loginSuccess = SQL.open();

                }
                if (loginSuccess == true)
                {
                    if (username.Text == "Admin")
                    {
                        AdminPage newpage = new AdminPage();
                        this.NavigationService.Navigate(newpage);
                    }
                    else if (username.Text == "Buyer")
                    {
                        BuyerPage newpage = new BuyerPage();
                        this.NavigationService.Navigate(newpage);
                    }
                    else if (username.Text == "Planner")
                    {
                        PlannerPage newpage = new PlannerPage();
                        this.NavigationService.Navigate(newpage);
                    }
                }

            }
        }

        private void Button_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ValidateLogIn(null, null); 
            }
        }
    }

}   

