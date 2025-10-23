using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IUserExpressionRepo
    {
        Task AddAsync(UserExpression userExpression); 
        Task<UserExpression?> GetByInputAsync(string input);
        Task<UserExpression?> GetAllAsync(); 
        Task UpdateAsync(UserExpression userExpression);
        Task DeleteAsync(int id); 
        Task SaveChangesAsync(); 

    }
}
