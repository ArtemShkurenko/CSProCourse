using System;
using Logistic.Models;

namespace Logistic.Core.Services
{
    public interface IRepository<TEntity>
        where TEntity : IRecord
    {
        TEntity GetRecordById(int Id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int Id);

    }
}
