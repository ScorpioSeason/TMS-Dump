using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public static class LoadCSV
    {

        public static bool Load()
        {


            bool worked = true;

            try
            {
                string localResourcePath = "C://dctemp/carriers.csv";
                string ReadInData = System.IO.File.ReadAllText(localResourcePath);

                ReadInData = ReadInData.Replace("\r\n", ",");
                ReadInData = ReadInData.Replace(",,", ",");

                String[] SeperaterStrings = ReadInData.Split(',');

                List<FC_Carrier> ReadInCarriers = new List<FC_Carrier>();
                List<FC_DepotCity> InDeoptCities = new List<FC_DepotCity>();

                int index = 7;
                bool CarrierFound = true;


                index = 7;

                do
                {
                    int CurrentCarrierID = SQL.GetNextID("FC_Carrier");

                    FC_Carrier current = new FC_Carrier(CurrentCarrierID, SeperaterStrings[index]);
                    index++;

                    bool cityFound = true;

                    do
                    {
                        if (ToCityID(SeperaterStrings[index]) != -1)
                        {
                            FC_DepotCity tempDepot = new FC_DepotCity(CurrentCarrierID, SeperaterStrings[index], int.Parse(SeperaterStrings[index + 1]), int.Parse(SeperaterStrings[index + 2]), double.Parse(SeperaterStrings[index + 3]), double.Parse(SeperaterStrings[index + 4]), double.Parse(SeperaterStrings[index + 5]));
                            InDeoptCities.Add(tempDepot);

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

                    SQL.Insert(current);

                } while (CarrierFound);

                foreach (FC_DepotCity x in InDeoptCities)
                {
                    SQL.Insert(x);
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

        public static string ToCityName(int inputCity)
        {

            if (inputCity == 0)
            {
                return "WINDSOR";
            }
            else if (inputCity == 1)
            {
                return "LONDON";
            }
            else if (inputCity == 2)
            {
                return "HAMILTON";
            }
            else if (inputCity == 3)
            {
                return "TORONTO";
            }
            else if (inputCity == 4)
            {
                return "OSHAWA";
            }
            else if (inputCity == 5)
            {
                return "BELLEVILLE";
            }
            else if (inputCity == 6)
            {
                return "KINGSTON";
            }
            else if (inputCity == 7)
            {
                return "OTTAWA";
            }

            return "-1";
        }
    }
}
