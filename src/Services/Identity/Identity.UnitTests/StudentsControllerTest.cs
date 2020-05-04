using AutoMapper;
using Bogus.DataSets;
using Identity.Api.Controllers;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Mappings;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Identity.UnitTests {

    [Collection("Tests")]
    public class StudentsControllerTest : IDisposable {

        private readonly TestFixture _fixture;

        // Setup 
        // Runs before each test in this test class
        public StudentsControllerTest(TestFixture fixture) {
            _fixture = fixture;
        }

        // Cleanup 
        // Runs after each test in this test class
        public void Dispose() {
            using var context = new IdentityContext(_fixture.DbContextOptions);
            context.Database.EnsureDeleted();
        }

        //[Fact]
        //public async Task GetStudent_WithInvalidId_ShouldReturnNotFoundResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);

        //    // Act
        //    var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.GetStudent(1);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetStudent_WithValidId_ShouldReturnStudent() {
        //    // Arrange
        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Users.Add(new Student { Id = 1, ProgramId = "prog1" });
        //        context.Programs.Add(new Program { Id = "prog1", SchoolId = "school1" });
        //        context.Schools.Add(new School { Id = "school1" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    ActionResult<StudentReadDto> result;

        //    // Use a clean instance of the context to run the test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);

        //        // Act
        //        var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.GetStudent(1);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    var student = Assert.IsAssignableFrom<StudentReadDto>(result.Value);
        //    Assert.NotNull(student);
        //    Assert.NotNull(student.Program); // Test if explicit loading is working
        //    Assert.NotNull(student.Program.School); // Test if explicit loading is working
        //    Assert.Equal(1, student.Id);
        //}

        //[Fact]
        //public async Task PutStudent_WithInvalidInfo_ShouldReturnBadRequestResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new StudentUpdateDto(); // All required fields are null

        //    // Act
        //    var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PutStudent(1, dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Never);
        //    userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Student>()), Times.Never);
        //    Assert.IsType<BadRequestResult>(result);
        //}

        //public static IEnumerable<object[]> GenerateInvalidIdAndStudentWithNonExistentProgram() {
        //    return new List<object[]> { 
        //        new object[] { 2, new StudentUpdateDto { FirstName = "test", LastName = "test", Initials = "test", ProgramId = "test"} },
        //        new object[] { 2, new StudentUpdateDto { FirstName = "test", LastName = "test", Initials = "test", ProgramId = "prog1"} },
        //        new object[] { 1, new StudentUpdateDto { FirstName = "test", LastName = "test", Initials = "test", ProgramId = "test"} }
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(GenerateInvalidIdAndStudentWithNonExistentProgram))]
        //public async Task PutStudent_WithInvalidIdAndNonExistentProgram_ShouldReturnNotFoundResult(int studentId, StudentUpdateDto dto) {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Students.Add(new Student { Id = 1 });
        //        context.Programs.Add(new Program { Id = "prog1" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    IActionResult result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);

        //        // Act
        //        var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PutStudent(studentId, dto);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync(studentId.ToString()), Times.Once);
        //    userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Student>()), Times.Never);
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public async Task PutStudent_WithValidIdAndInfo_ShouldUpdateStudentAndReturnNoContentResult() {
        //    // Arrange
        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Users.Add(new Student { Id = 1 });
        //        context.Programs.Add(new Program { Id = "prog1" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new StudentUpdateDto { FirstName = "test1", LastName = "test2", Initials = "test3", ProgramId = "prog1" };
        //    IActionResult result;

        //    // Use a clean instance of the context to run the test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);
        //        userManager.Setup(um => um.UpdateAsync(It.IsNotNull<Student>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Update(u);
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PutStudent(1, dto);
        //    }

        //    // Use a separate instance of the context to verify test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        // Assert
        //        userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //        userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Student>()), Times.Once);
        //        var student = await context.FindAsync<Student>(1);
        //        Assert.Equal(dto.FirstName, student.FirstName);
        //        Assert.Equal(dto.LastName, student.LastName);
        //        Assert.Equal(dto.Initials, student.Initials);
        //        Assert.Equal(dto.ProgramId, student.ProgramId);
        //        Assert.IsType<NoContentResult>(result);
        //    }
        //}

        //[Fact]
        //public async Task PostStudent_WithInvalidInfo_ShouldReturnBadRequest() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new StudentCreateDto(); // All required fields are null

        //    // Act
        //    var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PostStudent(dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Never);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Student>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Student>(), "Student"), Times.Never);
        //    Assert.IsType<BadRequestResult>(result.Result);
        //}

        //[Fact]
        //public async Task PostStudent_WithExistingInfo_ShouldReturnConflictResult() {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Users.Add(new Student { Email = "test@gmail.com" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new StudentCreateDto {
        //        FirstName = "test",
        //        LastName = "test",
        //        Initials = "test",
        //        Email = "test@gmail.com",
        //        DateOfBirth = DateTime.Now,
        //        Nationality = "test",
        //        ProgramId = "prog1"
        //    };
        //    ActionResult<Student> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);

        //        // Act
        //        var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PostStudent(dto);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Student>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Student>(), It.IsNotNull<string>()), Times.Never);
        //    Assert.IsType<ConflictResult>(result.Result);
        //}

        //[Fact]
        //public async Task PostStudent_WithNonExistentProgram_ShouldReturnNotFoundResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new StudentCreateDto { 
        //        FirstName = "test",
        //        LastName = "test",
        //        Initials = "test",
        //        Email = "test@gmail.com",
        //        DateOfBirth = DateTime.Now,
        //        Nationality = "test",
        //        ProgramId = "prog1"
        //    };

        //    // Act
        //    userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);
        //    var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PostStudent(dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Student>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Student>(), "Student"), Times.Never);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task PostStudent_WithValidInfo_ShouldAddStudentAndReturnCreatedAtActionResult() {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Programs.Add(new Program { Id = "prog1" });
        //        context.Roles.Add(new IdentityRole<int> { Id = 1, Name = "Student" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    var mockedContext = new Mock<IdentityContext>();
        //    var dto = new StudentCreateDto {
        //        FirstName = "test",
        //        LastName = "test",
        //        Initials = "test",
        //        Email = "test@gmail.com",
        //        DateOfBirth = DateTime.Now,
        //        Nationality = "test",
        //        ProgramId = "prog1"
        //    };
        //    ActionResult<Student> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        mockedContext.Setup(c => c.FindAsync<Program>(It.IsNotNull<string>()))
        //            .ReturnsAsync((object[] id) => context.FindAsync<Program>(id).Result);
        //        mockedContext.Setup(c => c.GetNextPcn()).ReturnsAsync("1");

        //        userManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
        //            .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);
        //        userManager.Setup(um => um.CreateAsync(It.IsNotNull<Student>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Add(u);
        //                context.SaveChanges();
        //            });
        //        userManager.Setup(um => um.AddToRoleAsync(It.IsNotNull<Student>(), "Student"))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User, string>((u, r) => {
        //                context.Add(new IdentityUserRole<int> { UserId = u.Id, RoleId = 1 });
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new StudentsController(mockedContext.Object, _fixture.Mapper, userManager.Object);
        //        result = await sut.PostStudent(dto);
        //    }

        //    // Assert
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //        userManager.Verify(um => um.CreateAsync(It.IsNotNull<Student>()), Times.Once);
        //        userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Student>(), "Student"), Times.Once);
        //        mockedContext.Verify(c => c.FindAsync<Program>(dto.ProgramId), Times.Once);
        //        mockedContext.Verify(c => c.GetNextPcn(), Times.Once);
        //        Assert.Equal(1, context.Students.Count());
        //        var res = Assert.IsType<CreatedAtActionResult>(result.Result);
        //        var student = Assert.IsAssignableFrom<Student>(res.Value);
        //        Assert.NotNull(student);
        //    }
        //}

        //[Fact]
        //public async Task DeleteStudent_WithInvalidId_ShouldReturnNotFoundResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);

        //    // Act
        //    var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.DeleteStudent(1);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Student>()), Times.Never);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task DeleteStudent_WithValidId_ShouldDeleteAndReturnStudent() {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Students.Add(new Student { Id = 1 });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    ActionResult<Student> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Student>(int.Parse(id)).Result);
        //        userManager.Setup(um => um.DeleteAsync(It.IsNotNull<Student>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Remove(u);
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new StudentsController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.DeleteStudent(1);
        //    }

        //    // Assert
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //        userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Student>()), Times.Once);
        //        Assert.Empty(context.Students);
        //        var student = Assert.IsAssignableFrom<Student>(result.Value);
        //        Assert.NotNull(student);
        //        Assert.Equal(1, student.Id);
        //    }
        //}

    }
}
