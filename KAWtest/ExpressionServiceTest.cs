using Moq;
using KAW.Application.Interfaces;
using KAW.Application.Services;
using KAW.Domain.Models;
using System.Security.Cryptography.X509Certificates;

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

    [TestMethod]
    public void GetExpressions_ShouldReturnExpressions_WhenSearchWordIsValid()
    {
        //ARRANGE
        var mockRepository = new Mock<IUserExpressionRepo>();
        IExpressionService expressionService = new ExpressionService(mockRepository.Object);


        string searchWord = "Kaw";
        var exptectedList = new List<UserExpression>
        {
            new UserExpression { Name = "Kaw", Description = "Noget er ånddssvagt" }
        };
        mockRepository.Setup(x => x.GetExpressions(searchWord)).Returns(exptectedList);
        //ACT
        var result = expressionService.GetExpressions(searchWord);

        //ASSERT
        mockRepository.Verify(x => x.GetExpressions(searchWord), Times.Once());
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Kaw", result[0].Name);


    }
    [TestMethod]
    public void AddExpression_ShouldThrowException_WhenEmptyEntry()
    {
        //ARRANGE

        var mockRepository = new Mock<IUserExpressionRepo>();
        IExpressionService expressionService = new ExpressionService(mockRepository.Object);
        var expr = new UserExpression { Name = " ", Description = "Der står noget her " };
        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentException> (() => expressionService.AddExpression(expr));
    }

    [TestMethod]
    public void GetExpressions_ShouldTrimSearchWord_WhenThereIsSpacing()
    {
        //ARRANGE
        var mockRepository = new Mock<IUserExpressionRepo>();
        IExpressionService expressionService = new ExpressionService(mockRepository.Object);
        mockRepository
            .Setup(x => x.GetExpressions(It.IsAny<string>()))
            .Returns(new List<UserExpression>());

        //ACT
        expressionService.GetExpressions(" Kaw");

        //ASSERT
        mockRepository.Verify(x => x.GetExpressions(It.Is<string> (s => s == "Kaw")), 
            Times.Once);
    }
}
