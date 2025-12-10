using KAW.Domain.Models;
using KAW.Application.Helpers;
using System.Text.RegularExpressions;
using KAW.Application.Ports.Outbound;
using KAW.Application.Ports.Inbound;
using Microsoft.IdentityModel.Tokens;

namespace KAW.Application.Services
{
    public class ExpressionService : IExpressionService
    {
        private readonly IUserExpressionRepo _expressionRepo;

        public ExpressionService(IUserExpressionRepo repo)
        {
            _expressionRepo = repo;
        }

        public async Task<bool> DeleteExpression(int expressionId, CancellationToken ct = default)
        {
            if (expressionId <= 0)
            {
                return false;
            }

            var deleted = await _expressionRepo.DeleteAsync(expressionId);

            if (!deleted)
            {
                return false;
            }
            await _expressionRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyCollection<UserExpression>> FetchAllExpressions(CancellationToken ct = default)
        {
            var allExpressions = await _expressionRepo.GetAllAsync(ct);
            return allExpressions;
        }

        public async Task<IReadOnlyCollection<UserExpression>> FindExpression(string input, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(
                    "Searchword must contain at least one character.", 
                    nameof(input));

            }

            var cleaned = Regex.Replace(input, @"[^\p{L}\p{N} \-']", "")
                .Trim(); 

            var foundExpressions = await _expressionRepo.GetByInputAsync(cleaned, ct);

            if (!foundExpressions.Any())
            {
                return Array.Empty<UserExpression>();
            }
            return foundExpressions;
        }

        public async Task<UserExpression> SaveExpression(UserExpression expression, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(expression.Name) || string.IsNullOrWhiteSpace(expression.Description))
            {
                throw new ArgumentException(
                $"UserExpression must contain both a name and description: {expression}"
                );
            }

            UserExpressionSanitizer.CleanExpression(expression);

            await _expressionRepo.AddAsync(expression, ct);
            await _expressionRepo.SaveChangesAsync();   
        
            return expression;
        }

        public async Task<UserExpression?> UpdateExpression(UserExpression userExpression, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userExpression.Name))
                throw new ArgumentException(
                    "Expression name must not be empty.", nameof(userExpression.Name));

            // find entity
            var expression = await _expressionRepo.FindExpressionById(userExpression.Id, ct);
            if (expression == null)
                throw new KeyNotFoundException(
                    $"Expression with id {userExpression.Id} not found.");

            //copy new domain values into EF entity
            expression.Name = userExpression.Name;
            expression.Description = userExpression.Description;

            //clean entity with new values
            UserExpressionSanitizer.CleanExpression(expression);

            await _expressionRepo.UpdateAsync(expression, ct);
            await _expressionRepo.SaveChangesAsync();

            return expression;
        }

    }
}
