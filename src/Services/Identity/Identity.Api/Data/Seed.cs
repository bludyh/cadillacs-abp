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

            //context.Database.EnsureDeleted();
            if (!context.Database.EnsureCreated()) return host;

            var faker = new Faker("nl");

            var buildings = new Faker<Building>()
                .StrictMode(false)
                .Rules((f, b) => {
                    b.Id = f.Random.Replace("?.##");
                    b.Rooms = new Faker<Room>().RuleFor(r => r.Id, f => f.Random.Replace("#.##")).Generate(5);
                })
                .Generate(5);
            context.Buildings.AddRange(buildings);

            var schools = new List<School> {
                new School {
                    Id = "ehv",
                    Name = "Fontys University of Applied Sciences Eindhoven",
                    StreetName = faker.Address.StreetName(),
                    HouseNumber = faker.Random.Int(1, 9999),
                    PostalCode = faker.Address.ZipCode("#### ??"),
                    City = "Eindhoven",
                    Country = "Netherlands",
                    Programs = new List<Models.Program> {
                        new Models.Program {
                            Id = "ict-s",
                            Name = "ICT & Software Engineering"
                        },
                        new Models.Program {
                            Id = "ict-t",
                            Name = "ICT & Technology"
                        },
                        new Models.Program {
                            Id = "ict-b",
                            Name = "ICT & Business"
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
                .Rules((f, e) => {
                    e.UserName = context.GetNextPcn().Result;
                    e.FirstName = f.Name.FirstName();
                    e.LastName = f.Name.LastName();
                    e.Initials = f.Random.Replace("?.?.");
                    e.Email = e.Initials.ToLower() + e.LastName.ToLower() + "@fontys.nl";
                    e.PhoneNumber = f.Phone.PhoneNumber("06 ########");
                    e.School = f.PickRandom(schools);
                    e.Room = f.PickRandom(f.PickRandom(e.School.SchoolBuildings).Building.Rooms);
                })
                .Generate(10);
            employees.ForEach(e => { 
                userManager.CreateAsync(e).Wait();
                userManager.AddToRoleAsync(e, "Employee").Wait();
            });

            var teachers = new Faker<Teacher>("nl")
                .Rules((f, t) => {
                    t.UserName = context.GetNextPcn().Result;
                    t.FirstName = f.Name.FirstName();
                    t.LastName = f.Name.LastName();
                    t.Initials = f.Random.Replace("?.?.");
                    t.Email = t.Initials.ToLower() + t.LastName.ToLower() + "@fontys.nl";
                    t.PhoneNumber = f.Phone.PhoneNumber("06 ########");
                    t.School = f.PickRandom(schools);
                    t.Room = f.PickRandom(f.PickRandom(t.School.SchoolBuildings).Building.Rooms);
                    t.EmployeePrograms = new List<EmployeeProgram> { 
                        new EmployeeProgram { Program = f.PickRandom(t.School.Programs) }
                    };
                })
                .Generate(20);
            teachers.ForEach(t => { 
                userManager.CreateAsync(t).Wait();
                userManager.AddToRoleAsync(t, "Employee").Wait();
                userManager.AddToRoleAsync(t, "Teacher").Wait();
            });

            var students = new Faker<Student>("nl")
                .Rules((f, s) => {
                    s.UserName = context.GetNextPcn().Result;
                    s.FirstName = f.Name.FirstName();
                    s.LastName = f.Name.LastName();
                    s.Initials = f.Random.Replace("?.?.");
                    s.Email = s.Initials.ToLower() + s.LastName.ToLower() + "@student.fontys.nl";
                    s.PhoneNumber = f.Phone.PhoneNumber("06 ########");
                    s.DateOfBirth = f.Date.Between(DateTime.Parse("01-01-1990"), DateTime.Parse("01-01-2000"));
                    s.Nationality = "Netherlands";
                    s.StreetName = f.Address.StreetName();
                    s.HouseNumber = f.Random.Int(1, 9999);
                    s.PostalCode = f.Address.ZipCode("#### ??");
                    s.City = f.Address.City();
                    s.Country = "Netherlands";
                    s.Program = f.PickRandom(f.PickRandom(schools).Programs);
                    s.Mentors = new List<Mentor> { 
                        new Mentor { 
                            MentorType = MentorType.STUDY,
                            Teacher = f.PickRandom(teachers.Where(t => t.EmployeePrograms.Any(ep => ep.Program == s.Program)))
                        }
                    };
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
