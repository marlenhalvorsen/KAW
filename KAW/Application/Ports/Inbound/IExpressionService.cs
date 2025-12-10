using KAW.Domain.Models;

namespace KAW.Application.Ports.Inbound
{
    public interface IExpressionService
    {
        Task<UserExpression>SaveExpression(UserExpression expression, CancellationToken ct = default);
        Task<UserExpression?> UpdateExpression(UserExpression updatedExpression, CancellationToken ct = default);
        Task<bool> DeleteExpression(int expressionId, CancellationToken ct = default);
        Task<IReadOnlyCollection<UserExpression>> FindExpression(string searchWord, CancellationToken ct = default);
        Task<IReadOnlyCollection<UserExpression>>FetchAllExpressions(CancellationToken ct = default);

    }
}
