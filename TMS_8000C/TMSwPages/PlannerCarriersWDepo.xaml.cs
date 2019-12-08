using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMSwPages.Classes;

namespace TMSwPages
{
    /// <summary>
    /// Interaction logic for PlannerCarriersWDepo.xaml
    /// </summary>
    public partial class PlannerCarriersWDepo : Page
    {
        public PlannerCarriersWDepo()
        {
            InitializeComponent();
            List<Carrier> ListCarrierList = new List<Carrier>();
        }

        public PlannerCarriersWDepo(object data) : this()
        {
            /// Bind to incoming log data.
            this.DataContext = data;
        }

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }

        private void PlannerPageClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            //AdminPage newpage = new AdminPage();
            //this.NavigationService.Navigate(newpage);
        }

       
    }

    class Depot
    {

    }

    
    
}
