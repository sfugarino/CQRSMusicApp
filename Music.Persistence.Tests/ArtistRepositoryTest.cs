using Microsoft.EntityFrameworkCore;
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
            using var factory = new InMemoryApplicationDbFactory("FetchAllDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext);

            await dbContext.Artists.AddRangeAsync(new[]
{
                new Artist { Id = Guid.NewGuid(), Name = "Artist 1" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 2" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 3" },
            });

            await dbContext.SaveChangesAsync();

            // Act
            var cancellationToken = new CancellationToken();
            var artists = await repository.GetAllAsync(cancellationToken);



            // Assert
            Assert.Equal(3, artists.Length);
            Assert.Equal("Artist 1", artists[0].Name);
            Assert.Equal("Artist 2", artists[1].Name);
            Assert.Equal("Artist 3", artists[2].Name);

        }

        [Fact]
        public async Task Fetch_Artist_By_Id_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("FetchByIdDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext);

            await dbContext.Artists.AddRangeAsync(new[]
{
                new Artist { Id = Guid.NewGuid(), Name = "Artist 1" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 2" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 3" },
            });

            await dbContext.SaveChangesAsync();
            var artist = dbContext.Artists.First();

            // Act
            var result = await repository.GetByIdAsync(artist.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(artist.Id, result.Id);
            Assert.Equal(artist.Name, result.Name);
        }

        [Fact]
        public async Task Add_Artist_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("AddDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext);
            var artist = new Artist { Id = Guid.NewGuid(), Name = "Artist 4" };

            // Act
            repository.Add(artist);
            await dbContext.SaveChangesAsync();

            // Assert
            var result = await repository.GetByIdAsync(artist.Id);
            Assert.NotNull(result);
            Assert.Equal(artist.Id, result.Id);
            Assert.Equal(artist.Name, result.Name);
        }

        [Fact]
        public async Task Remove_Artist_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("RemoveDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext);

            await dbContext.Artists.AddRangeAsync(new[]
{
                new Artist { Id = Guid.NewGuid(), Name = "Artist 1" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 2" },
                new Artist { Id = Guid.NewGuid(), Name = "Artist 3" },
            });

            await dbContext.SaveChangesAsync();
            var artist = dbContext.Artists.First();

            // Act
            repository.Remove(artist);
            await dbContext.SaveChangesAsync();

            // Assert
            var result = await repository.GetByIdAsync(artist.Id);
            Assert.Null(result);
        }
    }
}
