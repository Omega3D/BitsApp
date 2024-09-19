namespace BitsApp.DataAccess.Enitites
{
    public class PersonEntity
    {
        public int Id { get; set; }
        public string? Name {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool isMarried { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
