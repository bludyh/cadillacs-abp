using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedule.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Schedule.Api.Data
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSchedule>(us => {
                us.HasKey(us => new { us.Id });

                us.HasOne<User>(us => us.User)
                    .WithMany(u => u.UserSchedules)
                    .HasForeignKey(us => us.UserId);
            });
                

            modelBuilder.Entity<User>(u => {
                u.HasKey(u => new { u.Id });

                u.HasMany(u => u.UserSchedules)
                    .WithOne(us => us.User);
            });

            //modelBuilder.Entity<Student>(s => {
            //    s.HasKey(s => new { s.Id });

            //    s.HasMany(s => s.Enrollments)
            //        .WithOne(e => e.Student);
            //});

            //modelBuilder.Entity<Teacher>(t => {
            //    t.HasKey(t => new { t.Id });

            //    t.HasMany(t => t.Lecturers)
            //        .WithOne(l => l.Teacher);
            //});

            modelBuilder.Entity<Enrollment>(e => {
                e.HasKey(e => new { e.StudentId, e.ClassId, e.Semester, e.Year, e.CourseId });

                e.HasOne<Class>(e => e.Class)
                    .WithMany(c => c.Enrollments);
            });

            modelBuilder.Entity<Lecturer>(l => {
                l.HasKey(l => new { l.TeacherId, l.ClassId, l.Semester, l.Year, l.CourseId });

                l.HasOne<Class>(l => l.Class)
                    .WithMany(c => c.Lecturers);
            });

            modelBuilder.Entity<Class>(c => {
                c.HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId});

                c.HasMany(c => c.Enrollments)
                    .WithOne(e => e.Class);
                c.HasMany(c => c.Lecturers)
                    .WithOne(l => l.Class);
                c.HasMany(c => c.ClassSchedules)
                    .WithOne(cs => cs.Class);
            });

            modelBuilder.Entity<ClassSchedule>(cs => {
                cs.HasKey(cs => new { cs.Day, cs.StartTime, cs.EndTime, cs.ClassId, cs.Semester, cs.Year, cs.CourseId, cs.RoomId, cs.Building });
            });

            modelBuilder.Entity<Room>(r => {
                r.HasKey(r => new { r.Id, r.BuildingId});

                r.HasMany(r => r.ClassSchedules)
                    .WithOne(cs => cs.Room);
            });
        }

        //entities
        public DbSet<UserSchedule> UserSchedules { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<Room> Rooms{ get; set; } 
    }
}