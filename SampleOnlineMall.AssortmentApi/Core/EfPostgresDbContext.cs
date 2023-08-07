using Microsoft.EntityFrameworkCore;
using SampleOnlineMall.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core
{ 

    public class EfPostgresDbContext : DbContext
    {
        public string DbPath { get; private set; }

        public string Guid { get; private set; }

        public EfPostgresDbContext()
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=31.31.201.152:5432; Database=postgres; Username=postgres; password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommodityItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(250);
            });


          //  modelBuilder.Entity<Employee>();
           // modelBuilder.Entity<Role>();

            base.OnModelCreating(modelBuilder);
        }


    }
}
