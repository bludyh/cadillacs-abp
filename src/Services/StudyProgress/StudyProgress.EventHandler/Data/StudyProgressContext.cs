using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudyProgress.EventHandler.Models;

namespace StudyProgress.EventHandler.Data
{
    public partial class StudyProgressContext : DbContext
    {
        public StudyProgressContext()
        {
        }

        public StudyProgressContext(DbContextOptions<StudyProgressContext> options)
            : base(options)
        {
        }

        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
