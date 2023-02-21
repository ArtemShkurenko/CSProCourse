
using System;
using Logistic.ConsoleClient;


SuccessScenario();
ExceptionScenario();
Console.ReadLine();
static void SuccessScenario()
{
    Vehicle car1 = new Vehicle(1500, 12);
    car1.Type = VehicleType.Car;
    car1.Number = "AE1050";

    List<Cargo> cargos1 = new List<Cargo>()
    {
    new Cargo("item1",10, 1),
    new Cargo("item2", 100, 0.5),
    new Cargo("item3", 10, 0.3),
    new Cargo("item4", 10, 0.2),
    new Cargo("item5", 10, 3)
    };
    

    foreach (var cargo in cargos1)
    {
        car1.LoadCargo(cargo);
    }

    Console.WriteLine(car1.GetInformation());
}

static void ExceptionScenario()
{
    Vehicle car1 = new Vehicle(1000, 9);
    car1.Type = VehicleType.Car;
    car1.Number = "AE1050";

    List<Cargo> cargos1 = new List<Cargo>()
    {
    new Cargo("item1",100, 2.1),
    new Cargo("item2",200, 2.5),
    new Cargo("item3",300, 4.2),
    new Cargo("item4",200, 1.9),
    new Cargo("item5",300, 3)
    };

foreach (var cargo in cargos1)
    {
        car1.LoadCargo(cargo);
    }

    Console.WriteLine("\n{car1.GetInformation()}");
}



