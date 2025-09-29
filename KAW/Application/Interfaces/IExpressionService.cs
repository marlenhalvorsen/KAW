using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IExpressionService
    {
        public void AddExpression(UserExpression expression);
        public List<UserExpression> GetExpression(string searchWord);
        public List<UserExpression> GetAllExpressions();
    }
}
