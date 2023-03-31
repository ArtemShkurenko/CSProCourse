using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Logistic.ConsoleClient.Reports;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.DataBase
{
    internal class JsonFileRepository <TEntity, TId>: IReportRepository<TEntity, TId>
        where TEntity : IRecord<TId>
        where TId: struct, IEquatable<TId>
    {
        protected string fullFilePath { get; set; }
        public JsonFileRepository(string FullFilePath)
        {
            fullFilePath = FullFilePath;
        }
        public TEntity GetRecordById(TId id)
        {
            string dataReadFromFile = File.ReadAllText(fullFilePath);
            IEnumerable<TEntity> deserializedCollection = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(dataReadFromFile);
            if (deserializedCollection == null)
            {
                throw new Exception("Not found exception");
            }
            return deserializedCollection.FirstOrDefault(x => x.Id.Equals(id));
        }
        public void SaveRecords(IEnumerable<IRecord<TId>> records)
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
