using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Services;

namespace Logistic.ConsoleClient.DataBase
{
    internal class JsonFileRepository<TEntity> : IReportRepository<TEntity>

    {
        protected string fullFilePath { get; set; }
        public JsonFileRepository(string FullFilePath)
        {
            fullFilePath = FullFilePath;
        }
        public void SaveRecords(IEnumerable<IRecord> records)
        {
            var dataToSave = JsonConvert.SerializeObject(records);
            File.WriteAllText(fullFilePath, dataToSave);
        }
        public List<TEntity> ReadRecords(string fileName)
        {
            string dataReadFromFile = File.ReadAllText(fullFilePath);
            IEnumerable<TEntity> deserializedCollection = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(dataReadFromFile);
            if (deserializedCollection == null)
            {
                throw new Exception("Not found exception");
            }
            return deserializedCollection.ToList();
        }
    }
}
