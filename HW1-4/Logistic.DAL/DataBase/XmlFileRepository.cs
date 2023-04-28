using Logistic.Models;
using Logistic.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Logistic.DAL.DataBase
{
    public class XmlFileRepository<TEntity> : IReportRepository<TEntity>

    {
               
        public void SaveRecords(IEnumerable<IRecord> records, string fullFilePath)
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
            using (FileStream filestream = new FileStream(fileName, FileMode.OpenOrCreate))
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
