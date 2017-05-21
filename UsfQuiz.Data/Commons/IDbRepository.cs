﻿using UsfQuiz.Data.CommonModels;

namespace UsfQuiz.Data.Commons
{
    using System.Linq;

    public interface IDbRepository<T> : IDbRepository<T, int>
        where T : BaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey> : IDbGenericRepository<T, TKey>
        where T : BaseModel<TKey>
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);
    }

    public interface IDbGenericRepository<T, in TKey>
        where T : class
    {
        IQueryable<T> All();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void Save();
    }
}
