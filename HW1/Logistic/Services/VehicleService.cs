using System;
using System.Collections.Generic;
using System.Linq;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;
using AutoMapper;
using Logistic.ConsoleClient.CONST;




namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService
    {
        private readonly InMemoryRepository<Vehicle> _vehicleRepository;
        //private readonly InMemoryRepository<Cargo> _cargoRepository;
        //private int Id = 0;
        public VehicleService(InMemoryRepository<Vehicle> vehiceleRepository)
        {
            _vehicleRepository = vehiceleRepository;
        }
       /* public Vehicle DeepCopy(Vehicle vehicle)
        { 
            return _vehicleRepository.DeepCopy(vehicle);
        }*/
        
        public void Create(Vehicle vehicle)
        {
           // vehicle.Id = ++Id;
            _vehicleRepository.Create(vehicle);
        }
  
        public Vehicle GetById(int vehicleId)
        {
            return _vehicleRepository.GetRecordById(vehicleId);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _vehicleRepository.GetAll();
        }

        public void Delete(int vehicleId)
        {
           _vehicleRepository.Delete(vehicleId);
        }

        public void Update (Vehicle vehicle)
        {
           _vehicleRepository.Update(vehicle);
        }

        public void LoadCargo(Cargo cargo, int vehicleId)
        {
            var vehicle = _vehicleRepository.GetRecordById(vehicleId);
            var totalLoadedWeight = vehicle.Cargos.Sum(x => x.Weight);
            var totalLoadedVolume = vehicle.Cargos.Sum(x => x.Volume);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with Id {vehicleId} does not exist.");
            }            
            if (totalLoadedWeight > vehicle.MaxCargoWeightKg)
            {
                throw new Exception($"Vehile is overloaded: cargo {cargo.Id} //weight {cargo.Weight} kg");
            }
            if (totalLoadedVolume > vehicle.MaxCargoVolume)
            {
                throw new Exception($"Cargos don`t fit by volume: cargo {cargo.Id}//volume {cargo.Volume} m3");
            }
            totalLoadedWeight += cargo.Weight;
            totalLoadedVolume += cargo.Volume;
            vehicle.Cargos.Add(cargo);
            //return _vehicleRepository.Update(vehicle);
        }
        public void UnloadCargo(Guid cargoId, int vehicleId)
        {
            var vehicle = _vehicleRepository.GetRecordById(vehicleId);
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
           // return _vehicleRepository.Update(vehicle);
        }

    }
}
