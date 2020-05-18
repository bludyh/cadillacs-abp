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
            builder.Entity<Models.Announcement>(a => {
                a.HasOne(a => a.Employee)
                    .WithMany(e => e.Announcements);
            });

            builder.Entity<Class>().HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId });

            builder.Entity<ClassAnnouncement>().HasKey(ca =>
                new { ca.AnnouncementId, ca.ClassId, ca.ClassSemester, ca.ClassYear, ca.ClassCourseId });

            builder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedNever();
        }
    }
}