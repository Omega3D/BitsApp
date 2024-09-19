using BitsApp.Core.Interfaces;
using BitsApp.Core.Models;

namespace BitsApp.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository) 
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await _personRepository.Get();
        }

        public async Task<int> DeletePerson(int id)
        {
            return await _personRepository.Delete(id);
        }

        public async Task<int> UpdatePerson(int id, string name, DateOnly date_of_birth, bool is_married, string phone, decimal salary)
        {
            return await _personRepository.Update(id, name, date_of_birth, is_married, phone, salary);
        }

        public async Task<IEnumerable<Person>> CreatePerson(Stream file)
        {
            return await _personRepository.Create(file);
        }
    }
}
