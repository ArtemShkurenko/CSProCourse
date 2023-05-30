using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Logistic.Models;
using Logistic.Core.Services;

namespace Logistic.DAL.DataBase
{
    public class JsonFileRepository<TEntity> : IReportRepository<TEntity>

    {
       
        public void SaveRecords(IEnumerable<IRecord> records, string fullFilePath)
        {
            var dataToSave = JsonConvert.SerializeObject(records);
            File.WriteAllText(fullFilePath, dataToSave);
        }
        public List<TEntity> ReadRecords(string fileName)
        {
            string dataReadFromFile = File.ReadAllText(fileName);
            IEnumerable<TEntity> deserializedCollection = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(dataReadFromFile);
            if (deserializedCollection == null)
            {
                throw new Exception("Not found exception");
            }
            return deserializedCollection.ToList();
        }
    }
}
