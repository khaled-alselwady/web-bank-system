using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class BaseService<TEntity, TDto> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseService<TEntity, TDto>> _logger;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseService(DbContext context, IMapper mapper, ILogger<BaseService<TEntity, TDto>> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _dbSet = _context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> IncludeDependencies(IQueryable<TEntity> query)
        {
            // Override in derived classes if necessary to include navigation properties
            return query;
        }

        protected async Task<TDto?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = IncludeDependencies(_dbSet);
                var entity = await query.FirstOrDefaultAsync(predicate);

                return entity == null ? default : _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding entity by predicate: {Predicate}", predicate);
                throw;
            }
        }

        protected async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence by predicate: {Predicate}", predicate);
                throw;
            }
        }

        protected async Task<List<TView>?> GetAllUsingViewAsync<TView>() where TView : class
        {
            try
            {
                return await _context.Set<TView>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all entities");
                throw;
            }
        }

        protected async Task<List<TDetailsDto>?> GetAllUsingDtoAsync<TDetailsDto>() where TDetailsDto : class
        {
            try
            {
                return await _dbSet
                    .Select(e => _mapper.Map<TDetailsDto>(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all entities");
                throw;
            }
        }

        protected async Task<TDto> AddAsync<TCreateDto>(TCreateDto createDto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(createDto);
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding entity");
                throw;
            }
        }

        protected async Task<TDto?> UpdateAsync<TUpdateDto>(TUpdateDto updateDto, Func<IQueryable<TEntity>, Task<TEntity?>> findEntityFunc)
        {
            try
            {
                if (findEntityFunc == null)
                {
                    return default;
                }

                var entity = await findEntityFunc(IncludeDependencies(_dbSet.AsTracking()));

                if (entity == null)
                {
                    return default;
                }

                _mapper.Map(updateDto, entity);
                await _context.SaveChangesAsync();

                return _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity");
                throw;
            }
        }

        protected async Task<bool> HardDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                {
                    return false;
                }

                var rowsDeleted = await _dbSet.Where(predicate).ExecuteDeleteAsync();

                return rowsDeleted > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting entity");
                throw;
            }
        }

        protected async Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                await _dbSet
                .Where(predicate)
                .ExecuteUpdateAsync(set => set
                    .SetProperty(e => EF.Property<bool>(e, "IsActive"), false));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting entity");
                throw;
            }
        }

        protected async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                   ? await _dbSet.CountAsync()
                   : await _dbSet.CountAsync(predicate);
        }

        protected async Task<List<TView>> PagerAsync<TView, TKey>(short pageNumber, int pageSize, Expression<Func<TView, TKey>> orderBy) where TView : class
        {
            return await _context.Set<TView>()
                .OrderBy(orderBy)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        protected async Task<List<TView>> PagerAsync<TView, TKey>(
            Expression<Func<TView, bool>> lastKeyPredicate,
            int pageSize,
            Expression<Func<TView, TKey>> orderBy,
            bool isNextPage = true)
            where TView : class
        {
            var query = _context.Set<TView>().Where(lastKeyPredicate);

            if (isNextPage)
            {
                query = query.OrderBy(orderBy);
            }
            else
            {
                query = query.OrderByDescending(orderBy);
            }

            var result = await query.Take(pageSize).ToListAsync();

            if (!isNextPage)
            {
                result.Reverse();
            }

            return result;
        }
    }
}
