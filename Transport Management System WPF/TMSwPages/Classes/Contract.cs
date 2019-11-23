﻿// Contract FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Contract.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Ivan,Megan,Zena,Duane
 *  \brief	    This file contains the buyer functionality 
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the contract class holds all values for contracts and functions for translating their values to integers.
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class represents the contract table
namespace TMSwPages
{
    // Contract HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Contract
    *   \brief		This class holds the contracts
    *   \details	This class will mainly be used to pass contract data through out the application.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class Contract
    {
        public string client_Name 
        { 
            get;
            set; 
        }
        public bool job_Type 
        { 
            get; 
            set; 
        }
        public int quantity 
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
        public bool van_Type 
        { 
            get;
            set; 
        }

        public List<string> nominatedCarriers;




        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int ToCityID()
        *	\brief		Converts a city name to the corresponding int.
        *	\details	This function converts a city to the corresponding int by comparing the input string to city names and outputs the proper value for the city.
        *	\param[in]	string  inputCity       An incoming value meant to become the square's colour
        *	\param[out]	none
        *	\exception	none
        *	\see		none
        *	\return		cityID in int format
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
