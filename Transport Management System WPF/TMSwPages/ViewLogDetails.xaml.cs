// VIEWLOGDETAILS FILE HEADER COMMENT: ========================================================================
/**
 *  \file		Admin.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the interaction logic for the detailed view of a single TMSLog.  
 *  \see		ViewLogDetails.xaml
 *  \details    This file contains the functionality for the detailed view of a single TMSLog. This only 
 *              displays the simple details and allows for back-navigation. 
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
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public partial class ViewLogDetails : Page
    {
        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			public ViewLogDetails()
        *	\brief		Constructor for the ViewLogDetails page. 
        *	\details	Initializes the page. 
        *	\see		InitializeComponent()
        *	\return		N/A
        *
        * ---------------------------------------------------------------------------------------------------- */
        public ViewLogDetails()
        {
            InitializeComponent();
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			public ViewLogDetails(object data) : this()
        *	\brief		Alternative constructor for the ViewLogDetails
        *	\details	This constructor allows population of the page using data selected in a previous page. 
        *	\param[in]	object  data    this()		The value in the datagrid selected in the previous page.
        *	\return		N/A
        *
        * ---------------------------------------------------------------------------------------------------- */
        public ViewLogDetails(object data) : this()
        {
            /// Bind to incoming log data.
            this.DataContext = data;
        }

    }
}
