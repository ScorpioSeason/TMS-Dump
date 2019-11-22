﻿using System;
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
    /// Interaction logic for ViewLogDetails.xaml
    /// </summary>
    public partial class ViewLogDetails : Page
    {
        public ViewLogDetails()
        {
            InitializeComponent();
        }
        public ViewLogDetails(object data) : this()
        {
            // Bind to incoming log data.
            this.DataContext = data;
        }

    }
}
