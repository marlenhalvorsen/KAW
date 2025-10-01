using KAW.Application.Interfaces;
using KAW.Domain.Models;
namespace KAW.Application.Services
{
    public class ExpressionService : IExpressionService
    {
        private readonly IUserExpressionRepo _expressionRepo;

        public ExpressionService(IUserExpressionRepo expressionRepo)
        {
            _expressionRepo = expressionRepo;   
        }
        public void AddExpression(UserExpression expression)
        {
            if (String.IsNullOrWhiteSpace(expression.Name))
            { 
                throw new ArgumentException("Expressions must have a name", nameof(expression)); 
            }
            _expressionRepo.Save(expression);
        }

        public List<UserExpression> GetAllExpressions()
        {
            List<UserExpression> userExpressions = _expressionRepo.GetAllExpressions();
            return userExpressions;
        }

        public List<UserExpression> GetExpressions(string searchWord)
        {
            return _expressionRepo.GetExpressions(searchWord);
        }
    }
}
