namespace BitsApp.API.Contracts
{
    public record PersonRequest(
        string? Name,
        DateOnly DateOfBirth,
        bool isMarried,
        string? Phone,
        decimal Salary
        );
}
