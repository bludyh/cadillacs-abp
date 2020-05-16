﻿using System;
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
            //modelBuilder.Entity<UserSchedule>(us =>
            //{
            //    us.HasKey(us => new { us.Id });

            //    us.HasOne<User>(us => us.User)
            //        .WithMany(u => u.UserSchedules)
            //        .HasForeignKey(us => us.UserId);
            //});
            

            modelBuilder.Entity<User>(u => {
                u.HasKey(u => new { u.Id });

                //u.HasMany(u => u.UserSchedules)
                //    .WithOne(us => us.User);
            });

            modelBuilder.Entity<Student>(s =>
            {
                //s.HasKey(s => new { s.Id });
            });

            modelBuilder.Entity<Teacher>(t =>
            {
                //t.HasKey(t => new { t.Id });
            });

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
                cs.HasKey(cs => new { cs.Day, cs.StartTime, cs.EndTime, cs.ClassId, cs.ClassSemester, cs.ClassYear, cs.ClassCourseId, cs.RoomId, cs.RoomBuildingId });
            });

            modelBuilder.Entity<Room>(r => {
                r.HasKey(r => new { r.Id, r.BuildingId});
            });
        }

        //entities
        //public DbSet<UserSchedule> UserSchedules { get; set; }

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