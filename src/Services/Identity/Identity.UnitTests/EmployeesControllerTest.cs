using Identity.Api.Controllers;
using Identity.Api.Data;
using Identity.Api.Dtos;
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
    public class EmployeesControllerTest : IDisposable {

        private readonly TestFixture _fixture;

        // Setup 
        // Runs before each test in this test class
        public EmployeesControllerTest(TestFixture fixture) {
            _fixture = fixture;
        }

        // Cleanup 
        // Runs after each test in this test class
        public void Dispose() {
            using var context = new IdentityContext(_fixture.DbContextOptions);
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetEmployee_WithInvalidId_ShouldReturnNotFoundResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var userManager = _fixture.CreateUserManagerMock();
            userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);

            // Act
            var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
            var result = await sut.GetEmployee(1);

            // Assert
            userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetEmployee_WithValidId_ShouldReturnEmployee() {
            // Arrange
            // Insert seed data into the database using one instance of the context
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Employees.Add(new Employee { Id = 1, SchoolId = "school1", RoomId = "1", BuildingId = "A" });
                context.Schools.Add(new School { Id = "school1" });
                context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
                context.Buildings.Add(new Building { Id = "A" });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            ActionResult<EmployeeReadDto> result;

            // Use a clean instance of the context to run the test
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                    .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);

                // Act
                var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
                result = await sut.GetEmployee(1);
            }

            // Assert
            userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
            var employee = Assert.IsAssignableFrom<EmployeeReadDto>(result.Value);
            Assert.NotNull(employee);
            Assert.NotNull(employee.School); // Test if explicit loading is working
            Assert.NotNull(employee.Room); // Test if explicit loading is working
            Assert.Equal(1, employee.Id);
        }

        public static IEnumerable<object[]> Generate_PutEmployee_InvalidRequests() {
            return new List<object[]> { 
                new object[] { new EmployeeUpdateDto() }, // Missing required fields
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1" } }, // Incomplete Room foreign key
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", BuildingId = "A" } } // Incomplete Room foreign key
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PutEmployee_InvalidRequests))]
        public async Task PutEmployee_WithInvalidRequest_ShouldReturnBadRequestResult(EmployeeUpdateDto dto) {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var userManager = _fixture.CreateUserManagerMock();

            // Act
            var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
            var result = await sut.PutEmployee(1, dto);

            // Assert
            userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Never);
            userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Student>()), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }

        public static IEnumerable<object[]> Generate_PutEmployee_RequestsThatResultInNotFound() {
            return new List<object[]> {
                new object[] { 2, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test" } }, // Invalid Id
                new object[] { 1, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1" } }, // Invalid School foreign key
                new object[] { 1, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1", BuildingId = "A" } } // Invalid Room foreign key
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PutEmployee_RequestsThatResultInNotFound))]
        public async Task PutEmployee_WithRequestThatResultsInNotFound_ShouldReturnNotFoundResult(int employeeId, EmployeeUpdateDto dto) {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Employees.Add(new Employee { Id = 1 });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            IActionResult result;

            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                    .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);

                // Act
                var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
                result = await sut.PutEmployee(employeeId, dto);
            }

            // Assert
            userManager.Verify(um => um.FindByIdAsync(employeeId.ToString()), Times.Once);
            userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Student>()), Times.Never);
            Assert.IsType<NotFoundResult>(result);
        }

        public static IEnumerable<object[]> Generate_PutEmployee_ValidRequests() {
            return new List<object[]> { 
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test" } }, // Employee who is not in any schools and does not have a room
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1" } }, // Employee who does not have a room
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1", BuildingId = "A" } }, // Employee who is not in any schools
                new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1", RoomId = "1", BuildingId = "A" } }, // Employee who is not in any schools
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PutEmployee_ValidRequests))]
        public async Task PutEmployee_WithValidRequest_ShouldUpdateEmployeeAndReturnNoContentResult(EmployeeUpdateDto dto) {
            // Arrange
            // Insert seed data into the database using one instance of the context
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Employees.Add(new Employee { Id = 1 });
                context.Schools.Add(new School { Id = "school1" });
                context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            IActionResult result;

            // Use a clean instance of the context to run the test
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                    .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);
                userManager.Setup(um => um.UpdateAsync(It.IsNotNull<Employee>()))
                    .ReturnsAsync(IdentityResult.Success)
                    .Callback<User>(u => {
                        context.Update(u);
                        context.SaveChanges();
                    });

                // Act
                var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
                result = await sut.PutEmployee(1, dto);
            }

            // Use a separate instance of the context to verify test
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                // Assert
                userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
                userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Employee>()), Times.Once);
                var employee = await context.FindAsync<Employee>(1);
                Assert.Equal(dto.FirstName, employee.FirstName);
                Assert.Equal(dto.LastName, employee.LastName);
                Assert.Equal(dto.Initials, employee.Initials);
                Assert.Equal(dto.SchoolId, employee.SchoolId);
                Assert.Equal(dto.RoomId, employee.RoomId);
                Assert.Equal(dto.BuildingId, employee.BuildingId);
                Assert.IsType<NoContentResult>(result);
            }
        }

        public static IEnumerable<object[]> Generate_PostEmployee_InvalidRequests() {
            return new List<object[]> { 
                new object[] { new EmployeeCreateDto() },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1" } },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", BuildingId = "A" } }
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PostEmployee_InvalidRequests))]
        public async Task PostEmployee_WithInvalidRequest_ShouldReturnBadRequest(EmployeeCreateDto dto) {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var userManager = _fixture.CreateUserManagerMock();

            // Act
            var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
            var result = await sut.PostEmployee(dto);

            // Assert
            userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Never);
            userManager.Verify(um => um.CreateAsync(It.IsNotNull<Employee>()), Times.Never);
            userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Employee>(), "Employee"), Times.Never);
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PostEmployee_WithExistingInfo_ShouldReturnConflictResult() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Employees.Add(new Employee { Email = "test@gmail.com" });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            var dto = new EmployeeCreateDto {
                FirstName = "test",
                LastName = "test",
                Initials = "test",
                Email = "test@gmail.com"
            };
            ActionResult<Employee> result;

            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
                    .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);

                // Act
                var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
                result = await sut.PostEmployee(dto);
            }

            // Assert
            userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
            userManager.Verify(um => um.CreateAsync(It.IsNotNull<Employee>()), Times.Never);
            userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Employee>(), It.IsNotNull<string>()), Times.Never);
            Assert.IsType<ConflictResult>(result.Result);
        }

        public static IEnumerable<object[]> Generate_PostEmployee_RequestsWithInvalidForeignKeys() {
            return new List<object[]> { 
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1" } },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1", BuildingId = "A" } }
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PostEmployee_RequestsWithInvalidForeignKeys))]
        public async Task PostStudent_WithNonExistentProgram_ShouldReturnNotFoundResult(EmployeeCreateDto dto) {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var userManager = _fixture.CreateUserManagerMock();

            // Act
            userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
                .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);

            var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
            var result = await sut.PostEmployee(dto);

            // Assert
            userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
            userManager.Verify(um => um.CreateAsync(It.IsNotNull<Employee>()), Times.Never);
            userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Employee>(), "Employee"), Times.Never);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        public static IEnumerable<object[]> Generate_PostEmployee_ValidRequests() {
            return new List<object[]> { 
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com" } },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1" } },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1", BuildingId = "A" } },
                new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1", RoomId = "1", BuildingId = "A" } },
            };
        }

        [Theory]
        [MemberData(nameof(Generate_PostEmployee_ValidRequests))]
        public async Task PostEmployee_WithValidInfo_ShouldAddEmployeeAndReturnCreatedAtActionResult(EmployeeCreateDto dto) {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Schools.Add(new School { Id = "school1" });
                context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
                context.Buildings.Add(new Building { Id = "A" });
                context.Roles.Add(new IdentityRole<int> { Id = 1, Name = "Employee" });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            var mockedContext = new Mock<IdentityContext>();
            ActionResult<Employee> result;

            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                mockedContext.Setup(c => c.FindAsync<School>(It.IsNotNull<string>()))
                    .ReturnsAsync((object[] id) => context.FindAsync<School>(id).Result);
                mockedContext.Setup(c => c.FindAsync<Room>(It.IsNotNull<string>(), It.IsNotNull<string>()))
                    .ReturnsAsync((object[] id) => context.FindAsync<Room>(id).Result);
                mockedContext.Setup(c => c.GetNextPcn()).ReturnsAsync("1");

                userManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);
                userManager.Setup(um => um.CreateAsync(It.IsNotNull<Employee>()))
                    .ReturnsAsync(IdentityResult.Success)
                    .Callback<User>(u => {
                        context.Add(u);
                        context.SaveChanges();
                    });
                userManager.Setup(um => um.AddToRoleAsync(It.IsNotNull<Employee>(), "Employee"))
                    .ReturnsAsync(IdentityResult.Success)
                    .Callback<User, string>((u, r) => {
                        context.Add(new IdentityUserRole<int> { UserId = u.Id, RoleId = 1 });
                        context.SaveChanges();
                    });

                // Act
                var sut = new EmployeesController(mockedContext.Object, _fixture.Mapper, userManager.Object);
                result = await sut.PostEmployee(dto);
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
                userManager.Verify(um => um.CreateAsync(It.IsNotNull<Employee>()), Times.Once);
                userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Employee>(), "Employee"), Times.Once);
                mockedContext.Verify(c => c.FindAsync<School>(dto.SchoolId), Times.AtMostOnce);
                mockedContext.Verify(c => c.FindAsync<Room>(dto.RoomId), Times.AtMostOnce);
                mockedContext.Verify(c => c.GetNextPcn(), Times.Once);
                Assert.Equal(1, context.Employees.Count());
                var res = Assert.IsType<CreatedAtActionResult>(result.Result);
                var employee = Assert.IsAssignableFrom<Employee>(res.Value);
                Assert.NotNull(employee);
            }
        }

        [Fact]
        public async Task DeleteEmployee_WithInvalidId_ShouldReturnNotFoundResult() {
            // Arrange
            using var context = new IdentityContext(_fixture.DbContextOptions);
            var userManager = _fixture.CreateUserManagerMock();
            userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);

            // Act
            var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
            var result = await sut.DeleteEmployee(1);

            // Assert
            userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
            userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Employee>()), Times.Never);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteEmployee_WithValidId_ShouldDeleteAndReturnEmployee() {
            // Arrange
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                context.Employees.Add(new Employee { Id = 1 });
                context.SaveChanges();
            }

            var userManager = _fixture.CreateUserManagerMock();
            ActionResult<Employee> result;

            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
                    .ReturnsAsync((string id) => context.FindAsync<Employee>(int.Parse(id)).Result);
                userManager.Setup(um => um.DeleteAsync(It.IsNotNull<Employee>()))
                    .ReturnsAsync(IdentityResult.Success)
                    .Callback<User>(u => {
                        context.Remove(u);
                        context.SaveChanges();
                    });

                // Act
                var sut = new EmployeesController(context, _fixture.Mapper, userManager.Object);
                result = await sut.DeleteEmployee(1);
            }

            // Assert
            using (var context = new IdentityContext(_fixture.DbContextOptions)) {
                userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
                userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Employee>()), Times.Once);
                Assert.Empty(context.Employees);
                var employee = Assert.IsAssignableFrom<Employee>(result.Value);
                Assert.NotNull(employee);
                Assert.Equal(1, employee.Id);
            }
        }

    }
}
