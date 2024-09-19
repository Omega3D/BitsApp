using BitsApp.DataAccess.Enitites;
using CsvHelper.Configuration;

namespace BitsApp.DataAccess.Mapper
{
    public class PersonMap : ClassMap<PersonEntity>
    {
        public PersonMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Name).Name("Name");
            Map(m => m.DateOfBirth).Name("DateOfBirth");
            Map(m => m.isMarried).Name("isMarried");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Salary).Name("Salary");
        }
    }
}
