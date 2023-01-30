using KlementWeb.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KlementWeb.Data.Repositories
{
    public abstract class ParentalRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext webContext;
        protected DbSet<TEntity> dbSet;

        public ParentalRepository(ApplicationDbContext context)
        {
            webContext = context;
            dbSet = webContext.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            webContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (dbSet.Contains(entity))
                dbSet.Update(entity);
            else
                dbSet.Add(entity);

            webContext.SaveChanges();
        }

        public void Delete(int id)
        {
            TEntity entity = dbSet.Find(id);
            if(entity != null)
            {
                dbSet.Remove(entity);
                webContext.SaveChanges();
            }
            else
                webContext.Entry(entity).State = EntityState.Unchanged;
        }

        public TEntity FindWithId(int id)
        {
            return dbSet.Find(id);
        }

        public List<TEntity> ReturnEvery()
        {
            return dbSet.ToList();
        }
    }
}
