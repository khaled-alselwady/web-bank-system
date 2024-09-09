using AutoMapper;
using BankSystem.DTOs.UserDTOs;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankSystem.Business.Services
{
    public class UserService : BaseService<User, UserDetailsDto>
    {
        public UserService(AppDbContext context, IMapper mapper, ILogger<UserService> logger)
            : base(context, mapper, logger)
        {
        }

        protected override IQueryable<User> IncludeDependencies(IQueryable<User> query)
        {
            return query.Include(u => u.Person);
        }

        public async Task<UserDetailsDto?> FindByUserIdAsync(int id)
            => await FindAsync(u => u.Id == id);

        public async Task<UserDetailsDto?> FindByPersonIdAsync(int personId)
            => await FindAsync(u => u.PersonId == personId);

        public async Task<UserDetailsDto?> FindByUsernameAsync(string username)
            => await FindAsync(u => u.Username == username);

        public async Task<UserDetailsDto?> FindByUsernameAndPasswordAsync(string username, string password)
            => await FindAsync(u => u.Username == username && u.Password == password);

        public async Task<List<UserInfoView>?> GetAllUsersAsync()
            => await GetAllUsingViewAsync<UserInfoView>();

        public async Task<UserDetailsDto?> AddUserAsync(CreateOrUpdateUserDto newUserDto)
            => await AddAsync(newUserDto);

        public async Task<UserDetailsDto?> UpdateUserAsync(int id, CreateOrUpdateUserDto updatedUserDto)
            => await UpdateAsync(updatedUserDto, (query) => query.FirstOrDefaultAsync(u => u.Id == id));

        public async Task<bool> DeleteUserAsync(int id)
             => await SoftDeleteAsync(u => u.Id == id);

        public async Task<bool> ExistsByUserIdAsync(int id)
            => await ExistsAsync(u => u.Id == id);

        public async Task<bool> ExistsByPersonIdAsync(int personId)
            => await ExistsAsync(u => u.PersonId == personId);

        public async Task<bool> ExistsByUsernameAsync(string username)
            => await ExistsAsync(u => u.Username == username);

        public async Task<bool> ExistsByUsernameAndPasswordAsync(string username, string password)
            => await ExistsAsync(u => u.Username == username && u.Password == password);

        public async Task<bool> IsUserActive(int id)
            => await ExistsAsync(u => u.Id == id && u.IsActive);

    }
}
