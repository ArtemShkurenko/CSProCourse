using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.DataBase;



namespace Logistic.ConsoleClient.Services
{
    internal class ReportService<TEntity>
    
    {
        private readonly JsonFileRepository<TEntity> _JsonRepo;
        private readonly XmlFileRepository<TEntity> _XmlRepo;
        internal readonly string _reporDir;
        internal string appDir = Directory.GetCurrentDirectory();

        internal ReportService()
       {
            _reporDir = Path.Combine(appDir, "Reports");
            Directory.CreateDirectory(_reporDir);
       }
        public void CreateReport(ReportType reportType, IEnumerable<TEntity> entity)
        {
          
            string formatReport = reportType == ReportType.Xml ? "xml" : "json";
            var filepath = Path.Combine(_reporDir, $"{typeof(TEntity).Name}-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}.{formatReport}");
            if (reportType == ReportType.Json)
            {
                var _JsonRepo = new JsonFileRepository<TEntity>(filepath);
                _JsonRepo.SaveRecords((IEnumerable<IRecord>)entity);
                Console.WriteLine(filepath);
            }
            if (reportType == ReportType.Xml)
            {
                var _XmlRepo = new XmlFileRepository<TEntity>(filepath);
                _XmlRepo.SaveRecords((IEnumerable<IRecord>)entity);
                Console.WriteLine(filepath);
            }
        }
        internal IEnumerable<TEntity> LoadReport(string fileName)
        {
            var reportFilePath = Path.Combine(_reporDir, fileName);
            if (string.IsNullOrEmpty(reportFilePath))
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(reportFilePath))
                throw new FileNotFoundException($"File not found: {fileName}");
            
            var extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".xml":
                    {
                        var _XmlRepo = new XmlFileRepository<TEntity>(reportFilePath);
                        return (IEnumerable<TEntity>)_XmlRepo.ReadRecords(fileName);
                        break;
                    }

                case ".json":
                    {
                        var _JsonRepo = new JsonFileRepository<TEntity>(reportFilePath);
                        return (IEnumerable<TEntity>)_JsonRepo.ReadRecords(fileName);
                        break;
                    }
                default:
                    throw new ArgumentException($"Invalid report type");
            }
        }
    }
}
