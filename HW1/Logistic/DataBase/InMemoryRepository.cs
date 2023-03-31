using Logistic.ConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Logistic.ConsoleClient.Services;

namespace Logistic.ConsoleClient.DataBase
{
    internal abstract class InMemoryRepository<TEntity, TId> : IRepository<TEntity,TId>
        where TEntity : IRecord<TId>, new()
        where TId : struct, IEquatable<TId>
    {
        List<TEntity> _records = new List<TEntity>();
        protected TEntity DeepCopy(TEntity entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(TEntity), typeof(TEntity));
            });
            var mapper = config.CreateMapper();
            var copyEntity = mapper.Map<TEntity, TEntity>(entity);
            return (copyEntity);
        }

        public TEntity GetRecordById(TId Id)
        {
            var entity = _records.FirstOrDefault(x => x.Id.Equals(Id));
            return entity;           
        }
        public virtual TEntity? Delete(TId Id)
        {
            var entity = _records.FirstOrDefault(x => x.Id.Equals(Id));
            _records.Remove(DeepCopy(entity));
            return DeepCopy(entity);
        }
        public void Create(TEntity entity)
        {
            var entityCopy = DeepCopy(entity);
            _records.Add(entityCopy);
        }
        public virtual TEntity? Update(TEntity newEntity)
        {
            var oldEntity = _records.FirstOrDefault(x => x.Id.Equals(newEntity.Id));
            _records.Remove(oldEntity);
            _records.Add(newEntity);
            return DeepCopy(newEntity);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _records.Select(DeepCopy);
        }
    }
}

   