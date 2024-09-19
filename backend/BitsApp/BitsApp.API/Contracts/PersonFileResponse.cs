namespace BitsApp.API.Contracts
{
    public record PersonFileResponse(
        string? Name,
        DateOnly DateOfBirth,
        bool isMarried,
        string? Phone,
        decimal Salary
        );
}
