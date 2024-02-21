using Microsoft.EntityFrameworkCore;
using Moq;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.Scripts;
using Xunit;

public class ClientServiceTests
{
    [Fact]
    public async Task Get_ClientExists_ReturnsClient()
    {
        // Arrange
        var dbContextMock = new Mock<StorageDbContext>();
        var clientService = new ClientService(dbContextMock.Object);

        var existingClientId = "1";
        var existingClient = new Client { Id = existingClientId, FirstName = "John" };

        dbContextMock.Setup(x => x.Clients.FindAsync(existingClientId)).ReturnsAsync(existingClient);

        // Act
        var result = await clientService.Get(existingClientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingClientId, result.Id);
        Assert.Equal("John", result.FirstName);
    }
}