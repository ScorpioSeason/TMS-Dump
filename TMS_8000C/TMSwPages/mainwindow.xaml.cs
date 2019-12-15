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

using System.Windows.Navigation;
using TMSwPages.Classes;

namespace TMSwPages
{
    /// \summary This is the Interaction Logic for the main navigation window
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();

           

            TMSLogger.SetDefaultLogFilePath(); // Initialize logger location when app opens
            TMSBackup.SetDefaultBackupFilePath();
        }
    }
}
