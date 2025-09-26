using KAW.Application.Services;
using KAW.Infrastructure.Repository;
using KAW.Application.Interfaces;
using KAW.Domain.Models;

namespace KAWtest
{
    [TestClass]
    public sealed class UserExpressionRepoTest
    {
        [TestMethod]
        public void GetAllExpressions_ReturnsList()
        {
            //ARRANGE
            IUserExpressionRepo userExpressionRepo = new ExpressionRepo();
            userExpressionRepo.Save(new UserExpression { Name = "Kaw", Description = "Noget er svært" });
            userExpressionRepo.Save(new UserExpression { Name = "Udtryk", Description = "Bare noget" });

            //ACT
            List<UserExpression> foundExpressions = new List<UserExpression>();
            foundExpressions = userExpressionRepo.GetAllExpressions();

            //ASSERT
            Assert.AreEqual(2, foundExpressions.Count);
            Assert.AreEqual("Kaw", foundExpressions[0].Name);
            Assert.AreEqual("Udtryk", foundExpressions[1].Name);
        }
    }
}
