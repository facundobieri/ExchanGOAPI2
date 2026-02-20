using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ExchanGODbContext context) : base(context) { }

        public async Task<Currency?> GetByCodeAsync(string code) =>
            await _context.Currencies
                .FirstOrDefaultAsync(c => c.Code.ToUpper() == code.ToUpper());
    }
}
