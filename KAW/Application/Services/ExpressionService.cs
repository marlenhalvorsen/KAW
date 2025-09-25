using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Xml;
namespace KAW.Application.Services
{
    public class ExpressionService : IExpressionService
    {
        private readonly IRepo expressionRepo; 
        public void AddExpression(UserExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("Udtrykket må indeholde en eller flere tegn");
            }
            else expressionRepo.Save(expression);
        }

        public List<UserExpression> GetAllExpressions()
        {
            throw new NotImplementedException();
        }

        public UserExpression GetExpression(string searchWord)
        {
            throw new NotImplementedException();
        }
    }
}
