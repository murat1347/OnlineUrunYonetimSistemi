using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories;
using BrandsA.Domain.Entities.Common;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BrandsA.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }
                return query;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
            return null;

        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = Table.AsNoTracking();
                }
                return await query.FirstOrDefaultAsync(data => data.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = Table.AsNoTracking();
                }
                return await query.FirstOrDefaultAsync(method);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            try
            {
                var query = Table.Where(method);
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }
                return query;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }
    }
}
