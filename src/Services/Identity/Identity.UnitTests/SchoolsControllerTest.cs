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

    public class IdentityContextFixture : IDisposable {
        public IdentityContextFixture() {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("schools")
                .Options;
            IdentityContext = new IdentityContext(options);
            IdentityContext.AddRange(Seed());
            IdentityContext.SaveChanges();
        }

        public IdentityContext IdentityContext { get; private set; }

        public void Dispose() {
            IdentityContext.Database.EnsureDeleted();
        }

        private List<School> Seed() {
            return new List<School> {
                new School { 
                    Id = "school1",
                    Name = "Test School 1"
                },
                new School { 
                    Id = "school2",
                    Name = "Test School 2"
                }
            };
        }
    }

    public class SchoolsControllerTest : IClassFixture<IdentityContextFixture> {

        private readonly IdentityContextFixture _fixture;

        public SchoolsControllerTest(IdentityContextFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetSchools_WhenCalled_ReturnsAllSchools() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var count = _fixture.IdentityContext.Schools.Count();

            // Act
            var result = await schoolsController.GetSchools();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<SchoolDto>>>(result);
            var schools = Assert.IsAssignableFrom<IEnumerable<SchoolDto>>(result.Value);
            Assert.Equal(count, schools.Count());
        }

        [Fact]
        public async Task GetSchool_WithExistingId_ReturnsSchool() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var schoolId = "school1";

            // Act
            var result = await schoolsController.GetSchool(schoolId);

            // Assert
            Assert.IsType<ActionResult<SchoolDto>>(result);
            var school = Assert.IsAssignableFrom<SchoolDto>(result.Value);
            Assert.Equal(schoolId, school.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task GetSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);

            // Act
            var result = await schoolsController.GetSchool(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutSchool_WithExistingId_UpdatesSchoolAndReturnsNoContentResult() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var school = new SchoolDto { Id = "school1", Name = "Updated name" };

            // Act
            var result = await schoolsController.PutSchool(school.Id, school);

            // Assert
            Assert.Equal(school.Name, _fixture.IdentityContext.Schools.FindAsync(school.Id).Result.Name);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutSchool_WithMismatchedId_ReturnsBadRequestResult() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var schoolId = "school1";
            var school = new SchoolDto { Id = "school2", Name = "Updated name" };

            // Act
            var result = await schoolsController.PutSchool(schoolId, school);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task PutSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var school = new SchoolDto { Id = id, Name = "Updated name" };

            // Act
            var result = await schoolsController.PutSchool(school.Id, school);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostSchool_WithNonExistingSchool_AddSchoolAndReturnsCreatedAtActionResult() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var school = new SchoolDto { Id = "school3", Name = "Test school 3" };
            var count = _fixture.IdentityContext.Schools.Count();

            // Act
            var result = await schoolsController.PostSchool(school);

            // Assert
            Assert.Equal(++count, _fixture.IdentityContext.Schools.Count());
            Assert.Contains(_fixture.IdentityContext.Schools, s => s.Id == school.Id);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PostSchool_WithExistingSchool_ReturnsConflictResult() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var school = new SchoolDto { Id = "school1", Name = "Test school 1" };

            // Act
            var result = await schoolsController.PostSchool(school);

            // Assert
            Assert.IsType<ConflictResult>(result.Result);
        }

        [Fact]
        public async Task DeleteSchool_WithExistingId_DeletesSchoolAndReturnsSchool() {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);
            var schoolId = "school2";
            var count = _fixture.IdentityContext.Schools.Count();

            // Act
            var result = await schoolsController.DeleteSchool(schoolId);

            // Assert
            Assert.Equal(--count, _fixture.IdentityContext.Schools.Count());
            Assert.DoesNotContain(_fixture.IdentityContext.Schools, s => s.Id == schoolId);
            var school = Assert.IsAssignableFrom<School>(result.Value);
            Assert.Equal(schoolId, school.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        public async Task DeleteSchool_WithNonExistingId_ReturnsNotFoundResult(string id) {
            // Arrange
            var schoolsController = new SchoolsController(_fixture.IdentityContext);

            // Act
            var result = await schoolsController.DeleteSchool(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}
