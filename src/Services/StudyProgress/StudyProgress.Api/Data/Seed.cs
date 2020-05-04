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

            var faker = new Faker();

            var schools = new List<School>
            {
                new School
                {
                    Id = "ehv",
                    Name = "Fontys University of Applied Sciences Eindhoven"
                },
                new School
                {
                    Id = "tlb",
                    Name = "Fontys University of Applied Sciences Tilburg"
                }
            };
            context.Schools.AddRange(schools);

            var programs = new List<Models.Program>
            {
                new Models.Program
                {
                    Id = "ict-s",
                    Name = "ICT & Software Engineering",
                    Description = faker.Lorem.Sentence(),
                    TotalCredit = 240,
                    School = faker.PickRandom(schools)
                },
                new Models.Program
                {
                    Id = "ict-t",
                    Name = "ICT & Technology",
                    Description = faker.Lorem.Sentence(),
                    TotalCredit = 240,
                    School = faker.PickRandom(schools)
                },
                new Models.Program
                {
                    Id = "ict-b",
                    Name = "ICT & Business",
                    Description = faker.Lorem.Sentence(),
                    TotalCredit = 240,
                    School = faker.PickRandom(schools)
                }
            };
            context.Programs.AddRange(programs);

            var courses = new List<Course>
            {
                new Course
                {
                    Id = "prc1",
                    Name = "Programming C",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3
                },
                new Course
                {
                    Id = "prc2",
                    Name = "Programming C++",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3
                },
                new Course
                {
                    Id = "andr1",
                    Name = "Android 1",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3
                },
                new Course
                {
                    Id = "andr2",
                    Name = "Android 2",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3
                },
                new Course
                {
                    Id = "ipv",
                    Name = "Image Processing Vision",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3
                }
            };
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
                .Generate(5);
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

            var classes = new List<Class>
            {
                new Class
                {
                    Id = "E-S71",
                    Semester = 1,
                    Year = 2020,
                    Course = faker.PickRandom(courses),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future()
                },
                new Class
                {
                    Id = "E-S72",
                    Semester = 1,
                    Year = 2020,
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
                        s.Id = Convert.ToInt32(f.Random.ReplaceNumbers("#######"));
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
                .Generate(10);
            context.Enrollments.AddRange(enrollments);

            context.SaveChanges();

            return host;
        }
    }
}
