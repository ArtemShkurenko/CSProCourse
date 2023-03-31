
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


var vehicles = new VehicleService();
var warehouse = new WarehouseService();



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
                Console.WriteLine("Input type of vehicle: car/ship/plane/train");
                string input = Console.ReadLine();
                if (Enum.TryParse<VehicleType>(input, out VehicleType vehicleType))
                {
                    VehicleType Type = vehicleType;
                }
                else
                {
                    Console.WriteLine("Incorrect, add type car, please correct feature");
                }
                var vehicleToAdd = new Vehicle()
                {
                    MaxCargoWeightKg = weight,
                    MaxCargoVolume = volume,
                    Name = name,
                };
                vehicles.Create(vehicleToAdd);
                
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
                warehouse.Create(warehouseToAdd);
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
                    var allVehicles = vehicles.GetAll();
                    foreach (Vehicle vehicle in allVehicles)
                    {
                        Console.WriteLine($"\n{vehicle.GetInformation()}");
                        Console.WriteLine($"List cargos {vehicle.Name}");
                        foreach (var i in vehicle.Cargos)
                        {
                            Console.WriteLine($"cargoId: {i.Id}");
                        }
                    }
                    break;
                }
            case "warehouse":
                {
                    var allWarehouse = warehouse.GetAll();
                    foreach (Warehouse warehouse in allWarehouse)
                    {
                        Console.WriteLine($"\nName warehouse: {warehouse.Name}||| ID: {warehouse.Id}");
                        Console.WriteLine($"List cargos {warehouse.Name}");
                        foreach (var i in warehouse.Cargos)
                        {
                            Console.WriteLine($"cargoId: {i.Id}");
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
                vehicles.LoadCargo(cargo, vehicleID);
               
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
                warehouse.LoadCargo(cargo, wareHouseId);
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

    if (commandParts[1] == "vehicle")
    {
        if (commandParts[2] == "json")
        {
            var vehicleReportService = new ReportService<Vehicle>();
            var allVehicles = vehicles.GetAll();
            vehicleReportService.CreateReport(ReportType.Json, allVehicles);
        }
        else if (commandParts[2] == "xml")
        {
            var vehileReportService = new ReportService<Vehicle>();
            var allVehicles = vehicles.GetAll();
            vehileReportService.CreateReport(ReportType.Xml, allVehicles);
        }
        else
        {
            Console.WriteLine("Input correct command: create-report vehicle json OR create-report vehicle xml");
        }
    }
    else if (commandParts[1] == "warehouse")
    {
        if (commandParts[2] == "json")
        {
            var warehouseReportService = new ReportService<Warehouse>();
            var allVWareHouses = warehouse.GetAll();
            warehouseReportService.CreateReport(ReportType.Json, allVWareHouses);
        }
        else if (commandParts[2] == "xml")
        {
            var warehouseReportService = new ReportService<Warehouse>();
            var allVWareHouses = warehouse.GetAll();
            warehouseReportService.CreateReport(ReportType.Xml, allVWareHouses);
        }
        else
        {
            Console.WriteLine("Input correct command: create-report warehouse json OR create-report warehouse xml");
        }
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
                var vehicleReportService = new ReportService<Vehicle>();
                Console.WriteLine("Input file for reading:");
                string fileName = Console.ReadLine();
                var vehicles = (List<Vehicle>)vehicleReportService.LoadReport(fileName);
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"ID feature: {vehicle.Id} name:{vehicle.Type} ||| MaxCargoWeightKg{vehicle.MaxCargoWeightKg} ||| MaxCargoVolume{vehicle.MaxCargoVolume}");
                }
                break;
            }
        case "warehouse":
            {
                var warehouseReportService = new ReportService<Warehouse>();
                Console.WriteLine("Input file for reading:");
                string fileName = Console.ReadLine();
                var warehouses = (List<Warehouse>)warehouseReportService.LoadReport(fileName);
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
                    vehicles.UnloadCargo(cargoId, vehicleID);
                    break;
                }
            case "warehouse":
                {
                    Console.WriteLine("\nInput ID warehouse for unload:");
                    int warehoseID = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nInput ID cargo for unload:");
                    Guid cargoId = Guid.Parse(Console.ReadLine());
                    warehouse.UnloadCargo(cargoId, warehoseID);
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
public static class Commands
{
    public const string CREATE_COMMAND = "add";
    public const string GETALL_COMMAND = "get-all";
    public const string LOADCARGO_COMMAND = "load-cargo";
    public const string UNLOADCARGO_COMMAND = "unload-cargo";
    public const string CREATE_REPORT_COMMAND = "create-report";
    public const string LOAD_REPORT_COMMAND = "load-report";
}



