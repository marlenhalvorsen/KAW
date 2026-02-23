using KAW.Application.Contracts.Response;
using KAW.Domain.Models;

namespace KAW.Application.Ports.Inbound
{
    public interface IExpressionService
    {
        Task<int>SaveExpression(string name, string description, CancellationToken ct = default);
        Task UpdateExpression(int id, string name, string description, CancellationToken ct = default);
        Task DeleteExpression(int id, CancellationToken ct = default);
        Task<IReadOnlyCollection<ExpressionResponse>> SearchExpression(string? query, CancellationToken ct = default);
        Task<IReadOnlyCollection<UserExpression>>FetchAllExpressions(CancellationToken ct = default);

    }
}
