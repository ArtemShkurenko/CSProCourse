using Logistic.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Logistics.Wpf
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VehicleType Type { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoVolume { get; set; }
        public IEnumerable<VehicleType> AllVehicleTypes { get; set; }
        public VehicleViewModel()
        {
            AllVehicleTypes =  Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>(); 
        }
        public List<Cargo> Cargos { get; set; }

    }
}