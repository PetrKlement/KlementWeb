using System.Collections.Generic;

namespace KlementWeb.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Update(TEntity entity);

        void Insert(TEntity entity);

        void Delete(int id);

        TEntity FindWithId(int id);

        List<TEntity> ReturnEvery();
    }
}
