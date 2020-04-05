using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Data {
    public class IdentityContext : IdentityDbContext<User, IdentityRole<int>, int> {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Models.Program> Programs { get; set; }

        // Users is inherited from IdentityDbContext
        // UserRoles is inherited from IdentityDbContext
        // Roles is inherited from IdentityDbContext

        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<EmployeeProgram> EmployeePrograms { get; set; }
        public DbSet<SchoolBuilding> SchoolBuildings { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<User>(u => {
                u.Property(u => u.AccountStatus)
                .HasConversion(new EnumToStringConverter<AccountStatus>());
            });

            builder.Entity<Employee>(e => { 
                e.HasOne(e => e.Room)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => new { e.RoomId, e.BuildingId });
            });

            builder.Entity<Mentor>(m => {
                m.HasKey(m => new { m.TeacherId, m.StudentId, m.MentorType });

                m.Property(m => m.MentorType) 
                .HasConversion(new EnumToStringConverter<MentorType>());

                m.HasOne(m => m.Teacher)
                .WithMany(t => t.Mentors)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<EmployeeProgram>(ep => { 
                ep.HasKey(ep => new { ep.EmployeeId, ep.ProgramId });
            });

            builder.Entity<SchoolBuilding>(sb => { 
                sb.HasKey(sb => new { sb.SchoolId, sb.BuildingId });
            });

            builder.Entity<Room>(r => { 
                r.HasKey(r => new { r.Id, r.BuildingId });
            });

        }

    }
}
