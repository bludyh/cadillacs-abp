using Bogus;
using Microsoft.Extensions.Hosting;
using StudyProgress.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Data
{
    public static class HostExtensions
    {
        public static IHost Seed(this IHost host, StudyProgressContext context)
        {
            context.Database.EnsureDeleted();
            if (!context.Database.EnsureCreated()) return host;

            var schools = new Faker<School>()
                .StrictMode(false)
                .Rules((f, s) =>
                    {
                        s.Name = f.Company.CompanyName();
                    }
                )
                .Generate(3);
            context.Schools.AddRange(schools);

            var programs = new Faker<Models.Program>()
                .StrictMode(false)
                .Rules((f, p) =>
                    {
                        p.Name = f.Lorem.Word();
                        p.Description = f.Lorem.Sentences();
                        p.TotalCredit = 60;
                        p.School = f.PickRandom(schools);
                    }
                )
                .Generate(3);
            context.Programs.AddRange(programs);

            var courses = new Faker<Course>()
                .StrictMode(false)
                .Rules((f, c) =>
                    {
                        c.Name = f.Lorem.Word();
                        c.Description = f.Lorem.Sentence();
                        c.Credit = 3;
                    }
                )
                .Generate(10);
            context.Courses.AddRange(courses);

            var courseId = 0;
            var programCourses = new Faker<ProgramCourse>()
                .StrictMode(false)
                .Rules((f, pc) =>
                    {
                        pc.Program = f.PickRandom(programs);
                        pc.Course = courses[courseId++];
                    }
                )
                .Generate(10);
            context.ProgramCourses.AddRange(programCourses);

            var id = 0;
            var requirements = new Faker<Requirement>()
                .StrictMode(false)
                .Rules((f, r) =>
                     {
                         r.Course = courses[id++];
                         r.RequiredCourse = f.PickRandom(courses);
                     }
                )
                .Generate(5);
            context.Requirements.AddRange(requirements);

            var faker = new Faker();

            var classes = new List<Class>
            {
                new Class
                {
                    Id = "E-S71",
                    Semester = 1,
                    Year = 3,
                    Course = faker.PickRandom(courses),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future()
                },
                new Class
                {
                    Id = "E-S72",
                    Semester = 1,
                    Year = 3,
                    Course = faker.PickRandom(courses),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future()
                }
            };
            context.Classes.AddRange(classes);

            var students = new Faker<Student>()
                .StrictMode(false)
                .Rules((f, s) =>
                    {
                        s.Id = f.Random.ReplaceNumbers("#######");
                        s.FirstName = f.Name.FirstName();
                        s.LastName = f.Name.LastName();
                        s.Initials = f.Random.Replace("?.?.");
                    }
                )
                .Generate(15);
            context.Students.AddRange(students);

            var enrollmentStudentId = 0;
            var enrollments = new Faker<Enrollment>()
                .StrictMode(false)
                .Rules((f, e) =>
                    {
                        e.Class = f.PickRandom(classes);
                        e.Student = students[enrollmentStudentId++];
                    }
                )
                .Generate(15);
            context.Enrollments.AddRange(enrollments);

            context.SaveChanges();

            return host;
        }
    }
}
