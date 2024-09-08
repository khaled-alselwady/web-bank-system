using AutoMapper;
using BankSystem.DTOs.ClientDTOs;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;

        public ClientService(AppDbContext context, IMapper mapper, ILogger<ClientService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<ClientDetailsDto?> _FindAsync(Expression<Func<Client, bool>> predicate)
        {
            try
            {
                var client = await _context.Clients
                    .Include(c => c.Person)
                    .FirstOrDefaultAsync(predicate);

                if (client == null)
                {
                    return null;
                }

                return _mapper.Map<ClientDetailsDto>(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by predicate: {Predicate}", predicate);
                throw;
            }
        }

        private async Task<bool> _ExistsAsync(Expression<Func<Client, bool>> predicate)
        {
            try
            {
                return await _context.Clients.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence by predicate: {Predicate}", predicate);
                throw;
            }
        }

        public async Task<List<ClientInfoView>?> AllAsync()
        {
            try
            {
                return await _context.ClientsInfoView.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all clients");
                throw;
            }
        }

        public async Task<ClientDetailsDto?> FindByClientIdAsync(int id)
        {
            try
            {
                return await _FindAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by Id: {Id}", id);
                throw;
            }
        }

        public async Task<ClientDetailsDto?> FindByPersonIdAsync(int personId)
        {
            try
            {
                return await _FindAsync(c => c.PersonId == personId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by PersonId: {PersonId}", personId);
                throw;
            }
        }

        public async Task<ClientDetailsDto?> AddAsync(CreateOrUpdateClientDto newClientDto)
        {
            if (newClientDto == null)
            {
                return null;
            }

            try
            {
                var client = _mapper.Map<Client>(newClientDto);
                if (client == null)
                {
                    return null;
                }

                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClientDetailsDto?>(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding client");
                throw;
            }
        }

        public async Task<ClientDetailsDto?> UpdateAsync(int id, CreateOrUpdateClientDto updatedClientDto)
        {
            if (updatedClientDto == null)
            {
                return null;
            }

            try
            {
                var client = await _context.Clients
                    .AsTracking()
                    .Include(c => c.Person)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (client == null)
                {
                    return null;
                }

                _mapper.Map(updatedClientDto, client);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClientDetailsDto?>(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _context.Clients
                      .Where(c => c.Id == id)
                      .ExecuteUpdateAsync(set => set
                      .SetProperty(c => c.IsActive, false));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting client with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByClientIdAsync(int id)
            => await _ExistsAsync(c => c.Id == id);

        public async Task<bool> ExistsByPersonIdAsync(int personId)
            => await _ExistsAsync(c => c.PersonId == personId);
    }
}
