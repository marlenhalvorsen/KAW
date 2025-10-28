using KAW.Application.Interfaces;
using KAW.Domain.Models;
using Microsoft.EntityFrameworkCore;
using KAW.Infrastructure.Persistence;

namespace KAW.Infrastructure.Repository
{
    public sealed class ExpressionRepo : IUserExpressionRepo
    {
        private readonly AppDbContext _appDbContext;

        public ExpressionRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(UserExpression userExpression, CancellationToken ct)
        {
            await _appDbContext.UserExpressions.AddAsync(userExpression, ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            var userExpression = await _appDbContext.UserExpressions.FindAsync(id, ct);
            if (userExpression == null) return false; 

                _appDbContext.UserExpressions.Remove(userExpression);
            return true; 
        }

        public async Task<IEnumerable<UserExpression>> GetAllAsync(CancellationToken ct)
        {
            return await _appDbContext.UserExpressions.ToListAsync(ct);
        }

        public async Task<IEnumerable<UserExpression>> GetByInputAsync(string input, CancellationToken ct)
        {
            if(string.IsNullOrWhiteSpace(input))
                return Enumerable.Empty<UserExpression>(); 

            var normalizedInput = input.ToLowerInvariant(); 

            return await _appDbContext.UserExpressions
                .Where(x => x.Name.ToLower().Contains(normalizedInput))
                .ToListAsync(ct); 
        }


        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _appDbContext.SaveChangesAsync(ct);
        }

        public async Task<UserExpression?> UpdateAsync(UserExpression userExpression, CancellationToken ct)
        {
            var existing = await _appDbContext.UserExpressions.FindAsync(userExpression.Id);
            if (existing == null) return null; 
            existing.Name = userExpression.Name;
            existing.Description = userExpression.Description;

            return existing;
        }
    }
}
