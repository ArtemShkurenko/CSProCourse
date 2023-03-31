using System;
using System.Collections.Generic;
using System.Linq;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;
using AutoMapper;
using Logistic.ConsoleClient.CONST;




namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService : InMemoryRepository<Vehicle, int>
    {
       
        private int Id = 0;

        protected Vehicle DeepCopy(Vehicle vehicle)
        { 
            return base.DeepCopy(vehicle);
        }
        
        public void Create(Vehicle vehicle)
        {
            vehicle.Id = ++Id;
            base.Create(vehicle);
        }
  
        public Vehicle GetById(int vehicleId)
        {
            return base.GetRecordById(vehicleId);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return base.GetAll();
        }

        public Vehicle? Delete(int vehicleId)
        {
            return base.Delete(vehicleId);
        }

        public Vehicle LoadCargo(Cargo cargo, int vehicleId)
        {
            var vehicle = base.GetRecordById(vehicleId); 

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with Id {vehicleId} does not exist.");
            }            
            if (vehicle.totalWeight + cargo.Weight > vehicle.MaxCargoWeightKg)
            {
                    throw new Exception($"Vehile is overloaded: cargo {cargo.Id} //weight {cargo.Weight} kg");
            }
             if (vehicle.totalVolume + cargo.Volume > vehicle.MaxCargoVolume)
                {
                    throw new Exception($"Cargos don`t fit by volume: cargo {cargo.Id}//volume {cargo.Volume} m3");
                }
              vehicle.totalWeight += cargo.Weight;
              vehicle.totalVolume += cargo.Volume;
              vehicle.Cargos.Add(cargo);
              return base.Update(vehicle);
        }
        public Vehicle UnloadCargo(Guid cargoId, int vehicleId)
        {
            var vehicle = base.GetRecordById(vehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with Id {vehicleId} does not exist.");
            }
            var cargoToRemove = vehicle.Cargos.FirstOrDefault(c => c.Id == cargoId);
            if (cargoToRemove == null)
            {
                throw new ArgumentException($"Cargo with Id {cargoId} does not exist in Vehicle with Id {vehicleId}.");
            }          
            vehicle.Cargos.Remove(cargoToRemove);
            return base.Update(vehicle);
        }

    }
}
