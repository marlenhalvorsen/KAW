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

    

}
