using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    public struct Customer_Order
    {
        public int Customer_OrderID;
        public Customer Customer_Name;
        public Location Origin_City;
        public Location Destination_City;
        public int Size_in_Palettes;
        public bool Is_Reefer;
    };

    public struct Customer
    {
        public string Customer_Name;
        public string AddressID;
        public int Telephone;
    }

    public struct Trip_Ticket
    {
        public int TicketID;
        public Truck TruckID;
        public bool FTL_or_LTL;
        public bool Is_Reefer;
        public int Size_In_Palette;
        public bool Is_Complete;
        public double Days_Passed;
    }

    public struct Trip_Ticket_Line
    {
        public Trip_Ticket ticket;
        public Customer_Order order;
        public DateTime Date_Added;
    }

    public struct Location
    {
        public string CityName;
    }


    public struct Truck
    {
        public int TruckID;
        public int CurrentCityID;
        public Carrier carrier;
        public bool Is_Reefer;
        public int Free_Space;
        public bool Waiting_or_Transit;
        public bool FTL_or_LTL;
    };


    class PlannerClass
    {
        //    class Planner
        //    {
        //        public object SQLMagic(string input)
        //        {
        //            return null;
        //        }

        //        public int Recive_Customer_Order_From_Buyer(Customer_Order Incoming_Order, List<Carrier> Carrier_Nominations)
        //        {
        //            List<Trip_Ticket> tickets = Select_Carriers_From_Nominations(Carrier_Nominations, Incoming_Order);

        //            List<Trip_Ticket_Line> trip_Ticket_Lines = new List<Trip_Ticket_Line>();

        //            foreach (Trip_Ticket trip_Ticket in tickets)
        //            {
        //                Trip_Ticket_Line line = new Trip_Ticket_Line();
        //                line.Date_Added = DateTime.Now;
        //                line.order = Incoming_Order;
        //                line.ticket = trip_Ticket;

        //                trip_Ticket_Lines.Add(line);
        //            }



        //            //so now all the new tickets that were made are in a list "tickets"
        //            //and the data that connects them to a customer order is stored in "trip_Ticket_Lines"

        //            return 0;
        //        }


        //        private List<Trip_Ticket> Select_Carriers_From_Nominations(List<Carrier> list_of_carriers, Customer_Order order)
        //        {
        //            List<Truck> allTrucks = null;
        //            List<Truck> carrier_trucks = new List<Truck>();
        //            List<Trip_Ticket_Line> ticket_line_list = new List<Trip_Ticket_Line>();

        //            //This will be a list of lists that will hold a list of all the trucks for each carrier
        //            List<List<Truck>> trucks_all_carriers = new List<List<Truck>>();

        //            //get a list of trucks that are from the nominated carriers in the orgin city
        //            foreach (Carrier carrier in list_of_carriers)
        //            {
        //                allTrucks = (List<Truck>)SQLMagic("Gimmie all the trucks for carrier \"carrier\" in the city \"order.Origin_City\"");

        //                List<Truck> tempList = new List<Truck>();

        //                foreach (Truck truck in allTrucks)
        //                {
        //                    //check if the truck the correct type of truck
        //                    if (order.Is_Reefer == truck.Is_Reefer && order.Origin_City.CityName == truck.Current_Location.CityName)
        //                    {
        //                        tempList.Add(truck);
        //                    }
        //                }

        //                trucks_all_carriers.Add(tempList);
        //            }

        //            //will hold the tickets that will be needed to make the order
        //            List<Trip_Ticket> tickets = new List<Trip_Ticket>();

        //            const int MaxTruckPalettes = 26;

        //            //determine all the tickets that have to be made
        //            if (order.Size_in_Palettes == MaxTruckPalettes)
        //            {
        //                //we need one ticket that is ftl
        //                Trip_Ticket ticket = new Trip_Ticket();
        //                ticket.FTL_or_LTL = true; //lets say for now true means FTL
        //                ticket.Size_In_Palette = MaxTruckPalettes;
        //                ticket.Is_Reefer = order.Is_Reefer;

        //                tickets.Add(ticket);
        //            }
        //            else if (order.Size_in_Palettes < MaxTruckPalettes)
        //            {
        //                //we need one ticket that is LTL
        //                Trip_Ticket ticket = new Trip_Ticket();
        //                ticket.FTL_or_LTL = false; //lets say for now true means FTL
        //                ticket.Size_In_Palette = order.Size_in_Palettes;
        //                ticket.Is_Reefer = order.Is_Reefer;

        //                tickets.Add(ticket);
        //            }
        //            else if (order.Size_in_Palettes > MaxTruckPalettes)
        //            {
        //                int localPalettes = order.Size_in_Palettes;
        //                Trip_Ticket ticket;

        //                do
        //                {
        //                    ticket = new Trip_Ticket();
        //                    ticket.FTL_or_LTL = true; //lets say for now true means FTL
        //                    ticket.Is_Reefer = order.Is_Reefer;
        //                    ticket.Size_In_Palette = MaxTruckPalettes;

        //                    tickets.Add(ticket);

        //                    localPalettes -= MaxTruckPalettes;
        //                } while (localPalettes > MaxTruckPalettes);

        //                if (localPalettes > 0)
        //                {
        //                    //we need one ticket that is ltl
        //                    ticket = new Trip_Ticket();
        //                    ticket.FTL_or_LTL = false; //lets say for now true means FTL
        //                    ticket.Size_In_Palette = order.Size_in_Palettes;
        //                    ticket.Is_Reefer = order.Is_Reefer;

        //                    tickets.Add(ticket);
        //                }
        //            }

        //            //so now, the list "tickets" will contain all the tickets that this customer order needs

        //            //now we have to match all the tickets with trucks.

        //            //remove the carriers that dont have enough trucks
        //            foreach (List<Truck> trucks in trucks_all_carriers)
        //            {
        //                if (trucks.Count < tickets.Count)
        //                {
        //                    trucks_all_carriers.Remove(trucks);
        //                }
        //            }

        //            if (trucks_all_carriers.Count == 0)
        //            {
        //                //there are no carriers that have enough trucks in the origin city.
        //            }

        //            List<Truck> selected_carriers = new List<Truck>();

        //            Carrier selectedCarrier;

        //            //choose the lowest rate that will 
        //            while (trucks_all_carriers.Count != 1)
        //            {


        //                Carrier tempCarrier;

        //                float tempRate = 0;

        //                your working here, you need to make it so that it selects that carrier that has the lowest rate.


        //                foreach (List<Truck> trucks in trucks_all_carriers)
        //                {
        //                    tempCarrier = trucks[0].carrier;

        //                    if (trucks.)
        //                }

        //            }







        //            //determine how many trucks we will need.


        //            //if(order.Size_in_Palettes == MaxTruckPalettes)
        //            //{
        //            //    //we need one Full Truck
        //            //    Truck tempTruck = new Truck();

        //            //    tempTruck.Is_Reefer = order.Is_Reefer;
        //            //    tempTruck.FTL_or_LTL = true; //lets say for now true means FTL
        //            //    tempTruck.

        //            //}

        //            if (order.Size_in_Palettes > MaxTruckPalettes)
        //            {

        //            }










        //            //check which carriers have enough trucks in the city to fulfill order size.

        //            //create a new ticket based on the number of pallets that are needed.



        //            //for each ticket needed
        //            //{
        //            Trip_Ticket trip_Ticket = new Trip_Ticket();

        //            trip_Ticket.Is_Reefer = order.Is_Reefer;
        //            trip_Ticket.TicketID = 0; //create a way that we will make new ids
        //            trip_Ticket.TruckID = new Truck(); //claim on of the trucks from the city
        //            trip_Ticket.FTL_or_LTL = false; // determine based of size of pallets

        //            tickets.Add(trip_Ticket);
        //            //}


        //            return tickets;
        //        }



        //        public int Simulate_Time(int days)
        //        {

        //            return 0;
        //        }

        //    }
    }
}
