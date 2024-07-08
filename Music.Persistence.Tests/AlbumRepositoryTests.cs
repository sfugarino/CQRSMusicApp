using Music.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using Music.Persistence.Repositories;
namespace Music.Persistence.Tests
{
    public class AlbumRepositoryTests
    {
        [Fact]
        public async Task Fetch_All_Artist_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("AlbumFetchAllDb");
            var dbContext = factory.CreateDbContext();
            var albumRepository = new AlbumRepository(dbContext);

            Guid artistId = Guid.NewGuid();

            await dbContext.Artists.AddRangeAsync(new[]
{
                new Artist { Id = artistId, Name = "Artist 1" },
            });

            await dbContext.Albums.AddRangeAsync(new[]
{
                new Album { Id = Guid.NewGuid(), Title = "Album 1", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
                new Album { Id = Guid.NewGuid(), Title = "Album 2", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
                new Album { Id = Guid.NewGuid(), Title = "Album 3", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
            });

            await dbContext.SaveChangesAsync();

            // Act
            var cancellationToken = new CancellationToken();
            var albumns = await albumRepository.GetAllAsync(cancellationToken);

            // Assert
            Assert.Equal(3, albumns.Length);
            Assert.Equal("Album 1", albumns[0].Title);
            Assert.Equal("Album 2", albumns[1].Title);
            Assert.Equal("Album 3", albumns[2].Title);

        }

        [Fact]
        public async Task Fetch_Artist_By_Id_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("AlbumFetchByIdDb");
            var dbContext = factory.CreateDbContext();
            var albumRepository = new AlbumRepository(dbContext);

            Guid artistId = Guid.NewGuid();

            await dbContext.Artists.AddRangeAsync(new[]
{
                new Artist { Id = artistId, Name = "Artist 1" },
            });

            await dbContext.Albums.AddRangeAsync(new[]
{
                new Album { Id = Guid.NewGuid(), Title = "Album 1", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
                new Album { Id = Guid.NewGuid(), Title = "Album 2", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
                new Album { Id = Guid.NewGuid(), Title = "Album 3", ArtistId= artistId, Genre="Bluegrass", ReleaseDate=DateTime.Now },
            });

            await dbContext.SaveChangesAsync();
            var album = dbContext.Albums.First();

            // Act
            var result = await albumRepository.GetByIdAsync(album.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(album.Id, result.Id);
            Assert.Equal(album.Title, result.Title);
        }

        [Fact]
        public async Task Add_Artist_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("AlbumAddDb");
            var dbContext = factory.CreateDbContext();
            var repository = new AlbumRepository(dbContext);

            var artistId = Guid.NewGuid();
            var artist = new Artist { Id = artistId, Name = "Artist 4" };
            dbContext.Artists.Add(artist);
            await dbContext.SaveChangesAsync();

            var album = new Album { Id = Guid.NewGuid(), Title = "Album 1", ArtistId = artistId, Genre = "Bluegrass", ReleaseDate = DateTime.Now };
            // Act
            await repository.AddAsync(album);
            await dbContext.SaveChangesAsync();
            var expected = dbContext.Albums.First();
            // Assert
            var result = await repository.GetByIdAsync(album.Id);

            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Title, result.Title);
        }

        [Fact]
        public async Task Remove_Artist_Successfully()
        {
            // Arrange
            using var factory = new InMemoryApplicationDbFactory("AlbumRemoveDb");
            var dbContext = factory.CreateDbContext();
            var repository = new AlbumRepository(dbContext);

            var artistId = Guid.NewGuid();
            var artist = new Artist { Id = artistId, Name = "Artist 4" };
            dbContext.Artists.Add(artist);

            var album = new Album { Id = Guid.NewGuid(), Title = "Album 1", ArtistId = artistId, Genre = "Bluegrass", ReleaseDate = DateTime.Now };
            dbContext.Albums.Add(album);
            await dbContext.SaveChangesAsync();

            var expected = dbContext.Albums.First();

            // Act
            repository.Remove(album);
            await dbContext.SaveChangesAsync();

            // Assert
            var result = await repository.GetByIdAsync(expected.Id);

            Assert.Null(result);
        }

    }
}