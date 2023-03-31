using System;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;

namespace Logistic.ConsoleClient.Reports
{
    internal class VehicleXmlFileRepository : XmlFileRepository<Vehicle,int>
    {
        internal VehicleXmlFileRepository(string filePath) : base(filePath)
        {

        }
        
    }
}
