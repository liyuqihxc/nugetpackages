using System;
using System.Transactions;

using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;

using Microsoft.EntityFrameworkCore;

namespace TemplateName.Data.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedDb(IIocResolver iocResolver)
        {
            WithDbContext<AppDbContext>(iocResolver, SeedDb);
        }

        public static void SeedDb(AppDbContext context)
        {
            new SeedExample(context).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
        where TDbContext : DbContext
        {
            using(var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using(var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>();

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
