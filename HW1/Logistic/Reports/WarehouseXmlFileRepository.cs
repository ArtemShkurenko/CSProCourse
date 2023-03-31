using System;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;

namespace Logistic.ConsoleClient.Reports
{
    internal class WarehouseXmlFileRepository : XmlFileRepository<Warehouse, int>
    {
        internal WarehouseXmlFileRepository(string filePath) : base(filePath)
        {
        }

    }
}
