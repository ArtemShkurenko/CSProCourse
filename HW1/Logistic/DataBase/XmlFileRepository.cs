using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataBase
{
    internal class XmlFileRepository<TEntity> : IReportRepository<TEntity>

    {
        protected string fullFilePath { get; set; }
        public XmlFileRepository(string FullFilePath)
        {
            fullFilePath = FullFilePath;
        }
        
        public void SaveRecords(IEnumerable<IRecord> records)
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
            List<TEntity> entity;
            using (FileStream filestream = new FileStream(fullFilePath, FileMode.OpenOrCreate))
            {
                XmlSerializer deserializedObject = new XmlSerializer(typeof(List<TEntity>));
                entity = deserializedObject.Deserialize(filestream) as List<TEntity>;
            }
            if (entity == null)
            {
                throw new Exception("Not found exception");
            }
            return entity.ToList();

        }
    }
}
