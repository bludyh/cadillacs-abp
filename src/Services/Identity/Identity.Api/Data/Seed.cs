using Bogus;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.Data {
    public static class HostExtensions {

        public static IHost Seed(this IHost host, IdentityContext context, UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var faker = new Faker("nl");

            var schools = new School[] {
                new School {
                    Id = "tec",
                    Name = "School of Technology",
                    StreetName = faker.Address.StreetName(),
                    HouseNumber = faker.Random.Int(1, 9999),
                    PostalCode = faker.Address.ZipCode("#### ??"),
                    City = faker.Address.City(),
                    Country = "Netherlands"
                }
            };
            context.Schools.AddRange(schools);

            var programs = new Models.Program[] {
                new Models.Program {
                    Id = "ict",
                    Name = "Information Communication Technology",
                    School = schools.First(s => s.Id == "tec")
                }
            };
            context.Programs.AddRange(programs);

            var buildings = new Faker<Building>()
                .RuleFor(b => b.Id, f => f.Random.Replace("?.#"))
                .Generate(5);
            context.Buildings.AddRange(buildings);

            var rooms = new Faker<Room>()
                .RuleFor(r => r.Id, f => f.Random.Replace("#.##"))
                .RuleFor(r => r.Building, f => buildings.ElementAt(f.Random.Int(0, buildings.Count() - 1)))
                .Generate(5);
            context.Rooms.AddRange(rooms);

            var roles = new List<IdentityRole<int>> {
                new IdentityRole<int> { Name = "Secretariat" },
                new IdentityRole<int> { Name = "Teacher" },
                new IdentityRole<int> { Name = "Student" }
            };
            roles.ForEach(r => { roleManager.CreateAsync(r).Wait(); });

            var pcn = 100000;
            var students = new Faker<Student>("nl")
                .RuleFor(s => s.UserName, f => pcn++.ToString())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName, "uni.com"))
                .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("## ### ### ##"))
                .RuleFor(s => s.AccountStatus, f => AccountStatus.ACTIVE)
                .RuleFor(s => s.School, f => schools.First(s => s.Id == "tec"))
                .RuleFor(s => s.DateOfBirth, f => f.Date.Between(DateTime.Parse("01-01-1980"), DateTime.Parse("01-01-2000")))
                .RuleFor(s => s.Nationality, f => f.Address.Country())
                .RuleFor(s => s.StreetName, f => f.Address.StreetName())
                .RuleFor(s => s.HouseNumber, f => f.Random.Int(1, 9999))
                .RuleFor(s => s.PostalCode, f => f.Address.ZipCode("#### ??"))
                .RuleFor(s => s.City, f => f.Address.City())
                .RuleFor(s => s.Country, f => "Netherlands")
                .RuleFor(s => s.Program, f => programs.First(p => p.Id == "ict"))
                .Generate(5);
            students.ForEach(s => { 
                userManager.CreateAsync(s, "@Test123").Wait();
                userManager.AddToRoleAsync(s, "Student").Wait();
            });

            var employees = new Faker<Employee>("nl")
                .RuleFor(e => e.UserName, f => pcn++.ToString())
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(e => e.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName, "uni.com"))
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("## ### ### ##"))
                .RuleFor(e => e.AccountStatus, f => AccountStatus.ACTIVE)
                .RuleFor(e => e.School, f => schools.First(s => s.Id == "tec"))
                .RuleFor(e => e.Room, f => rooms.ElementAt(f.Random.Int(0, rooms.Count() - 1))).
                Generate(2);
            employees.ForEach(e => { 
                userManager.CreateAsync(e, "@Test123").Wait();
                userManager.AddToRoleAsync(e, "Secretariat").Wait();
            });

            var teachers = new Faker<Teacher>("nl")
                .RuleFor(t => t.UserName, f => pcn++.ToString())
                .RuleFor(t => t.FirstName, f => f.Name.FirstName())
                .RuleFor(t => t.LastName, f => f.Name.LastName())
                .RuleFor(t => t.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(t => t.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName, "uni.com"))
                .RuleFor(t => t.PhoneNumber, f => f.Phone.PhoneNumber("## ### ### ##"))
                .RuleFor(t => t.AccountStatus, f => AccountStatus.ACTIVE)
                .RuleFor(t => t.School, f => schools.First(s => s.Id == "tec"))
                .RuleFor(t => t.Room, f => rooms.ElementAt(f.Random.Int(0, rooms.Count() - 1)))
                .Generate(3);
            int count = 0;
            teachers.ForEach(t => { 
                count++;

                userManager.CreateAsync(t, "@Test123").Wait();
                userManager.AddToRoleAsync(t, "Teacher").Wait();

                if (count == 1)
                    userManager.AddToRoleAsync(t, "Secretariat").Wait();

                context.EmployeePrograms.Add(new EmployeeProgram {
                    Employee = t,
                    Program = programs.First(p => p.Id == "ict")
                });
            });

            count = 0;
            students.ForEach(s => { 
                count++;

                context.Mentors.Add(new Mentor {
                    Student = s,
                    Teacher = teachers.ElementAt(faker.Random.Int(0, teachers.Count() - 1)),
                    MentorType = MentorType.STUDY
                });

                if (count == 1 || count == 4)
                    context.Mentors.Add(new Mentor { 
                        Student = s,
                        Teacher = teachers.ElementAt(faker.Random.Int(0, teachers.Count() - 1)),
                        MentorType = MentorType.INTERNSHIP
                    });

                if (count == 4)
                    context.Mentors.Add(new Mentor { 
                        Student = s,
                        Teacher = teachers.ElementAt(faker.Random.Int(0, teachers.Count() - 1)),
                        MentorType = MentorType.GRADUATION
                    });
            });

            context.SchoolBuildings.AddRange(new SchoolBuilding[] { 
                new SchoolBuilding {
                    School = schools.First(s => s.Id == "tec"),
                    Building = buildings.ElementAt(faker.Random.Int(0, buildings.Count() - 1))
                }
            });

            context.SaveChanges();

            return host;
        }

    }
}
