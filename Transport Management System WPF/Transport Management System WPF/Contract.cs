// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Admin.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the admin functionality	  
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class represents the contract table
namespace Transport_Management_System_WPF
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class Contract
    {
        public string client_Name 
        { 
            get;
            set; 
        }
        public string job_Type 
        { 
            get; 
            set; 
        }
        public string quantity 
        { 
            get;
            set; 
        }
        public string origin 
        { 
            get;
            set; 
        }
        public string destination 
        { 
            get;
            set; 
        }
        public string van_Type 
        { 
            get;
            set; 
        }

        public List<string> nominatedCarriers;



        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public static int ToCityID(string inputCity)
        {
            if(inputCity == "Windsor")
            {
                return 0;
            }
            else if(inputCity == "London")
            {
                return 1;
            }
            else if (inputCity == "Hamilton")
            {
                return 2;
            }
            else if (inputCity == "Toronto")
            {
                return 3;
            }
            else if (inputCity == "Oshawa")
            {
                return 4;
            }
            else if (inputCity == "Belleville")
            {
                return 5;
            }
            else if (inputCity == "Kingston")
            {
                return 6;
            }
            else if (inputCity == "Ottawa")
            {
                return 7;
            }

            return -1;
        }
    }



}
