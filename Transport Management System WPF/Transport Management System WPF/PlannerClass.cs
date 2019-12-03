// PLANNER CLASS HEADER COMMENT: =================================================================================
/**
 *  \file		PlannerClass.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Duane,Megan,Ivan,Zena
 *  \brief	    This file contains the Planner functionality
 *  \see		MainWindow.xaml
 *  \details    See the class description for the more details                            
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This file represents the UI functionality of the Planner class. 
// This includes selecting carriers for partially complete orders, 
// viewing active orders, viewing delivered orders pending completion, 
// and generating invoice reports. Furthermore the planner can also increment time. 

namespace Transport_Management_System_WPF
{
    public struct Customer
    {
        public string Customer_Name; ///The Customer Name
    }

    public class Truck
    {
        public int TruckID;
        public string Current_Location;
        public int CarrierID;
        public int Is_Reefer;
    }


    //public struct Trip_Ticket
    //{
    //    public int TicketID; ///The ticket ID Number
    //    public Truck TruckID; ///The Truck that the ticket will attached to
    //    public bool FTL_or_LTL; ///If the ticket is FTL or LTL
    //    public bool Is_Reefer; ///If the ticket is reefer
    //    public int Size_In_Palette; ///Size of the ticket
    //    public bool Is_Complete; ///If the ticket is marked as complete
    //    public int Days_Passed; ///The days passed for this ticket.
    //}


    //public struct Trip_Ticket_Line
    //{
    //    public Trip_Ticket Ticket; ///The ticket for this order
    //    public RouteData Route;
    //    public DateTime Date_Added;
    //}

    //public struct Location
    //{
    //    public string CityName;
    //}

    //public struct Truck
    //{
    //    public int TruckID;
    //    public int CurrentCityID;
    //    //public Carrier carrier;
    //    public bool Is_Reefer;
    //    public int Free_Space;
    //    public bool Waiting_or_Transit;
    //    public bool FTL_or_LTL;
    //};

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		PlannerClass
    *   \brief		This class runs the Planner functionality
    *   \details	This class will interface with the XAML main window to preform the Planner tasks.
    *               This class will use many other classes to complete its task. At this point, much of the actual PlannerClass Functionality is obfuscated.
    *               Most of support classes have been complete.
    *               
    *   \see        MappingClass.cs TimePass.cs 
    *   
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class PlannerClass
    {
        class Planner
        {

            // Receive_Customer_Order_From_Buyer METHOD HEADER COMMENT -------------------------------------------------------------------------------
            /**
            *	\fn			int Receive_Customer_Order_From_Buyer()
            *	\brief		This method will read the contracts that the buy has set aside.
            *	\details	The method will read from the data base. The buy will have flagged orders and nominated carries for each. 
            *	            First the method will determine which carrier is the best option. It will then use the Mapping Class to determine the trip the trucks will take.
            *	            All of this data will be saved into the data base after.
            *	         
            *	\param[in]	null
            *	\param[out]	null
            *	\exception	null
            *	\see		MappingClass
            *	\return		None
            *
            * ---------------------------------------------------------------------------------------------------- */
            public int Receive_Customer_Order_From_Buyer()
            {
                PlannerSQL sql = new PlannerSQL();

                sql.Open();
                List<Noninated_Contract> noninated_Contracts = new List<Noninated_Contract>();
                noninated_Contracts = sql.LoadNominatedContracts();

                List<CompleteNomination> completeNominations = new List<CompleteNomination>();

                completeNominations = sql.AddCarriersToNominatedContracts(noninated_Contracts);


                sql.Close();

                MappingClass map = new MappingClass();

                List<RouteData> TripRoutes = new List<RouteData>();



                Contract NewContract = null; //from the database
                //List<Carrier> NominatedCarriers = new List<Carrier>(); //from the database

                    
                //List<Trip_Ticket> tickets = Select_Carriers_From_Nominations(NominatedCarriers, NewContract);

                //List<Trip_Ticket_Line> trip_Ticket_Lines = new List<Trip_Ticket_Line>();

                //foreach (Trip_Ticket trip_Ticket in tickets)
                //{
                //    Trip_Ticket_Line line = new Trip_Ticket_Line();
                //    line.Date_Added = DateTime.Now;
                //    line.order = Incoming_Order;
                //    line.ticket = trip_Ticket;

                //    trip_Ticket_Lines.Add(line);
                //}



                //so now all the new tickets that were made are in a list "tickets"
                //and the data that connects them to a customer order is stored in "trip_Ticket_Lines"

                return 0;
            }

          
            //private List<Trip_Ticket> Select_Carriers_From_Nominations(List<Carrier> list_of_carriers, Contract InContract)
            //{
            //    ////check if the each carrier has enough trucks in the origin city

            //    //List<Truck> carrier_trucks = new List<Truck>(); 

            //    //List<Trip_Ticket_Line> ticket_line_list = new List<Trip_Ticket_Line>();

            //    ////This will be a list of lists that will hold a list of all the trucks for each carrier
            //    //List<List<Truck>> trucks_all_carriers = new List<List<Truck>>();

            //    ////get a list of trucks that are from the nominated carriers in the orgin city
            //    //foreach (Carrier carrier in list_of_carriers)
            //    //{
            //    //    allTrucks = (List<Truck>)SQLMagic("Gimmie all the trucks for carrier \"carrier\" in the city \"order.Origin_City\"");

            //    //    List<Truck> tempList = new List<Truck>();

            //    //    foreach (Truck truck in allTrucks)
            //    //    {
            //    //        //check if the truck the correct type of truck
            //    //        if (order.Is_Reefer == truck.Is_Reefer && order.Origin_City.CityName == truck.Current_Location.CityName)
            //    //        {
            //    //            tempList.Add(truck);
            //    //        }
            //    //    }

            //    //    trucks_all_carriers.Add(tempList);
            //    //}

            //    ////will hold the tickets that will be needed to make the order
            //    //List<Trip_Ticket> tickets = new List<Trip_Ticket>();

            //    //const int MaxTruckPalettes = 26;

            //    ////determine all the tickets that have to be made
            //    //if (order.Size_in_Palettes == MaxTruckPalettes)
            //    //{
            //    //    //we need one ticket that is ftl
            //    //    Trip_Ticket ticket = new Trip_Ticket();
            //    //    ticket.FTL_or_LTL = true; //lets say for now true means FTL
            //    //    ticket.Size_In_Palette = MaxTruckPalettes;
            //    //    ticket.Is_Reefer = order.Is_Reefer;

            //    //    tickets.Add(ticket);
            //    //}
            //    //else if (order.Size_in_Palettes < MaxTruckPalettes)
            //    //{
            //    //    //we need one ticket that is LTL
            //    //    Trip_Ticket ticket = new Trip_Ticket();
            //    //    ticket.FTL_or_LTL = false; //lets say for now true means FTL
            //    //    ticket.Size_In_Palette = order.Size_in_Palettes;
            //    //    ticket.Is_Reefer = order.Is_Reefer;

            //    //    tickets.Add(ticket);
            //    //}
            //    //else if (order.Size_in_Palettes > MaxTruckPalettes)
            //    //{
            //    //    int localPalettes = order.Size_in_Palettes;
            //    //    Trip_Ticket ticket;

            //    //    do
            //    //    {
            //    //        ticket = new Trip_Ticket();
            //    //        ticket.FTL_or_LTL = true; //lets say for now true means FTL
            //    //        ticket.Is_Reefer = order.Is_Reefer;
            //    //        ticket.Size_In_Palette = MaxTruckPalettes;

            //    //        tickets.Add(ticket);

            //    //        localPalettes -= MaxTruckPalettes;
            //    //    } while (localPalettes > MaxTruckPalettes);

            //    //    if (localPalettes > 0)
            //    //    {
            //    //        //we need one ticket that is ltl
            //    //        ticket = new Trip_Ticket();
            //    //        ticket.FTL_or_LTL = false; //lets say for now true means FTL
            //    //        ticket.Size_In_Palette = order.Size_in_Palettes;
            //    //        ticket.Is_Reefer = order.Is_Reefer;

            //    //        tickets.Add(ticket);
            //    //    }
            //    //}

            //    ////so now, the list "tickets" will contain all the tickets that this customer order needs

            //    ////now we have to match all the tickets with trucks.

            //    ////remove the carriers that dont have enough trucks
            //    //foreach (List<Truck> trucks in trucks_all_carriers)
            //    //{
            //    //    if (trucks.Count < tickets.Count)
            //    //    {
            //    //        trucks_all_carriers.Remove(trucks);
            //    //    }
            //    //}

            //    //if (trucks_all_carriers.Count == 0)
            //    //{
            //    //    //there are no carriers that have enough trucks in the origin city.
            //    //}

            //    //List<Truck> selected_carriers = new List<Truck>();

            //    //Carrier selectedCarrier;

            //    ////choose the lowest rate that will 
            //    //while (trucks_all_carriers.Count != 1)
            //    //{


            //    //    Carrier tempCarrier;

            //    //    float tempRate = 0;

            //    //    your working here, you need to make it so that it selects that carrier that has the lowest rate.


            //    //        foreach (List<Truck> trucks in trucks_all_carriers)
            //    //    {
            //    //        tempCarrier = trucks[0].carrier;

            //    //        if (trucks.)
            //    //        }

            //    //}







            //    //determine how many trucks we will need.


            //    //if(order.Size_in_Palettes == MaxTruckPalettes)
            //    //{
            //    //    //we need one Full Truck
            //    //    Truck tempTruck = new Truck();

            //    //    tempTruck.Is_Reefer = order.Is_Reefer;
            //    //    tempTruck.FTL_or_LTL = true; //lets say for now true means FTL
            //    //    tempTruck.

            //    //}

            //    //if (order.Size_in_Palettes > MaxTruckPalettes)
            //    //{

            //    //}










            //    //check which carriers have enough trucks in the city to fulfill order size.

            //    //create a new ticket based on the number of pallets that are needed.



            //    ////for each ticket needed
            //    ////{
            //    //Trip_Ticket trip_Ticket = new Trip_Ticket();

            //    //trip_Ticket.Is_Reefer = order.Is_Reefer;
            //    //trip_Ticket.TicketID = 0; //create a way that we will make new ids
            //    //trip_Ticket.TruckID = new Truck(); //claim on of the trucks from the city
            //    //trip_Ticket.FTL_or_LTL = false; // determine based of size of pallets

            //    //tickets.Add(trip_Ticket);
            //    ////}


            //    //return tickets;

            //    return null;
            //}


        }
    }
}
