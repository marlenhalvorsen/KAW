﻿using KAW.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KAW.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<UserExpression> UserExpressions {get; set; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
