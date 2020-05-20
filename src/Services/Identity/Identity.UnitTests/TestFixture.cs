using AutoMapper;
using Identity.Api.Mappings;
using Identity.Common.Data;
using Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Identity.UnitTests
{
    public class TestFixture : IDisposable
    {

        // Setup 
        // Runs before all tests among all test classes
        public TestFixture()
        {
            DbContextOptions = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase("test")
                .Options;

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });
            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        // Cleanup 
        // Runs after all tests among all test classes
        public void Dispose() { }

        public Mock<UserManager<User>> CreateUserManagerMock()
                => new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    null, null, null, null, null, null, null, null);

        public DbContextOptions<IdentityContext> DbContextOptions { get; private set; }
        public IMapper Mapper { get; private set; }

    }

    [CollectionDefinition("Tests")]
    public class CollectionFixture : ICollectionFixture<TestFixture> { }

}
