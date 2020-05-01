using Bogus;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.Data {
    public static class HostExtensions {

        public static IHost Seed(this IHost host, IdentityContext context, UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager) {

            context.Database.EnsureDeleted();
            if (!context.Database.EnsureCreated()) return host;

            var faker = new Faker("nl");

            var buildings = new Faker<Building>()
                .RuleFor(b => b.Id, f => f.Random.Replace("?.##"))
                .RuleFor(b => b.Rooms, f => new Faker<Room>()
                    .RuleFor(r => r.Id, f => f.Random.Replace("#.##"))
                    .Generate(f.Random.Int(1, 5)))
                .Generate(5);
            context.Buildings.AddRange(buildings);

            var schools = new List<School> {
                new School {
                    Id = "tec",
                    Name = "School of Technology",
                    StreetName = faker.Address.StreetName(),
                    HouseNumber = faker.Random.Int(1, 9999),
                    PostalCode = faker.Address.ZipCode("#### ??"),
                    City = faker.Address.City(),
                    Country = "Netherlands",
                    Programs = new List<Models.Program> {
                        new Models.Program {
                            Id = "ict",
                            Name = "Information Communication Technology"
                        },
                        new Models.Program {
                            Id = "csc",
                            Name = "Computer Science"
                        }
                    },
                    SchoolBuildings = new Faker<SchoolBuilding>()
                        .RuleFor(sb => sb.Building, f => f.PickRandom(buildings))
                        .Generate(1)
                },
                new School {
                    Id = "bus",
                    Name = "School of Business",
                    StreetName = faker.Address.StreetName(),
                    HouseNumber = faker.Random.Int(1, 9999),
                    PostalCode = faker.Address.ZipCode("#### ??"),
                    City = faker.Address.City(),
                    Country = "Netherlands",
                    Programs = new List<Models.Program> {
                        new Models.Program {
                            Id = "iba",
                            Name = "International Business Administration",
                        },
                        new Models.Program {
                            Id = "fin",
                            Name = "Finance",
                        }
                    },
                    SchoolBuildings = new Faker<SchoolBuilding>()
                        .RuleFor(sb => sb.Building, f => f.PickRandom(buildings))
                        .Generate(1)
                }
            };
            context.Schools.AddRange(schools);

            var roles = new List<IdentityRole<int>> {
                new IdentityRole<int>("Employee"),
                new IdentityRole<int>("Teacher"),
                new IdentityRole<int>("Student")
            };
            roles.ForEach(r => { roleManager.CreateAsync(r).Wait(); });

            var employees = new Faker<Employee>("nl")
                .RuleFor(e => e.UserName, f => context.GetNextPcn().Result)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(e => e.Email, (f, e) => e.Initials.ToLower() + e.LastName.ToLower() + "@uni.com")
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("06 ########"))
                .RuleFor(e => e.School, f => f.PickRandom(schools))
                .RuleFor(e => e.Room, (f, e) => f.PickRandom(f.PickRandom(e.School.SchoolBuildings).Building.Rooms))
                .Generate(5);
            employees.ForEach(e => { 
                userManager.CreateAsync(e).Wait();
                userManager.AddToRoleAsync(e, "Employee").Wait();
            });

            var teachers = new Faker<Teacher>("nl")
                .RuleFor(t => t.UserName, f => context.GetNextPcn().Result)
                .RuleFor(t => t.FirstName, f => f.Name.FirstName())
                .RuleFor(t => t.LastName, f => f.Name.LastName())
                .RuleFor(t => t.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(t => t.Email, (f, t) => t.Initials.ToLower() + t.LastName.ToLower() + "@uni.com")
                .RuleFor(t => t.PhoneNumber, f => f.Phone.PhoneNumber("06 ########"))
                .RuleFor(t => t.School, f => f.PickRandom(schools))
                .RuleFor(t => t.Room, (f, t) => f.PickRandom(f.PickRandom(t.School.SchoolBuildings).Building.Rooms))
                .RuleFor(t => t.EmployeePrograms, (f, t) => new List<EmployeeProgram> {
                    new EmployeeProgram { Program = f.PickRandom(t.School.Programs) }
                })
                .Generate(20);
            teachers.ForEach(t => { 
                userManager.CreateAsync(t).Wait();
                userManager.AddToRoleAsync(t, "Employee").Wait();
                userManager.AddToRoleAsync(t, "Teacher").Wait();
            });

            var students = new Faker<Student>("nl")
                .RuleFor(s => s.UserName, f => context.GetNextPcn().Result)
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Initials, f => f.Random.Replace("?.?."))
                .RuleFor(s => s.Email, (f, s) => s.Initials.ToLower() + s.LastName.ToLower() + "@student.uni.com")
                .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("06 ########"))
                .RuleFor(s => s.DateOfBirth, f => f.Date.Between(DateTime.Parse("01-01-1990"), DateTime.Parse("01-01-2000")))
                .RuleFor(s => s.Nationality, f => "Netherlands")
                .RuleFor(s => s.StreetName, f => f.Address.StreetName())
                .RuleFor(s => s.HouseNumber, f => f.Random.Int(1, 9999))
                .RuleFor(s => s.PostalCode, f => f.Address.ZipCode("#### ??"))
                .RuleFor(s => s.City, f => f.Address.City())
                .RuleFor(s => s.Country, f => "Netherlands")
                .RuleFor(s => s.Program, (f, s) => f.PickRandom(f.PickRandom(schools).Programs))
                .RuleFor(s => s.Mentors, (f, s) => new List<Mentor> {
                    new Mentor {
                        MentorType = MentorType.STUDY,
                        Teacher = f.PickRandom(teachers.Where(t => t.EmployeePrograms.Any(ep => ep.Program == s.Program)))
                    }
                })
                .Generate(20);
            students.ForEach(s => { 
                userManager.CreateAsync(s).Wait();
                userManager.AddToRoleAsync(s, "Student").Wait();
            });

            context.SaveChanges();

            return host;
        }

    }
}
