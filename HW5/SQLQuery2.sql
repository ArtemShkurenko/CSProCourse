INSERT INTO Vehicle (Id, Name,VehicleType, MaxCargoVolume, MaxCargoWeightKg) VALUES
(1,'AE1203' ,'Car', 15.0, 100),
(2,'FGFH123', 'ship', 100.5,1000),
(3,'A541', 'plane', 40.0,500),
(4,'DF576','train', 200, 1000);
INSERT INTO Warehouse(Id, Name) VALUES
(1,'Main'),
(2,'Service');
INSERT INTO Cargo(Id, Code, Volume, Weight, WarehouseId, VehicleId) VALUES
('f15769ca-71b8-4f88-9e11-47b1890e2db1', 'NP01', 1.0, 10, 1, 1),
('ba0f14b7-ee72-406e-bd3e-86c0f64eaf2c', 'NP02', 1.5, 50, 1, 2),
('04a0d641-7b25-44b8-a3c3-cce3d663dcf1', 'NP03', 25.0, 40, 1, 3),
('8e57f050-89e5-40f9-82c8-912d1022cb51', 'NP04', 50.8, 120, 2, 4),
('d4b4f373-0f2a-4b5e-9a20-17d5415ec5c5', 'NP05', 35.2, 23, 1, 1),
('b5e8d3f3-96d3-4320-8e8c-82a5b5f91335', 'NP06', 12.1, 10, 2, 1);
INSERT INTO Invoice(Id, RecipientAddress, SenderAddress, RecipientPhoneNumber, SenderPhoneNumber, CargoId) VALUES
('1b3e7ec3-6a05-4d70-a6c4-f6bca8306b64', 'KYIV', 'DNIPRO', NULL, NULL, 'f15769ca-71b8-4f88-9e11-47b1890e2db1'),
('ad14d4e4-4ca4-4d7a-9f5c-7ee53c9248b7', 'LVIV', 'DNIPRO', NULL, NULL, 'ba0f14b7-ee72-406e-bd3e-86c0f64eaf2c'),
('c0f1377b-1c8a-4b2f-9c43-9bfe6a18a481', 'LUTSK', 'DNIPRO', NULL, NULL, '04a0d641-7b25-44b8-a3c3-cce3d663dcf1'),
('f0650591-2c84-4c27-a43a-3b52e9f8c0e7', 'ODESSA', 'DNIPRO','097221133', '093112344', '8e57f050-89e5-40f9-82c8-912d1022cb51'),
('d58b1d2e-0bb8-4163-86a3-cc87c9c42f8e','VOLNOGORSK','DNIPRO','097445607','093112344','d4b4f373-0f2a-4b5e-9a20-17d5415ec5c5'),
('a601d256-bde5-49f8-a137-2aa39c08919e','BORISPOL','DNIPRO','093454564','093112344','b5e8d3f3-96d3-4320-8e8c-82a5b5f91335');
SELECT *
FROM Invoice;