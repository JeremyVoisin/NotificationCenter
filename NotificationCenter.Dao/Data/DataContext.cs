using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class DataContext : DbContext
    {
        public DbSet<History> Histories { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<InputProvider> InputProviders { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<OutputProvider> OutputProviders { get; set; }
        public DbSet<OutputParameter> OutputParameters { get; set; }
        public DbSet<Mapping> Mappings { get; set; }

        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=NotificationCenter.db");
        }
    }
}
