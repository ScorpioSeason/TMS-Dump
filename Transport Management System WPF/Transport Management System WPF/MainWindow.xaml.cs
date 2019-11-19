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
    public partial class MainWindow : Window
    {
        public static string a = "";
        public MainWindow()
        {
            InitializeComponent();
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
            BuyerClass buyer = new BuyerClass();
            buyer.ParseContracts();
            DG1.ItemsSource = buyer.Contracts;



            int k = 0;
        }
    }
}
