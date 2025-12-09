using FluentAssertions;
using KAW.Application.Ports.Outbound;
using KAW.Application.Services;
using KAW.Domain.Models;
using Moq;
using Xunit;

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
    [Fact]
    public async Task DeleteExpression_ShouldReturnFalse_WhenDeletionFailed()
    {
        // Arrange
        _repoMock
            .Setup(r => r.DeleteAsync(2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act 
        var result = await _service.DeleteExpression(2);

        // Assert
        Assert.False(result);
        _repoMock.Verify(r => r.DeleteAsync(2, It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task FetchAllExpressions_ShouldReturnExpressions()
    {
        // Arrange
        var testList = new List<UserExpression>
        {
            new UserExpression{ Id = 1, Description = "something", Name = "new"},
            new UserExpression{ Id = 1, Description = "somethingElse", Name = "newElse"},
        };

        _repoMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(testList);

        // Act 
        var result = await _service.FetchAllExpressions();

        // Assert
        result.Should().BeEquivalentTo(testList, options => options.WithStrictOrdering());
        //checks for same number of elements, elementvalues and structure
        result.Should().BeEquivalentTo(testList);
        _repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
    

}
