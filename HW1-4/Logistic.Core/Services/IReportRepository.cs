using System;
using Logistic.Models;


namespace Logistic.Core.Services
{
    public interface IReportRepository<TEntity>
    {
        public void SaveRecords(IEnumerable<IRecord> records, string fullFilePath);
        public List<TEntity> ReadRecords(string fileName);
    }
}
