using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Diagnostics.Eventing.Reader;

namespace KAW.Infrastructure.Repository
{
    public class ExpressionRepo : IUserExpressionRepo
    {
        List<UserExpression> _expressions = new List<UserExpression>();
        public List<UserExpression> GetAllExpressions(string word)
        {
            throw new NotImplementedException();
        }

        public UserExpression GetExpression(string word)
        {
            throw new NotImplementedException();
        }

        public void Save(UserExpression expression)
        {
            _expressions.Add(expression);
        }
    }
}
