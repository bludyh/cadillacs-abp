using Microsoft.EntityFrameworkCore;
using StudyProgress.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Data
{
    public class StudyProgressContext : DbContext
    {
        public StudyProgressContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Program>()
                .HasOne<School>(p => p.School)
                .WithMany(s => s.Programs)
                .HasForeignKey(p => p.SchoolId);

            modelBuilder.Entity<ProgramCourse>().HasKey(pc => new { pc.ProgramId, pc.CourseId });

            modelBuilder.Entity<Course>()
                .HasMany(c => c.RequiredCourses);

            modelBuilder.Entity<Class>()
                .HasOne<Course>(cl => cl.Course)
                .WithMany(co => co.Classes)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.ClassId, e.StudentId });
        }

        //entities
        public DbSet<School> Schools { get; set; }
        public DbSet<Models.Program> Programs { get; set; }
        public DbSet<ProgramCourse> ProgramCourses{ get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
