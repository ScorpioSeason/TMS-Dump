using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    class LoadCSV
    {

        public static MySqlConnection connection;

        public static bool Load(MySqlConnection inConnect)
        {
            connection = inConnect;

            bool worked = true;

            try
            {
                string localResourcePath = "C://dctemp/carriers.csv";
                string ReadInData = System.IO.File.ReadAllText(localResourcePath);

                ReadInData = ReadInData.Replace("\r\n", ",");
                ReadInData = ReadInData.Replace(",,", ",");

                String[] SeperaterStrings = ReadInData.Split(',');

                List<Carrier> ReadInCarriers = new List<Carrier>();

                int index = 7;
                bool CarrierFound = true;
                int CarrierID = 0;

                index = 7;

                do
                {
                    Carrier current = new Carrier(CarrierID, SeperaterStrings[index]);
                    index++;
                    CarrierID++;

                    bool cityFound = true;

                    do
                    {
                        if (ToCityID(SeperaterStrings[index]) != -1)
                        {
                            current.AddCity(SeperaterStrings[index], int.Parse(SeperaterStrings[index + 1]), int.Parse(SeperaterStrings[index + 2]), double.Parse(SeperaterStrings[index + 3]), double.Parse(SeperaterStrings[index + 4]), double.Parse(SeperaterStrings[index + 5]));


                            index += 6;
                        }
                        else
                        {
                            cityFound = false;
                        }

                    } while (cityFound);

                    ReadInCarriers.Add(current);

                    if (SeperaterStrings[index] == "")
                    {
                        CarrierFound = false;
                    }


                } while (CarrierFound);



                foreach (Carrier x in ReadInCarriers)
                {
                    string query = "insert into Carrier(CarrierID, Carrier_Name) value (" +
                         x.CarrierID.ToString() + "," +
                         "\"" + x.CarrierName + "\");";

                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    var r = cmd.ExecuteNonQuery();

                    foreach (CarrierDepot y in x.CityList)
                    {
                        query = "insert into CarrierDepot(CarrierID, CityName, FTL_Availibility, LTL_Availibility, FTL_Rate, LTL_Rate, Reefer_Charge) values(" +
                             x.CarrierID.ToString() + ",\"" +
                             y.CityName + "\"," +
                             y.FTL_Availibility.ToString() + "," +
                             y.LTL_Availibility.ToString() + "," +
                             y.FTL_Rate.ToString() + "," +
                             y.LTL_Rate.ToString() + "," +
                             y.reeferCharge.ToString() + ");";

                        cmd = new MySqlCommand(query, connection);
                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                worked = false;
            }

            return worked;

        }



        public static int ToCityID(string inputCity)
        {
            inputCity = inputCity.ToUpper();

            if (inputCity == "WINDSOR")
            {
                return 0;
            }
            else if (inputCity == "LONDON")
            {
                return 1;
            }
            else if (inputCity == "HAMILTON")
            {
                return 2;
            }
            else if (inputCity == "TORONTO")
            {
                return 3;
            }
            else if (inputCity == "OSHAWA")
            {
                return 4;
            }
            else if (inputCity == "BELLEVILLE")
            {
                return 5;
            }
            else if (inputCity == "KINGSTON")
            {
                return 6;
            }
            else if (inputCity == "OTTAWA")
            {
                return 7;
            }

            return -1;
        }

    }
}
