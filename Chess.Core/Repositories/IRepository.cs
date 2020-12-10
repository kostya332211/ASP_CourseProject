using System;
using System.Collections.Generic;

namespace Chess.Core.Repositories
{
    public interface IRepository<TEntity, in TKey>
    {
        TEntity Get(TKey id);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> All(Func<TEntity, bool> predicate);

        int Count();

        void Insert(TEntity entity);

        void Delete(TKey id);
    }
}
