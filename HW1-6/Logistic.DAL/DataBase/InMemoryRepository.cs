using Logistic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Logistic.Core.Services;

namespace Logistic.DAL.DataBase
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : IRecord, new()
      
    {
        public List<TEntity> _records = new List<TEntity>();
        private int idCounter = 1;
        internal TEntity DeepCopy(TEntity entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(TEntity), typeof(TEntity));
            });
            var mapper = config.CreateMapper();
            var copyEntity = mapper.Map<TEntity, TEntity>(entity);
            return (copyEntity);
        }

        public TEntity GetRecordById(int Id)
        {
            var entity = _records.FirstOrDefault(x => x.Id.Equals(Id));
            return DeepCopy(entity);           
        }
        public void Delete(int Id)
        {
            var entity = _records.FirstOrDefault(x => x.Id.Equals(Id));
            _records.Remove(entity);
        }
        public void Create(TEntity entity)
        {
            var entityCopy = DeepCopy(entity);
            entityCopy.Id = idCounter++;
            _records.Add(entityCopy);
        }
        public void Update(TEntity newEntity)
        {
            var oldEntity = _records.FirstOrDefault(x => x.Id.Equals(newEntity.Id));
            _records.Remove(oldEntity);
            _records.Add(DeepCopy(newEntity));
           
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _records.Select(DeepCopy);
        }
    }
}

   