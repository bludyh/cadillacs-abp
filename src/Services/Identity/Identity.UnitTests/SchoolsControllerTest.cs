using Identity.Api.Controllers;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Identity.UnitTests {

    public class SchoolsControllerTest {

        [Fact]
        public async Task GetSchools_WhenCalled_ReturnsAllSchools() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("GetSchools_WhenCalled_ReturnsAllSchools_Database")
                .Options;

            using (var context = new IdentityContext(options)) {
                context.Schools.AddRange(new List<School> { 
                    new School { Id = "school1" },
                    new School { Id = "school2" }
                });
                context.SaveChanges();
            }

            using (var context = new IdentityContext(options)) {
                var sut = new SchoolsController(context);
                var count = context.Schools.Count();
                var result = await sut.GetSchools();

                Assert.IsType<ActionResult<IEnumerable<SchoolDto>>>(result);
                var schools = Assert.IsAssignableFrom<IEnumerable<SchoolDto>>(result.Value);
                Assert.Equal(count, schools.Count());
            }
        }

        [Fact]
        public async Task GetSchool_WithExistingId_ReturnsSchool() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("GetSchool_WithExistingId_ReturnsSchool_Database")
                .Options;

            using (var context = new IdentityContext(options)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            using (var context = new IdentityContext(options)) {
                var sut = new SchoolsController(context);
                var schoolId = "school1";
                var result = await sut.GetSchool(schoolId);

                Assert.IsType<ActionResult<SchoolDto>>(result);
                var school = Assert.IsAssignableFrom<SchoolDto>(result.Value);
                Assert.Equal(schoolId, school.Id);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task GetSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("GetSchool_WithNonExistingId_ReturnsNotFoundResult_Database")
                .Options;

            using var context = new IdentityContext(options);
            var sut = new SchoolsController(context);
            var result = await sut.GetSchool(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutSchool_WithExistingId_UpdatesSchoolAndReturnsNoContentResult() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("PutSchool_WithExistingId_UpdatesSchoolAndReturnsNoContentResult_Database")
                .Options;

            using (var context = new IdentityContext(options)) {
                context.Schools.Add(new School { Id = "school1", Name = "School One" });
                context.SaveChanges();
            }

            using (var context = new IdentityContext(options)) {
                var sut = new SchoolsController(context);
                var school = new SchoolDto { Id = "school1", Name = "Updated name" };
                var result = await sut.PutSchool(school.Id, school);

                Assert.Equal(school.Name, context.Schools.FindAsync(school.Id).Result.Name);
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task PutSchool_WithMismatchedId_ReturnsBadRequestResult() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("PutSchool_WithMismatchedId_ReturnsBadRequestResult_Database")
                .Options;

            using var context = new IdentityContext(options);
            var sut = new SchoolsController(context);
            var schoolId = "school1";
            var school = new SchoolDto { Id = "school2" };
            var result = await sut.PutSchool(schoolId, school);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task PutSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("PutSchool_WithNonExistingId_ReturnsNotFoundResult_Database")
                .Options;

            using var context = new IdentityContext(options);
            var sut = new SchoolsController(context);
            var school = new SchoolDto { Id = id };
            var result = await sut.PutSchool(school.Id, school);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostSchool_WithNonExistingSchool_AddSchoolAndReturnsCreatedAtActionResult() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("PostSchool_WithNonExistingSchool_AddSchoolAndReturnsCreatedAtActionResult_Database")
                .Options;

            using var context = new IdentityContext(options);
            var sut = new SchoolsController(context);
            var school = new SchoolDto { Id = "school3" };
            var count = context.Schools.Count();
            var result = await sut.PostSchool(school);

            Assert.Equal(++count, context.Schools.Count());
            Assert.Contains(context.Schools, s => s.Id == school.Id);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PostSchool_WithExistingSchool_ReturnsConflictResult() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("PostSchool_WithExistingSchool_ReturnsConflictResult_Database")
                .Options;

            using (var context = new IdentityContext(options)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            using (var context = new IdentityContext(options)) {
                var sut = new SchoolsController(context);
                var school = new SchoolDto { Id = "school1" };
                var result = await sut.PostSchool(school);

                Assert.IsType<ConflictResult>(result.Result);
            }
        }

        [Fact]
        public async Task DeleteSchool_WithExistingId_DeletesSchoolAndReturnsSchool() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("DeleteSchool_WithExistingId_DeletesSchoolAndReturnsSchool_Database")
                .Options;

            using (var context = new IdentityContext(options)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            using (var context = new IdentityContext(options)) {
                var sut = new SchoolsController(context);
                var schoolId = "school1";
                var count = context.Schools.Count();
                var result = await sut.DeleteSchool(schoolId);

                Assert.Equal(--count, context.Schools.Count());
                Assert.DoesNotContain(context.Schools, s => s.Id == schoolId);
                var school = Assert.IsAssignableFrom<School>(result.Value);
                Assert.Equal(schoolId, school.Id);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task DeleteSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("DeleteSchool_WithNonExistingId_ReturnsNotFoundResult_Database")
                .Options;

            using var context = new IdentityContext(options);
            var sut = new SchoolsController(context);
            var result = await sut.DeleteSchool(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}
