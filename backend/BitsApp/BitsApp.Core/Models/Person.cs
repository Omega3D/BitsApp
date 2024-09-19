using System.Numerics;

namespace BitsApp.Core.Models
{
    public class Person
    {
        public Person() { }

        public Person(int id, string name, DateOnly dateOfBirth, bool is_married, string phone, decimal salary)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            isMarried = is_married;
            Phone = phone;
            Salary = salary;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool isMarried { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
