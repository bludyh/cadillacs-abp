using Course.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Course.Common.Data
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasKey(c => new { c.Id, c.Semester, c.Year, c.CourseId });

            modelBuilder.Entity<Class>()
                .HasOne<Models.Course>(cl => cl.Course)
                .WithMany(co => co.Classes)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<Enrollment>().HasKey(e =>
                new { e.StudentId, e.ClassId, e.ClassSemester, e.ClassYear, e.ClassCourseId });

            modelBuilder.Entity<ClassSchedule>().HasKey(cs =>
                new { cs.TimeSlotId, cs.RoomId, cs.RoomBuildingId });

            modelBuilder.Entity<Assignment>(a =>
            {
                a.Property(a => a.Type)
                    .HasConversion(new EnumToStringConverter<AssignmentType>());
            });

            modelBuilder.Entity<Lecturer>().HasKey(l =>
                new { l.TeacherId, l.ClassId, l.ClassSemester, l.ClassYear, l.ClassCourseId });

            modelBuilder.Entity<StudentEvaluation>().HasKey(se =>
                new { se.StudentId, se.TeacherId, se.AssignmentId, se.AttachmentId, se.EvaluationId });

            modelBuilder.Entity<GroupEvaluation>().HasKey(ge =>
                new { ge.GroupId, ge.TeacherId, ge.AssignmentId, ge.AttachmentId, ge.EvaluationId });

            modelBuilder.Entity<StudyMaterialAttachment>().HasKey(sma =>
               new { sma.StudyMaterialId, sma.AttachmentId });

            modelBuilder.Entity<AssignmentAttachment>().HasKey(aa =>
               new { aa.AssignmentId, aa.AttachmentId });
        }

        //entities
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentAttachment> AssignmentAttachments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Models.Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEvaluation> GroupEvaluations { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentEvaluation> StudentEvaluations { get; set; }
        public DbSet<StudyMaterial> StudyMaterials { get; set; }
        public DbSet<StudyMaterialAttachment> StudyMaterialAttachments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
    }
}
