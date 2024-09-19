namespace BitsApp.API.Contracts
{
    public record PersonResponse(
        int Id,
        string? Name,
        DateOnly DateOfBirth,
        bool isMarried, 
        string? Phone, 
        decimal Salary 
        );
}
