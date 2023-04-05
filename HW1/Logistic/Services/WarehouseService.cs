using AutoMapper;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Services
{
    internal class WarehouseService
    {
        private readonly InMemoryRepository<Warehouse> _warehouseRepository;
        //private int Id = 0;
        public WarehouseService(InMemoryRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
       /* public Warehouse DeepCopy(Warehouse warehouse)
        {
             return _warehouseRepository.DeepCopy(warehouse);
        }*/
        public void Create(Warehouse warehouse)
        {
            //warehouse.Id = ++Id;
            _warehouseRepository.Create(warehouse);
        }

        public Warehouse GetById(int warehouseId)
        {
            return _warehouseRepository.GetRecordById(warehouseId);
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _warehouseRepository.GetAll();
        }

        public void Delete(int warehouseId)
        {
           _warehouseRepository.Delete(warehouseId);
        }
        public void Update(Warehouse warehouse)
        {
            _warehouseRepository.Update(warehouse);
        }
        public void LoadCargo(Cargo cargo, int warehouseId)
        {
            var warehouse = GetById(warehouseId);
            if (warehouse != null)
            {
                warehouse.Cargos.Add(cargo);
            }
        }
        public void UnloadCargo(Guid cargoId, int warehouseId)
        {
            var warehouse = _warehouseRepository.GetRecordById(warehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException($"Vehicle with Id {warehouseId} does not exist.");
            }
          
            var cargoToRemove = warehouse.Cargos.FirstOrDefault(c => c.Id == cargoId);
            if (cargoToRemove == null)
            {
                throw new ArgumentException($"Cargo with Id {cargoId} does not exist in Vehicle with Id {warehouseId}.");
            }
            warehouse.Cargos.Remove(cargoToRemove);
            //return _warehouseRepository.Update(warehouse);

        }
    }
}
