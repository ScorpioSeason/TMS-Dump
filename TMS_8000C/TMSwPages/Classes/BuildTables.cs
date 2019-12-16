using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public class BuildTables
    {
        public static string tableBuilder = "" +
            "-- CREATE the database \n" +
            "DROP DATABASE IF EXISTS duane_test; " +
            "CREATE DATABASE duane_test; " +
            "-- select the database \n" +
            "USE duane_test; " +

            "create table FC_LocalContract " +
            "( " +
            "FC_LocalContractID int not null, " +
            "Client_Name varchar(45), " +
            "Job_type int, " +
            "Quantity int, " +
            "Origin varchar(45), " +
            "Destination varchar(45), " +
            "Van_type tinyint, " +
            "Contract_Status int, " +
            "primary key(FC_LocalContractID) " +
            "); " +

            "create table FC_Carrier " +
            "( " +
            "FC_CarrierID int not null, " +
            "Carrier_Name varchar(45), " +
            "primary key(FC_CarrierID) " +
            "); " +

            //"select* from FC_Carrier; " +
            "create table FC_TripTicket " +
            "( " +
            "FC_TripTicketID int not null, " +
            "FC_CarrierID int not null, " +
            "CurrentLocation varchar(45), " +
            "Size_in_Palettes int, " +
            "Days_Passes int, " +
            "Is_Complete tinyint, " +
            "primary key(FC_TripTicketID), " +
            "FOREIGN KEY(FC_CarrierID) " +
            "REFERENCES FC_Carrier(FC_CarrierID)" +
            "); " +

            "create table FC_RouteSeg " +
            "( " +
            "FC_TripTicketID int not null, " +
            "CityA int, " +
            "CityB int, PickUpTime double, " +
            "DropOffTime double, " +
            "LtlTime double, " +
            "DrivenTime double, " +
            "KM int, " +
            "FOREIGN KEY (FC_TripTicketID) " +
            "REFERENCES FC_TripTicket(FC_TripTicketID)" +
            ");" +

            "create table FC_DepotCity " +
            "( " +
            "FC_CarrierID int not null, " +
            "CityName varchar(45), " +
            "FTL_Availibility int, " +
            "LTL_Availibility int, " +
            "FTL_Rate double, " +
            "LTL_Rate double, " +
            "Reefer_Charge double, " +
            "FOREIGN KEY(FC_CarrierID) " +
            "REFERENCES FC_Carrier(FC_CarrierID)" +
            "); " +

            "create table FC_TripTicketLine " +
            "( " +
            "FC_TripTicketID int not null, " +
            "FC_LocalContractID int not null, " +
            "PalletsOnTicket int, " +
            "FOREIGN KEY (FC_TripTicketID) " +
            "REFERENCES FC_TripTicket(FC_TripTicketID), " +
            "FOREIGN KEY (FC_LocalContractID) " +
            "REFERENCES FC_LocalContract(FC_LocalContractID)" +
            "); " +

            "create table FC_Invoice " +
            "( " +
            "FC_InvoiceID int not null, " +
            "TotalCost double, " +
            "primary key(FC_InvoiceID) " +
            "); " +

            "create table FC_InvoiceContractLine " +
            "( " +
            "FC_LocalContractID int not null, " +
            "FC_InvoiceID int not null, " +
            "FOREIGN KEY (FC_LocalContractID) " +
            "REFERENCES FC_LocalContract(FC_LocalContractID), " +
            "FOREIGN KEY (FC_InvoiceID) " +
            "REFERENCES FC_Invoice(FC_InvoiceID)" +
            "); " +

            "create table FC_BuyerToPlannerContract " +
            "( " +
            "FC_BuyerToPlannerContractID int not null, " +
            "FC_LocalContractID int not null, " +
            "primary key(FC_BuyerToPlannerContractID), " +
            "FOREIGN KEY (FC_LocalContractID) " +
            "REFERENCES FC_LocalContract(FC_LocalContractID) " +
            "); " +

            "create table FC_CarrierNom " +
            "( " +
            "FC_BuyerToPlannerContractID int not null, " +
            "FC_CarrierID int not null, " +
            "FOREIGN KEY (FC_BuyerToPlannerContractID) " +
            "REFERENCES FC_BuyerToPlannerContract(FC_BuyerToPlannerContractID), " +
            "FOREIGN KEY (FC_CarrierID) " +
            "REFERENCES FC_Carrier(FC_CarrierID)" +
            ");" ;
    }
}
