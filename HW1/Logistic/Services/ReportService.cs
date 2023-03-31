using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Logistic.ConsoleClient.Reports;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;


namespace Logistic.ConsoleClient.Services
{
    internal class ReportService<TEntity>     
    { 
        private readonly VehicleJsonFileRepository vehicleJsonRepo;
        private readonly VehicleXmlFileRepository vehicleXmlRepo;
        private readonly WarehouseJsonFileRepository warehouseJsonRepo;
        private readonly WarehouseXmlFileRepository warehouseXmlRepo;
        internal string appDir = Directory.GetCurrentDirectory();
        internal string reporDir;
        internal ReportService()
            {
            reporDir = Path.Combine(appDir, "Reports");
            Directory.CreateDirectory(reporDir);
            }
        public void CreateReport(ReportType reportType, IEnumerable<TEntity> entity)
        {
          
            string formatReport = reportType == ReportType.Xml ? "xml" : "json";
            var filepath = Path.Combine(reporDir, $"{typeof(TEntity).Name}-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}.{formatReport}");
            if (reportType == ReportType.Json)
            {
                if (typeof(TEntity).Name == "Vehicle")
                {
                    var vehicleJsonRepo = new VehicleJsonFileRepository(filepath);
                    vehicleJsonRepo.SaveRecords((IEnumerable<IRecord<int>>)entity);
                    Console.WriteLine(filepath);
                }
                else
                {
                    var warehouseJsonRepo = new WarehouseJsonFileRepository(filepath);
                    warehouseJsonRepo.SaveRecords((IEnumerable<IRecord<int>>)entity);
                    Console.WriteLine(filepath);
                }
            }
            if (reportType == ReportType.Xml)
            {
                if (typeof(TEntity).Name == "Vehicle")
                {
                    var vehicleXmlRepo = new VehicleXmlFileRepository(filepath);
                    vehicleXmlRepo.SaveRecords((IEnumerable<IRecord<int>>)entity);
                    Console.WriteLine(filepath);
                }
                else
                {
                    var warehouseXmlRepo = new WarehouseXmlFileRepository(filepath);
                    warehouseXmlRepo.SaveRecords((IEnumerable<IRecord<int>>)entity);
                    Console.WriteLine(filepath);
                }
            }
        }
        internal IEnumerable<TEntity> LoadReport(string fileName)
        {
            var reportFilePath = Path.Combine(reporDir, fileName);
            if (string.IsNullOrEmpty(reportFilePath))
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(reportFilePath))
                throw new FileNotFoundException($"File not found: {fileName}");
            
            var extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".xml":
                    {
                        if (typeof(TEntity).Name == "Vehicle")
                        {
                           var vehicleXmlRepo = new VehicleXmlFileRepository(reportFilePath);
                           return (IEnumerable<TEntity>)vehicleXmlRepo.ReadRecords(fileName);
                        }
                        else
                        {
                            var warehouseXmlRepo = new WarehouseXmlFileRepository(reportFilePath);
                            return (IEnumerable<TEntity>)warehouseXmlRepo.ReadRecords(fileName);
                            break;
                        }
                        
                    }
                case ".json":
                    {
                        if (typeof(TEntity).Name == "Vehicle")
                        {
                            var vehicleJsonRepo = new VehicleJsonFileRepository(reportFilePath);
                            return (IEnumerable<TEntity>)vehicleJsonRepo.ReadRecords(fileName);                          
                        }
                        else
                        {
                            var warehouseJsonRepo = new WarehouseJsonFileRepository(reportFilePath);
                            return (IEnumerable<TEntity>)warehouseJsonRepo.ReadRecords(fileName);
                            break;
                        }
                    }
                default:
                    throw new ArgumentException($"Invalid report type");
            }
        }

    }


    public enum ReportType
    {
        Json,
        Xml
    }
}
