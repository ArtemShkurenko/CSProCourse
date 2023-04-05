
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Logistic.ConsoleClient.CONST;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;
using Logistic.ConsoleClient.Services;


Console.WriteLine("LOGISTIC");
Console.WriteLine("\n\nDescription of the main commands: ");
Console.WriteLine("Create feature: add vehicle  OR   add warehouse");
Console.WriteLine("Get all features: get-all vehicle  OR   get-all warehouse");
Console.WriteLine("Load cargo in vehicle OR warehouse: load-cargo vehicle  OR   load-cargo warehouse");
Console.WriteLine("Unload cargo in vehicle OR warehouse: unload-cargo vehicle  OR   unload-cargo warehouse");
Console.WriteLine("Create report in file(json or xml): create-report vehicle json/create-report vehicle xml OR create-report warehouse json/create-report warehouse xml");
Console.WriteLine("Read report from file: load-report vehicle OR load report warehouse");


var vehicleService = new VehicleService(new InMemoryRepository<Vehicle>());
var warehouseService = new WarehouseService(new InMemoryRepository<Warehouse>());
var vehicleReportService = new ReportService<Vehicle>();
var warehouseReportService = new ReportService<Warehouse>();



while (true)
{
    var command = Console.ReadLine();
    var commandParts = command.Split(" ");
    try
    {

        switch (commandParts[0])
    {
        case Commands.CREATE_COMMAND:
            ExecudeAdd(commandParts);
        break;
        case Commands.GETALL_COMMAND:
            ExecudeGetAll(commandParts);
        break;
        case Commands.LOADCARGO_COMMAND:
            ExecudeLoadCargo(commandParts);
        break;
        case Commands.UNLOADCARGO_COMMAND:
            ExecudeUnloadCargo(commandParts);
        break;
        case Commands.CREATE_REPORT_COMMAND:
            ExecudeCreateReport(commandParts);
        break;
        case Commands.LOAD_REPORT_COMMAND:
            ExecudeLoadReportFilename(commandParts);
        break;
        default:
            Console.WriteLine("Unknown command....");
        break;
    }
       
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("Repeat your input,please.....");
    }
}

void ExecudeAdd(String[] commandParts)
{
    switch (commandParts[1])
    {
        case "vehicle":
            {
                Console.WriteLine("\nInput name:");
                string name = Console.ReadLine();
                Console.WriteLine("Input max load weight:");
                var weight = int.Parse(Console.ReadLine());
                Console.WriteLine("Input max load volume:");
                var volume = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine("Input type of vehicle: car/ship/plane/train");
                    string input = Console.ReadLine();
                    if (Enum.TryParse(input,true, out VehicleType vehicleType))
                    {
                       VehicleType Type = vehicleType;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input. Please enter either 'car', 'ship', 'plane', or 'train'.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                var vehicleToAdd = new Vehicle()
                {
                    MaxCargoWeightKg = weight,
                    MaxCargoVolume = volume,
                    Name = name,
                };
                vehicleService.Create(vehicleToAdd);
                
                break;
            }
        case "warehouse":
            {
                Console.WriteLine("\nInput name:");
                string name = Console.ReadLine();
                var warehouseToAdd = new Warehouse()
                {
                    Name = name,
                };
                warehouseService.Create(warehouseToAdd);
                break;
            }
        default:
            {
               Console.WriteLine("Incorrect input, please input: add vehicle or add warehouse");
               break; 
            }
    }
}
void ExecudeGetAll(string[] commandParts)
{
    try
    {
        switch (commandParts[1])
        {

            case "vehicle":
                {
                    var allVehicles = vehicleService.GetAll();
                    foreach (Vehicle vehicle in allVehicles)
                    {
                        Console.WriteLine($"\n{vehicle.ToString()}");
                        foreach (var cargo in vehicle.Cargos)
                        {
                            Console.WriteLine($"{cargo.ToString}");
                        }
                    }
                    break;
                }
            case "warehouse":
                {
                    var allWarehouse = warehouseService.GetAll();
                    foreach (Warehouse warehouse in allWarehouse)
                    {
                        Console.WriteLine($"\n{warehouse.ToString()}");
                        foreach (var cargo in warehouse.Cargos)
                        {
                            Console.WriteLine($"{cargo.ToString}");
                        }
                    }
                    break;
                }
            default:
                {
                    Console.WriteLine("Incorrect input, please input: get-all vehicle  OR   get-all warehouse");
                    break;
                }
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Repeat your input,please.....");
    } 
}

void ExecudeLoadCargo(string[] commandParts)
{
    switch (commandParts[1])
    {
        case "vehicle":
            {
                Console.WriteLine("\nInput ID vehicle for loading:");
                int vehicleID = int.Parse(Console.ReadLine());
                Console.WriteLine("\nInput cargo:");
                List<Cargo> cargos = new List<Cargo>();
                var cargo = new Cargo();
                Console.WriteLine("Input code cargo");
                cargo.Code = Console.ReadLine();
                Console.WriteLine("Input weight cargo");
                cargo.Weight = int.Parse(Console.ReadLine());
                Console.WriteLine("Input volume cargo");
                cargo.Volume = double.Parse(Console.ReadLine());
                vehicleService.LoadCargo(cargo, vehicleID);
               
                break;
            }
        case "warehouse":
            {
                Console.WriteLine("\nInput ID warehouse for loading:");
                int wareHouseId = int.Parse(Console.ReadLine());
                var cargo = new Cargo();
                Console.WriteLine("Input code cargo");
                cargo.Code = Console.ReadLine();
                Console.WriteLine("Input weight cargo");
                cargo.Weight = int.Parse(Console.ReadLine());
                Console.WriteLine("Input volume cargo");
                cargo.Volume = double.Parse(Console.ReadLine());
                warehouseService.LoadCargo(cargo, wareHouseId);
                break;
            }
        default:
            {
                Console.WriteLine("Incorrect input, please input: load-cargo vehicle  OR   load-cargo warehouse");
                break;
            }
    }

}
void ExecudeCreateReport(String[] commandParts)
{
    String example = commandParts[2];
    Enum.TryParse<ReportType>(example, out ReportType reportType);

    if (commandParts[1] == "vehicle")
    {
        var allVehicles = vehicleService.GetAll();
        vehicleReportService.CreateReport(reportType, allVehicles);
    }
    else if (commandParts[1] == "warehouse")
    { 
        var allVWareHouses = warehouseService.GetAll();
        warehouseReportService.CreateReport(reportType, allVWareHouses);
    }    
    else
    {
        Console.WriteLine("Incorrect input, please input: create-report vehicle json/create-report vehicle xml OR create-report warehouse json/create-report warehouse xml");
    }

}
void ExecudeLoadReportFilename(String[] commandParts)
{
    switch (commandParts[1])
    {
        case "vehicle":
            {              
                Console.WriteLine("Input file for reading:");
                string fileName = Console.ReadLine();
                var vehicles = vehicleReportService.LoadReport(fileName);
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"ID feature: {vehicle.Id} name:{vehicle.Type} ||| MaxCargoWeightKg{vehicle.MaxCargoWeightKg} ||| MaxCargoVolume{vehicle.MaxCargoVolume}");
                }
                break;
            }
        case "warehouse":
            {
                Console.WriteLine("Input file for reading:");
                string fileName = Console.ReadLine();
                var warehouses = warehouseReportService.LoadReport(fileName);
                foreach (var warehose in warehouses)
                {
                    Console.WriteLine($"ID feature: {warehose.Id} name:{warehose.Name} |||");
                }
                break;
            }
        default:
            {
                Console.WriteLine("Incorrect input, please input: load-report vehicle name.file OR   load-report warehouse name.file");
                break;
            }
    }
    
}
void ExecudeUnloadCargo(String[] commandParts)
{
    try
    {
        switch (commandParts[1])
        {
            case "vehicle":
                {
                    Console.WriteLine("\nInput ID vehicle for unload:");
                    int vehicleID = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nInput ID cargo for unload:");
                    Guid cargoId = Guid.Parse(Console.ReadLine());
                    vehicleService.UnloadCargo(cargoId, vehicleID);
                    break;
                }
            case "warehouse":
                {
                    Console.WriteLine("\nInput ID warehouse for unload:");
                    int warehoseID = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nInput ID cargo for unload:");
                    Guid cargoId = Guid.Parse(Console.ReadLine());
                    warehouseService.UnloadCargo(cargoId, warehoseID);
                    break;
                }
            default:
                {
                    Console.WriteLine("Incorrect input, please input: unload-cargo vehicle  OR   unload-cargo warehouse");
                    break;
                }
        }
    }
    
    catch (Exception)
    {
        Console.WriteLine("Repeat your input,please.....");
    }
}



