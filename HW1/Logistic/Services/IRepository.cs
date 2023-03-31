using System;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Services
{
    internal interface IRepository<TEntity, TId>
        where TEntity : IRecord<TId>
        where TId : struct, IEquatable<TId>
    {
        TEntity GetRecordById(TId Id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TId Id);

    }
}
