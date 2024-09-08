using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class BaseService<TEntity, TDto> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BaseService<TEntity, TDto>> _logger;

        public BaseService(DbContext context, IMapper mapper, ILogger<BaseService<TEntity, TDto>> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
                var query = IncludeDependencies(_context.Set<TEntity>());
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
                return await _context.Set<TEntity>().AnyAsync(predicate);
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
                return await _context.Set<TEntity>()
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
                await _context.Set<TEntity>().AddAsync(entity);
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

                var entity = await findEntityFunc(IncludeDependencies(_context.Set<TEntity>().AsTracking()));

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

                var rowsDeleted = await _context.Set<TEntity>().Where(predicate).ExecuteDeleteAsync();

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
                await _context.Set<TEntity>()
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
    }
}
