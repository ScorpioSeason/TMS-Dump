using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    class SQL_FAKEIT
    {
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
        public List<string>[] Select_Carriers()
        {
            //Create a list to store the result
            List<string>[] list = new List<string>[7];

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            return list;
        }
    }
}
