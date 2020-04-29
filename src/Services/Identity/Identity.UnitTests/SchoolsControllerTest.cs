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

    [Collection("Tests")]
    public class SchoolsControllerTest : IDisposable {

        private readonly TestFixture _fixture;

        // Setup
        // Runs before each test in this test class
        public SchoolsControllerTest(TestFixture fixture) {
            _fixture = fixture;
        }

        // Cleanup
        // Runs after each test in this test class
        public void Dispose() {
            using var context = new IdentityContext(_fixture.DbContextOptions);
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetSchools_ShouldReturnAllSchools() {
            //Arrange
            // Insert seed data into the database using one instance of the context
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.AddRange(new List<School> { 
                    new School { Id = "school1" },
                    new School { Id = "school2" }
                });
                context.SaveChanges();
            }

            ActionResult<IEnumerable<SchoolCreateReadDto>> result;

            // Act
            // Use a clean instance of the context to run the test
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.GetSchools();
            }

            // Assert
            var schools = Assert.IsAssignableFrom<IEnumerable<SchoolCreateReadDto>>(result.Value);
            Assert.Equal(2, schools.Count());
        }

        [Fact]
        public async Task GetSchool_WithInvalidId_ShouldReturnNotFoundResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);

            // Act
            var sut = new SchoolsController(context, _fixture.Mapper);
            var result = await sut.GetSchool("school1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSchool_WithValidId_ShouldReturnSchool() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            ActionResult<SchoolCreateReadDto> result;

            // Act
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.GetSchool("school1");
            }

            // Assert
            var school = Assert.IsAssignableFrom<SchoolCreateReadDto>(result.Value);
            Assert.Equal("school1", school.Id);
        }

        [Fact]
        public async Task PutSchool_WithInvalidInfo_ShouldReturnBadRequestResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var dto = new SchoolUpdateDto(); // All required fields are null, hence invalid

            // Act
            var sut = new SchoolsController(context, _fixture.Mapper);
            var result = await sut.PutSchool("school1", dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutSchool_WithInvalidId_ShouldReturnNotFoundResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);

            var dto = new SchoolUpdateDto { Name = "School 1" };

            // Act
            var sut = new SchoolsController(context, _fixture.Mapper);
            var result = await sut.PutSchool("school1", dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutSchool_WithValidInfo_ShouldUpdateSchoolAndReturnNoContentResult() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.Add(new School { Id = "school1", Name = "School 1" });
                context.SaveChanges();
            }

            var dto = new SchoolUpdateDto { Name = "School One" };
            IActionResult result;

            // Act
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.PutSchool("school1", dto);
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                Assert.Equal(dto.Name, context.Schools.FindAsync("school1").Result.Name);
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task PostSchool_WithInvalidInfo_ShouldReturnBadRequestResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);

            var dto = new SchoolCreateReadDto(); // All required fields are null, hence invalid

            // Act
            var sut = new SchoolsController(context, _fixture.Mapper);
            var result = await sut.PostSchool(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PostSchool_WithExistingInfo_ShouldReturnConflictResult() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            var dto = new SchoolCreateReadDto { Id = "school1", Name = "School One" };
            ActionResult<School> result;

            // Act
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.PostSchool(dto);
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                Assert.IsType<ConflictResult>(result.Result);
                Assert.Equal(1, context.Schools.Count());
            }
        }

        [Fact]
        public async Task PostSchool_WithValidInfo_ShouldAddSchoolAndReturnCreatedAtActionResult() {
            // Arrange
            var dto = new SchoolCreateReadDto { Id = "school1" , Name = "School One"};
            ActionResult<School> result;

            // Act
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.PostSchool(dto);
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                Assert.Equal(1, context.Schools.Count());
                Assert.Contains(context.Schools, s => s.Id == dto.Id);
                Assert.IsType<CreatedAtActionResult>(result.Result);
            }
        }

        [Fact]
        public async Task DeleteSchool_WithInvalidId_ShouldReturnNotFoundResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);

            // Act
            var sut = new SchoolsController(context, _fixture.Mapper);
            var result = await sut.DeleteSchool("school1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteSchool_WithValidId_ShouldDeleteAndReturnSchool() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.Add(new School { Id = "school1" });
                context.SaveChanges();
            }

            ActionResult<School> result;

            // Act
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                var sut = new SchoolsController(context, _fixture.Mapper);
                result = await sut.DeleteSchool("school1");
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                Assert.Equal(0, context.Schools.Count());
                Assert.DoesNotContain(context.Schools, s => s.Id == "school1");
                var school = Assert.IsAssignableFrom<School>(result.Value);
                Assert.Equal("school1", school.Id);
            }
        }

    }
}
