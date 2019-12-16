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
using System.Windows;
using System.Windows.Navigation;
using TMSwPages.Classes;

namespace TMSwPages
{
    public delegate void LogStatusDelegate(TMSLog log);

    /// \summary This is the Interaction Logic for the main navigation window
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            TMSStartup(); 

        }

        private void TMSStartup()
        {
            TMSLogger.SetDefaultLogFilePath(); // Initialize logger location when app opens
            TMSBackup.SetDefaultBackupFilePath();
            TMSLogger.LogStatusEvent += LogStatusEventHandler;
        }

        public static void LogStatusEventHandler(TMSLog log) 
        { 
            // Handle the event (send it to the status bar)
        }

        private void NavigationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQL.close(); 
        }
    }

}
