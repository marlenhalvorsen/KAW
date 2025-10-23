using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IUserExpressionRepo
    {
        Task AddAsync(UserExpression userExpression); 
        Task<IEnumerable<UserExpression>> GetByInputAsync(string input);
        Task<IEnumerable<UserExpression>> GetAllAsync(); 
        Task UpdateAsync(UserExpression userExpression);
        Task<bool> DeleteAsync(int id); 
        Task SaveChangesAsync(); 

    }
}
