using System.Collections.Generic;

namespace WebApiCore.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All { get; }
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        string Update(TEntity entity);
        TEntity FindById(string Id);

    }
}