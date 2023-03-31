using System;
using Logistic.ConsoleClient.Models;


namespace Logistic.ConsoleClient.Reports
{
    internal interface IReportRepository<TEntity, TId>
        where TEntity : IRecord<TId>
        where TId : struct, IEquatable<TId>
    {
        public void SaveRecords(IEnumerable<IRecord<TId>> records);
        public List<TEntity> ReadRecords(string fileName);
        public TEntity GetRecordById(TId Id);
    }
}
