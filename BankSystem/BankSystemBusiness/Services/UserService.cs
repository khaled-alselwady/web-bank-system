using AutoMapper;
using BankSystem.DTOs.UserDTOs;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<UserDetailsDto?> _FindAsync(Expression<Func<User, bool>> predicate)
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

        private async Task<bool> _ExistsAsync(Expression<Func<User, bool>> predicate)
            => await _context.Users.AnyAsync(predicate);


        public async Task<List<UserInfoView>?> AllAsync()
           => await _context.UsersInfoView.ToListAsync();

        public async Task<UserDetailsDto?> FindByUserIdAsync(int id)
            => await _FindAsync(u => u.Id == id);

        public async Task<UserDetailsDto?> FindByPersonIdAsync(int personId)
            => await _FindAsync(u => u.PersonId == personId);

        public async Task<UserDetailsDto?> FindByUsernameAsync(string username)
            => await _FindAsync(u => u.Username == username);

        public async Task<UserDetailsDto?> FindByUsernameAndPasswordAsync(string username, string password)
            => await _FindAsync(u => (u.Username == username && u.Password == password));

        public async Task<UserDetailsDto?> AddAsync(CreateOrUpdateUserDto newUserDto)
        {
            if (newUserDto == null)
            {
                return null;
            }

            var user = _mapper.Map<User>(newUserDto);

            if (user == null)
            {
                return null;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDetailsDto?>(user);
        }

        public async Task<UserDetailsDto?> UpdateAsync(int id, CreateOrUpdateUserDto updatedUserDto)
        {
            if (id <= 0 || updatedUserDto == null)
            {
                return null;
            }

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

        public async Task<bool> DeleteAsync(int id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(set => set
                    .SetProperty(u => u.IsActive, false));

            return true;
        }

        public async Task<bool> ExistsByUserId(int id)
            => await _ExistsAsync(u => u.Id == id);

        public async Task<bool> ExistsByPersonId(int personId)
            => await _ExistsAsync(u => u.PersonId == personId);

        public async Task<bool> ExistsByUsername(string username)
            => await _ExistsAsync(u => u.Username == username);

        public async Task<bool> ExistsByUsernameAndPassword(string username, string password)
            => await _ExistsAsync(u => (u.Username == username && u.Password == password));
    }
}
