using Core.Entities;
using Core.Interfaces;
using Infrastructure.Specifications;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        public TEntity AddEntityToDB(TEntity entity)
        {
            _session.Save(entity);
            return entity;
        }

        public void DeleteEntityFromDB(int id)
        {
            _session.Delete(_session.Load<TEntity>(id));
            _session.Flush();
        }

        public IReadOnlyList<TEntity> GetAllEntitiesFromDB()
        {
            return _session.Query<TEntity>().ToList();
        }

        public TEntity GetEntityByIdFromDB(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public TEntity UpdateEntityInDB(TEntity entity)
        {
            TEntity persistantEntity = _session.Load<TEntity>(entity.Id);
            persistantEntity = entity;
            _session.Flush();
            return persistantEntity;
        }

        public TEntity MergeEntityInDB(TEntity entity)
        {
            _session.Merge(entity);
            return entity;
        }

        public IReadOnlyList<TEntity> GetEntitiesWithSpec(ISpecification<TEntity> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public TEntity GetEntityWithSpec(ISpecification<TEntity> spec)
        {
            return ApplySpecification(spec).FirstOrDefault();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_session.Query<TEntity>(), spec);
        }
    }
}