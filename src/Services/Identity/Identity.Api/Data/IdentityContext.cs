using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Data {
    public class IdentityContext : IdentityDbContext<User, IdentityRole<int>, int> {

        public IdentityContext() { }
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Models.Program> Programs { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<SchoolBuilding> SchoolBuildings { get; set; }

        // Users is inherited from IdentityDbContext
        // UserRoles is inherited from IdentityDbContext
        // Roles is inherited from IdentityDbContext

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<EmployeeProgram> EmployeePrograms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mentor> Mentors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.HasSequence<int>("UserId", "dbo")
                .StartsAt(1000000)
                .IncrementsBy(1);

            builder.HasSequence<int>("Pcn", "dbo")
                .StartsAt(100000)
                .IncrementsBy(1);

            builder.Entity<School>(s => {
                s.Property(s => s.Name).IsRequired();
            });

            builder.Entity<Models.Program>(p => {
                p.Property(p => p.Name).IsRequired();
                p.Property(p => p.SchoolId).IsRequired();
            });

            builder.Entity<Room>(r => { 
                r.HasKey(r => new { r.Id, r.BuildingId });
            });

            builder.Entity<SchoolBuilding>(sb => { 
                sb.HasKey(sb => new { sb.SchoolId, sb.BuildingId });
            });

            builder.Entity<User>(u => {
                u.Property(u => u.Id).HasDefaultValueSql("next value for dbo.UserId");
                u.Property(u => u.FirstName).IsRequired();
                u.Property(u => u.LastName).IsRequired();
                u.Property(u => u.Initials).IsRequired();
                u.Property(u => u.AccountStatus)
                    .HasConversion(new EnumToStringConverter<AccountStatus>())
                    .HasDefaultValue(AccountStatus.INACTIVE);
            });

            builder.Entity<Student>(s => {
                s.Property(s => s.DateOfBirth).HasColumnType("date");
                s.Property(s => s.Nationality).IsRequired();
                s.Property(s => s.ProgramId).IsRequired();
            });

            builder.Entity<Employee>(e => { 
                e.HasOne(e => e.Room)
                    .WithMany(r => r.Employees)
                    .HasForeignKey(e => new { e.RoomId, e.BuildingId });
            });

            builder.Entity<EmployeeProgram>(ep => { 
                ep.HasKey(ep => new { ep.EmployeeId, ep.ProgramId });

                ep.HasOne(ep => ep.Program)
                    .WithMany(p => p.EmployeePrograms)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Mentor>(m => {
                m.HasKey(m => new { m.TeacherId, m.StudentId, m.MentorType });

                m.Property(m => m.MentorType) 
                    .HasConversion(new EnumToStringConverter<MentorType>());

                m.HasOne(m => m.Teacher)
                    .WithMany(t => t.Mentors)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }

        public virtual async Task<string> GetNextPcn() {
            var result = new SqlParameter { 
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await Database.ExecuteSqlInterpolatedAsync($"set {result} = next value for dbo.Pcn");

            return result.Value.ToString();
        }

    }
}
