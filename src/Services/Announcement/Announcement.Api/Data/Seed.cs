using Bogus;
using Microsoft.Extensions.Hosting;
using Announcement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Data
{
    public static class HostExtensions
    {
        public static IHost Seed(this IHost host, AnnouncementContext context)
        {
            context.Database.EnsureDeleted();
            if (!context.Database.EnsureCreated()) return host;

            var employees = new Faker<Employee>()
                .StrictMode(false)
                .Rules((f, s) =>
                {
                    s.Id = Convert.ToInt32(f.Random.ReplaceNumbers("#####"));
                    s.FirstName = f.Lorem.Word();
                    s.LastName = f.Lorem.Word();
                    s.Initials = f.Random.Replace("?.?.");
                })
                .Generate(10);
            context.Employees.AddRange(employees);

            var announcements = new Faker<Models.Announcement>()
                .StrictMode(false)
                .Rules((f, s) =>
                {
                    s.EmployeeId = f.Random.ReplaceNumbers("#####");
                    s.Employee = f.PickRandom(employees);
                    s.Title = f.Lorem.Word();
                    s.Body = f.Lorem.Sentence();
                    s.DateTime = f.Date.Past();
                })
                .Generate(3);
            context.Announcements.AddRange(announcements);

            int courseId = 0;
            var faker = new Faker();

            var classes = new List<Class>
            {
                new Class
                {
                    Id = "E-S41",
                    Semester = 2,
                    Year = 2,
                    CourseId = courseId++,
                    CourseName = faker.Lorem.Word()
                },
                new Class
                {
                    Id = "E-S42",
                    Semester = 2,
                    Year = 2,
                    CourseId = courseId++,
                    CourseName = faker.Lorem.Word()
                }
            };
            context.Classes.AddRange(classes);

            var classAnnouncements = new Faker<ClassAnnouncement>()
                .StrictMode(false)
                .Rules((f, s) =>
                {
                    s.Class = f.PickRandom(classes);
                    s.Announcement = f.PickRandom(announcements);
                })
                .Generate(5);
            context.ClassAnnouncements.AddRange(classAnnouncements);


            context.SaveChanges();

            return host;
        }
    }
}
