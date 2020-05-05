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
            //context.Database.EnsureDeleted();
            if (!context.Database.EnsureCreated()) return host;

            var faker = new Faker();

            var schools = new List<School>
            {
                new School
                {
                    Id = "ehv",
                    Name = "Fontys University of Applied Sciences Eindhoven",
                    Programs = new List<Models.Program> { 
                        new Models.Program
                        {
                            Id = "ict-s",
                            Name = "ICT & Software Engineering",
                            Description = faker.Lorem.Sentence(),
                            TotalCredit = 240
                        },
                        new Models.Program
                        {
                            Id = "ict-t",
                            Name = "ICT & Technology",
                            Description = faker.Lorem.Sentence(),
                            TotalCredit = 240,
                        },
                        new Models.Program
                        {
                            Id = "ict-b",
                            Name = "ICT & Business",
                            Description = faker.Lorem.Sentence(),
                            TotalCredit = 240,
                        }
                    }
                }
            };
            context.Schools.AddRange(schools);

            var courses = new List<Course>
            {
                new Course
                {
                    Id = "prc1",
                    Name = "Programming C",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3,
                    ProgramCourses = new List<ProgramCourse> { 
                        new ProgramCourse { ProgramId = "ict-b" },
                        new ProgramCourse { ProgramId = "ict-s" },
                        new ProgramCourse { ProgramId = "ict-t" }
                    },
                    Classes = new List<Class>
                    {
                        new Class
                        {
                            Id = "e-s71",
                            Semester = 1,
                            Year = 2020,
                            StartDate = faker.Date.Past(),
                            EndDate = faker.Date.Future()
                        },
                        new Class
                        {
                            Id = "e-s72",
                            Semester = 1,
                            Year = 2020,
                            StartDate = faker.Date.Past(),
                            EndDate = faker.Date.Future()
                        },
                        new Class
                        {
                            Id = "e-s71",
                            Semester = 2,
                            Year = 2020,
                            StartDate = faker.Date.Past(),
                            EndDate = faker.Date.Future()
                        }
                    }
                },
                new Course
                {
                    Id = "prc2",
                    Name = "Programming C++",
                    Description = faker.Lorem.Sentence(),
                    Credit = 3,
                    ProgramCourses = new List<ProgramCourse> { 
                        new ProgramCourse { ProgramId = "ict-b" },
                        new ProgramCourse { ProgramId = "ict-s" },
                        new ProgramCourse { ProgramId = "ict-t" }
                    },
                    Requirements = new List<Requirement> { 
                        new Requirement { RequiredCourseId = "prc1" }
                    },
                    Classes = new List<Class>
                    {
                        new Class
                        {
                            Id = "e-s71",
                            Semester = 1,
                            Year = 2020,
                            StartDate = faker.Date.Past(),
                            EndDate = faker.Date.Future()
                        },
                        new Class
                        {
                            Id = "e-s71",
                            Semester = 2,
                            Year = 2020,
                            StartDate = faker.Date.Past(),
                            EndDate = faker.Date.Future()
                        }
                    }
                }
            };
            context.Courses.AddRange(courses);

            var students = new Faker<Student>()
                .StrictMode(false)
                .Rules((f, s) =>
                    {
                        s.Id = Convert.ToInt32(f.Random.ReplaceNumbers("#######"));
                        s.FirstName = f.Name.FirstName();
                        s.LastName = f.Name.LastName();
                        s.Initials = f.Random.Replace("?.?.");
                        s.Enrollments = new List<Enrollment> { 
                            new Enrollment { Class = f.PickRandom(f.PickRandom(courses).Classes) }
                        };
                    }
                )
                .Generate(15);
            context.Students.AddRange(students);

            context.SaveChanges();

            return host;
        }
    }
}
