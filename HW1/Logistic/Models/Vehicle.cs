using System;
using Logistic.ConsoleClient.Services;
using Logistic.ConsoleClient.CONST;

namespace Logistic.ConsoleClient.Models
{

    public class Vehicle : IRecord<int>
    {
        
        protected internal int totalWeight;
        protected internal double totalVolume;
        public int Id { get; set; }
        public string Name { get; set; }
        public VehicleType Type { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }


        public Vehicle()
        {
            Cargos = new List<Cargo>();
        }
        public string GetCargoVolumeLeft()
        {
            double volumLeft = MaxCargoVolume - totalVolume;
            return $"Left to load by volume: {volumLeft} m3";
        }
        public string GetCargoWeightLeft(WeightUnit weightUnit)
        {
            MaxCargoWeightPnd = MaxCargoWeightKg * Constants.CONVERT_TO_POUNDS;
            double weightLeft = MaxCargoWeightKg - totalWeight;
            double weightLeftPnd = MaxCargoWeightPnd - totalWeight * Constants.CONVERT_TO_POUNDS;
            if (WeightUnit.Kilograms == weightUnit)
            {
                return $"Left to load by volume: {weightLeft} kg";
            }
            else
            {
                return $"Left to load by volume: {weightLeftPnd} lb";
            }
        }
  

        public string GetInformation()
        {
            return $"Info about {Type} with ID: {Id} name: {Name}/// max load: weight {MaxCargoWeightKg} kg / {MaxCargoWeightPnd} lb,  volume {MaxCargoVolume} m3 //// Cargos count {Cargos.Count}pcs, cargos weight: {totalWeight}kg/{totalWeight * Constants.CONVERT_TO_POUNDS}lb, cargos volume: {totalVolume}m3. Info about load: {GetCargoVolumeLeft()} ///"; /*{GetCargoWeightLeft(WeightUnit.Kilograms)} /// {GetCargoWeightLeft(WeightUnit.Pounds)}*/
        }

        
    }
    public enum VehicleType
    {
        car,
        ship,
        plane,
        train
    }

    public enum WeightUnit
    {
        Kilograms,
        Pounds
    }

}
