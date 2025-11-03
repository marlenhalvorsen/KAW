using KAW.Domain.Models;

namespace KAW.Application.Interfaces
{
    public interface IUserExpressionRepo
    {
        Task AddAsync(UserExpression userExpression, CancellationToken ct = default); 
        Task<IEnumerable<UserExpression>> GetByInputAsync(string input, CancellationToken ct = default);
        Task<IEnumerable<UserExpression>> GetAllAsync(CancellationToken ct = default); 
        Task<UserExpression?> UpdateAsync(UserExpression userExpression, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default); 
        Task SaveChangesAsync(CancellationToken ct = default); 
        Task<UserExpression?> FindExpressionById(int id, CancellationToken ct = default);

    }
}
