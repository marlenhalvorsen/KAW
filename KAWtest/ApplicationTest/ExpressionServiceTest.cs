using FluentAssertions;
using KAW.Application.Ports.Outbound;
using KAW.Application.Services;
using KAW.Domain.Models;
using KAW.Infrastructure.Repository;
using Moq;
using Sprache;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
    [Fact]
    public async Task FindExpression_ShouldReturnExpression_WhenInputIsValid()
    {
        // Arrange 
        var input = "something";
        var listOfExpression = new List<UserExpression> { 
            new UserExpression 
            { Id = 1, Name = "something", Description = "somethingElse" } };
        _repoMock
            .Setup(r => r.GetByInputAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(listOfExpression);

        // Act
        var result = await _service.FindExpression(input);

        // Assert
        result.Should().BeEquivalentTo(listOfExpression);
        _repoMock.Verify(r => r.GetByInputAsync(input, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task FindExpression_ShpuldReturnEmptyList_WhenInputIsValid()
    {
        // Arrange
        var input = "kaw";
        var emptyList = new List<UserExpression>();
        _repoMock
            .Setup(r => r.GetByInputAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyList);

        // Act
        var emptyResult = await _service.FindExpression(input);

        // Assert
        emptyResult.Should().BeEmpty();
        _repoMock.Verify(r => r.GetByInputAsync(input, It.IsAny<CancellationToken>()),Times.Once);
    }

    [Fact]
    public async Task FindExpression_ShouldThrowEception_WhenInputIsNullOrWhiteSpace()
    {
        // Arrange
        var input = string.Empty;
        Func<Task> act = () => _service.FindExpression(input);

        // Act and Assert
        await act.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("Searchword must contain at least one character.*");

        var exception = await act.Should().ThrowAsync<ArgumentException>();
        exception.Which.ParamName.Should().Be("input");
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("Kaw", null)]
    [InlineData(null, "Noget der er træls")]
    [InlineData("", "Noget der er træls")]
    [InlineData("Kaw", "")]
    public async Task SaveExpression_ShouldThrowArgumentException_WhenNameOrDescriptionIsMissing(
        string? name,
        string? description)
    {
        // Arrange
        var expr = new UserExpression { Name = name, Description = description };
        Func<Task> act = () => _service.SaveExpression(expr);

        // Act & Assert
        await act.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("UserExpression must contain both a name and description*");
    }

    [Fact]
    public async Task SaveExpression_ShouldCallAddAsyncOnce_WhenExpressionAreSaved()
    {
        // Arrange
        var userExpression = new UserExpression { Name = "Kaw", Description = "Noget der er træls" };

        // Act
        var result = await _service.SaveExpression(userExpression);

        // Assert
        _repoMock.Verify(r => r.AddAsync(userExpression, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveExpression_ShouldCallSaveChangesOnce_WhenExpressionAreSaved()
    {
        // Arrange
        var userExpression = new UserExpression { Name = "Kaw", Description = "Noget der er træls" };

        // Act
        var result = await _service.SaveExpression(userExpression, It.IsAny<CancellationToken>());

        // Assert
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task SaveExpression_ShouldReturnExpression_WhenExpressionAreSavedSuccessfully()
    {
        // Arrange
        var userExpression = new UserExpression { Name = "Kaw", Description = "Noget der er træls" };

        // Act
        var result = await _service.SaveExpression(userExpression, It.IsAny<CancellationToken>());

        // Assert
        result.Should().Be(userExpression);
    }
    [Fact]
    public async Task UpdateExpression_ShouldThrowException_IfNameOfExpressionIsNullOrWhitespace()
    {
        // Arrange
        var userExpression = new UserExpression { Description = "Noget der er træls" };
        Func<Task> act = () => _service.UpdateExpression(userExpression, It.IsAny<CancellationToken>());

        // Act and Assert
        await act.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("Expression name must not be empty.*");
    }
    [Fact]
    public async Task UpdateExpression_ShouldThrowException_WhenKeyNotFound()
    {
        // Arrange
        var userExpression = new UserExpression { Name = "Kaw" };
        _repoMock
            .Setup(r => r.FindExpressionById(userExpression.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserExpression?)null);

        Func<Task> act = () => _service.UpdateExpression(userExpression, It.IsAny<CancellationToken>());
        
        // Act and Assert
        await act.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Expression with id {userExpression.Id} not found*");
    }
    [Fact]
    public async Task UpdateExpression_ShouldReturnExpression_WhenSavedSuccessfully()
    {
        // Arrange
        var userExpression = new UserExpression { Id = 1, Name = "Kaw", Description = "Noget der er træls" };
        var existingUserExpression = new UserExpression { Id = 1, Name = "old name", Description = "old description" };

        _repoMock
            .Setup(r => r.FindExpressionById(userExpression.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUserExpression);

        _repoMock
          .Setup(r => r.UpdateAsync(existingUserExpression, It.IsAny<CancellationToken>()))
          .Returns(Task.CompletedTask);

        _repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        // Act
        var result = await _service.UpdateExpression(userExpression);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(existingUserExpression);

        result.Name.Should().Be(userExpression.Name);
        result.Description.Should().Be(userExpression.Description);

        _repoMock.Verify(r => r.FindExpressionById(1, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.UpdateAsync(existingUserExpression, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
