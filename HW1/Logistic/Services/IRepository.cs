﻿using System;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Services
{
    internal interface IRepository<TEntity>
        where TEntity : IRecord
    {
        TEntity GetRecordById(int Id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int Id);

    }
}
