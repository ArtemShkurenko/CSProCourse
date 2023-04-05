using System;
using Logistic.ConsoleClient.Services;
using Logistic.ConsoleClient.CONST;


namespace Logistic.ConsoleClient.Models
{

    public class Vehicle : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VehicleType Type { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }


        public Vehicle()
        {
            MaxCargoWeightPnd = MaxCargoWeightKg * Constants.CONVERT_TO_POUNDS;
           // Cargos = new List<Cargo>();
        }
        public override string ToString()
        {
            return $"Info about {Type} with ID: {Id} name: {Name}/// max load: weight {MaxCargoWeightKg} kg / {MaxCargoWeightPnd} lb,  volume {MaxCargoVolume} m3 ";
        }     
    }

}
