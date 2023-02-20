
using System;
using Logistic.ConsoleClient;


static void SuccessScenario()
{
    Vehicle car1 = new Vehicle(1500, 12);
    car1.Type = VehicleType.Car;
    car1.Number = "AE1050";

    Cargo cargo1 = new Cargo("item1",10, 1);
    Cargo cargo2 = new Cargo("item2",100, 0.5);
    Cargo cargo3 = new Cargo("item3",10, 0.3);
    Cargo cargo4 = new Cargo("item4",10, 0.2);
    Cargo cargo5 = new Cargo("item5",10, 3);

    List<Cargo> cargos1 = new List<Cargo>();
    cargos1.Add(cargo1);
    cargos1.Add(cargo2);
    cargos1.Add(cargo3);
    cargos1.Add(cargo4);
    cargos1.Add(cargo5);
    foreach (var cargo in cargos1)
    {
        car1.LoadCargo(cargo);
    }

    Console.WriteLine(car1.GetInformation());
}

SuccessScenario();


static void ExceptionScenario()
{
    Vehicle car1 = new Vehicle(1000, 9);
    car1.Type = VehicleType.Car;
    car1.Number = "AE1050";

    Cargo cargo1 = new Cargo("item1",100, 2.1);
    Cargo cargo2 = new Cargo("item2",200, 2.5);
    Cargo cargo3 = new Cargo("item3",300, 4.2);
    Cargo cargo4 = new Cargo("item4",200, 1.9);
    Cargo cargo5 = new Cargo("item5",300, 3);

    List<Cargo> cargos1 = new List<Cargo>();
    cargos1.Add(cargo1);
    cargos1.Add(cargo2);
    cargos1.Add(cargo3);
    cargos1.Add(cargo4);
    cargos1.Add(cargo5);
    foreach (var cargo in cargos1)
    {
        car1.LoadCargo(cargo);
    }

    Console.WriteLine("\n{car1.GetInformation()}");
}

ExceptionScenario();


Console.ReadLine();
