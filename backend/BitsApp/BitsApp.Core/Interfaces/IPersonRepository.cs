using BitsApp.Core.Models;

namespace BitsApp.Core.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> Get();
        Task<IEnumerable<Person>> Create(Stream file);
        Task<int> Update(int id, string name, DateOnly date_of_birth, bool is_married, string phone, decimal salary);
        Task<int> Delete(int id);
    }
}
