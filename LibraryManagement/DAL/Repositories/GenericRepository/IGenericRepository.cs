// <copyright file="IGenericRepository.cs" company="HP">(C) Copyright 2018-2019 HP Development Company, L.P.</copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryManagement.DAL.Repositories
{
    public interface IGenericRepository<T> : IDisposable
        where T : class
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Add(T entity);

        void AddRange(IEnumerable<T> entity);

        T Clone(T realObject);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);

        void Edit(T entity);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<TType> Get<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select)
            where TType : class;

        int Save();
    }
}