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

namespace SampleWFPUsingPages
{
    /// <summary>
    /// Interaction logic for ViewLogDetailsPage.xaml
    /// </summary>
    public partial class ViewLogDetailsPage : Page
    {
        public ViewLogDetailsPage()
        {
            InitializeComponent();
        }
        public ViewLogDetailsPage(object data) : this()
        {
            // Bind to incoming log data.
            this.DataContext = data;
        }

    }
}
