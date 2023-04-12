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
        private readonly IReportRepository<TEntity> _xmlRepository;
        private readonly IReportRepository<TEntity> _jsonRepository;

        internal readonly string _reporDir;
        internal string appDir = Directory.GetCurrentDirectory();

        internal ReportService(IReportRepository<TEntity> xmlRepository, IReportRepository<TEntity> jsonRepository)
       {
            _jsonRepository = jsonRepository;
            _xmlRepository = xmlRepository;
            _reporDir = Path.Combine(appDir, "Reports");
            Directory.CreateDirectory(_reporDir);
       }
        public void CreateReport(ReportType reportType, IEnumerable<TEntity> entity)
        {
          
            string formatReport = reportType == ReportType.Xml ? "xml" : "json";
            var filepath = Path.Combine(_reporDir, $"{typeof(TEntity).Name}-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}.{formatReport}");
            if (reportType == ReportType.Json)
            {
                var _jsonRepository = new JsonFileRepository<TEntity>();

                _jsonRepository.SaveRecords((IEnumerable<IRecord>)entity, filepath);
                Console.WriteLine(filepath);
            }
            if (reportType == ReportType.Xml)
            {
                var _xmlRepository = new XmlFileRepository<TEntity>();
                _xmlRepository.SaveRecords((IEnumerable<IRecord>)entity, filepath);
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
                        var _xmlRepository = new XmlFileRepository<TEntity>();
                        return (IEnumerable<TEntity>)_xmlRepository.ReadRecords(reportFilePath);
                        break;
                    }

                case ".json":
                    {
                        var _jsonRepository = new JsonFileRepository<TEntity>();
                        return (IEnumerable<TEntity>)_jsonRepository.ReadRecords(reportFilePath);
                        break;
                    }
                default:
                    throw new ArgumentException($"Invalid report type");
            }
        }
    }
}
