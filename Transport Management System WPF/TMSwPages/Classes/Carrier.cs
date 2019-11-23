// CARRIER FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Carrier.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan,Ivan,Zena,Duane
 *  \brief	    This file contains the admin functionality	  
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the Carrier class This file holds the functionality of the
 *              contract class holds all values for contracts and functions for translating their values to 
 *              integers.                                    
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /*   
    *   \class		Carrier
    *   \brief		This class holds all the values for the carriers and their locations of operation.
    *   \details	This class holds all the values for the carriers and their locations of operation, 
    *   along with all the values for the amount of vehicles and their types. 
    *   
    * -------------------------------------------------------------------------------------------------------- */
    class Carrier
    {
        public string Carrier_Name { get; set; }
        public string Depot_City { get; set; }
        public int FTL { get; set; }
        public int LTL { get; set; }
        public float FTL_Rate { get; set; }
        public float LTL_Rate { get; set; }
        public float Reefer_Charge { get; set; }
    }
}
