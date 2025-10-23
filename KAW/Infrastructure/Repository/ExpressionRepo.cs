using KAW.Application.Interfaces;
using KAW.Domain.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using KAW.Infrastructure.Persistence;

namespace KAW.Infrastructure.Repository
{
    public class ExpressionRepo : IUserExpressionRepo
    {
        AppDbContext _appDbContext;

        public ExpressionRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(UserExpression userExpression)
        {
            await _appDbContext.AddAsync(userExpression);
        }

        public async Task DeleteAsync(int id)
        {
            var userExpression = await _appDbContext.UserExpressions.FindAsync(id);
            if(userExpression != null)
                _appDbContext.UserExpressions.Remove(userExpression);
        }

        public async Task<IEnumerable<UserExpression>> GetAllAsync()
        {
            return await _appDbContext.UserExpressions.ToListAsync();
        }

        public List<UserExpression> GetAllExpressions()
        {
            throw new NotImplementedException();
        }

        public Task<UserExpression?> GetByInputAsync(string input)
        {
            throw new NotImplementedException();
        }


        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(UserExpression userExpression)
        {
            throw new NotImplementedException();
        }
    }
}
