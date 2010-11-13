using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NBlog.Web.Application.Storage
{
    public interface IRepository
    {
        TEntity Single<TEntity, TKey>(TKey key);
        void Save<TEntity>(TEntity item);
        IQueryable<TEntity> All<TEntity>();
    }
}
