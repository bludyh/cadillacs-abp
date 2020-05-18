using Identity.Common.Data;
using System;
using Xunit;

namespace Identity.UnitTests
{
    [Collection("Tests")]
    public class TeachersControllerTest : IDisposable
    {

        private readonly TestFixture _fixture;

        // Setup 
        // Runs before each test in this test class
        public TeachersControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        // Cleanup 
        // Runs after each test in this test class
        public void Dispose()
        {
            using var context = new IdentityContext(_fixture.DbContextOptions);
            context.Database.EnsureDeleted();
        }

        //[Fact]
        //public async Task GetTeacher_WithInvalidId_ShouldReturnNotFoundResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);

        //    // Act
        //    var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.GetTeacher(1);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetTeacher_WithValidId_ShouldReturnTeacher() {
        //    // Arrange
        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Teachers.Add(new Teacher { Id = 1, SchoolId = "school1", RoomId = "1", BuildingId = "A" });
        //        context.Schools.Add(new School { Id = "school1" });
        //        context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
        //        context.Buildings.Add(new Building { Id = "A" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    ActionResult<EmployeeReadDto> result;

        //    // Use a clean instance of the context to run the test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);

        //        // Act
        //        var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.GetTeacher(1);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    var teacher = Assert.IsAssignableFrom<EmployeeReadDto>(result.Value);
        //    Assert.NotNull(teacher);
        //    Assert.NotNull(teacher.School); // Test if explicit loading is working
        //    Assert.NotNull(teacher.Room); // Test if explicit loading is working
        //    Assert.Equal(1, teacher.Id);
        //}

        //public static IEnumerable<object[]> Generate_PutTeacher_InvalidRequests() {
        //    return new List<object[]> { 
        //        new object[] { new EmployeeUpdateDto() }, // Missing required fields
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1" } }, // Incomplete Room foreign key
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", BuildingId = "A" } } // Incomplete Room foreign key
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PutTeacher_InvalidRequests))]
        //public async Task PutTeacher_WithInvalidRequest_ShouldReturnBadRequestResult(EmployeeUpdateDto dto) {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();

        //    // Act
        //    var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PutTeacher(1, dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Never);
        //    userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    Assert.IsType<BadRequestResult>(result);
        //}

        //public static IEnumerable<object[]> Generate_PutTeacher_RequestsThatResultInNotFound() {
        //    return new List<object[]> {
        //        new object[] { 2, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test" } }, // Invalid Id
        //        new object[] { 1, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1" } }, // Invalid School foreign key
        //        new object[] { 1, new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1", BuildingId = "A" } } // Invalid Room foreign key
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PutTeacher_RequestsThatResultInNotFound))]
        //public async Task PutTeacher_WithRequestThatResultsInNotFound_ShouldReturnNotFoundResult(int teacherId, EmployeeUpdateDto dto) {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Teachers.Add(new Teacher { Id = 1 });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    IActionResult result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);

        //        // Act
        //        var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PutTeacher(teacherId, dto);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync(teacherId.ToString()), Times.Once);
        //    userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //public static IEnumerable<object[]> Generate_PutTeacher_ValidRequests() {
        //    return new List<object[]> { 
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test" } }, // Teacher who is not in any schools and does not have a room
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1" } }, // Teacher who does not have a room
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", RoomId = "1", BuildingId = "A" } }, // Teacher who is not in any schools
        //        new object[] { new EmployeeUpdateDto { FirstName = "test", LastName = "test", Initials = "test", SchoolId = "school1", RoomId = "1", BuildingId = "A" } }, // Teacher who is not in any schools
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PutTeacher_ValidRequests))]
        //public async Task PutTeacher_WithValidRequest_ShouldUpdateTeacherAndReturnNoContentResult(EmployeeUpdateDto dto) {
        //    // Arrange
        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Teachers.Add(new Teacher { Id = 1 });
        //        context.Schools.Add(new School { Id = "school1" });
        //        context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    IActionResult result;

        //    // Use a clean instance of the context to run the test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);
        //        userManager.Setup(um => um.UpdateAsync(It.IsNotNull<Teacher>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Update(u);
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PutTeacher(1, dto);
        //    }

        //    // Use a separate instance of the context to verify test
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        // Assert
        //        userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //        userManager.Verify(um => um.UpdateAsync(It.IsNotNull<Teacher>()), Times.Once);
        //        var teacher = await context.FindAsync<Teacher>(1);
        //        Assert.Equal(dto.FirstName, teacher.FirstName);
        //        Assert.Equal(dto.LastName, teacher.LastName);
        //        Assert.Equal(dto.Initials, teacher.Initials);
        //        Assert.Equal(dto.SchoolId, teacher.SchoolId);
        //        Assert.Equal(dto.RoomId, teacher.RoomId);
        //        Assert.Equal(dto.BuildingId, teacher.BuildingId);
        //        Assert.IsType<NoContentResult>(result);
        //    }
        //}

        //public static IEnumerable<object[]> Generate_PostTeacher_InvalidRequests() {
        //    return new List<object[]> { 
        //        new object[] { new EmployeeCreateDto() },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1" } },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", BuildingId = "A" } }
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PostTeacher_InvalidRequests))]
        //public async Task PostTeacher_WithInvalidRequest_ShouldReturnBadRequest(EmployeeCreateDto dto) {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();

        //    // Act
        //    var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PostTeacher(dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Never);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), It.IsNotNull<string>()), Times.Never);
        //    Assert.IsType<BadRequestResult>(result.Result);
        //}

        //[Fact]
        //public async Task PostTeacher_WithExistingInfo_ShouldReturnConflictResult() {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Teachers.Add(new Teacher { Email = "test@gmail.com" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    var dto = new EmployeeCreateDto {
        //        FirstName = "test",
        //        LastName = "test",
        //        Initials = "test",
        //        Email = "test@gmail.com"
        //    };
        //    ActionResult<Teacher> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);

        //        // Act
        //        var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.PostTeacher(dto);
        //    }

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), It.IsNotNull<string>()), Times.Never);
        //    Assert.IsType<ConflictResult>(result.Result);
        //}

        //public static IEnumerable<object[]> Generate_PostTeacher_RequestsWithInvalidForeignKeys() {
        //    return new List<object[]> { 
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1" } },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1", BuildingId = "A" } }
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PostTeacher_RequestsWithInvalidForeignKeys))]
        //public async Task PostStudent_WithNonExistentProgram_ShouldReturnNotFoundResult(EmployeeCreateDto dto) {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();

        //    // Act
        //    userManager.Setup(um => um.FindByEmailAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);

        //    var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.PostTeacher(dto);

        //    // Assert
        //    userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //    userManager.Verify(um => um.CreateAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), It.IsNotNull<string>()), Times.Never);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //public static IEnumerable<object[]> Generate_PostTeacher_ValidRequests() {
        //    return new List<object[]> { 
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com" } },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1" } },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", RoomId = "1", BuildingId = "A" } },
        //        new object[] { new EmployeeCreateDto { FirstName = "test", LastName = "test", Initials = "test", Email = "test@gmail.com", SchoolId = "school1", RoomId = "1", BuildingId = "A" } },
        //    };
        //}

        //[Theory]
        //[MemberData(nameof(Generate_PostTeacher_ValidRequests))]
        //public async Task PostTeacher_WithValidInfo_ShouldAddTeacherAndReturnCreatedAtActionResult(EmployeeCreateDto dto) {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Schools.Add(new School { Id = "school1" });
        //        context.Rooms.Add(new Room { Id = "1", BuildingId = "A" });
        //        context.Buildings.Add(new Building { Id = "A" });
        //        context.Roles.Add(new IdentityRole<int> { Id = 1, Name = "Employee" });
        //        context.Roles.Add(new IdentityRole<int> { Id = 2, Name = "Teacher" });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    var mockedContext = new Mock<IdentityContext>();
        //    ActionResult<Teacher> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        mockedContext.Setup(c => c.FindAsync<School>(It.IsNotNull<string>()))
        //            .ReturnsAsync((object[] id) => context.FindAsync<School>(id).Result);
        //        mockedContext.Setup(c => c.FindAsync<Room>(It.IsNotNull<string>(), It.IsNotNull<string>()))
        //            .ReturnsAsync((object[] id) => context.FindAsync<Room>(id).Result);
        //        mockedContext.Setup(c => c.GetNextPcn()).ReturnsAsync("1");

        //        userManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
        //            .ReturnsAsync((string email) => context.Users.SingleOrDefaultAsync(u => u.Email == email).Result);
        //        userManager.Setup(um => um.CreateAsync(It.IsNotNull<Teacher>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Add(u);
        //                context.SaveChanges();
        //            });
        //        userManager.Setup(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), "Employee"))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User, string>((u, r) => {
        //                context.Add(new IdentityUserRole<int> { UserId = u.Id, RoleId = 1 });
        //                context.SaveChanges();
        //            });
        //        userManager.Setup(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), "Teacher"))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User, string>((u, r) => {
        //                context.Add(new IdentityUserRole<int> { UserId = u.Id, RoleId = 2 });
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new TeachersController(mockedContext.Object, _fixture.Mapper, userManager.Object);
        //        result = await sut.PostTeacher(dto);
        //    }

        //    // Assert
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        //        userManager.Verify(um => um.CreateAsync(It.IsNotNull<Teacher>()), Times.Once);
        //        userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), "Employee"), Times.Once);
        //        userManager.Verify(um => um.AddToRoleAsync(It.IsNotNull<Teacher>(), "Teacher"), Times.Once);
        //        mockedContext.Verify(c => c.FindAsync<School>(dto.SchoolId), Times.AtMostOnce);
        //        mockedContext.Verify(c => c.FindAsync<Room>(dto.RoomId), Times.AtMostOnce);
        //        mockedContext.Verify(c => c.GetNextPcn(), Times.Once);
        //        Assert.Equal(1, context.Teachers.Count());
        //        var res = Assert.IsType<CreatedAtActionResult>(result.Result);
        //        var teacher = Assert.IsAssignableFrom<Teacher>(res.Value);
        //        Assert.NotNull(teacher);
        //    }
        //}

        //[Fact]
        //public async Task DeleteTeacher_WithInvalidId_ShouldReturnNotFoundResult() {
        //    // Arrange
        //    using var context = new IdentityContext(_fixture.DbContextOptions);
        //    var userManager = _fixture.CreateUserManagerMock();
        //    userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //        .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);

        //    // Act
        //    var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //    var result = await sut.DeleteTeacher(1);

        //    // Assert
        //    userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //    userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Teacher>()), Times.Never);
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task DeleteTeacher_WithValidId_ShouldDeleteAndReturnTeacher() {
        //    // Arrange
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        context.Teachers.Add(new Teacher { Id = 1 });
        //        context.SaveChanges();
        //    }

        //    var userManager = _fixture.CreateUserManagerMock();
        //    ActionResult<Teacher> result;

        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Setup(um => um.FindByIdAsync(It.IsNotNull<string>()))
        //            .ReturnsAsync((string id) => context.FindAsync<Teacher>(int.Parse(id)).Result);
        //        userManager.Setup(um => um.DeleteAsync(It.IsNotNull<Teacher>()))
        //            .ReturnsAsync(IdentityResult.Success)
        //            .Callback<User>(u => {
        //                context.Remove(u);
        //                context.SaveChanges();
        //            });

        //        // Act
        //        var sut = new TeachersController(context, _fixture.Mapper, userManager.Object);
        //        result = await sut.DeleteTeacher(1);
        //    }

        //    // Assert
        //    using (var context = new IdentityContext(_fixture.DbContextOptions)) {
        //        userManager.Verify(um => um.FindByIdAsync($"{1}"), Times.Once);
        //        userManager.Verify(um => um.DeleteAsync(It.IsNotNull<Teacher>()), Times.Once);
        //        Assert.Empty(context.Teachers);
        //        var teacher = Assert.IsAssignableFrom<Teacher>(result.Value);
        //        Assert.NotNull(teacher);
        //        Assert.Equal(1, teacher.Id);
        //    }
        //}


    }
}
