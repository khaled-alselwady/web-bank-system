using AutoMapper;
using BankSystemBusiness.HelperClasses;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDTOs.PersonDTOs;
using Microsoft.EntityFrameworkCore;

namespace BankSystemBusiness.Services
{
    public class PersonService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PersonService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool _IsValid(CreateOrUpdatePersonDto person)
        {
            return ValidationHelper.Validate(
                entity: person,
                valueCheck: (person) => !string.IsNullOrWhiteSpace(person.FirstName) &&
                                        !string.IsNullOrWhiteSpace(person.LastName) &&
                                        !string.IsNullOrWhiteSpace(person.Phone)
                );
        }

        public async Task<List<PersonDetailsDto>?> All()
        {
            var people = await _context.People
                .Select(p => _mapper.Map<PersonDetailsDto>(p))
                .ToListAsync();

            return people;
        }

        public async Task<PersonDetailsDto?> Find(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return null;
            }

            return _mapper.Map<PersonDetailsDto?>(person);
        }

        public async Task<PersonDetailsDto?> Add(CreateOrUpdatePersonDto newPersonDto)
        {
            if (newPersonDto == null || !_IsValid(newPersonDto))
            {
                return null;
            }

            var person = _mapper.Map<CreateOrUpdatePersonDto, Person>(newPersonDto);

            if (person == null)
            {
                return null;
            }

            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();

            return _mapper.Map<PersonDetailsDto?>(person);
        }

        public async Task<PersonDetailsDto?> Update(int id, CreateOrUpdatePersonDto updatedPersonDto)
        {
            if (id <= 0 || updatedPersonDto == null || !_IsValid(updatedPersonDto))
            {
                return null;
            }

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

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var deletedRows = await _context.People.Where(x => x.Id == id).ExecuteDeleteAsync();

            return deletedRows > 0;
        }

        public async Task<bool> Exists(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return await _context.People.AnyAsync(p => p.Id == id);
        }
    }
}
