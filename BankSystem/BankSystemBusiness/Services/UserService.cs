using AutoMapper;
using BankSystem.DTOs.UserDTOs;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, IMapper mapper, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<UserDetailsDto?> _FindAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Person)
                    .FirstOrDefaultAsync(predicate);

                if (user == null)
                {
                    return null;
                }

                return _mapper.Map<UserDetailsDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by predicate: {Predicate}", predicate);
                throw;
            }
        }

        private async Task<bool> _ExistsAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await _context.Users.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence by predicate: {Predicate}", predicate);
                throw;
            }
        }

        public async Task<List<UserInfoView>?> AllAsync()
        {
            try
            {
                return await _context.UsersInfoView.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all users");
                throw;
            }
        }

        public async Task<UserDetailsDto?> FindByUserIdAsync(int id)
        {
            try
            {
                return await _FindAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by Id: {Id}", id);
                throw;
            }
        }

        public async Task<UserDetailsDto?> FindByPersonIdAsync(int personId)
        {
            try
            {
                return await _FindAsync(u => u.PersonId == personId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by PersonId: {PersonId}", personId);
                throw;
            }
        }

        public async Task<UserDetailsDto?> FindByUsernameAsync(string username)
        {
            try
            {
                return await _FindAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by Username: {Username}", username);
                throw;
            }
        }

        public async Task<UserDetailsDto?> FindByUsernameAndPasswordAsync(string username, string password)
        {
            try
            {
                return await _FindAsync(u => u.Username == username && u.Password == password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by Username: {Username} and Password", username);
                throw;
            }
        }

        public async Task<UserDetailsDto?> AddAsync(CreateOrUpdateUserDto newUserDto)
        {
            if (newUserDto == null)
            {
                return null;
            }

            try
            {
                var user = _mapper.Map<User>(newUserDto);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserDetailsDto?>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user");
                throw;
            }
        }

        public async Task<UserDetailsDto?> UpdateAsync(int id, CreateOrUpdateUserDto updatedUserDto)
        {
            if (id <= 0 || updatedUserDto == null)
            {
                return null;
            }

            try
            {
                var user = await _context.Users
                    .AsTracking()
                    .Include(u => u.Person)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return null;
                }

                _mapper.Map(updatedUserDto, user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserDetailsDto?>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _context.Users
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(set => set
                        .SetProperty(u => u.IsActive, false));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting user with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByUserIdAsync(int id)
            => await _ExistsAsync(u => u.Id == id);

        public async Task<bool> ExistsByPersonIdAsync(int personId)
            => await _ExistsAsync(u => u.PersonId == personId);

        public async Task<bool> ExistsByUsernameAsync(string username)
            => await _ExistsAsync(u => u.Username == username);

        public async Task<bool> ExistsByUsernameAndPasswordAsync(string username, string password)
            => await _ExistsAsync(u => u.Username == username && u.Password == password);
    }
}
