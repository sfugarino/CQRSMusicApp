﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Music.Domain.Entities;
using Music.Persistence.Repositories;

namespace Music.Persistence.Tests
{
    public class ArtistRepositoryTest
    {

        [Fact]
        public async Task Fetch_All_Artist_Successfully()
        {
            // Arrange
            var mockCache = new Mock<IDistributedCache>();

            using var factory = new InMemoryApplicationDbFactory("ArtistFetchAllDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext, mockCache.Object);

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
            var mockCache = new Mock<IDistributedCache>();

            using var factory = new InMemoryApplicationDbFactory("ArtistFetchByIdDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext, mockCache.Object);

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
            var mockCache = new Mock<IDistributedCache>();

            using var factory = new InMemoryApplicationDbFactory("ArtistsAddDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext, mockCache.Object);
            var artist = new Artist { Id = Guid.NewGuid(), Name = "Artist 4" };

            // Act
            await repository.AddAsync(artist);
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
            var mockCache = new Mock<IDistributedCache>();

            using var factory = new InMemoryApplicationDbFactory("ArtistRemoveDb");
            var dbContext = factory.CreateDbContext();
            var repository = new ArtistRepository(dbContext, mockCache.Object);

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
