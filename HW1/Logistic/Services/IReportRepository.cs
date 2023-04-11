using System;
using Logistic.ConsoleClient.Models;


namespace Logistic.ConsoleClient.Services
{
    internal interface IReportRepository<TEntity>
    {
        public void SaveRecords(IEnumerable<IRecord> records, string fullFilePath);
        public List<TEntity> ReadRecords(string fileName);
    }
}
