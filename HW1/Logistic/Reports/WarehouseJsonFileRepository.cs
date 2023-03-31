using System;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;

namespace Logistic.ConsoleClient.Reports
{
    internal class WarehouseJsonFileRepository : JsonFileRepository<Warehouse,int>
    {
        internal WarehouseJsonFileRepository(string filePath) : base(filePath)
        {

        }
       
    }
}
