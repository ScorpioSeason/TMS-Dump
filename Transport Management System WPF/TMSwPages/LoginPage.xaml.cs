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
            InitializeComponent();
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            //if (LogsList.SelectedItem != null)
            //{
                ViewLogDetails newpage = new ViewLogDetails(/*this.LogsList.SelectedItem*/);
                this.NavigationService.Navigate(newpage);
            //}
        }

        private void BuyerClick(object sender, RoutedEventArgs e)
        {
            //if (LogsList.SelectedItem != null)
            //{
                ViewLogDetails newpage = new ViewLogDetails(/*this.LogsList.SelectedItem*/);
                this.NavigationService.Navigate(newpage);
            //}
        }

        private void PlannerClick(object sender, RoutedEventArgs e)
        {
            //if (LogsList.SelectedItem != null)
            //{
                ViewLogDetails newpage = new ViewLogDetails(/*this.LogsList.SelectedItem*/);
                this.NavigationService.Navigate(newpage);
            //}
        }

    }
}
