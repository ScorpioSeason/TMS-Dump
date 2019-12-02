using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Transport_Management_System_WPF
{
    public class PlannerSQL
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public PlannerSQL()
        {
            server = "35.193.37.75";
            database = "duane_test";
            uid = "test";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public bool InsertIntoTruck(List<Truck> inList)
        {
            bool worked = true;

            try
            {
                foreach (Truck x in inList)
                {
                    string query = "insert into Truck(TruckID, Current_Location, CarrierID, Is_Reefer) value(" +
                         x.TruckID.ToString() + "," +
                         "\"" + x.Current_Location + "\"," +
                         x.CarrierID.ToString() + "," +
                         x.Is_Reefer.ToString() + ");";

                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                worked = false;
            }

            return worked;
        }

        public List<Truck> ReadFromTrucks()
        {
            string query = "select * from trucks;";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();


            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["TruckID"] + "");
                list[1].Add(dataReader["Current_Location"] + "");
                list[2].Add(dataReader["CarrierID"] + "");
                list[3].Add(dataReader["Is_Reefer"] + "");
            }

            //close Data Reader
            dataReader.Close();

            List<Truck> output = new List<Truck>(); 

            for (int i = 0; i < list[0].Count; i++)
            {
                Truck current = new Truck();

                current.TruckID = int.Parse(list[0][i]);
                current.Current_Location = list[1][i];
                current.CarrierID = int.Parse(list[2][i]);
                current.Is_Reefer = int.Parse(list[3][i]);

                output.Add(current);
            }

            return output;
        }



        public bool LoadTheCSV()
        {
            LoadCSV.Load(connection);
            return true;
        }

        public bool Open()
        {
            bool worked = true;

            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                worked = false;
            }

            return worked;
        }

        public bool Close()
        {
            bool worked = true;

            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                worked = false;
            }

            return worked;
        }

        public bool PushLocalContracts(List<Contract> contracts)
        {
            bool worked = true;

            try
            {
                foreach (Contract x in contracts)
                {
                    string query = "insert into CustomerOrder (Client_Name, Job_type, Quantity, Origin, Destination, Van_Type) values(" +
                         "\"" + x.client_Name + "\"," +
                         x.job_Type.ToString() + "," +
                         x.quantity.ToString() + "," +
                         "\"" + x.origin + "\"," +
                         "\"" + x.destination + "\"," +
                         x.van_Type.ToString() + ");";

                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    var r = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                worked = false;
            }

            return worked;
        }

        public int GetHighestCustomerOrderID()
        {
            List<Contract> contracts = new List<Contract>();

            //PlannerSQL plannerSQL = new PlannerSQL();
          
            contracts = PullDownAllCustomerOrders();

            int highestNumber = 0;

            foreach (Contract x in contracts)
            {
                if (x.ContractID > highestNumber)
                {
                    highestNumber = x.ContractID;
                }
            }

            return ++highestNumber;

        }

        public struct Contract_for_Planner
        {
            public int New_ContractID;
            public int Customer_orderID;
        }

        public void UpdateCityList(Carrier inCarrier)
        {
            inCarrier.CityList.Clear();

            string query = "SELECT * FROM CarrierDepot where CarrierID = " + inCarrier.CarrierID.ToString() + ";";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();


            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["CityName"] + "");
                list[1].Add(dataReader["CarrierID"] + "");
                list[2].Add(dataReader["FTL_Availibility"] + "");
                list[3].Add(dataReader["LTL_Availibility"] + "");
                list[4].Add(dataReader["FTL_Rate"] + "");
                list[5].Add(dataReader["LTL_Rate"] + "");
                list[6].Add(dataReader["Reefer_Charge"] + "");
            }

            //close Data Reader
            dataReader.Close();


            for (int i = 0; i < list[0].Count; i++)
            {
                CarrierDepot current = new CarrierDepot();

                current.CityName = list[0][i];
                current.CarrierID = int.Parse(list[1][i]);
                current.FTL_Availibility = int.Parse(list[2][i]);
                current.LTL_Availibility = int.Parse(list[3][i]);
                current.FTL_Rate = double.Parse(list[4][i]);
                current.LTL_Rate = double.Parse(list[5][i]);
                current.reeferCharge = double.Parse(list[6][i]);

                inCarrier.CityList.Add(current);
            }
        }



        public List<Noninated_Contract> LoadNominatedContracts()
        {

            List<Noninated_Contract> OutList = new List<Noninated_Contract>();
            string query = "SELECT * FROM BuyerToPlannerContacts;";

            //Create a list to store the result
            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();


            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["NewContactID"] + "");
                list[1].Add(dataReader["Customer_OrderID"] + "");
            }

            //close Data Reader
            dataReader.Close();

            List<Contract_for_Planner> inContracts = new List<Contract_for_Planner>();

            for (int i = 0; i < list[0].Count; i++)
            {
                Contract_for_Planner current = new Contract_for_Planner();

                current.New_ContractID = int.Parse(list[0][i]);
                current.Customer_orderID = int.Parse(list[1][i]);

                inContracts.Add(current);
            }

            //so all the contracts from the buyer are now in inContracts


            foreach (Contract_for_Planner x in inContracts)
            {
                Noninated_Contract temp = new Noninated_Contract();
                temp.TheContract = LoadSingleContract(x.Customer_orderID);
                temp.New_ContractID = x.New_ContractID;
                OutList.Add(temp);
            }

            return OutList;
        }

        public List<Contract> PullDownAllCustomerOrders()
        {

            string query = "select * from CustomerOrder;";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {

                list[0].Add(dataReader["Client_Name"] + "");
                list[1].Add(dataReader["Job_Type"] + "");
                list[2].Add(dataReader["Quantity"] + "");
                list[3].Add(dataReader["Origin"] + "");
                list[4].Add(dataReader["Destination"] + "");
                list[5].Add(dataReader["Van_Type"] + "");
                list[6].Add(dataReader["Customer_OrderID"] + "");
            }

            //close Data Reader
            dataReader.Close();

            List<Contract> readInContracts = new List<Contract>();

            for (int i = 0; i < list[0].Count; i++)
            {
                Contract current = new Contract();

                current.client_Name = list[0][i];
                current.job_Type = int.Parse(list[1][i]);
                current.quantity = int.Parse(list[2][i]);
                current.origin = list[3][i];
                current.destination = list[4][i];
                current.van_Type = int.Parse(list[5][i]);
                current.ContractID = int.Parse(list[6][i]);

                readInContracts.Add(current);
            }

            return readInContracts;
        }

        public Contract LoadSingleContract(int ContractID)
        {
            string query = "select * from CustomerOrder where Customer_OrderID = " + ContractID.ToString() + ";";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {

                list[0].Add(dataReader["Client_Name"] + "");
                list[1].Add(dataReader["Job_Type"] + "");
                list[2].Add(dataReader["Quantity"] + "");
                list[3].Add(dataReader["Origin"] + "");
                list[4].Add(dataReader["Destination"] + "");
                list[5].Add(dataReader["Van_Type"] + "");
                list[6].Add(dataReader["Customer_OrderID"] + "");
            }

            //close Data Reader
            dataReader.Close();

            List<Contract> readInContracts = new List<Contract>();

            for (int i = 0; i < list[0].Count; i++)
            {
                Contract current = new Contract();

                current.client_Name = list[0][i];
                current.job_Type = int.Parse(list[1][i]);
                current.quantity = int.Parse(list[2][i]);
                current.origin = list[3][i];
                current.destination = list[4][i];
                current.van_Type = int.Parse(list[5][i]);
                current.ContractID = int.Parse(list[6][i]);

                readInContracts.Add(current);
            }

            if (readInContracts.Count > 1)
            {
                //something went wrong
            }
            else
            {
                return readInContracts[0];
            }

            return null;
        }

        public Carrier LoadSingleCarrier(int CarrierID)
        {
            string query = "select * from Carrier where CarrierID = " + CarrierID.ToString() + ";";

            //Create a list to store the result
            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();


            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["CarrierID"] + "");
                list[1].Add(dataReader["Carrier_Name"] + "");

            }

            //close Data Reader
            dataReader.Close();

            List<Carrier> readInCarriers = new List<Carrier>();

            for (int i = 0; i < list[0].Count; i++)
            {
                Carrier current = new Carrier();

                current.CarrierID = int.Parse(list[0][i]);
                current.CarrierName = list[1][i];

                readInCarriers.Add(current);
            }

            if (readInCarriers.Count > 1)
            {
                //something went wrong
            }
            else
            {
                UpdateCityList(readInCarriers[0]);
                return readInCarriers[0];
            }



            return null;
        }


        public void AddContractForPlanner(Noninated_Contract inContract)
        {
            //string query = " insert into BuyerToPlannerContacts(NewContactID, Customer_OrderID) values(" +
            //             contractID + "," +
            //             "\"" + x.client_Name + "\"," +
            //             x.job_Type.ToString() + "," +
            //             x.quantity.ToString() + "," +
            //             "\"" + x.origin + "\"," +
            //             "\"" + x.destination + "\"," +
            //             x.van_Type.ToString() + ");";

            //create command and assign the query and connection from the constructor
            // MySqlCommand cmd = new MySqlCommand(query, connection);

            //Execute command
            //cmd.ExecuteNonQuery();
        }

        public List<CompleteNomination> AddCarriersToNominatedContracts(List<Noninated_Contract> inlist)
        {
            List<CompleteNomination> OutputData = new List<CompleteNomination>();

            foreach (Noninated_Contract x in inlist)
            {

                string query = "select * from CarrierLine where NewContractID = " + x.New_ContractID.ToString() + ";";

                //Create a list to store the result
                List<string>[] list = new List<string>[2];
                list[0] = new List<string>();
                list[1] = new List<string>();

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["NewContractID"] + "");
                    list[1].Add(dataReader["CarrierID"] + "");
                }

                //close Data Reader
                dataReader.Close();

                List<CarrierLine> readInData = new List<CarrierLine>();

                for (int i = 0; i < list[0].Count; i++)
                {
                    CarrierLine current = new CarrierLine();

                    current.New_ContractID = int.Parse(list[0][i]);
                    current.CarrierID = int.Parse(list[1][i]);

                    readInData.Add(current);
                }

                CompleteNomination currentNominaiton = new CompleteNomination();

                currentNominaiton.theContract = x.TheContract;
                currentNominaiton.New_ContractID = x.New_ContractID;

                foreach (CarrierLine y in readInData)
                {
                    currentNominaiton.ListOfCarriers.Add(LoadSingleCarrier(y.CarrierID));
                }

                OutputData.Add(currentNominaiton);
            }

            return OutputData;

        }
    }
}
