using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Diagnostics.Eventing.Reader;
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

        public async Task AddAsync(UserExpression userExpression)
        {
            await _appDbContext.UserExpressions.AddAsync(userExpression);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userExpression = await _appDbContext.UserExpressions.FindAsync(id);
            if (userExpression == null) return false; 

            if(userExpression != null)
                _appDbContext.UserExpressions.Remove(userExpression);
            return true; 
        }

        public async Task<IEnumerable<UserExpression>> GetAllAsync()
        {
            return await _appDbContext.UserExpressions.ToListAsync();
        }

        public async Task<IEnumerable<UserExpression>> GetByInputAsync(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
                return Enumerable.Empty<UserExpression>(); 

            var normalizedInput = input.ToLower(); 

            return await _appDbContext.UserExpressions
                .Where(x => x.Name.ToLower().Contains(normalizedInput))
                .ToListAsync(); 
        }


        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<UserExpression?> UpdateAsync(UserExpression userExpression)
        {
            var existing = await _appDbContext.UserExpressions.FindAsync(userExpression.Id);
            if (existing == null) return null; 
            existing.Name = userExpression.Name;
            existing.Description = userExpression.Description;

            return existing;
        }
    }
}
