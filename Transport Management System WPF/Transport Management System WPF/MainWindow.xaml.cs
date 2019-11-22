using Microsoft.VisualC.StlClr;
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

namespace Transport_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        List<Contract> acceptedContracts = new List<Contract>();
        public static BuyerClass buyer = new BuyerClass();
        public static string a = "";
        public MainWindow()
        {
            InitializeComponent();
            DG1.ItemsSource = buyer.Contracts;
            DG2.ItemsSource = buyer.Contracts; //this.acceptedContracts;
        }

        public void SetOutput(string outputString)
        {
            Output.Text = outputString;
        }

        private void RedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Output.Text = "Yeet";
            //Megan.Append("Yeet");
        }

        private void Button_Click(object sender, RoutedEventArgs e)//load/ refresh button
        {
            
            buyer.ParseContracts();
            DG1.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DG2.Items.Refresh();
        }

        private void Button_AddContract(object sender, RoutedEventArgs e)
        {
            DataGrid DG = DG2;
            foreach (Contract c in DG.SelectedItems)
            {
                acceptedContracts.Add(c);
            }
            
        }
    }
}
