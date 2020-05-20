using Microsoft.EntityFrameworkCore;
using Schedule.Common.Models;

namespace Schedule.Common.Data
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(c =>
            {
                c.HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId });
            });

            modelBuilder.Entity<ClassSchedule>(cs =>
            {
                cs.HasKey(cs => new { cs.TimeSlotId, cs.Date, cs.RoomId, cs.RoomBuildingId });
            });

            modelBuilder.Entity<Room>(r =>
            {
                r.HasKey(r => new { r.Id, r.BuildingId });
            });
        }

        //entities
        public DbSet<Class> Classes { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}