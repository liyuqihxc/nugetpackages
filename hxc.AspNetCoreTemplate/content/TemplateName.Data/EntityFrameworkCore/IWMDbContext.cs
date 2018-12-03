using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateName.Data.EntityFrameworkCore
{
    public class TemplateNameDbContext : AbpDbContext
    {
        public TemplateNameDbContext(DbContextOptions<TemplateNameDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
