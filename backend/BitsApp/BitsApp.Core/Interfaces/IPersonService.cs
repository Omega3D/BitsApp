using BitsApp.Core.Models;

namespace BitsApp.Core.Interfaces
{
    public interface IPersonService
    {
        Task<List<Person>> GetAllPersons();
        Task<IEnumerable<Person>> CreatePerson(Stream file);
        Task<int> UpdatePerson(int id, string name, DateOnly date_of_birth, bool is_married, string phone, decimal salary);
        Task<int> DeletePerson(int id);
    }
}
