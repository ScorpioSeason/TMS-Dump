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
            TMSLogger.SetDefaultLogFilePath(); // Initialize logger location when app opens
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            AdminPage newpage = new AdminPage();
            this.NavigationService.Navigate(newpage);

        }

        private void BuyerClick(object sender, RoutedEventArgs e)
        {
            BuyerPage newpage = new BuyerPage();
            this.NavigationService.Navigate(newpage);

        }

        private void PlannerClick(object sender, RoutedEventArgs e)
        {
            PlannerPage newpage = new PlannerPage();
            this.NavigationService.Navigate(newpage);

        }

    }
}
