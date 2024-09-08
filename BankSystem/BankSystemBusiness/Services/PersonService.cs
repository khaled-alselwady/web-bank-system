using AutoMapper;
using BankSystem.Business.Services;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDTOs.PersonDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankSystemBusiness.Services
{
    public class PersonService : BaseService<Person, PersonDetailsDto>
    {
        public PersonService(AppDbContext context, IMapper mapper, ILogger<PersonService> logger)
            : base(context, mapper, logger)
        {
        }

        public async Task<List<PersonDetailsDto>?> GetAllPeopleAsync()
            => await GetAllUsingDtoAsync<PersonDetailsDto>();

        public async Task<PersonDetailsDto?> FindByIdAsync(int id)
            => await FindAsync(p => p.Id == id);

        public async Task<PersonDetailsDto?> AddPersonAsync(CreateOrUpdatePersonDto newPersonDto)
            => await AddAsync(newPersonDto);

        public async Task<PersonDetailsDto?> UpdatePersonAsync(int id, CreateOrUpdatePersonDto updatedPersonDto)
            => await UpdateAsync(updatedPersonDto, (query) => query.FirstOrDefaultAsync(p => p.Id == id));

        public async Task<bool> DeletePersonAsync(int id)
            => await HardDeleteAsync(p => p.Id == id);

        public async Task<bool> ExistsByIdAsync(int id)
            => await ExistsAsync(p => p.Id == id);
    }
}
