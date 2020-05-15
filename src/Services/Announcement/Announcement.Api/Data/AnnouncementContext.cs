using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Announcement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Data
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext(DbContextOptions<AnnouncementContext> options) : base(options) { }
        
        public DbSet<Models.Announcement> Announcements{ get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassAnnouncement> ClassAnnouncements { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            builder.Entity<Models.Announcement>(a => {
                a.HasOne(a => a.Employee)
                    .WithMany(e => e.Announcements);
            });

            //builder.Entity<Class>(c => {
            //    c.HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId });
            //    c.HasMany(c => c.ClassAnnouncements)
            //        .WithOne(c => c.Class);
            //});

            builder.Entity<Class>().HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId });

            //builder.Entity<ClassAnnouncement>(ca => {
            //    ca.HasKey(ca => new { ca.AnnouncementId, ca.ClassId, ca.Semester, ca.Year, ca.CourseId });
            //    ca.HasOne(ca => ca.Class)
            //        .WithMany(ca => ca.ClassAnnouncements);
            //    ca.HasOne(ca => ca.Announcement)
            //        .WithMany(ca => ca.ClassAnnouncements);
            //});

            builder.Entity<ClassAnnouncement>().HasKey(ca =>
                new { ca.AnnouncementId, ca.ClassId, ca.ClassSemester, ca.ClassYear, ca.ClassCourseId });

            builder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedNever();
        }
    }
}