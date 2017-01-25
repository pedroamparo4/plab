using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace PayLab_BP.EntityFramework.Repositories
{
    public abstract class PayLab_BPRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PayLab_BPDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected PayLab_BPRepositoryBase(IDbContextProvider<PayLab_BPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class PayLab_BPRepositoryBase<TEntity> : PayLab_BPRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected PayLab_BPRepositoryBase(IDbContextProvider<PayLab_BPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
