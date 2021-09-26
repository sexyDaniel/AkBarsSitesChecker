using Microsoft.EntityFrameworkCore;
using SiteCheck.Application.Interfaces;
using SiteCheck.Domain;
using SiteCheck.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCheck.Persistence
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<History> Histories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new HistoryConfiguration());
            builder.ApplyConfiguration(new SiteConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
