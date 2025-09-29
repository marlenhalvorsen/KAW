using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IUserExpressionRepo
    {
        public void Save(UserExpression expression);
        public List<UserExpression> GetExpressions(string word);
        public List<UserExpression> GetAllExpressions(); 
    }
}
