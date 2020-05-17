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
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Enrollment>(e => {
                e.HasKey(e => new { e.StudentId, e.ClassId, e.ClassSemester, e.ClassYear, e.ClassCourseId });
            });

            modelBuilder.Entity<Lecturer>(l => {
                l.HasKey(l => new { l.TeacherId, l.ClassId, l.ClassSemester, l.ClassYear, l.ClassCourseId });
            });

            modelBuilder.Entity<Class>(c => {
                c.HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId});
            });

            modelBuilder.Entity<ClassSchedule>(cs => {
                cs.HasKey(cs => new { cs.TimeSlotId, cs.Date, cs.RoomId, cs.RoomBuildingId });
            });

            modelBuilder.Entity<Room>(r => {
                r.HasKey(r => new { r.Id, r.BuildingId});
            });
        }

        //entities
        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<Room> Rooms{ get; set; } 
    }
}