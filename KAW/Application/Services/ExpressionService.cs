using KAW.Domain.Models;
using System.Text.RegularExpressions;
using KAW.Application.Ports.Outbound;
using KAW.Application.Ports.Inbound;
using Microsoft.IdentityModel.Tokens;
using KAW.Application.Helpers;
using KAW.Application.Contracts.Request;
using KAW.Application.Contracts.Response;


namespace KAW.Application.Services
{
    public class ExpressionService : IExpressionService
    {
        private readonly IUserExpressionRepo _expressionRepo;

        public ExpressionService(IUserExpressionRepo repo)
        {
            _expressionRepo = repo;
        }

        public async Task DeleteExpression(int id, CancellationToken ct = default)
        {
            var entity = await _expressionRepo.FindExpressionById(id)
                ?? throw new KeyNotFoundException();

            await _expressionRepo.DeleteAsync(id, ct);
            await _expressionRepo.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<UserExpression>> FetchAllExpressions(CancellationToken ct = default)
        {
            var allExpressions = await _expressionRepo.GetAllAsync(ct);
            return allExpressions;
        }

        public async Task<IReadOnlyCollection<ExpressionResponse>> SearchExpression(string? query, CancellationToken ct)
        {
            var cleaned = string.IsNullOrWhiteSpace(query)
                ?null
                : Regex.Replace(query, @"[^\p{L}\p{N} \-']", "").Trim();

            var entities = string.IsNullOrEmpty(cleaned)
                ? await _expressionRepo.GetAllAsync(ct) 
                : await _expressionRepo.GetByInputAsync(cleaned, ct);
            return entities
                .Select(e => new ExpressionResponse(e.Id, e.Name, e.Description))
                .ToList();
        }

        public async Task<int> SaveExpression(string name, string description, CancellationToken ct = default)
        {
            if (await _expressionRepo.ExistsByName(name, ct))
                throw new InvalidOperationException("Expression already exists");

            var entity = new UserExpression(name, description);

            await _expressionRepo.AddAsync(entity, ct);
            await _expressionRepo.SaveChangesAsync();   
            
            return entity.Id;
        }

        public async Task UpdateExpression(int id, string name, string description, CancellationToken ct = default)
        {
            var entity = await _expressionRepo.FindExpressionById(id, ct)
                ?? throw new KeyNotFoundException();

            entity.Update(name, description);

            await _expressionRepo.SaveChangesAsync();
        }

    }
}
