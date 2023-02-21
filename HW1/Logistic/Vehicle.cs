using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Logistic.ConsoleClient
{
    
    internal class Vehicle
    {
        private const double CONVERT_TO_POUNDS = 2.20462;
        protected int totalWeight;
        protected double totalVolume;
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }

        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }

        
        public Vehicle(int MaxCargoWeightKg, double MaxCargoVolum)
        {
            this.MaxCargoWeightKg = MaxCargoWeightKg;
            this.MaxCargoVolume = MaxCargoVolum;
            MaxCargoWeightPnd = MaxCargoWeightKg * CONVERT_TO_POUNDS;
            Cargos = new List<Cargo>();
         }
        public string GetCargoVolumeLeft()
        {
            double volumLeft = MaxCargoVolume - totalVolume;
            return $"Left to load by volume: {volumLeft} m3";
        }

        public string GetCargoWeightLeft(WeightUnit weightUnit)
        {      
            double weightLeft = MaxCargoWeightKg - totalWeight;
            double weightLeftPnd = MaxCargoWeightPnd - totalWeight * CONVERT_TO_POUNDS;
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
            return $"Info about {Type} with number: {Number} /// max load: weight {MaxCargoWeightKg} kg / {MaxCargoWeightPnd} lb,  volume {MaxCargoVolume} m3 //// Cargos count {Cargos.Count}pcs, cargos weight: {totalWeight}kg/{totalWeight * CONVERT_TO_POUNDS}lb, cargos volume: {totalVolume}m3";
        }

        public void LoadCargo(Cargo cargo)
        {

            Cargos.Add(cargo);
            totalWeight = 0;
            totalVolume = 0;

            foreach (var i in Cargos) {
                if (i.Weight > MaxCargoWeightKg)
                {
                    throw new Exception($"Cargo weight  {i.Code} is too much: {i.Weight} kg");
                }
                if (i.Volume > MaxCargoVolume)
                {
                    throw new Exception($"Cargo volume {i.Code} is too much {i.Volume} m3");
                }
                totalWeight += i.Weight;
                totalVolume += i.Volume;
                if (totalWeight > MaxCargoWeightKg)
                {
                    throw new Exception($"Vehile is overloaded: cargo {i.Code} //weight {i.Weight} kg");
                }
                if (totalVolume > MaxCargoVolume)
                {
                    throw new Exception($"Cargos don`t fit by volume: cargo {i.Code}//volume {i.Volume} m3");
                }              
            }
        }        
    }
    public enum VehicleType
    {
        Car,
        Ship,
        Plane,
        Train
    }

    public enum WeightUnit
    {
        Kilograms,
        Pounds
    }

}
