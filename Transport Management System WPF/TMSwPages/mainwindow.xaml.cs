// MAINNAVWINDOW FILE HEADER COMMENT: ==========================================================================
/**
 *  \file		mainwindow.xaml.cs
 *  \ingroup	TMS
 *  \date		November 22, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the Interaction Logic for the Main navigation window Page (code-behind)  
 *  \see		mainwindow.xaml
 *  \details    This file contains the Interaction Logic for the Main navigation window Page. Right now it 
 *              doesn't do anything except inherit from NavigationWindow
 *
 * =========================================================================================================== */

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
    /// \summary This is the Interaction Logic for the main nagivation window
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
