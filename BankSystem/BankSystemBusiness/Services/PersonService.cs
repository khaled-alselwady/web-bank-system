using AutoMapper;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDTOs.PersonDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankSystemBusiness.Services
{
    public class PersonService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(AppDbContext context, IMapper mapper, ILogger<PersonService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PersonDetailsDto>?> AllAsync()
        {
            try
            {
                var people = await _context.People
                    .Select(p => _mapper.Map<PersonDetailsDto>(p))
                    .ToListAsync();

                return people;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all people");
                throw;
            }
        }

        public async Task<PersonDetailsDto?> FindAsync(int id)
        {
            try
            {
                var person = await _context.People.FindAsync(id);

                if (person == null)
                {
                    return null;
                }

                return _mapper.Map<PersonDetailsDto?>(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding person by Id: {Id}", id);
                throw;
            }
        }

        public async Task<PersonDetailsDto?> AddAsync(CreateOrUpdatePersonDto newPersonDto)
        {
            if (newPersonDto == null)
            {
                return null;
            }

            try
            {
                var person = _mapper.Map<Person>(newPersonDto);

                if (person == null)
                {
                    return null;
                }

                await _context.People.AddAsync(person);
                await _context.SaveChangesAsync();

                return _mapper.Map<PersonDetailsDto?>(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding person");
                throw;
            }
        }

        public async Task<PersonDetailsDto?> UpdateAsync(int id, CreateOrUpdatePersonDto updatedPersonDto)
        {
            if (id <= 0 || updatedPersonDto == null)
            {
                return null;
            }

            try
            {
                var person = await _context.People.FindAsync(id);

                if (person == null)
                {
                    return null;
                }

                _mapper.Map(updatedPersonDto, person);
                _context.Entry(person).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return _mapper.Map<PersonDetailsDto?>(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating person with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            try
            {
                var deletedRows = await _context.People
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();

                return deletedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting person with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            try
            {
                return await _context.People.AnyAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of person with Id: {Id}", id);
                throw;
            }
        }
    }
}
