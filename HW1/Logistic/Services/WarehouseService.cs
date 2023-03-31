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
    internal class WarehouseService : InMemoryRepository<Warehouse, int>
    {
        private int Id = 0;
        protected Warehouse DeepCopy(Warehouse warehouse)
        {
             return base.DeepCopy(warehouse);
        }
        public void Create(Warehouse warehouse)
        {
            warehouse.Id = ++Id;
            base.Create(warehouse);
        }

        public Warehouse GetById(int warehouseId)
        {
            return base.GetRecordById(warehouseId);
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return base.GetAll();
        }

        public Warehouse? Delete(int warehouseId)
        {
            return base.Delete(warehouseId);
        }
        public void LoadCargo(Cargo cargo, int warehouseId)
        {
            var warehouse = GetById(warehouseId);
            if (warehouse != null)
            {
                warehouse.Cargos.Add(cargo);
            }
        }

        public Warehouse UnloadCargo(Guid cargoId, int warehouseId)
        {
            var warehouse = base.GetRecordById(warehouseId);
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
            return base.Update(warehouse);

        }
    }
}
