using BitsApp.Core.Interfaces;
using BitsApp.Core.Models;
using BitsApp.DataAccess.Enitites;
using BitsApp.DataAccess.Mapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BitsApp.DataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BitsAppDbContext _context;

        public PersonRepository(BitsAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> Get()
        {
            var personsEntities = await _context.Persons
                .AsNoTracking()
                .ToListAsync();

            var persons = personsEntities
                .Select(p => new Person(p.Id, p.Name!, p.DateOfBirth, p.isMarried, p.Phone!, p.Salary))
                .ToList();

            return persons;
        }

        public async Task<IEnumerable<Person>> Create(Stream fileStream)
        {
                using var reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    DetectDelimiter = true,
                    TrimOptions = TrimOptions.Trim,
                    HeaderValidated = null,
                    MissingFieldFound = null
                });

                csv.Context.RegisterClassMap<PersonMap>();

                var records = new List<Person>();
                await foreach (var record in csv.GetRecordsAsync<Person>())
                {
                    records.Add(record);
                }

            var existingPersons = await _context.Persons
                .Where(p => records.Select(r => r.Phone).Contains(p.Phone))
                .ToListAsync();

            var personEntities = records
                .Where(p => !existingPersons.Any(ep => ep.Phone == p.Phone))
                .Select(p => new PersonEntity
                {
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth,
                    isMarried = p.isMarried,
                    Phone = p.Phone,
                    Salary = p.Salary
                })
                .ToList();

            if (personEntities.Any())
            {
                await _context.Persons.AddRangeAsync(personEntities);
                await _context.SaveChangesAsync();

                return records;
            }

            throw new Exception("Users already exist");
        }
        
        public async Task<int> Delete(int id)
        {
            await _context.Persons
                .Where(p =>  p.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<int> Update(int id, string name, DateOnly date_of_birth, bool is_married, string phone, decimal salary)
        {
            await _context.Persons
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, name)
                    .SetProperty(p => p.DateOfBirth, date_of_birth)
                    .SetProperty(p => p.isMarried, is_married)
                    .SetProperty(p => p.Phone, phone)
                    .SetProperty(p => p.Salary, salary));

            return id;
        }
    }
}
