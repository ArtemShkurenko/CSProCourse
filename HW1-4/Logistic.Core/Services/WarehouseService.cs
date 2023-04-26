
using Logistic.Models;
using System;
using System.Linq;





namespace Logistic.Core.Services
{
    public class WarehouseService
    {
        private readonly IRepository<Warehouse> _warehouseRepository;
        public WarehouseService(IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public void Create(Warehouse warehouse)
        {
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
        }
    }
}
