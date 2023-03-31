using System;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;

namespace Logistic.ConsoleClient.Reports
{
    internal class VehicleJsonFileRepository : JsonFileRepository<Vehicle,int>
    {
        internal VehicleJsonFileRepository(string filePath) : base(filePath)
        {

        }

    }
}
