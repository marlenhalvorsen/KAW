using Moq;
using KAW.Application.Interfaces;
using KAW.Application.Services;
using KAW.Domain.Models;

namespace KAWtest;

[TestClass]
public class ExpressionServiceTest
{
    [TestMethod]
    public void GetAllExpressions_ShouldReturnList()
    {
        //ARRANGE
        var mockRepository = new Mock<IUserExpressionRepo>();

        var expectedList = new List<UserExpression>
        {
            new UserExpression { Name = "Kaw", Description = "Noget er åndssvagt" },
            new UserExpression { Name = "Pangel", Description = "Billig lort" }
        };
        mockRepository.Setup(x => x.GetAllExpressions()).Returns(expectedList);
        IExpressionService expressionService = new ExpressionService(mockRepository.Object);


        //ACT
        List<UserExpression> foundExpressions = expressionService.GetAllExpressions();

        //ASSERT
        Assert.AreEqual(2, foundExpressions.Count);
    }
    [TestMethod]
    public void AddExpression_ShouldCallSave_WhenExpressionIsValid()
    {
        //ARRANGE
        var mockRepository = new Mock<IUserExpressionRepo>();
        IExpressionService expressionService = new ExpressionService(mockRepository.Object);

        UserExpression userExpression = new UserExpression { Name = "Kaw", Description = "Noget er åndssvagt" };

        //ACT
        expressionService.AddExpression(userExpression);

        //ASSERT
        mockRepository.Verify(x => x.Save(It.IsAny<UserExpression>()), Times.Once());
        mockRepository.Verify(x => x.Save(
            It.Is<UserExpression>(e => e.Name == "Kaw" && e.Description == "Noget er åndssvagt")),
            Times.Once());
        mockRepository.VerifyNoOtherCalls();
    }


}
