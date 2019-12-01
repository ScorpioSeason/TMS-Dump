CREATE SCHEMA `TheRealDB` ;

CREATE TABLE `TheRealDB`.`Truck` (
  `TruckID` INT NOT NULL,
  `Carrier_Location` VARCHAR(45) NULL,
  `Carrier_Name` VARCHAR(45) NULL,
  `Is_Reefer` TINYINT NULL,
  PRIMARY KEY (`TruckID`));

CREATE TABLE `TheRealDB`.`Carrier` (
  `Carrier_Name` VARCHAR(45) NOT NULL,
  `FTL_Rate` DOUBLE(5,4) NULL,
  `LTL_Rate` DOUBLE(5,4) NULL,
  `Reefer_Charge` DOUBLE(5,4) NULL,
  `FTL_Availibility` INT NULL,
  `LTL_Availibility` INT NULL,
  PRIMARY KEY (`Carrier_Name`));

CREATE TABLE `TheRealDB`.`CarrierDepot` (
  `Carrier_Name` VARCHAR(45) NOT NULL,
  `Depot_CityID` VARCHAR(45) NULL,
  PRIMARY KEY (`Carrier_Name`));

CREATE TABLE `TheRealDB`.`Location` (
  `CityID` VARCHAR(45) NOT NULL,
  `CityName` VARCHAR(45) NULL,
  PRIMARY KEY (`CityID`));

CREATE TABLE `TheRealDB`.`TripTicket` (
  `TicketID` INT NOT NULL,
  `TruckID` VARCHAR(45) NULL,
  `FTL_or_LTL` VARCHAR(45) NULL,
  `IS_Reefer` TINYINT NULL,
  `Size_In_Palettes` INT NULL,
  `Days_Passed` INT NULL,
  `Is_Complete` TINYINT NULL,
  PRIMARY KEY (`TicketID`));

CREATE TABLE `TheRealDB`.`TripTicketLine` (
  `TicketID` VARCHAR(45) NOT NULL,
  `Customer_OrderID` VARCHAR(45) NULL,
  `Date_Added` VARCHAR(45) NULL,
  PRIMARY KEY (`TicketID`));

CREATE TABLE `TheRealDB`.`CustomerOrder` (
  `Customer_OrderID` VARCHAR(45) NOT NULL,
  `Client_Name` VARCHAR(45) NULL,
  `Job_Type` VARCHAR(45) NULL,
  `Quantity` INT NULL,
  `Origin` VARCHAR(45) NULL,
  `Destination` VARCHAR(45) NULL,
  `Van_Type` TINYINT NULL,
  PRIMARY KEY (`Customer_OrderID`));

CREATE TABLE `TheRealDB`.`Invoice` (
  `Invoice_ID` VARCHAR(45) NOT NULL,
  `Customer_OrderID` VARCHAR(45) NULL,
  `Amount` DOUBLE(10,4) NULL,
  PRIMARY KEY (`Invoice_ID`));

CREATE TABLE `TheRealDB`.`CarrierLine` (
  `CarrierLine_ID` INT NOT NULL,
  `Carrier_Name` VARCHAR(45) NULL,
  PRIMARY KEY (`CarrierLine_ID`));


CREATE TABLE `TheRealDB`.`BuyerToPlannerContacts` (
  `NewContactID` VARCHAR(45) NOT NULL,
  `Customer_OrderID` VARCHAR(45) NULL,
  PRIMARY KEY (`NewContactID`));

CREATE TABLE `TheRealDB`.`TicketRouteLine` (
  `TicketID` VARCHAR(45) NOT NULL,
  `RouteID` VARCHAR(45) NULL,
  `TicketRouteDate` VARCHAR(45) NULL,
  PRIMARY KEY (`TicketID`));

CREATE TABLE `TheRealDB`.`RouteData` (
  `RouteDataID` VARCHAR(45) NOT NULL,
  `CityA` VARCHAR(45) NULL,
  `CityB` VARCHAR(45) NULL,
  `PickUpTime` DOUBLE(2,2) NULL,
  `DropOffTime` DOUBLE(2,2) NULL,
  `LtlTime` DOUBLE(2,2) NULL,
  `DrivenTime` DOUBLE(2,2) NULL,
  PRIMARY KEY (`RouteDataID`));

