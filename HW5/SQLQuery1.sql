IF EXISTS 
   (
   SELECT name FROM master.dbo.sysdatabases 
   WHERE name = N'Logistics'
   )
BEGIN    
   PRINT 'Database already exist.'
END
ELSE
BEGIN
   CREATE DATABASE Logistics;
   PRINT 'Database created successfully.'
END

USE Logistics;
GO

CREATE TABLE Warehouse (
    Id INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL);
 GO
 
 CREATE TABLE Vehicle (
      Id INT PRIMARY KEY,
      Name VARCHAR(50) NOT NULL,
      VehicleType VARCHAR(50) DEFAULT 'Car' NOT NULL,
      MaxCargoWeightKg INT NOT NULL,
      MaxCargoVolume DECIMAL (10, 2) NOT NULL);
GO
    
CREATE TABLE Cargo (
     Id UNIQUEIDENTIFIER PRIMARY KEY,
     Code VARCHAR(50) NOT NULL,
     Volume DECIMAL(10, 2) NOT NULL,
     Weight INT NOT NULL,
     WarehouseId INT NULL DEFAULT NULL,
     VehicleId INT NULL DEFAULT NULL,
     FOREIGN KEY (WarehouseId) REFERENCES Warehouse(Id),
     FOREIGN KEY (VehicleId) REFERENCES Vehicle(Id)
     );
GO

CREATE TABLE Invoice (
     Id UNIQUEIDENTIFIER PRIMARY KEY,
     RecipientAddress VARCHAR(50) NOT NULL,
     SenderAddress VARCHAR(50) NOT NULL,
     RecipientPhoneNumber VARCHAR(50),
     SenderPhoneNumber VARCHAR(50),
     CargoId UNIQUEIDENTIFIER,
     FOREIGN KEY (CargoId) REFERENCES Cargo(Id)
     );
GO






