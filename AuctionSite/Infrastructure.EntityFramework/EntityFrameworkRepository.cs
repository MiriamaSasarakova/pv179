﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;
        protected DbContext Context => ((EntityFrameworkUnitOfWork)provider.GetUnitOfWorkInstance()).Context;

        public EntityFrameworkRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if(entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
           Context.Database.Log = Console.WriteLine;
           var foundEntity = Context.Set<TEntity>().Find(entity.Id);
           Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx
                .SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        
    }
}
