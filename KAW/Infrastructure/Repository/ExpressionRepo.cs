using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Diagnostics.Eventing.Reader;

namespace KAW.Infrastructure.Repository
{
    public class ExpressionRepo : IUserExpressionRepo
    {
        List<UserExpression> _expressions = new List<UserExpression>();
        public List<UserExpression> GetAllExpressions()
        {
            return _expressions;
        }

        public List<UserExpression> GetExpressions(string word)
        {
            if (string.IsNullOrEmpty(word)) return _expressions;

            //helper method to check for null cases in source
            bool ContainsIgnoreCase(string? source, string? value) =>
                !string.IsNullOrEmpty(source) &&
                source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0;
            
            return _expressions
                .Where(e => e != null && (ContainsIgnoreCase(e.Name, word) ||
                ContainsIgnoreCase(e.Description, word)))
                .ToList();
        }

        public void Save(UserExpression expression)
        {
            _expressions.Add(expression);
        }
    }
}
