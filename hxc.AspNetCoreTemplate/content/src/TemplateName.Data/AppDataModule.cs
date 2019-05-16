using System;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;

using TemplateName.Core;
using TemplateName.Data.EntityFrameworkCore;

namespace TemplateName.Data
{
    [DependsOn(typeof(AppCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class AppDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<AppDbContext>(configuration =>
            {
                // TODO: config db connection.
                // configuration.DbContextOptions.UseMySql(configuration.ConnectionString);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(this.GetType().Assembly);
        }

        public override void PostInitialize()
        {
            EntityFrameworkCore.Seed.SeedHelper.SeedDb(IocManager);
        }
    }
}
