using Moq;
using Music.Domain.Entities;

namespace Music.Persistence.Tests
{
    public class ArtistRepositoryTest
    {
        [Fact]
        public async Task Fetch_All_Artist_Successfully()
        {
            // Arrange
            var factory = new InMemoryApplicationDbFactory();
            var dbContext = factory.CreateDbContext();

            dbContext.Artists.AddRange(new[]
            {
                new Artist { Id = Guid.NewGuid(), Name = "Artist 1" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 2" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 3" },
            });

            dbContext.SaveChanges();

            var repository = new ArtistRepository(dbContext);

            // Act
            var cancellationToken = new CancellationToken();
            var artists = await repository.GetAllAsync(cancellationToken);

            // Assert
            Assert.Equal(3, artists.Length);
            Assert.Equal("Artist 1", artists[0].Name);
            Assert.Equal("Artist 2", artists[1].Name);
            Assert.Equal("Artist 3", artists[2].Name);

        }
    }
}
