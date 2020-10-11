using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.Repositories
{
    public abstract class GenericRepository<T> :
     IGenericRepository<T>
        where T : class, new()
    {

        private ApplicationDbContext entities;

        private DbSet<T> dbset;

        private bool disposed = false;

        public GenericRepository(
            ApplicationDbContext context)
        {
            this.entities = context;
            this.dbset = context.Set<T>();
        }
        public ApplicationDbContext Context
        {
            get { return this.entities; }
            set { this.entities = value; }
        }

        /// <summary>
        /// Gets or sets object variable of type foster db set.
        /// </summary>
        public DbSet<T> DbSet
        {
            get { return this.dbset; }
            set { this.dbset = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = this.dbset;
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this.dbset.Where(predicate);
            return query;
        }

        public IQueryable<TType> Get<TType>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, TType>> select)
            where TType : class
        {
            return this.dbset.Where(where).Select(select);
        }

        public virtual Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Add(T entity)
        {
            return this.dbset.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entity)
        {
            this.dbset.AddRange(entity);
        }

        public T Clone(T entity)
        {
            return (T)this.Context.Entry(entity).CurrentValues.ToObject();
        }

        public virtual void Delete(T entity)
        {
            this.dbset.Remove(entity);
        }

        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            List<T> entityList = this.entities.Set<T>().Where(predicate).ToList();
            this.dbset.RemoveRange(entityList);
        }

        public virtual void Edit(T entity)
        {
            this.entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual int Save()
        {
            return this.entities.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.entities.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}