----------------------------------------------------------------
-- 				TRANSPORT MANAGEMENT UPDATE SYSTEM			  --
----------------------------------------------------------------


-- Create schema 8000Cig

DROP DATABASE IF EXISTS 8000Cig;

CREATE DATABASE IF NOT EXISTS 8000Cig;

USE 8000Cig;

----------------------------------------------------------------
-- 			  CREATE USERS AND ADD PRIVILLAGES HERE			  --
----------------------------------------------------------------


-- Truck Table
-- Primary Key: TruckID
-- Foreign Key(s): Current_Location, Carrier_Name

DROP TABLE IF EXISTS Truck;
CREATE TABLE Truck (
  TruckID VARCHAR(100),
  Current_Location VARCHAR(100),
  Carrier_Name VARCHAR(100),
  Is_Reefer BOOL,
  PRIMARY KEY(TruckID),
  FOREIGN KEY(Current_Location) REFERENCES Location(CityID),
  FOREIGN KEY(Carrier_Name) REFERENCES Carrier(Carrier_Name)
) 

INSERT INTO Truck (TruckID,Current_Location,Carrier_Name,Is_Reefer) VALUES 
 (1, 57674747, 85687687, 89678967, 98796789),
 (2, Windsor, Hamilton, Oshawa, Belleville, Ottawa),
 (3, Schooner, Tillman Transport, Planet Express, We Haul),
 (4, TRUE, FALSE, FALSE, TRUE);


-- Carrier Table
-- Primary Key: Carrier_Name

DROP TABLE IF EXISTS Carrier;
CREATE TABLE Carrier (
  Carrier_Name VARCHAR(100),
  FTL_Rate DOUBLE(5, 4),
  LTL_Rate DOUBLE(5, 4),
  Reefer_Charge DOUBLE(5, 4),
  FTL_Availability INT,
  LTL_Availability INT,
  PRIMARY KEY(Carrier_Name)
)  

INSERT INTO Carrier (Carrier_Name,FTL_Rate,LTL_Rate, Reefer_Charge, FTL_Availability, LTL_Availability) VALUES 
 (1, Schooner, Tillman Transport, Planet Express, We Haul),
 (2, 5.21, 5.21, 5.2, 5.05),
 (3, 0.3621, 0, 0.3621, 0.3012),
 (4, 0.08, 0.07, 0.065, 0.09),
 (5, 50, 18, 11, 11),
 (6, 640, 640, 45, 35);


-- CarrierDepot
-- Primary Key: Carrier_Name
-- Foreign Key: Depot_CityID

DROP TABLE IF EXISTS CarrierDepot;
CREATE TABLE CarrierDepot (
  Carrier_Name VARCHAR(100),
  Depot_CityID VARCHAR(100),
  PRIMARY KEY(Carrier_Name)
) 

INSERT INTO CarrierDepot (Carrier_Name,FTL_Rate,LTL_Rate, Reefer_Charge, FTL_Availability, LTL_Availability) VALUES 
 (1, Schooner, Tillman Transport, Planet Express, We Haul),
 (2, 634645, 64563, 63456, 56346);


-- Location Table
-- Primary Key: Carrier_Name
-- Foreign Key: Depot_CityID

DROP TABLE IF EXISTS Location;
CREATE TABLE Location (
  CityID VARCHAR(100),
  CityName VARCHAR(100),
  PRIMARY KEY(CityID)
) 

INSERT INTO Location (CityID,CityName) VALUES 
 (1, 634645, 64563, 63456, 56346),
 (2, Windsor, Oshawa, Belleville, Kingston);


-- TripTicket Table
-- Primary Key: TicketID
-- Foreign Key: TruckID 

DROP TABLE IF EXISTS TripTicket;
CREATE TABLE TripTicket (
  TicketID VARCHAR(100),
  TruckID VARCHAR(100),
  FTL_or_LTL VARCHAR(10),
  Is_Reefer BOOL,
  Size_In_Palets INT,
  Days_Passed INT,
  Is_Complete BOOL,
  PRIMARY KEY(TicketID),
  FOREIGN KEY(TruckID) REFERENCES Truck(TruckID)
) 

INSERT INTO TripTicket (TicketID, TruckID, FTL_or_LTL, Is_Reefer, Size_In_Palets, Days_Passed, Is_Complete) VALUES 
 (1, 4563456456, 65346346, 546346, 564564564567),
 (2, 765567567, 67567221, 123423656, 02918347),
 (3, FTL, LTL, LTL, FTL),
 (4, FALSE, FALSE, TRUE, FALSE),
 (5, 34, 56, 11, 21),
 (6, 23, 10, 45, 35),
 (7, TRUE, TRUE, FALSE, TRUE),



-- TripTicketLine Table
-- Primary Key: TicketID
-- Foreign Key:  

DROP TABLE IF EXISTS TripTicketLine;
CREATE TABLE TripTicketLine (
  TicketID VARCHAR(100),
  Customer_OrderID VARCHAR(100),
  Date_Added VARCHAR(100),
  PRIMARY KEY(TicketID),
  FOREIGN KEY(Customer_OrderID) REFERENCES CustomerOrder(Customer_OrderID)
) 

INSERT INTO TripTicketLine (TicketID, Customer_OrderID, Date_Added) VALUES 
 (1, 4563456456, 65346346, 546346, 564564564567),
 (2, 765567567, 67567221, 123423656, 02918347),
 (3, 08.14.18, 05.21.19, 02.13.19, 05.17.18);


-- CustomerOrder Table
-- Primary Key: Customer_OrderID
-- Foreign Key: Client_Name 

DROP TABLE IF EXISTS CustomerOrder;
CREATE TABLE CustomerOrder (
  Customer_OrderID VARCHAR(100),
  Client_Name VARCHAR(100),
  Job_Type VARCHAR(100),
  Quantity INT,
  Origin VARCHAR(100),
  Destination VARCHAR(100),
  Van_Type BOOL,
  PRIMARY KEY(Customer_OrderID)
) 

INSERT INTO CustomerOrder (Customer_OrderID, Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type) VALUES 
 (1, 4563456456, 65346346, 546346, 564564564567),
 (2, John Smith, Jane Smith, Dan Guess, Katie Dinner),
 (3, FTL, LTL, LTL, FTL),
 (4, 34, 16, 95, 34),
 (5, London, Hamilton, Toronto, Oshawa),
 (6, Kingston, Ottawa, Kingston, Kingston),
 (7, TRUE, TRUE, FALSE, TRUE);


-- Invoice Table
-- Primary Key: Invoice_ID
-- Foreign Key: Customer_OrderID 

DROP TABLE IF EXISTS Invoice;
CREATE TABLE Invoice (
  Invoice_ID VARCHAR(100),
  Customer_OrderID VARCHAR(100),
  Amount DOUBLE(10, 4),
  PRIMARY KEY(Invoice_ID),
  FOREIGN KEY(Customer_OrderID) REFERENCES CustomerOrder(Customer_OrderID)
) 

INSERT INTO Invoice (TicketID, Customer_OrderID, Date_Added) VALUES 
 (1, 4563456456, 65346346, 546346, 564564564567),
 (2, 765567567, 67567221, 123423656, 02918347),
 (3, 13,4343.54, 3,433.87, 12,567.43, 897.23);


-- CarrierLine Table
-- Primary Key: NewContractID
-- Foreign Key: Carrier_Name 

DROP TABLE IF EXISTS CarrierLine;
CREATE TABLE CarrierLine (
  CarrierLine_ID VARCHAR(100),
  Carrier_Name VARCHAR(100),
  PRIMARY KEY(NewContractID)
  FOREIGN KEY(Carrier_Name) REFERENCES Carrier(Carrier_Name)
) 

INSERT INTO CarrierLine (CarrierLine_ID, Carrier_Name) VALUES 
 (1, 856856865, 12292, 0054335, 0274896),
 (2, Schooner, Tillman Transport, Planet Express, We Haul);


-- BuyerToPlannerContacts Table
-- Primary Key: NewContractID
-- Foreign Key: Customer_OrderID 

DROP TABLE IF EXISTS BuyerToPlannerContacts;
CREATE TABLE BuyerToPlannerContacts (
  NewContactID VARCHAR(100),
  Customer_OrderID VARCHAR(100),
  PRIMARY KEY(NewContractID),
  FOREIGN KEY(CustomerOrder) REFERENCES CustomerOrder(Customer_OrderID)
) 

INSERT INTO BuyerToPlannerContacts (NewContactID, Customer_OrderID) VALUES 
 (1, 121299043, 342453245, 0120291, 4899877),
 (2, 096809586, 1290443, 122323, 90899);


-- TicketRouteLine Table
-- Primary Key: TicketID
-- Foreign Key:  

DROP TABLE IF EXISTS TicketRouteLine;
CREATE TABLE TicketRouteLine (
  TicketID VARCHAR(100),
  RouteID VARCHAR(100),
  Date VARCHAR(100),
  PRIMARY KEY(TicketID),
  FOREIGN KEY(RouteID) REFERENCES RouteData(RouteDataID)
) 

INSERT INTO TicketRouteLine (TicketID, RouteID, Date) VALUES 
 (1, 023982, 12901293, 09854545, 0980342142),
 (2, 448798121, 1023890432, 123424, 90239042),
 (3, 01.20.18, 05.16.19, 07.14.18, 08.19.19);


-- RouteData Table
-- Primary Key: RouteDataIDe
-- Foreign Key: CityA, CityB  

DROP TABLE IF EXISTS RouteData;
CREATE TABLE RouteData (
  RouteDataID VARCHAR(100),
  CityA VARCHAR(100),
  CityB VARCHAR(100),
  PickUpTime DOUBLE(2, 2),
  DropOffTime DOUBLE(2, 2),
  LtlTime DOUBLE(2, 2),
  DrivenTime DOUBLE(2, 2),
  PRIMARY KEY(RouteDataID),
  FOREIGN KEY(CityA) REFERENCES Location(CityID),
  FOREIGN KEY(CityB) REFERENCES Location(CityID)
) 

INSERT INTO RouteData (RouteDataID, CityA, CityB, PickUpTime, DropOffTime, LtlTime, DrivenTime) VALUES 
 (1, 5329057, 941498203, 98749081273, 8090546),
 (2, Windsor, Oshawa, Kingston, Hamilton),
 (3, Oshawa, London, Belleville, Kingston),
 (4, 12.56, 22.17, 07.43, 08.21),
 (5, 05.23, 19.08, 01.23, 15.83),
 (6, 03.24, 08.38, 04.32, 20.45),
 (7, 01.35, 13.41, 23.14, 12.09);