using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IUserExpressionRepo
    {
        public void Save(UserExpression expression);
        public UserExpression GetExpression(string word);
        public List<UserExpression> GetAllExpressions(string word); 
    }
}
