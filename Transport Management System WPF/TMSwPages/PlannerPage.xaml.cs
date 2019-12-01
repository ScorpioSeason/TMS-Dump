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
    /// Interaction logic for PlannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        public PlannerPage()
        {
            InitializeComponent();
        }

        public PlannerPage(SQL_Query_TMS validatedConnection)
        {
            InitializeComponent();

            // Load SQL Connection
            //admin.SetTMSConnection(validatedConnection);

            /// Bind to incoming log data.
            //this.DataContext = data;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)//load/ refresh button
        //{
        //    buyer.ParseContracts();
        //    DG1.Items.Refresh();
        //}

        private void SwitchUserClick(object sender, RoutedEventArgs e)
        {
            LoginPage newpage = new LoginPage();
            this.NavigationService.Navigate(newpage);
        }



    }
}
