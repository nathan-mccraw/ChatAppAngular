using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IReadOnlyList<TEntity> GetAllEntitiesFromDB();

        public TEntity GetEntityByIdFromDB(int id);

        public TEntity GetEntityWithSpec(ISpecification<TEntity> spec);

        IReadOnlyList<TEntity> GetEntitiesWithSpec(ISpecification<TEntity> spec);

        public TEntity AddEntityToDB(TEntity entity);

        public TEntity UpdateEntityInDB(TEntity entity);

        public void DeleteEntityFromDB(int id);
    }
}