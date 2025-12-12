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
    public async Task DeleteExpression_ShouldThroException_WhenKeyNotFound()
    {
        // Arrange
        Func<Task> act = () => _service.DeleteExpression(0);

        // Act and Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
    [Fact]
    public async Task DeleteExpression_ShouldDeleteExpression_WhenInputIsValid()
    {
        // Arrange
        var name = "name";
        var description = "description";

        _repoMock
            .Setup(r => r.FindExpressionById(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserExpression(name, description));

        _repoMock
            .Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteExpression(1);

        // Assert
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);

    }
}
