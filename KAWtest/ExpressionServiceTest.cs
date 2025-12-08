using Xunit;
using Moq;
using System.Threading;
using KAW.Application.Services;
using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Threading.Tasks;
using System;

public class ExpressionServiceTests
{
    private readonly Mock<IUserExpressionRepo> _repoMock;
    private readonly ExpressionService _service;

    public ExpressionServiceTests()
    {
        _repoMock = new Mock<IUserExpressionRepo>();
        _service = new ExpressionService(_repoMock.Object);
    }

    [Fact]
    public async Task DeleteExpression_ShouldReturnFalse_WhenIdIsInvalid()
    {
        // Act
        var result = await _service.DeleteExpression(0);

        // Assert
        Assert.False(result);
        _repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteExpression_ShouldReturnTrue_WhenDeletionIsSuccessful()
    {
        // Arrange
        _repoMock
            .Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteExpression(1);

        // Assert
        Assert.True(result);
        _repoMock.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
