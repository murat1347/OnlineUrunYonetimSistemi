using BrandsA.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog;

namespace BrandsA.Persistence.Repositories
{
    public class WriteRepository<T> :IWriteRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> Table => _context.Set<T>();
        public WriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public async Task<bool> AddAsync(T model)
        {
            try
            {
                EntityEntry<T> entityEntry = await Table.AddAsync(model);
                entityEntry.State = EntityState.Added;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }

        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            try
            {
                await Table.AddRangeAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }

        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                T model = await Table.FindAsync(id);
                if (model == null)
                    return false;

                Table.Remove(model);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Log.Error($"{ex.Message}", ex);
                return false;
            }
        }

        public bool Remove(T model)
        {
            try
            {
                Table.Remove(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return false;
            }
        }

        public bool RemoveRange(List<T> model)
        {
            try
            {
                Table.RemoveRange(model);
                return true;
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                return false;
            }

        }

        public async Task<int> SaveChanges()
        => await _context.SaveChangesAsync();

        public async Task<bool> UpdateAsync(T model)
        {
            try
            {
                var existingModel = Table.FirstOrDefault(p => p.Id == model.Id);
                if (existingModel != null)
                {
                    // Set the values of other properties
                    _context.Entry(existingModel).CurrentValues.SetValues(model);

                    // Change the state to Modified
                    _context.Entry(existingModel).State = EntityState.Modified;
                    SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);

                return false;
            }
            return false;
        }
    }
}
