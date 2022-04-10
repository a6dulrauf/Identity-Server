using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Nami.DXP.Domain;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Nami.DXP.Persistence
{
    public partial class NAMI_PLANT_DATAContext : DbContext
    {
        public NAMI_PLANT_DATAContext(DbContextOptions<NAMI_PLANT_DATAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FacelookMainBak102720> FacelookMainBak102720 { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=192.168.10.15;Initial Catalog=NAMI_PLANT_DATA;User ID=devops1;Password=NisshinboDO2020@)@)");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacelookMainBak102720>(entity =>
            {
                entity.HasNoKey();
            });

           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
