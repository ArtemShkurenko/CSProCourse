using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Reports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataBase
{
    internal class XmlFileRepository<TEntity, TId> : IReportRepository<TEntity, TId>
        where TEntity : IRecord<TId>
        where TId : struct, IEquatable<TId>
    {
        protected string fullFilePath { get; set; }
        public XmlFileRepository(string FullFilePath)
        {
            fullFilePath = FullFilePath;
        }
        public TEntity GetRecordById(TId id)
        {
            FileStream filestream = new FileStream(fullFilePath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader streamreader = new StreamReader(filestream);
            XmlSerializer deserializedObject = new XmlSerializer(typeof(IEnumerable<TEntity>));
            IEnumerable<TEntity> deserializedCollection = (IEnumerable<TEntity>)deserializedObject.Deserialize(streamreader);
            if (deserializedCollection == null)
            {
                throw new Exception("Not found exception");
            }
            return deserializedCollection.FirstOrDefault(x => x.Id.Equals(id));
        }
        public void SaveRecords(IEnumerable<IRecord<TId>> records)
        {
            List<TEntity> concreteList = new List<TEntity>((IEnumerable<TEntity>)records);
            XmlSerializer dataToSave = new XmlSerializer(typeof(List<TEntity>));
            using (StreamWriter writer = new StreamWriter(fullFilePath))
            { 
                dataToSave.Serialize(writer, concreteList);
            }

        }
        public List<TEntity> ReadRecords(string fileName)
        {
            FileStream filestream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            StreamReader streamreader = new StreamReader(filestream);
            XmlSerializer deserializedObject = new XmlSerializer(typeof(List<TEntity>));
            IEnumerable<TEntity> deserializedCollection = (IEnumerable<TEntity>)deserializedObject.Deserialize(streamreader);
                if (deserializedCollection == null)
                {
                    throw new Exception("Not found exception");
                }
                return deserializedCollection.ToList();           
        }
    }

}
