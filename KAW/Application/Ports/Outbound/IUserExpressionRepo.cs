using KAW.Domain.Models;

namespace KAW.Application.Ports.Outbound
{
    public interface IUserExpressionRepo
    {
        Task AddAsync(UserExpression userExpression, CancellationToken ct = default); 
        Task<IReadOnlyCollection<UserExpression>> GetByInputAsync(string input, CancellationToken ct = default);
        Task<IReadOnlyCollection<UserExpression>> GetAllAsync(CancellationToken ct = default); 
        Task UpdateAsync(UserExpression userExpression, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default); 
        Task SaveChangesAsync(CancellationToken ct = default); 
        Task<UserExpression?> FindExpressionById(int id, CancellationToken ct = default);

    }
}
