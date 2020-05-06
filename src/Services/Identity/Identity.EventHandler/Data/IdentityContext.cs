using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Identity.EventHandler.Models;

namespace Identity.EventHandler.Data
{
    public partial class IdentityContext : DbContext
    {
        public IdentityContext()
        {
        }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Program> Programs { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Program>(entity =>
            {
                entity.HasIndex(e => e.SchoolId);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.SchoolId).IsRequired();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.SchoolId);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.HasSequence<int>("Pcn").StartsAt(100000);

            modelBuilder.HasSequence<int>("UserId").StartsAt(1000000);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
